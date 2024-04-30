using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsumerToDealerDB
{
    public class ConfluentListener
    {
        public IConfiguration readConfig()
        {
            // reads the client configuration from client.properties
            // and returns it as a configuration object
            return new Microsoft.Extensions.Configuration.ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddIniFile("client.properties", false)
            .Build();
        }

        public async void Listen(String groupId = "toDealerDB", String autoOffsetReset = "earliest")
        {
            // read topics from App.config
            string[] topics = System.Configuration.ConfigurationManager.AppSettings["Topics"].Split(';');

            // read config from client.properties
            IConfiguration confluentClientConfig = readConfig();

            confluentClientConfig["group.id"] = groupId;
            confluentClientConfig["auto.offset.reset"] = autoOffsetReset;

            // creates a new consumer instance 
            using (var consumer = new ConsumerBuilder<string, string>(confluentClientConfig.AsEnumerable()).Build())
            {
                Console.WriteLine("Start Listen Group \'" + groupId + "\'");
                Console.Write("Topics: ");
                foreach (var topic in topics)
                {
                    Console.Write("\'" + topic + "\' ");
                }
                Console.WriteLine();

                consumer.Subscribe(topics);
                while (true)
                {
                    // consumes messages from the subscribed topic and prints them to the console
                    var cr = consumer.Consume();
                    new ConfluentApprovedConsumerRepository().WriteTopicKeyValue(cr);
                    Console.WriteLine($"Consumed event from topic {cr.Topic}: key = {cr.Message.Key,-10} value = {cr.Message.Value}");
                }

                // closes the consumer connection
                consumer.Close();
            }
        }
    }
}
