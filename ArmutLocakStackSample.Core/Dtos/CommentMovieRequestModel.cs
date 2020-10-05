using System;

namespace ArmutLocalStackSample.Core.Dtos
{
    public class CommentMovieRequestModel
    {
        public Guid MovieId { get; set; }

        public string Comment { get; set; }
    }
}