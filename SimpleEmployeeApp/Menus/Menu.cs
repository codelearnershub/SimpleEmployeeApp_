using SimpleEmployeeApp.Enums;
using SimpleEmployeeApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleEmployeeApp.Menus
{
    public class Menu
    {
        private readonly IEmployeeService employeeService = new EmployeeService();

        public void MyMenu()
        {
            var flag = true;

            while (flag)
            {
                PrintMenu();
                Console.Write("\nPlease enter your option: ");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        Console.Write("Enter your emloyee code: ");
                        var code = Console.ReadLine();
                        Console.Write("Enter your password: ");
                        var password = Console.ReadLine();
                        var employee = employeeService.Login(code, password);
                        if(employee == null)
                        {
                            Console.WriteLine("Invalid code or password!");
                        }
                        else
                        {
                            if (employee.Role == Role.Admin)
                            {
                                AdminMenu();
                            }
                            else
                            {
                                StaffMenu(employee);
                            }
                        }

                        break;
                    case "0":
                        flag = false;
                        Console.WriteLine("Thank you for using our App...");
                        break;
                    default:
                        Console.WriteLine("Invalid input!");
                        break;
                }
            }
        }

        public void AdminMenu()
        {
            var flag = true;

            while (flag)
            {
                PrintAdminMenu();
                Console.Write("\nPlease enter your option: ");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        Console.Write("Enter employee first name: ");
                        var firstName = Console.ReadLine();
                        Console.Write("Enter employee last name: ");
                        var lastName = Console.ReadLine();
                        Console.Write("Enter employee email: ");
                        var email = Console.ReadLine();
                        Console.Write("Enter employee phone number: ");
                        var phone = Console.ReadLine();
                        var password = phone;

                        int role;
                        do
                        {
                            Console.Write("Enter employee role: \nenter 1 for Admin\nenter 2 for Security\nenter 3 for Cleaner\nenter 4 for Manager: ");
                        } while (!(int.TryParse(Console.ReadLine(), out role) && IsValid(role, 1, 4)));
                        
                        
                        int gender;
                        do
                        {
                            Console.Write("Enter employee gender: \nEnter 1 for Male\nEnter 2 for Female\n 3 for RatherNotSay: ");
                        } while( !(int.TryParse(Console.ReadLine(), out gender) && IsValid(gender, 1,3)) );

                        var employee = employeeService.Create(firstName, lastName, email, password, (Gender)gender, (Role)role, phone);

                        Console.WriteLine("Employee added successfully.");
                        HoldScreen();
                        break;

                    case "2":
                        employeeService.GetAll();
                        HoldScreen();
                        break;

                    case "3":
                        Console.Write("Enter the id of employee to view: ");
                        int id = int.Parse(Console.ReadLine());
                        var emp = employeeService.GetById(id); 

                        if(emp != null)
                        {
                            employeeService.PrintEmployee(emp);
                        }
                        else
                        {
                            Console.WriteLine("Employee not found!");
                        }
                        HoldScreen();
                        break;
                        
                    case "4":
                        Console.Write("Enter the id of employee to view: ");
                        int editId = int.Parse(Console.ReadLine());

                        Console.Write("Enter employee first name: ");
                        var fName = Console.ReadLine();
                        Console.Write("Enter employee last name: ");
                        var lName = Console.ReadLine();
                        Console.Write("Enter employee email: ");
                        
                        var phoneNo = Console.ReadLine();

                        int newRole;
                        do
                        {
                            Console.Write("Enter employee role: \nenter 1 for Admin\nenter 2 for Security\nenter 3 for Cleaner\nenter 4 for Manager: ");
                        } while (!(int.TryParse(Console.ReadLine(), out newRole) && IsValid(newRole, 1, 4)));


                        int newGender;
                        do
                        {
                            Console.Write("Enter employee gender: \nEnter 1 for Male\nEnter 2 for Female\n 3 for RatherNotSay: ");
                        } while (!(int.TryParse(Console.ReadLine(), out newGender) && IsValid(newGender, 1, 3)));

                        employeeService.Update(editId, fName, lName, (Gender)newGender, (Role)newRole, phoneNo);

                        HoldScreen();
                        break;

                    case "5":
                        Console.Write("Enter the id of employee to delete: ");
                        int empId = int.Parse(Console.ReadLine());
                        employeeService.Delete(empId);

                        HoldScreen();
                        break;
                        
                    case "0":
                        flag = false;
                        break;
                    default:
                        Console.WriteLine("Invalid input!");
                        break;
                }
            }
        }

        public void StaffMenu(Employee employee)
        {
            var flag = true;

            while (flag)
            {
                PrintStaffMenu();
                Console.Write("\nPlease enter your option: ");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        employeeService.PrintEmployee(employee);
                        HoldScreen();

                        break;                 
                   
                    case "2":                  

                        Console.Write("Enter your first name: ");
                        var fName = Console.ReadLine();
                        Console.Write("Enter your last name: ");
                        var lName = Console.ReadLine();
                        Console.Write("Enter your phone nummber: ");
                        var phoneNo = Console.ReadLine();

                        int newGender;
                        do
                        {
                            Console.Write("Enter your gender: \nEnter 1 for Male\nEnter 2 for Female\nEnter 3 for RatherNotSay: ");
                        } while (!(int.TryParse(Console.ReadLine(), out newGender) && IsValid(newGender, 1, 3)));

                        employeeService.Update(employee.Id, fName, lName, (Gender)newGender, employee.Role, phoneNo);

                        HoldScreen();
                        break;

                    case "3":
                        Console.Write("Enter your old password: ");
                        var oldPassword = Console.ReadLine();
                        Console.Write("Enter your new password: ");
                        var newPassword = Console.ReadLine();
                        employeeService.ChangePassword(employee.Code, oldPassword, newPassword);

                        HoldScreen();
                        break;
                    case "0":
                        flag = false;
                        break;
                    default:
                        Console.WriteLine("Invalid input!");
                        break;
                }
            }
        }

        public void PrintMenu()
        {
            Console.WriteLine("Enter 1 to login.");
            Console.WriteLine("Enter 0 to exit.");
        }

        public void PrintAdminMenu()
        {
            Console.WriteLine("Enter 1 to add new Employee.");
            Console.WriteLine("Enter 2 to view all employees.");
            Console.WriteLine("Enter 3 to view an employee.");
            Console.WriteLine("Enter 4 to update an employee.");
            Console.WriteLine("Enter 5 to delete an employee.");
            Console.WriteLine("Enter 0 to go back to main menu.");
        }

        public void PrintStaffMenu()
        {
            Console.WriteLine("Enter 1 to view your details.");
            Console.WriteLine("Enter 2 to edit your profile.");
            Console.WriteLine("Enter 3 to change your password.");
            Console.WriteLine("Enter 0 to go back to main menu.");
        }

        private bool IsValid(int num, int start, int end)
        {
            return num >= start && num <= end;
        }
        
        private void HoldScreen()
        {
            Console.WriteLine("Press Enter key to continue.");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
