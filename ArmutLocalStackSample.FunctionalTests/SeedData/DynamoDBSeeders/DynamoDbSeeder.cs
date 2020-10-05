using System;
using System.Linq;
using System.Text.Json;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;

namespace ArmutLocalStackSample.FunctionalTests.SeedData.DynamoDBSeeders
{
    public class DynamoDbSeeder
    {
        public static void Seed(AmazonDynamoDBClient client)
        {
            var installers = typeof(DynamoDbMovieSeeder).Assembly.ExportedTypes
                .Where(m => typeof(IDynamoDbSeeder).IsAssignableFrom(m) && !m.IsInterface && !m.IsAbstract)
                .Select(Activator.CreateInstance)
                .Cast<IDynamoDbSeeder>()
                .ToList();

            installers.ForEach(m => m.Seed(client));
        }

        public static void CreateTable(AmazonDynamoDBClient client)
        {
            var installers = typeof(DynamoDbMovieSeeder).Assembly.ExportedTypes
                .Where(m => typeof(IDynamoDbSeeder).IsAssignableFrom(m) && !m.IsInterface && !m.IsAbstract)
                .Select(Activator.CreateInstance)
                .Cast<IDynamoDbSeeder>()
                .ToList();

            installers.ForEach(m => m.CreateTable(client));
        }

        public static void Add<TModel>(AmazonDynamoDBClient client, TModel model)
        {
            DynamoDBContext context = new DynamoDBContext(client);
            var table = context.GetTargetTable<TModel>();

            JsonSerializerOptions options = new JsonSerializerOptions
            {
                IgnoreNullValues = true
            };
            var modelJson = JsonSerializer.Serialize(model, options);
            Document item = Document.FromJson(modelJson);
            table.PutItemAsync(item).GetAwaiter().GetResult();
        }
    }
}