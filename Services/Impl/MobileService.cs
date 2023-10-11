using ACBAbankTask.DataModels;
using ACBAbankTask.Repository.Impl;
using ACBAbankTask.Repository.Interfaces;
using ACBAbankTask.Services.Interfaces;

namespace ACBAbankTask.Services.Impl
{
    public class MobileService : BaseService, IMobileService
    {
        private readonly IMobileRepository _mobileRepository;
        public MobileService(IMobileRepository mobileRepository)
        {
            _mobileRepository = mobileRepository;
        }

        public async Task<int> SaveMobile(MobileDto number)
        {
            try
            {
                return await _mobileRepository.SaveMobile(number);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
