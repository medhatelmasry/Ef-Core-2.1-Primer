using System;
using System.Data.SqlClient;
using System.Linq;
using EFCorePrimer.NW;
using Microsoft.EntityFrameworkCore;

namespace EFCorePrimer
{
    class Program
    {
        static NorthwindContext context = new NorthwindContext();
        static void Main(string[] args)
        {
            GetAllCategories();
            //GetCategoriesByFirstCharacterLambda("C");
            //GetCategoriesByFirstCharacterQuery("C");
            //GetCategoriesByFirstCharacterLambdaDescendingOrder();
            // GetCategoriesSpecificColumns();
            //GetProductsByCategory();
            //GetProductsByCategoryAlias();

            //GetAllCategories();
            //InsertCategory("Pop", "Sprite");
            //UpdateCategory(10,"Fruit", "Pineapple");
            //DeleteCategory(12);
            //CallInsertCategoryStoredProcedure("Bakery", "Cake");
            //GetAllCategories();
        }

        static void GetAllCategories()
        {
            foreach (var c in context.Categories)
            {
                Console.WriteLine($"{c.CategoryId}\t{c.CategoryName}\t{c.Description}");
            }
            Console.WriteLine("====================================");
        }

        static void GetCategoriesByFirstCharacterLambda(string startChar)
        {
            var qry = context.Categories
                .Where(c => c.CategoryName.StartsWith("C"));

            foreach (var c in qry)
            {
                Console.WriteLine($"{c.CategoryId}\t{c.CategoryName}\t{c.Description}");
            }

        }

        static void GetCategoriesByFirstCharacterQuery(string startChar)
        {
            var qry = from c in context.Categories
                where c.CategoryName.StartsWith("C")
                select c;

            foreach (var c in qry)
            {
                Console.WriteLine($"{c.CategoryId}\t{c.CategoryName}\t{c.Description}");
            }

        }

        static void GetCategoriesByFirstCharacterLambdaDescendingOrder()
        {
            var qry = context.Categories
                .OrderByDescending(c => c.CategoryId);

            foreach (var c in qry)
            {
                Console.WriteLine($"{c.CategoryId}\t{c.CategoryName}\t{c.Description}");
            }

        }

        static void GetCategoriesSpecificColumns()
        {
            var qry = context.Categories
                .Select(c => new {c.CategoryId, c.CategoryName});

            foreach (var c in qry)
            {
                Console.WriteLine($"{c.CategoryId}\t{c.CategoryName}");
            }

        }

        static void GetProductsByCategory() {
            var qry = context.Products
                .Select(p => new {p.ProductId, p.ProductName, p.Category.CategoryName});

            foreach (var c in qry)
            {
                Console.WriteLine($"{c.ProductId}\t{c.ProductName}\t{c.CategoryName}");
            }
        }

        static void GetProductsByCategoryAlias() {
            var qry = context.Products
                .Select(p => new {
                    Id = p.ProductId, 
                    Product = p.ProductName, 
                    Category = p.Category.CategoryName
                });

            foreach (var c in qry)
            {
                Console.WriteLine($"{c.Id}\t{c.Product}\t{c.Category}");
            }
        }

        static void InsertCategory(string name, string desc) {
            var newCategory = new Categories() {
                CategoryName = name,
                Description = desc
            };

            context.Categories.Add(newCategory);

            context.SaveChanges();
        }

        static void UpdateCategory(int id, string name, string desc) {
            var CategoryToUpdate = context.Categories
                .Find(id);
            
            CategoryToUpdate.CategoryName = name;
            CategoryToUpdate.Description = desc;

            context.SaveChanges();
        }

        static void DeleteCategory(int id) {
            var CategoryToDelete = context.Categories
                .Find(id);
            
            context.Categories.Remove(CategoryToDelete);

            context.SaveChanges();
        }

        public static void CallInsertCategoryStoredProcedure(string name, string desc) {
            var pName = new SqlParameter("@CategoryName", name);
            var pDesc = new SqlParameter("@Description", desc);

            var result = context.Database.ExecuteSqlCommand("dbo.MzeInsertCategory @CategoryName, @Description",
                pName, pDesc );
        }

    }
}
