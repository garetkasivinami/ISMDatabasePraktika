using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using DataBase;
namespace ConsoleApp
{
    class Program
    {
        public static string[] HeadTableWords = { "Id", "Ім'я", "Прізвище", "По батькові", "Логін", "Пароль", "Код", "Номер", "Адмін", "Країна"};
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;
            bool is_work = true;
            while (is_work)
            {
                Console.WriteLine("Оберіть таблицю:\n 1 - користувачі\n 2 - бренди\n 3 - категорії\n 4 - предмети\n 0 - вихід");
                switch(Console.ReadLine()) 
                {
                    case "0":
                        is_work = false;
                        break;
                    case "1":
                        UsersTable();
                        break;
                    case "2":
                        BrandsTable();
                        break;
                    case "3":
                        CategoriesTable();
                        break;
                    case "4":
                        ItemsTable();
                        break;
                }
            }
            Console.ReadKey();
        }
        public static void BrandsTable()
        {
            bool is_work = true;
            while (is_work)
            {
                Console.WriteLine("1 - вивести таблицю\n2 - добавити новий запис\n0- вихід");
                switch (Console.ReadLine())
                {
                    case "0":
                        is_work = false;
                        break;
                    case "1":
                        Console.WriteLine("Brands:");
                        string[] rows = Actions.ToStringBrands();
                        Console.WriteLine($"{HeadTableWords[0],-5}|{HeadTableWords[1],-25}|{HeadTableWords[9],-25}|");
                        foreach (string row in rows)
                        {
                            Console.WriteLine(row);
                        }
                        break;
                    case "2":
                        Brand brand = new Brand();
                        Console.WriteLine("Напишіть імя бренда");
                        brand.Name = ValidateReadLine(50);
                        Console.WriteLine("Напишіть країну бренда");
                        brand.Country = ValidateReadLine(50);
                        Actions.AddBrand(brand);
                        break;
                    default:
                        throw new Exception("String is wrong");
                }
            }
        }
        public static void CategoriesTable()
        {
            bool is_work = true;
            while (is_work)
            {
                Console.WriteLine("1 - вивести таблицю\n2 - добавити новий запис\n0- вихід");
                switch (Console.ReadLine())
                {
                    case "0":
                        is_work = false;
                        break;
                    case "1":
                        Console.WriteLine("Categories:");
                        string[] rows = Actions.ToStringCategories();
                        foreach (string row in rows)
                        {
                            Console.WriteLine(row);
                        }
                        break;
                    case "2":
                        Brand brand = new Brand();
                        Console.WriteLine("Напишіть імя категорії");
                        brand.Name = ValidateReadLine(50);
                        Console.WriteLine("Напишіть опис");
                        brand.Country = Console.ReadLine();
                        Actions.AddBrand(brand);
                        break;
                    default:
                        throw new Exception("String is wrong");
                }
            }
        }
        public static void ItemsTable()
        {
            bool is_work = true;
            while (is_work)
            {
                Console.WriteLine("1 - вивести таблицю\n2 - добавити новий запис\n0- вихід");
                switch (Console.ReadLine())
                {
                    case "0":
                        is_work = false;
                        break;
                    case "1":
                        Console.WriteLine("Items:");
                        // none
                        string[] rows = Actions.ToStringItems();
                        foreach (string row in rows)
                        {
                            Console.WriteLine(row);
                        }
                        break;
                    case "2":
                        using (ModelOfShop db = new ModelOfShop())
                        {
                            Item item = new Item();
                            Console.WriteLine("Напишіть Id категорії, в яку необхідно додати предмет");
                            Console.WriteLine("Категорії:");
                            foreach (Category category in db.Categories)
                            {
                                Console.WriteLine($"[{category.Id}] - {category.Name}");
                            }
                            db.Categories.Find(int.Parse(Console.ReadLine())).Items.Add(item);
                            Console.WriteLine("Напишіть Id бренда, в якией необхідно додати предмет");
                            Console.WriteLine("Категорії:");
                            foreach (Brand brand in db.Brands)
                            {
                                Console.WriteLine($"[{brand.Id}] - {brand.Name}");
                            }
                            db.Brands.Find(int.Parse(Console.ReadLine())).Items.Add(item);
                            Console.WriteLine("Напишіть імя предмета");
                            item.Name = ValidateReadLine(50);
                            Console.WriteLine("Напишіть ціну предмета");
                            item.Price = float.Parse(Console.ReadLine());
                            db.Items.Add(item);
                            db.SaveChanges();
                        }
                        break;
                    default:
                        throw new Exception("String is wrong");
                }
            }
        }
        public static void UsersTable()
        {
            bool is_work = true;
            while (is_work)
            {
                Console.WriteLine("1 - вивести таблицю\n2 - добавити новий запис\n0- вихід");
                switch (Console.ReadLine())
                {
                    case "0":
                        is_work = false;
                        break;
                    case "1":
                        Console.WriteLine("Users:");
                        string[] rows = Actions.ToStringUsers();
                        Console.WriteLine($"{HeadTableWords[0],-5}|{HeadTableWords[1],-25}|{HeadTableWords[2],-25}|{HeadTableWords[3],-25}|{HeadTableWords[4],-15}|{HeadTableWords[5],-15}|{HeadTableWords[6],-4}|{HeadTableWords[7],-10}|{HeadTableWords[8],5}|");
                        foreach (string row in rows)
                        {
                            Console.WriteLine(row);
                        }
                        break;
                    case "2":
                        User user = new User();
                        Console.WriteLine("Напишіть імя користувача");
                        user.FirsName = ValidateReadLine(50);
                        Console.WriteLine("Напишіть прізвище користувача");
                        user.LastName = ValidateReadLine(50);
                        Console.WriteLine("Напишіть як по батькові користувача");
                        user.MiddleName = ValidateReadLine(50);
                        Console.WriteLine("Напишіть логін користувача");
                        user.Login = ValidateReadLine(50);
                        Console.WriteLine("Напишіть пароль користувача");
                        user.Password = ValidateReadLine(50);
                        Console.WriteLine("Напишіть код країни користувача:");
                        for (int i = 0; i < User.CountryCodes.Length; i++)
                        {
                            Console.WriteLine($"[{i}] - {User.CountryCodes[i]}");
                        }
                        user.CountryCode = short.Parse(Console.ReadLine());
                        Console.WriteLine("Напишіть номер користувача (без кода країни)");
                        user.Phone = ReadLong();
                        Console.WriteLine("Це адміністратор?(1 - так)");
                        user.IsAdmin = (Console.ReadLine() == "1") ? true : false;
                        Actions.AddUser(user);
                        break;
                    default:
                        throw new Exception("String is wrong");
                }
            }
        }
        public static string ValidateReadLine(int maxLenght)
        {
            string line = Console.ReadLine();
            if (line.Length > maxLenght)
            {
                throw new Exception("String is very long!");
            }
            return line;
        }
        public static long ReadLong()
        {
            long result;
            if (!long.TryParse(Console.ReadLine(), out result))
            {
                throw new Exception("This is not number");
            }
            return result;
        }
    }
}
