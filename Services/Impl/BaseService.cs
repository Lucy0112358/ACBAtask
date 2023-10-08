using ACBAbankTask.Services.Interfaces;

namespace ACBAbankTask.Services.Impl
{
    public class BaseService : IBaseService
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}