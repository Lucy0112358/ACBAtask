using ACBAbankTask.DataModels;
using ACBAbankTask.Models;
using ACBAbankTask.Repository.Interfaces;
using ACBAbankTask.Services.Interfaces;

namespace ACBAbankTask.Services.Impl
{
    public class CustomerService : BaseService, ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public Task<int> CreateCustomer(CustomerDto customer, List<DocumentDto> documents, List<AddressDto> address, List<MobileDto> mobile)
        {
            return _customerRepository.CreateCustomer(customer, documents, address, mobile);
        }

        public Task<bool> EditCustomerAsync(int customerId, CustomerDto updatedCustomer)
        {
            return _customerRepository.EditCustomerAsync(customerId, updatedCustomer);
        }

        public List<object> SearchCustomers(string name, string surname, string email, string mobile, int pageNumber, string document)
        {
            return _customerRepository.SearchCustomers(name, surname, email, mobile, pageNumber, document);

        }

        public Task<bool> DeleteCustomerAsync(int customerId)
        {
            return _customerRepository.DeleteCustomerAsync(customerId);
        }

    }
}
