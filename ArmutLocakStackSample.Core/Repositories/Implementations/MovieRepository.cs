using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using ArmutLocalStackSample.Core.Entities;
using ArmutLocalStackSample.Core.Repositories.Contracts;

namespace ArmutLocalStackSample.Core
{
    public class MovieRepository : Repository<MovieEntity>, IMovieRepository
    {
        public MovieRepository(IDynamoDBContext context) : base(context)
        {
        }

        public async Task<MovieEntity> GetMovieByIdAsync(Guid movieId, CancellationToken token = default)
        {
            DynamoDBOperationConfig operationConfig = new DynamoDBOperationConfig
            {
                IndexName = Constants.MoiveTableMovieIdGsi,
            };

            List<MovieEntity> result = await _context.QueryAsync<MovieEntity>(movieId, operationConfig)
                .GetRemainingAsync(token);

            return result.FirstOrDefault();
        }
    }
}