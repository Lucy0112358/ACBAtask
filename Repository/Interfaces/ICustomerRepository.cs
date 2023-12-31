﻿using ACBAbankTask.DataModels;
using ACBAbankTask.Models;

namespace ACBAbankTask.Repository.Interfaces
{
    public interface ICustomerRepository
    {
        Task<int> CreateCustomer(CustomerDto customer);
        Task<bool> EditCustomerAsync(int customerId, CustomerDto updatedCustomer);
        Task<bool> DeleteCustomerAsync(int customerId);
        List<object> SearchCustomers(string name, string surname, string email, string mobile, int pageNumber, string document);

    }
}
