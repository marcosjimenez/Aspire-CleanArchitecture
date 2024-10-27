using CleanArchitectureSample.Core.Aggregates;

namespace CleanArchitectureSample.Application.Contracts
{
    public interface IContactRepository : IBaseRepository<int, ContactEntity>
    {
        void CreateFakeContacts(int contactCount);
    }
}
