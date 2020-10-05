using System;

namespace ArmutLocalStackSample.Core.Dtos
{
    public class AddMovieResponseViewModel
    {
        public Guid DirectorId { get; set; }

        public string CreateDate { get; set; }

        public Guid MovieId { get; set; }
    }
}