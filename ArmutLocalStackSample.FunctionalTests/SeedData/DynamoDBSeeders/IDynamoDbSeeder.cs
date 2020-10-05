using Amazon.DynamoDBv2;

namespace ArmutLocalStackSample.FunctionalTests.SeedData.DynamoDBSeeders
{
    public interface IDynamoDbSeeder
    {
        void Seed(AmazonDynamoDBClient client);

        void CreateTable(AmazonDynamoDBClient client);
    }
}