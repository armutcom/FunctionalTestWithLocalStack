using System;
using System.Threading;
using System.Threading.Tasks;
using ArmutLocalStackSample.Core.Entities;

namespace ArmutLocalStackSample.Core.Repositories.Contracts
{
    public interface IMovieRepository : IRepository<MovieEntity>
    {
        Task<MovieEntity> GetMovieByIdAsync(Guid movieId, CancellationToken token = default);
    }
}