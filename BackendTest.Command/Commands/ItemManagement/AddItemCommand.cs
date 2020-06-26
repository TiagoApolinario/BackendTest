using BackendTest.Command.EFInfraStructure;
using BackendTest.Domain;
using BackendTest.Utils;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BackendTest.Command.Commands.ItemManagement
{
    public sealed class AddItemCommand : IRequest<Result<Guid>>
    {
        public string Name { get; }

        public AddItemCommand(string name)
        {
            Name = name;
        }

        public sealed class AddItemCommandHandler : IRequestHandler<AddItemCommand, Result<Guid>>
        {
            private readonly UnitOfWork _unitOfWork;

            public AddItemCommandHandler(string connectionString)
            {
                _unitOfWork = new UnitOfWork(connectionString);
            }

            public async Task<Result<Guid>> Handle(AddItemCommand request, CancellationToken cancellationToken)
            {
                var defaultStatusList = await _unitOfWork.Status.GetAllAsync();
                var newItemResult = Item.Create(request.Name, defaultStatusList);
                if (newItemResult.IsFailure)
                    return Result.Failure<Guid>(newItemResult.ErrorMessage);

                await _unitOfWork.Item.AddAsync(newItemResult.Value);
                await _unitOfWork.CompleteAsync();

                return Result.Success(newItemResult.Value.Id);
            }
        }
    }
}
