using System;
using System.Net.Http;
using System.Threading.Tasks;
using GrpcService;
using Grpc.Net.Client;

namespace GrpcGreeterClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // The port number(5001) must match the port of the gRPC server.
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client =  new Greeter.GreeterClient(channel);

            Boolean isexit = true;
            while (isexit)
            {
                Console.WriteLine("Enter Your Name to send request to the server : ");
                String myName = Console.ReadLine();

                var reply = await client.SayHelloAsync(
                                  new HelloRequest { Name = myName });
                Console.WriteLine("Hello world : " + reply.Message);


                Console.WriteLine("Enter YourFriend  Name  : ");
                String friendName = Console.ReadLine();

                var serverreply = await client.ServerMessageAsync(
                                  new HelloRequest { Name = friendName });
                Console.WriteLine("Message from Server -> " + serverreply.Message);


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


            //var reply = await client.SayHelloAsync(
            //                  new HelloRequest { Name = "GreeterClient" });
            //Console.WriteLine("Greeting: " + reply.Message);
            //Console.WriteLine("Press any key to exit...");
            //Console.ReadKey();
        }
    }
}
