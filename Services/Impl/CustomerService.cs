using System.Transactions;
using ACBAbankTask.DataModels;
using ACBAbankTask.Models;
using ACBAbankTask.Repository.Impl;
using ACBAbankTask.Repository.Interfaces;
using ACBAbankTask.Services.Interfaces;

namespace ACBAbankTask.Services.Impl
{
    public class CustomerService : BaseService, ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IDocumentRepository _documentRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IMobileRepository _mobileRepository;

        public CustomerService(ICustomerRepository customerRepository, IDocumentRepository documentRepository, IAddressRepository addressRepository, IMobileRepository mobileRepository)
        {
            _customerRepository = customerRepository;
            _documentRepository = documentRepository;
            _addressRepository = addressRepository;
            _mobileRepository = mobileRepository;
        }

        public async Task<int> CreateCustomer(CustomerDto customer, List<DocumentDto> documents, List<AddressDto> addresses, List<MobileDto> mobiles)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    int customerId = await _customerRepository.CreateCustomer(customer);

                    foreach (var document in documents)
                    {
                        document.customerId = customerId;
                        await _documentRepository.CreateDocumentAsync(document);
                    }

                    foreach (var address in addresses)
                    {
                        address.CustomerId = customerId;
                        await _addressRepository.SaveAddress(address);
                    }

                    foreach (var mobile in mobiles)
                    {
                        mobile.CustomerId = customerId;
                        await _mobileRepository.SaveMobile(mobile);
                    }

                    scope.Complete();

                    return customerId;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
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
