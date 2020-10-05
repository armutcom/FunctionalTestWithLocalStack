using Xunit;

namespace ArmutLocalStackSample.FunctionalTests.Fixtures
{
    [CollectionDefinition(nameof(ApiTestCollection))]
    public class ApiTestCollection : ICollectionFixture<LocalStackFixture>, ICollectionFixture<TestServerFixture>
    {

    }
}