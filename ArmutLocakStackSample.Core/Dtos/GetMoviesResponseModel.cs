using System.Collections.Generic;

namespace ArmutLocalStackSample.Core.Dtos
{
    public class GetMoviesResponseModel
    {
        public IList<GetMovieResponseModel> Movies { get; set; }
    }
}