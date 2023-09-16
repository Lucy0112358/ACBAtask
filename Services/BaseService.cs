namespace ACBAbankTask.Services
{
    public class BaseService : IBaseService
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}