using System;
using System.Linq;
using Amazon.SQS;

namespace ArmutLocalStackSample.FunctionalTests.SeedData.SqsSeeders
{
    public class SqsSeeder
    {
        public static void CreateQueue(AmazonSQSClient client)
        {
            var installers = typeof(MovieLikeSeeder).Assembly.ExportedTypes
                .Where(m => typeof(ISqsSeeder).IsAssignableFrom(m) && !m.IsInterface && !m.IsAbstract)
                .Select(Activator.CreateInstance)
                .Cast<ISqsSeeder>()
                .ToList();

            installers.ForEach(m => m.CreateQueue(client));
        }
    }
}