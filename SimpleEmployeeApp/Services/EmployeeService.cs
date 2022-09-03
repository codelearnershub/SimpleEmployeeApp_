using SimpleEmployeeApp.Entities;
using SimpleEmployeeApp.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleEmployeeApp.Services
{
    public class EmployeeService : IEmployeeService
    {
        public static List<Employee> employees = new List<Employee>()
        {
            new Employee()
            {
                Id = 1,
                Code = GenerateCode(1),
                FirstName = "Admin",
                LastName =  "Boss",
                Email = "admin@admin.com",
                Password = "password",
                Phone = "08099889988",
                Gender = Gender.Male,
                Role = Role.Admin,
                DateJoined = DateTime.Now,

            }
        };

        public Employee Create(string firstName, string lastName, string email, string password, Gender gender, Role role, string phone)
        {
            int id = employees.Count != 0 ? employees[employees.Count - 1].Id + 1 : 1;
            string code = GenerateCode(id);
            DateTime dateJoined = DateTime.Now;

            var employee = new Employee
            {
                Id = id,
                Code = code,
                FirstName = firstName,
                LastName = lastName,
                Phone = phone,
                Email = email,
                Password = password,
                Gender = gender,
                Role = role,
                DateJoined = dateJoined

            };

            employees.Add(employee);
            return employee;

        }

        public void Delete(int id)
        {
            var employee = GetById(id);

            if(employee == null)
            {
                Console.WriteLine($"Employee with the id: {id} not found");
            }
            employees.Remove(employee);
            Console.WriteLine($"Employee with the id: {id} successfully deleted.");
        }

        public void PrintEmployee(Employee employee)
        {
            Console.WriteLine($"Id : {employee.Id}\tNamd: {employee.LastName} {employee.FirstName}\tCode: {employee.Code}");
        }

        public void GetAll()
        {
            foreach(var employee in employees)
            {
                PrintEmployee(employee);
            }
        }

        public Employee GetByCode(string employeeCode)
        {
            foreach (Employee employee in employees)
            {
                if (employee.Code == employeeCode)
                {
                    return employee;
                }
            }
            return null;
        }

        public Employee GetById(int id)
        {
            foreach(Employee employee in employees)
            {
                if(employee.Id == id)
                {
                    return employee;
                }
            }
            return null;
        }

        public Employee Update(int id, string firstName, string lastName, Gender gender, Role role, string phone)
        {
            var employee = GetById(id);

            if (employee == null)
            {
                Console.WriteLine($"Employee with the id: {id} not found");
            }
            employee.FirstName = firstName;
            employee.LastName = lastName;
            employee.Gender = gender;
            employee.Role = role;
            employee.Phone = phone;

            return employee;

        }

        public void ChangePassword(string code, string oldPassword, string newPassword)
        {
            var employee = GetByCode(code);

            if(employee == null)
            {
                Console.WriteLine($"Employee with the code: {code} not found");
                return;
            }

            if(employee.Password != oldPassword)
            {
                Console.WriteLine("Invalid code or password!");
                return;
            }

            employee.Password = newPassword;
            Console.WriteLine("You successfully change your password.");

        }

        public Employee Login(string code, string password)
        {
            var employee = GetByCode(code);

            if(employee != null && employee.Password == password)
            {
                return employee;
            }
            return null;
        }

        private static string GenerateCode(int id)
        {
            return $"EMP-{id.ToString("0000")}";
        }

    }
}
