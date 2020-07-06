using System;
using System.Threading.Tasks;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;

namespace MyQueueApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string connectionString = Environment.GetEnvironmentVariable("AZURE_STORAGE_CONNECTION_STRING");
            QueueClient queue = new QueueClient(connectionString, "mystoragequeue");
            await queue.CreateIfNotExistsAsync();
            Console.WriteLine($"Queue created sucessfully");
            while(true)
            {
                Console.WriteLine("Type A to write message, B to read message or any letter to delete the queue");
                string option = Console.ReadLine();
                if(option == "A")
                {
                    Console.WriteLine("what is the message:");
                    string message = Console.ReadLine();
                    await queue.SendMessageAsync(message);
                    System.Console.WriteLine("Message sent into the queue ");
                }

                else if (option == "B")
                {
                    QueueMessage[] retrievedMessage = await queue.ReceiveMessagesAsync();
                    string theMessage = retrievedMessage[0].MessageText;
                    Console.WriteLine($"message: {theMessage}");
                }
            
                else
                {
                    await queue.DeleteIfExistsAsync();
                    Console.WriteLine( "The queue was deleted.");
                }
                }
            

        }
    }
}
