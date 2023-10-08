using ACBAbankTask.DataModels;
using ACBAbankTask.Entities;
using ACBAbankTask.Models;

namespace ACBAbankTask.Services.Interfaces
{
    public interface ICustomerService : IBaseService
    {
        Task<int> CreateCustomer(CustomerDto customer, List<DocumentDto> documents, List<AddressDto> address, List<MobileDto> mobile);

        Task<bool> EditCustomerAsync(int customerId, CustomerDto updatedCustomer);

        List<object> SearchCustomers(string name, string surname, string email, string mobile, int pageNumber, string document);

        Task<bool> DeleteCustomerAsync(int customerId);

    }
}