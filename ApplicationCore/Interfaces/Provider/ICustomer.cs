using System;
using System.Collections.Generic;
using System.Text;
using ApplicationCore.Entities;

namespace ApplicationCore.Interfaces.Provider
{
    public interface IUserCustomer 
    {
        Customer saveCustomer(int id,string firstName,string lastName,string email,string password, string confirmPassword, string phoneNumber);

        void deleteCustomer(int id);
    }
}
