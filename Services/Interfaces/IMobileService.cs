using ACBAbankTask.DataModels;

namespace ACBAbankTask.Services.Interfaces
{
    public interface IMobileService
    {
        Task<int> SaveMobile(MobileDto number);
    }
}
