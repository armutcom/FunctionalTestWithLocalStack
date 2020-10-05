using System.Collections.Generic;
using System.Text.Json;
using Amazon.SQS;
using Amazon.SQS.Model;

namespace ArmutLocalStackSample.FunctionalTests.SeedData.SqsSeeders
{
    public class MovieLikeSeeder : ISqsSeeder
    {
        public void CreateQueue(AmazonSQSClient client)
        {
            CreateQueueRequest createDlqRequest = new CreateQueueRequest
            {
                QueueName = "ArmutLocalStack-Test-DLQ.fifo",
                Attributes = new Dictionary<string, string>
                {
                    {
                        "FifoQueue", "true"
                    },
                }
            };

            CreateQueueResponse createDlqResult =
                client.CreateQueueAsync(createDlqRequest).GetAwaiter().GetResult();

            var attributes = client.GetQueueAttributesAsync(new GetQueueAttributesRequest
            {
                QueueUrl = createDlqResult.QueueUrl,
                AttributeNames = new List<string> { "QueueArn" }
            }).GetAwaiter().GetResult();

            var redrivePolicy = new
            {
                maxReceiveCount = "1",
                deadLetterTargetArn = attributes.Attributes["QueueArn"]
            };

            CreateQueueRequest createQueueRequest = new CreateQueueRequest
            {
                QueueName = "ArmutLocalStack-Test.fifo",
                Attributes = new Dictionary<string, string>
                {
                    {
                        "FifoQueue", "true"
                    },
                    {
                        "RedrivePolicy", JsonSerializer.Serialize(redrivePolicy)
                    },
                }
            };
            CreateQueueResponse createQueueResult =
                client.CreateQueueAsync(createQueueRequest).GetAwaiter().GetResult();
        }
    }
}