using ACBAbankTask.DataModels;

namespace ACBAbankTask.Repository.Interfaces
{
    public interface IAddressRepository
    {
        Task<int> SaveAddress(AddressDto address);
    }
}
