using System.Threading;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using System.Text.Json;

namespace ArmutLocalStackSample.Core
{
    public interface IRepository<TModel>
    {
        Task<Document> AddAsync(TModel model, CancellationToken token = default);
    }

    public abstract class Repository<TModel> : IRepository<TModel>
    {
        protected readonly IDynamoDBContext _context;

        protected readonly Table _table;

        protected Repository(IDynamoDBContext context)
        {
            _context = context;
            _table = _context.GetTargetTable<TModel>();
        }

        public Task<Document> AddAsync(TModel model, CancellationToken token = default)
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                IgnoreNullValues = true
            };
            var modelJson = JsonSerializer.Serialize(model, options);
            var item = Document.FromJson(modelJson);
            var task = _table.PutItemAsync(item, token);
            return task;
        }
    }
}