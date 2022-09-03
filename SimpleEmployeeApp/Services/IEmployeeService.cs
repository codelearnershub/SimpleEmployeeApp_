using SimpleEmployeeApp.Entities;
using SimpleEmployeeApp.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleEmployeeApp.Services
{
    public interface IEmployeeService
    {
        Employee Login(string code, string password);
        Employee Create(string firstName, string lastName, string email, string password, Gender gender, Role role, string phone);

        void GetAll();
        Employee GetById(int id);
        Employee GetByCode(string employeeCode);
        Employee Update(int id, string firstName, string lastName, Gender gender, Role role, string phone);
        void Delete(int id);
        void PrintEmployee(Employee employee);
        void ChangePassword(string code, string oldPassword, string newPassword);
    }
}
