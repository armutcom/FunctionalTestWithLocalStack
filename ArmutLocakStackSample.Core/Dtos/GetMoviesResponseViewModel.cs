using System.Collections.Generic;

namespace ArmutLocalStackSample.Core.Dtos
{
    public class GetMoviesResponseViewModel
    {
        public IList<GetMovieResponseViewModel> Movies { get; set; }
    }
}