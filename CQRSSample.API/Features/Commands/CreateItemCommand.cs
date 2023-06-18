using CQRSSample.Data;
using CQRSSample.Models;
using Dapper;
using MediatR;
using System;

namespace CQRSSample.Features.Commands
{
    public class CreateItemCommand : IRequest<int>
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    public class CreateItemCommandHandler : IRequestHandler<CreateItemCommand, int>
    {
        private readonly AppDbContext _dbContext;

        public CreateItemCommandHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Handle(CreateItemCommand request, CancellationToken cancellationToken)
        {
            using (var connection = _dbContext.CreateConnection())
            {
                var newItem = new Item
                {
                    Name = request.Name,
                    Price = request.Price
                };

                var sql = @"INSERT INTO Items (Name, Price)
                        VALUES (@Name, @Price);
                        SELECT last_insert_rowid()";

                var itemId = await connection.ExecuteScalarAsync<int>(sql, newItem);

                return itemId;
            }
        }
    }
}
