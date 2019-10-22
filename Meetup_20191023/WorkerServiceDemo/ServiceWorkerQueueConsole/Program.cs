using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using System;

namespace ServiceWorkerQueueConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            Boolean isexit = true;
            while (isexit)
            {
                Console.WriteLine("Enter Your Message to send to the queue: ");
                String message = Console.ReadLine();

                btnSend(message);


                Console.WriteLine("Do you want to continue say Y or N");
                string YN = Console.ReadLine();
                if (YN.ToLower() == "y")
                {
                    isexit = true;
                }
                else
                {
                    isexit = false;
                }
                Console.WriteLine("==========================  ============");
                Console.WriteLine("");
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();


        }


        private static string queueConnectionString = "DefaultEndpointsProtocol=https;AccountName=netcore3;EndpointSuffix=core.windows.net";
        private static void btnSend(string text)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(queueConnectionString);
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
            CloudQueue queue = queueClient.GetQueueReference("demoqueue");
            queue.AddMessageAsync(new CloudQueueMessage(text));

        }

    }
}
