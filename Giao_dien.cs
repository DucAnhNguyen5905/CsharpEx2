using Data_Process;
using Data_Process.STRUCT;
using System;

namespace CsharpBaitap2
{
    class Program
    {
        static void Main(string[] args)
        {
            Giao_dien giaoDien = new Giao_dien();
            giaoDien.Run();
        }
    }

    class Giao_dien
    {
        private EmployeeManager manager = new EmployeeManager();

        public void Run()
        {
            while (true)
            {
                Console.WriteLine("Chon loai nhan vien de them:");
                Console.WriteLine("1. Nhan vien toan thoi gian");
                Console.WriteLine("2. Nhan vien ban thoi gian");
                Console.WriteLine("3. Nhan vien thuc tap");
                Console.WriteLine("4. Hien thi danh sach nhan vien");
                Console.Write("Lua chon cua ban: ");

                if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > 4)
                {
                    Console.WriteLine("Lua chon khong hop le. Vui long chon lai.");
                    continue;
                }

                if (choice == 4)
                    break;

                string name = GetValidEmployeeName();
                string id = GetValidEmployeeId();

                Employee employee = null;
                switch (choice)
                {
                    case 1:
                        employee = CreateFullTimeEmployee(id, name);
                        break;
                    case 2:
                        employee = CreatePartTimeEmployee(id, name);
                        break;
                    case 3:
                        employee = CreateIntern(id, name);
                        break;
                }

                manager.AddEmployee(employee);
                Console.WriteLine("Nhan vien da duoc them thanh cong!\n");
            }

            DisplayEmployees();
        }

        private string GetValidEmployeeName()
        {
            string name;
            do
            {
                Console.Write("Nhap ten nhan vien: ");
                name = Console.ReadLine();
                if (!Validate_Data.Validate.ValidateName(name))
                {
                    Console.WriteLine("Ten khong hop le. Vui long nhap lai.");
                }
            } while (!Validate_Data.Validate.ValidateName(name));
            return name;
        }

        private string GetValidEmployeeId()
        {
            string id;
            do
            {
                Console.Write("Nhap ID nhan vien (chi chu va so): ");
                id = Console.ReadLine();
                if (!Validate_Data.Validate.ValidateId(id))
                {
                    Console.WriteLine("ID khong hop le. Vui long nhap lai.");
                }
            } while (!Validate_Data.Validate.ValidateId(id));
            return id;
        }

        private FullTimeEmployee CreateFullTimeEmployee(string id, string name)
        {
            Console.Write("Nhap luong hang thang: ");
            double monthlySalary = double.Parse(Console.ReadLine());
            return new FullTimeEmployee { Id = id, Name = name, MonthlySalary = monthlySalary };
        }

        private PartTimeEmployee CreatePartTimeEmployee(string id, string name)
        {
            Console.Write("Nhap luong theo gio: ");
            double hourlyRate = double.Parse(Console.ReadLine());
            Console.Write("Nhap so gio lam viec: ");
            int hoursWorked = int.Parse(Console.ReadLine());
            return new PartTimeEmployee { Id = id, Name = name, HourlyRate = hourlyRate, HoursWorked = hoursWorked };
        }

        private Intern CreateIntern(string id, string name)
        {
            Console.Write("Nhap tro cap: ");
            double allowance = double.Parse(Console.ReadLine());
            return new Intern { Id = id, Name = name, Allowance = allowance };
        }

        private void DisplayEmployees()
        {
            Console.WriteLine("\nThong tin nhan vien:");
            foreach (var emp in manager.GetAllEmployees())
            {
                Console.WriteLine($"ID: {emp.Id}, Ten: {emp.Name}");

                if (emp is FullTimeEmployee fullTimeEmp)
                {
                    Console.WriteLine($"Loai nhan vien: Toan thoi gian");
                    Console.WriteLine($"Luong hang thang: {fullTimeEmp.MonthlySalary}");
                }
                else if (emp is PartTimeEmployee partTimeEmp)
                {
                    Console.WriteLine($"Loai nhan vien: Ban thoi gian");
                    Console.WriteLine($"Luong theo gio: {partTimeEmp.HourlyRate}");
                    Console.WriteLine($"So gio lam viec: {partTimeEmp.HoursWorked}");
                }
                else if (emp is Intern internEmp)
                {
                    Console.WriteLine($"Loai nhan vien: Thuc tap");
                    Console.WriteLine($"Tro cap: {internEmp.Allowance}");
                }

                Console.WriteLine($"Tong luong: {emp.CalculateSalary()}");
                Console.WriteLine(); // Dòng trống để ngăn cách giữa các nhân viên
                Console.ReadKey();
            }
        }

    }
}

