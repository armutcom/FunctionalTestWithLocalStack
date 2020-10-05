using System;

namespace ArmutLocalStackSample.Core.Dtos
{
    public class AddMovieRequestViewModel
    {
        public Guid DirectorId { get; set; }

        public string MovieName { get; set; }
    }
}