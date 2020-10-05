using System;
using System.Threading;
using System.Threading.Tasks;
using ArmutLocalStackSample.Core.Dtos;

namespace ArmutLocalStackSample.Core.Services.Contracts
{
    public interface IMovieService
    {
        Task<GetMovieResponseModel> GetMovieByIdAsync(Guid movieId, CancellationToken token = default);

        Task<AddMovieResponseModel> AddMovieAsync(AddMovieRequestModel model, CancellationToken token = default);

        Task<bool> CommentMovie(CommentModel model, CancellationToken token = default);
    }
}