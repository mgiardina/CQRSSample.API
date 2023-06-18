using CQRSSample.Data;
using CQRSSample.Models;
using Dapper;
using MediatR;

namespace CQRSSample.Features.Queries
{
    public class GetItemsQuery : IRequest<List<Item>>
    {
    }

    public class GetItemsQueryHandler : IRequestHandler<GetItemsQuery, List<Item>>
    {
        private readonly AppDbContext _dbContext;

        public GetItemsQueryHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Item>> Handle(GetItemsQuery request, CancellationToken cancellationToken)
        {
            using (var connection = _dbContext.CreateConnection())
            {
                var sql = "SELECT * FROM Items";
                var items = await connection.QueryAsync<Item>(sql);

                return items.AsList();
            }
        }
    }
}
