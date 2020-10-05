using Amazon.SQS;

namespace ArmutLocalStackSample.FunctionalTests.SeedData.SqsSeeders
{
    public interface ISqsSeeder
    {
        void CreateQueue(AmazonSQSClient client);
    }
}