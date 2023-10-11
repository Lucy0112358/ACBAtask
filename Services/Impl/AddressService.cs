using ACBAbankTask.DataModels;
using ACBAbankTask.Repository.Interfaces;
using ACBAbankTask.Services.Interfaces;

namespace ACBAbankTask.Services.Impl
{
    public class AddressService : BaseService, IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        public AddressService(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public async Task<int> SaveAddress(AddressDto address)
        {
            return await _addressRepository.SaveAddress(address);
        }
    }
}
