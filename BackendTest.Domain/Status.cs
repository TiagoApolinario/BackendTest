using BackendTest.Domain.Core;

namespace BackendTest.Domain
{
    public class Status : AggregateRoot
    {
        public string Name { get; private set; }

        protected Status() { }
    }
}
