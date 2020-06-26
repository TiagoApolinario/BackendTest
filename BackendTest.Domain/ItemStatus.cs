using BackendTest.Domain.Core;
using System;

namespace BackendTest.Domain
{
    public class ItemStatus : Entity
    {
        public DateTime StatusChangedDate { get; private set; }
        public virtual Item Item { get; private set; }
        public virtual Status Status { get; private set; }

        protected ItemStatus() { }

        //'internal' method. Only the parent entit 'item' can create a new ItemStatus
        internal ItemStatus(Status status)
            : this()
        {
            Status = status;
            StatusChangedDate = DateTime.Now;
        }
    }
}
