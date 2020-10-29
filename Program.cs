using System;
using System.Diagnostics;
using System.Threading;
using Client.code;
using Client.code.checker;
using Client.code.diod.master;
using MQTTnet;
using MQTTnet.Client.Options;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            // Use TCP connection.
            var options = new MqttClientOptionsBuilder()
                
                .WithTcpServer("imi.biz.ua", 1883) // Port is optional
                .Build();

            var mqttClient = new MqttFactory().CreateMqttClient();
            mqttClient.ConnectAsync(options, CancellationToken.None).Wait();

            new Thread(() => {
                Console.ReadLine();
                Environment.Exit(1);
            }).Start();
            
            IComandGenerator generator; 
            
            bool status = true;
            while (true) {
                Stopwatch sw = Stopwatch.StartNew();
                Stopwatch sw2 = Stopwatch.StartNew();
                if (status) {
                    // generator = new code.diod.master.SoundLevel();
                    // generator = new code.diod.master.ScreenTopRepeater(1920);
                    generator = new code.diod.master.ScreenTopRepeater(3840);
                } else {
                    generator = new MasterDebug();
                }

                // status = !status;
                
                IChecker checker = new CheckerDebug();
                // IChecker checker = new Muse();
                var message = new MqttApplicationMessageBuilder()
                    .WithTopic("led")
                    .WithPayload(checker.Check(generator.GetCommands()))
                    .WithExactlyOnceQoS()
                    .WithRetainFlag()
                    .Build();

                mqttClient.PublishAsync(message, CancellationToken.None).Wait();
                sw.Stop();
                int time = (int)sw.ElapsedMilliseconds;
                int pause = 50;
                if (time < pause) {
                    Thread.Sleep(pause - time);
                }
                // Console.WriteLine(sw2ElapsedMilliseconds);
            }

            // StartAsync returns immediately, as it starts a new thread using Task.Run, 
            // and so the calling thread needs to wait.
            Console.ReadLine();
        }
    }
}
