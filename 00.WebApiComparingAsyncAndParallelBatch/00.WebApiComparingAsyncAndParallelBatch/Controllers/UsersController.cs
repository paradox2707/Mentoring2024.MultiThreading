using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> AddUsersBatch([FromQuery] int batchSize = 100)
        {
            var usersAsync = new List<User>();
            var usersParallel = new List<User>();
            for (int i = 0; i < 100000; i++)
            {
                usersAsync.Add(new User { Name = $"User_{i}_Async" });
                usersParallel.Add(new User { Name = $"User_{i}_Parallel" });
            }

            var asyncTime = await AddUsersAsync(usersAsync, batchSize);
            var parallelTime = await AddUsersParallel(usersParallel, batchSize);

            return Ok(new { asyncTime, parallelTime });
        }

        private async Task<long> AddUsersAsync(List<User> users, int batchSize)
        {
            var stopwatch = Stopwatch.StartNew();

            int maxPoolSize = 100; // This should match the Max Pool Size in your connection string
            int maxConcurrentTasks = maxPoolSize / 10; // Adjust based on your observations
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

            await Parallel.ForEachAsync(users.Chunk(batchSize), async (batch, cancellationToken) =>
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
    }
}
