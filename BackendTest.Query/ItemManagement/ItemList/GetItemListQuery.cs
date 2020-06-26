using Dapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace BackendTest.Query.ItemManagement.ItemList
{
    public sealed class GetItemListQuery : IRequest<IEnumerable<ItemDto>>
    {
        public sealed class GetItemListQueryHandler : IRequestHandler<GetItemListQuery, IEnumerable<ItemDto>>
        {
            private readonly string _connectionString;

            public GetItemListQueryHandler(string connectionString)
            {
                _connectionString = connectionString;
            }

            public async Task<IEnumerable<ItemDto>> Handle(GetItemListQuery request, CancellationToken cancellationToken)
            {
                var sql = "SELECT [Id], [Name] FROM [Items] ORDER BY [Name];";

                using (var connection = new SqlConnection(_connectionString))
                {
                    var items = await connection.QueryAsync<ItemDto>(sql);
                    return items;
                }
            }
        }
    }

    //Dapper can map to private properties using reflection, that way this dto can be set only by this query
    public sealed class ItemDto
    {
        public Guid Id { get; }
        public string Name { get; }
    }
}