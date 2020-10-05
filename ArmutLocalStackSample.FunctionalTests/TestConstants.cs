using System;

namespace ArmutLocalStackSample.FunctionalTests
{
    public static class TestConstants
    {
        private static Guid _commentId;

        public static Guid GetDirectorId()
        {
            if (_commentId == Guid.Empty)
            {
                _commentId = Guid.NewGuid();
            }

            return _commentId;
        }

        private static Guid _postId;

        public static Guid GetMovieId()
        {
            if (_postId == Guid.Empty)
            {
                _postId = Guid.NewGuid();
            }

            return _postId;
        }

        public static string GetMovieName() => "Ocean's Eleven";

        public static string QueueName = "ArmutLocalStack-Test.fifo";
    }
}