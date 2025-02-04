using System;
using System.IO;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MainProcessingService
{
    class Program
    {
        private static readonly SemaphoreSlim semaphore = new SemaphoreSlim(5); // Limit to 5 concurrent messages

        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost", UserName = "guest", Password = "guest" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            // Declare a queue
            channel.QueueDeclare(queue: "image_processing_queue",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            Console.WriteLine("Waiting for messages...");

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, ea) =>
            {
                await semaphore.WaitAsync();
                try
                {
                    var filePath = System.Text.Encoding.UTF8.GetString(ea.Body.ToArray());
                    Console.WriteLine($"Received file path: {filePath}");

                    // Define the destination path
                    string destinationDirectory = Path.Combine("ProcessedImages");
                    string destinationPath = Path.Combine(destinationDirectory, Path.GetFileName(filePath));

                    // Create the directory if it does not exist
                    Directory.CreateDirectory(destinationDirectory);

                    try
                    {
                        // Simulate processing
                        File.Copy(filePath, destinationPath);
                        Console.WriteLine($"Processed and saved: {destinationPath}");

                        // Move the file to an archive folder after processing
                        string archiveDirectory = Path.Combine(Path.GetDirectoryName(filePath), "ArchivedImages");
                        Directory.CreateDirectory(archiveDirectory);
                        string archivedPath = Path.Combine(archiveDirectory, Path.GetFileName(filePath));
                        File.Move(filePath, archivedPath);
                        Console.WriteLine($"Moved original file to archive: {archivedPath}");

                        // Manually acknowledge the message
                        channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Failed to process the file: {ex.Message}");
                        // Optionally, you can reject the message to send it to a dead-letter queue
                        channel.BasicNack(deliveryTag: ea.DeliveryTag, multiple: false, requeue: false);
                    }
                }
                finally
                {
                    semaphore.Release();
                }
            };

            channel.BasicConsume(queue: "image_processing_queue",
                                 autoAck: false, // Set to false for manual acknowledgment
                                 consumer: consumer);

            Console.ReadLine();
        }
    }
}