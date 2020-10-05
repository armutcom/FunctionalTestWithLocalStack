using System;

namespace ArmutLocalStackSample.FunctionalTests.Routes
{
    public class InteractionRoutes
    {
        internal static readonly string Root = $"api/interaction";

        internal static string LikePost(Guid postId) => $"/{Root}/like/post/{postId}";

        internal static string UnLikePost(Guid postId) => $"/{Root}/unlike/post/{postId}";

        internal static string LikeComment(Guid commentId) => $"/{Root}/like/comment/{commentId}";

        internal static string UnLikeComment(Guid commentId) => $"/{Root}/unlike/comment/{commentId}";

        internal static string GetStatusChanges(Guid postId, bool includeComments = false) => $"/{Root}/exchange?id={postId}&include_comments={includeComments}";
    }
}