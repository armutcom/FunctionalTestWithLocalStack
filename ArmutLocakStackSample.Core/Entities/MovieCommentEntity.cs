using System;
using Amazon.DynamoDBv2.DataModel;

namespace ArmutLocalStackSample.Core.Entities
{
    [DynamoDBTable("MovieComments")]
    public class MovieCommentEntity
    {
        [DynamoDBHashKey]
        public Guid MovieId { get; set; }

        [DynamoDBRangeKey]
        public string CreateDate { get; set; }

        public string Comment { get; set; }

        public Guid CommentId { get; set; }
    }
}