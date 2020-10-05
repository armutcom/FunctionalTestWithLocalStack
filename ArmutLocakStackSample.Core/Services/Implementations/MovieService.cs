using System;
using System.Net;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Amazon.SQS;
using Amazon.SQS.Model;
using ArmutLocalStackSample.Core.Dtos;
using ArmutLocalStackSample.Core.Entities;
using ArmutLocalStackSample.Core.Repositories.Contracts;
using ArmutLocalStackSample.Core.Services.Contracts;
using ArmutLocalStackSample.Core.Validators;
using Mapster;
using Microsoft.Extensions.Options;

namespace ArmutLocalStackSample.Core.Services.Implementations
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IValidatorService _validatorService;
        private readonly IAmazonSQS _amazonSqs;
        private readonly SqsQueueConfig _sqsQueueConfig;

        public MovieService(IMovieRepository movieRepository, IValidatorService validatorService, IAmazonSQS amazonSqs, IOptions<SqsQueueConfig> options)
        {
            _movieRepository = movieRepository;
            _validatorService = validatorService;
            _amazonSqs = amazonSqs;
            _sqsQueueConfig = options.Value;
        }

        public async Task<GetMovieResponseModel> GetMovieByIdAsync(Guid movieId, CancellationToken token)
        {
            Contract.Requires<Exception>(movieId != Guid.Empty, nameof(movieId));

            MovieEntity movieEntity = await _movieRepository.GetMovieByIdAsync(movieId, token);

            GetMovieResponseModel responseModel = await movieEntity.BuildAdapter().AdaptToTypeAsync<GetMovieResponseModel>();

            return responseModel;
        }

        public async Task<AddMovieResponseModel> AddMovieAsync(AddMovieRequestModel model, CancellationToken token = default)
        {
            try
            {
                await _validatorService.ValidationCheck<AddMovieRequestModelValidator, AddMovieRequestModel>(model);

                MovieEntity entity = await model.BuildAdapter().AdaptToTypeAsync<MovieEntity>();

                await _movieRepository.AddAsync(entity, token);

                AddMovieResponseModel responseModel = await entity.BuildAdapter().AdaptToTypeAsync<AddMovieResponseModel>();

                return responseModel;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> CommentMovie(CommentModel model, CancellationToken token = default)
        {
            try
            {
                await _validatorService.ValidationCheck<CommentModelValidator, CommentModel>(model);

                string serializedObject = JsonSerializer.Serialize(model);

                string queueName = _sqsQueueConfig.QueueName;

                GetQueueUrlResponse response = await _amazonSqs.GetQueueUrlAsync(queueName, token);

                var sendMessageRequest = new SendMessageRequest
                {
                    QueueUrl = response.QueueUrl,
                    MessageGroupId = model.MovieId.ToString(),
                    MessageDeduplicationId = Guid.NewGuid().ToString(),
                    MessageBody = serializedObject
                };

                var messageResponse = await _amazonSqs.SendMessageAsync(sendMessageRequest, token);

                if (messageResponse.HttpStatusCode != HttpStatusCode.OK)
                {
                    return false;
                }

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}