using Dapper;
using MediatR;
using System;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace BackendTest.Query.ItemManagement.ItemDetails
{
    public sealed class ItemDto
    {
        public Guid Id { get; }
        public string Name { get; }
        public DateTime StatusChangedDate { get; }
        public string Status { get; }
    }

    public sealed class GetItemDetailsQuery : IRequest<ItemDto>
    {
        public Guid Id { get; }

        public GetItemDetailsQuery(Guid id)
        {
            Id = id;
        }

        public sealed class GetItemDetailsQueryHandler : IRequestHandler<GetItemDetailsQuery, ItemDto>
        {
            private readonly string _connectionString;

            public GetItemDetailsQueryHandler(string connectionString)
            {
                _connectionString = connectionString;
            }

            public async Task<ItemDto> Handle(GetItemDetailsQuery request, CancellationToken cancellationToken)
            {
                var sql = @"
                    SELECT TOP 1
	                    [Items].[Id],
	                    [Items].[Name],
	                    [ItemStatus].[StatusChangedDate],
	                    [Status].[Name] AS [Status]
                    FROM [ItemStatus]
                    INNER JOIN [Items] ON [Items].[Id] = [ItemStatus].[ItemId]
                    INNER JOIN [Status] ON [Status].[Id] = [ItemStatus].[StatusId]
                    WHERE [ItemStatus].[ItemId] = @Id
                    ORDER BY [ItemStatus].[StatusChangedDate] DESC;
                ";

                using (var connection = new SqlConnection(_connectionString))
                {
                    var items = await connection.QuerySingleOrDefaultAsync<ItemDto>(sql, new { request.Id });
                    return items;
                }
            }
        }
    }
}
