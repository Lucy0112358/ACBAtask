using ACBAbankTask.DataModels;

namespace ACBAbankTask.Repository.Interfaces
{
    public interface IMobileRepository
    {
        Task<int> SaveMobile(MobileDto number);
    }
}
