using CQRSSample.Data;
using CQRSSample.Models;
using Dapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace CQRSSample.Features.Queries
{
    public class GetItemByIdQuery : IRequest<Item>
    {
        public int Id { get; set; }
    }

    public class GetItemByIdQueryHandler : IRequestHandler<GetItemByIdQuery, Item>
    {
        private readonly AppDbContext _dbContext;

        public GetItemByIdQueryHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Item> Handle(GetItemByIdQuery request, CancellationToken cancellationToken)
        {
            using (var connection = _dbContext.CreateConnection())
            {
                var sql = "SELECT * FROM Items WHERE Id = @Id";
                var item = await connection.QuerySingleOrDefaultAsync<Item>(sql, new { Id = request.Id });
                return item;
            }

        
        }
    }
}
