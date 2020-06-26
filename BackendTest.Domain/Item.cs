using BackendTest.Domain.Core;
using BackendTest.Utils;
using System.Collections.Generic;
using System.Linq;

namespace BackendTest.Domain
{
    public class Item : AggregateRoot
    {
        //Following DDD principles, the entity should always limit access to its properties, that's why all properties are private
        public string Name { get; private set; }

        //That's a good approach to not expose lists. It should be imutable and adding or removing items is knowledge of the entity itself
        private readonly List<ItemStatus> _statusHistory = new List<ItemStatus>();
        public virtual IReadOnlyList<ItemStatus> StatusHistory => _statusHistory.ToList();

        //It should be "protected" instead of private because EF needs a contructor
        protected Item() { }

        //Private constructor. Following DDD principles, the Entity is the one responsible to know how it gets created
        private Item(string name, Status status)
            : this()
        {
            Name = name;
            _statusHistory.Add(new ItemStatus(status));
        }

        //This method is the only one responsible to create a new Item
        //It will always create an item in a valid status, with all the validations in just one single place
        public static Result<Item> Create(string name, IEnumerable<Status> statusList)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Result.Failure<Item>("'Name' is required");

            if (statusList == null || statusList.Count() == 0)
                return Result.Failure<Item>("A list of default status have not been received");

            //It will always create a new item with the default status of "A"
            //OBS1: There is a better way to solve this situation where an enum is mapped to a table in the DB,
            //but for simplicity I'm doning that way;
            //OBS2: I'm getting a whole list of status to remove logic from the command, the domain object "Item"
            //is the one that should know its default status in every creation. Getting the whole list of status 
            //could lead to performance problems, but as said in OBS1, there's a better way to address this situation
            var defaultStatus = statusList.Single(status => status.Name.Equals(DefaultStatus.A.ToString()));

            return Result.Success(new Item(name, defaultStatus));
        }
    }
}
