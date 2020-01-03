using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
    public static class Actions
    {
        public static bool IsIdenticaLogin(string login)
        {
            using (ModelOfShop db = new ModelOfShop())
            {
                var equalRecords = db.Users.Where(x => x.Login.Equals(login)); 
                if (equalRecords == null || equalRecords.Count() == 0)
                {
                    return true;
                } else
                {
                    return false;
                }
            }
        }
        public static void AddUser(User user)
        {
            using (ModelOfShop db = new ModelOfShop())
            {
                db.Users.Add(user);
                db.SaveChanges();
            }
        }
        public static void AddUser(params User[] user)
        {
            using (ModelOfShop db = new ModelOfShop())
            {
                db.Users.AddRange(user);
                db.SaveChanges();
            }
        }
        public static void RemoveUser(User user)
        {
            using (ModelOfShop db = new ModelOfShop())
            {
                db.Users.Remove(user);
                db.SaveChanges();
            }
        }
        public static void AddBrand(Brand brand)
        {
            using (ModelOfShop db = new ModelOfShop())
            {
                db.Brands.Add(brand);
                db.SaveChanges();
            }
        }
        public static void AddBrand(params Brand[] brand)
        {
            using (ModelOfShop db = new ModelOfShop())
            {
                db.Brands.AddRange(brand);
                db.SaveChanges();
            }
        }
        public static void RemoveBrand(Brand brand)
        {
            using (ModelOfShop db = new ModelOfShop())
            {
                db.Brands.Remove(brand);
                db.SaveChanges();
            }
        }
        //
        public static void AddCategory(Category category)
        {
            using (ModelOfShop db = new ModelOfShop())
            {
                db.Categories.Add(category);
                db.SaveChanges();
            }
        }
        public static void AddCategory(params Category[] category)
        {
            using (ModelOfShop db = new ModelOfShop())
            {
                db.Categories.AddRange(category);
                db.SaveChanges();
            }
        }
        public static void RemoveCategory(Category category)
        {
            using (ModelOfShop db = new ModelOfShop())
            {
                db.Categories.Remove(category);
                db.SaveChanges();
            }
        }
        public static string[] ToStringUsers()
        {
            string[] result;
            using (ModelOfShop db = new ModelOfShop())
            {
                result = new string[db.Users.Count()];
                int temp = 0;
                foreach(User user in db.Users)
                {
                    result[temp] = $"{user.Id:D5}|{user.FirsName,-25}|{user.LastName,-25}|{user.MiddleName,-25}|{user.Login,-15}|{user.Password,-15}|{User.CountryCodes[(int)user.CountryCode],4}-{user.Phone,-10}|{user.IsAdmin,5}|";
                    temp++;
                }
            }
            return result;
        }
        public static string[] ToStringBrands()
        {
            string[] result;
            using (ModelOfShop db = new ModelOfShop())
            {
                result = new string[db.Brands.Count()];
                int temp = 0;
                foreach (Brand brand in db.Brands)
                {
                    result[temp] = $"{brand.Id:D5}|{brand.Name, -25}|{brand.Country, -25}|";
                    temp++;
                }
            }
            return result;
        }
        public static string[] ToStringCategories()
        {
            string[] result;
            using (ModelOfShop db = new ModelOfShop())
            {
                result = new string[db.Categories.Count()];
                int temp = 0;
                foreach (Category category in db.Categories)
                {
                    result[temp] = $"{category.Name}\n\t{category.Description}";
                    temp++;
                }
            }
            return result;
        }
        public static string[] ToStringItems()
        {
            string[] result;
            using (ModelOfShop db = new ModelOfShop())
            {
                result = new string[db.Items.Count()];
                int temp = 0;
                foreach (Item item in db.Items)
                {
                    result[temp] = $"{item.Category.Name}|{item.Name}|{item.Brand1.Name}|{item.Price}";
                    temp++;
                }
            }
            return result;
        }
    }
    // трохи занесло сюди
    public partial class User
    {
        /// <summary>
        /// 0 - Америка
        /// 1 - Україна
        /// 2 - Росія
        /// 3 - Білорусь
        /// 4 - Сербія
        /// 5 - Польща
        /// </summary>
        public static readonly string[] CountryCodes =
        {
            "+1",
            "+380",
            "+7",
            "+375",
            "+381",
            "+48"
        };
    }
}
