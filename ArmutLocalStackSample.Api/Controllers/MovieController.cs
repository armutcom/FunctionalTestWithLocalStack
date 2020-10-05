using System;
using System.Threading;
using System.Threading.Tasks;
using ArmutLocalStackSample.Core.Dtos;
using ArmutLocalStackSample.Core.Services.Contracts;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArmutLocalStackSample.Api.Controllers
{
    [Route("api/movies")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(AddMovieResponseViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("")]
        public async Task<IActionResult> AddMovie([FromBody] AddMovieRequestViewModel addMovieModel, CancellationToken token = default)
        {
            AddMovieRequestModel requestModel = await addMovieModel.BuildAdapter().AdaptToTypeAsync<AddMovieRequestModel>();

            AddMovieResponseModel response = await _movieService.AddMovieAsync(requestModel, token);

            AddMovieResponseViewModel responseViewModel =
                await response.BuildAdapter().AdaptToTypeAsync<AddMovieResponseViewModel>();

            if (responseViewModel == null)
            {
                return BadRequest();
            }

            return Ok(responseViewModel);
        }

        [HttpGet]
        [ProducesResponseType(typeof(GetMovieResponseViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMovie([FromQuery] Guid movieId, CancellationToken token = default)
        {
            if (movieId == Guid.Empty)
            {
                return BadRequest();
            }

            GetMovieResponseModel result = await _movieService.GetMovieByIdAsync(movieId, token);

            GetMovieResponseViewModel response = await result.BuildAdapter().AdaptToTypeAsync<GetMovieResponseViewModel>();

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpPost]
        [Route("comment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CommentMovie([FromBody] CommentMovieRequestModel requestModel,
            CancellationToken token = default)
        {
            CommentModel commentModel = await requestModel.BuildAdapter().AdaptToTypeAsync<CommentModel>();

            bool response = await _movieService.CommentMovie(commentModel, token);

            if (!response)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
