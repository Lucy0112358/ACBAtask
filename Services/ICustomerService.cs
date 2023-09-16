using ACBAbankTask.Models;

namespace ACBAbankTask.Services
{
    public interface ICustomerService : IBaseService
    {
        Task<int> CreateCustomer(CustomerDto customer);

        Task<bool> EditCustomerAsync(int customerId, CustomerDto updatedCustomer);

        Task<List<object>> SearchCustomers(string name, string surname, string email, string mobile);

        Task<bool> DeleteCustomerAsync(int customerId);

        Task<List<object>> GetAllCustomersAsync();
    }
}