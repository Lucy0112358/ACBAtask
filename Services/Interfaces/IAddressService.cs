using ACBAbankTask.DataModels;

namespace ACBAbankTask.Services.Interfaces
{
    public interface IAddressService
    {
        Task<int> SaveAddress(AddressDto address);
    }
}
