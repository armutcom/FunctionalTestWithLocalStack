using System;
using ArmutLocalStackSample.Core.Dtos;

namespace ArmutLocalStackSample.FunctionalTests.Routes
{
    public class MovieRoutes
    {
        internal static readonly string Root = $"api/movies";

        internal static string GetMovieById(Guid movieId) => $"{Root}?movieId={movieId}";

        internal static string GetNewsFeedByPagination(int pageSize, int areaLevel3Id, string variantId) =>
            $"{Root}/newsfeed?page_size={pageSize}&area_level3_id={areaLevel3Id}&variant_id={variantId}";

        internal static string CommentMovie() => $"{Root}/comment";
    }
}