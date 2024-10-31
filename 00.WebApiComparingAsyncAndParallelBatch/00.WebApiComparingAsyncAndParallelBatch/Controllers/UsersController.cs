using EFCore.BulkExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Diagnostics;

namespace WebApiComparingAsyncAndParallelBatch.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

        public UsersController(ApplicationDbContext context, IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _context = context;
            _contextFactory = contextFactory;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("User is null.");
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost("batch")]
        public async Task<IActionResult> AddUsersBatch()
        {
            var userCounts = new[] { 100, 1000, 10000, 100000 };
            var batchSizes = new[] { 100, 1000, 10000, 100000 };
            var results = new List<string>();

            foreach (var userCount in userCounts)
            {
                var usersBulk = new List<User>();
                for (int i = 0; i < userCount; i++)
                {
                    usersBulk.Add(new User { Name = $"User_{i}_Bulk" });
                }

                var bulkInsertTime = await AddUsersBulkInsertAsync(usersBulk);
                results.Add($"bulkInsertTime for EF Core - Bulk insert, for {userCount} users: {bulkInsertTime} ms");

                var nativeBulkInsertTime = await AddUsersNativeBulkInsertAsync(usersBulk);
                results.Add($"nativeBulkInsertTime for ADO.NET - Bulk insert, for {userCount} users: {nativeBulkInsertTime} ms");

                var nativeBulkInsertParallelTime = await AddUsersNativeBulkInsertParallelAsync(usersBulk, 1000); // Adjust batch size as needed
                results.Add($"nativeBulkInsertParallelTime for ADO.NET - Bulk insert with parallelism, for {userCount} users: {nativeBulkInsertParallelTime} ms");

                foreach (var batchSize in batchSizes)
                {
                    if (batchSize > userCount) continue; // Skip if batch size is greater than user count

                    var usersAsync = new List<User>();
                    var usersParallel = new List<User>();
                    for (int i = 0; i < userCount; i++)
                    {
                        usersAsync.Add(new User { Name = $"User_{i}_Async" });
                        usersParallel.Add(new User { Name = $"User_{i}_Parallel" });
                    }

                    var asyncTime = await AddUsersAsync(usersAsync, batchSize);
                    var parallelTime = await AddUsersParallel(usersParallel, batchSize);

                    results.Add($"asyncTime for EF Core - Add range and save, for {userCount} users with batch size {batchSize}: {asyncTime} ms");
                    results.Add($"parallelTime for EF Core - Add range and save, for {userCount} users with batch size {batchSize}: {parallelTime} ms");
                }
            }

            return Ok(string.Join("\n", results));
        }

        private async Task<long> AddUsersAsync(List<User> users, int batchSize)
        {
            var stopwatch = Stopwatch.StartNew();

            int maxPoolSize = 1000; // This should match the Max Pool Size in your connection string
            int maxConcurrentTasks = maxPoolSize / 2; // Adjust based on your observations
            var semaphore = new SemaphoreSlim(maxConcurrentTasks);
            var tasks = new List<Task>();

            for (int i = 0; i < users.Count; i += batchSize)
            {
                var batch = users.Skip(i).Take(batchSize).ToList();
                await semaphore.WaitAsync();
                tasks.Add(Task.Run(async () =>
                {
                    try
                    {
                        using (var dbContext = _contextFactory.CreateDbContext())
                        {
                            await dbContext.Users.AddRangeAsync(batch);
                            await dbContext.SaveChangesAsync();
                        }
                    }
                    finally
                    {
                        semaphore.Release();
                    }
                }));
            }

            await Task.WhenAll(tasks);

            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        private async Task<long> AddUsersParallel(List<User> users, int batchSize)
        {
            var stopwatch = Stopwatch.StartNew();

            var options = new ParallelOptions
            {
                MaxDegreeOfParallelism = Environment.ProcessorCount * 2 // Adjust based on your observations
            };

            await Parallel.ForEachAsync(users.Chunk(batchSize), options, async (batch, cancellationToken) =>
            {
                using (var dbContext = _contextFactory.CreateDbContext())
                {
                    await dbContext.Users.AddRangeAsync(batch);
                    await dbContext.SaveChangesAsync(cancellationToken);
                }
            });

            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        private async Task<long> AddUsersBulkInsertAsync(List<User> users)
        {
            var stopwatch = Stopwatch.StartNew();

            using (var dbContext = _contextFactory.CreateDbContext())
            {
                await dbContext.BulkInsertAsync(users);
            }

            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        private async Task<long> AddUsersNativeBulkInsertAsync(List<User> users)
        {
            var stopwatch = Stopwatch.StartNew();

            var dataTable = ConvertToDataTable(users);

            using (var connection = new SqlConnection(_context.Database.GetConnectionString()))
            {
                await connection.OpenAsync();

                using (var bulkCopy = new SqlBulkCopy(connection))
                {
                    bulkCopy.DestinationTableName = "Users";

                    // Map columns
                    bulkCopy.ColumnMappings.Add("Id", "Id");
                    bulkCopy.ColumnMappings.Add("Name", "Name");

                    await bulkCopy.WriteToServerAsync(dataTable);
                }
            }

            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        private DataTable ConvertToDataTable(List<User> users)
        {
            var dataTable = new DataTable();
            dataTable.Columns.Add("Id", typeof(int));
            dataTable.Columns.Add("Name", typeof(string));

            foreach (var user in users)
            {
                var row = dataTable.NewRow();
                row["Id"] = user.Id;
                row["Name"] = user.Name;
                dataTable.Rows.Add(row);
            }

            return dataTable;
        }
        private async Task<long> AddUsersNativeBulkInsertParallelAsync(List<User> users, int batchSize)
        {
            var stopwatch = Stopwatch.StartNew();

            var userChunks = users.Chunk(batchSize).ToList();

            var tasks = userChunks.Select(async chunk =>
            {
                var dataTable = ConvertToDataTable(chunk.ToList());

                using (var connection = new SqlConnection(_context.Database.GetConnectionString()))
                {
                    await connection.OpenAsync();

                    using (var bulkCopy = new SqlBulkCopy(connection))
                    {
                        bulkCopy.DestinationTableName = "Users";

                        // Map columns
                        bulkCopy.ColumnMappings.Add("Id", "Id");
                        bulkCopy.ColumnMappings.Add("Name", "Name");

                        await bulkCopy.WriteToServerAsync(dataTable);
                    }
                }
            });

            await Task.WhenAll(tasks);

            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }
    }
}
