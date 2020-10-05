using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using Amazon.SQS;
using Amazon.SQS.Model;
using ArmutLocalStackSample.Core;
using ArmutLocalStackSample.Core.Dtos;
using ArmutLocalStackSample.Core.Entities;
using ArmutLocalStackSample.FunctionalTests.Fixtures;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace ArmutLocalStackSample.FunctionalTests
{
    public abstract class BaseScenario
    {
        protected internal readonly TestServer TestServer;

        protected internal readonly HttpClient HttpClient;

        internal readonly IDynamoDBContext DynamoDbContext;

        internal readonly IAmazonSQS SqsClient;

        protected BaseScenario(TestServerFixture testServerFixture)
        {
            TestServer = testServerFixture.Server;
            HttpClient = testServerFixture.CreateClient();
            DynamoDbContext = TestServer.Services.GetRequiredService<IDynamoDBContext>();
            SqsClient = TestServer.Services.GetRequiredService<IAmazonSQS>();
        }

        protected async Task<bool> CheckMovieByMovieIdAsync(Guid movieId, CancellationToken token = default)
        {
            DynamoDBOperationConfig operationConfig = new DynamoDBOperationConfig
            {
                IndexName = Constants.MoiveTableMovieIdGsi,
            };

            List<MovieEntity> result = await DynamoDbContext.QueryAsync<MovieEntity>(movieId, operationConfig)
                .GetRemainingAsync(token);

            return result.Any(b => b.MovieId == movieId);
        }

        protected async Task<bool> IsItemInQueueAsync(Guid id)
        {
            GetQueueUrlResponse getQueueUrlResponse = await SqsClient.GetQueueUrlAsync(TestConstants.QueueName);

            ReceiveMessageRequest req = new ReceiveMessageRequest
            {
                MaxNumberOfMessages = 1,
                QueueUrl = getQueueUrlResponse.QueueUrl
            };
            ReceiveMessageResponse receiveMessages = await SqsClient.ReceiveMessageAsync(req);

            Message currentMessage = receiveMessages.Messages.FirstOrDefault();

            CommentModel deserializedObject = JsonSerializer.Deserialize<CommentModel>(currentMessage?.Body);

            return deserializedObject.MovieId == id;
        }
    }
}