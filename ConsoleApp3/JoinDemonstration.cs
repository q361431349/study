using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    class JoinDemonstration
    {
        #region Data

        class Product
        {
            public string Name { get; set; }
            public int CategoryID { get; set; }
        }

        class Category
        {
            public string Name { get; set; }
            public int ID { get; set; }
        }



        List<Category> categories = new List<Category>()
        {
            new Category {Name="Beverages", ID=001},
            new Category {Name="Condiments", ID=002},
            new Category {Name="Vegetables", ID=003},
            new Category {Name="Grains", ID=004},
            new Category {Name="Fruit", ID=005}
        };


        List<Product> products = new List<Product>()
        {
            new Product {Name="Cola",  CategoryID=001},
            new Product {Name="Tea",  CategoryID=001},
            new Product {Name="Mustard", CategoryID=002},
            new Product {Name="Pickles", CategoryID=002},
            new Product {Name="Carrots", CategoryID=003},
            new Product {Name="Bok Choy", CategoryID=003},
            new Product {Name="Peaches", CategoryID=005},
            new Product {Name="Melons", CategoryID=005},
        };
        #endregion


        public void InnerJoin()
        {

            var innerJoinQuery =
               from category in categories
               join prod in products on category.ID equals prod.CategoryID
               select new { Category = category.ID, Product = prod.Name };

            var resultInnerJoinQuery = categories.Join(products, u => u.ID, d => d.CategoryID, (u, d) => new { Category = u.ID, Product = d.Name }).Select(o => o);

            Console.WriteLine("InnerJoin:");

            foreach (var item in resultInnerJoinQuery)
            {
                Console.WriteLine("{0,-20}{1}", item.Product, item.Category);
            }
            Console.WriteLine("InnerJoin: {0} items in 1 group.", innerJoinQuery.Count());
            Console.WriteLine(Environment.NewLine);
        }

        public void GroupJoin()
        {

            var groupJoinQuery =
               from category in categories
               join prod in products on category.ID equals prod.CategoryID into prodGroup
               select prodGroup;
            var resultInnerJoinQuery = categories.GroupJoin(products, u => u.ID, d => d.CategoryID, (u, d) => new { prodGroup = d }).Select(o => o.prodGroup);

            int totalItems = 0;

            Console.WriteLine("Simple GroupJoin:");


            foreach (var prodGrouping in resultInnerJoinQuery)
            {
                Console.WriteLine("Group:");
                foreach (var item in prodGrouping)
                {
                    totalItems++;
                    Console.WriteLine("   {0,-10}{1}", item.Name, item.CategoryID);
                }
            }
            Console.WriteLine("Unshaped GroupJoin: {0} items in {1} unnamed groups", totalItems, groupJoinQuery.Count());
            Console.WriteLine(Environment.NewLine);
        }

        public void GroupInnerJoin()
        {
            var groupJoinQuery2 =
                from category in categories
                orderby category.ID
                join prod in products on category.ID equals prod.CategoryID into prodGroup
                select new
                {
                    Category = category.Name,
                    Products = from prod2 in prodGroup
                               orderby prod2.Name
                               select prod2
                };
            var resultInnerJoinQuery = categories.OrderBy(c => c.ID).GroupJoin(products, u => u.ID, d => d.CategoryID, (u, d) => new { category = u, prodGroup = d }).Select(o =>
            new { Category = o.category.Name, Products = o.prodGroup.OrderBy(prod2 => prod2.Name).Select(prod2 => prod2) });

            int totalItems = 0;

            Console.WriteLine("GroupInnerJoin:");
            foreach (var productGroup in resultInnerJoinQuery)
            {
                Console.WriteLine(productGroup.Category);
                foreach (var prodItem in productGroup.Products)
                {
                    totalItems++;
                    Console.WriteLine("  {0,-10} {1}", prodItem.Name, prodItem.CategoryID);
                }
            }
            Console.WriteLine("GroupInnerJoin: {0} items in {1} named groups", totalItems, groupJoinQuery2.Count());
            Console.WriteLine(Environment.NewLine);
        }

        public void GroupJoin3()
        {

            var groupJoinQuery3 =
                from category in categories
                join product in products on category.ID equals product.CategoryID into prodGroup
                from prod in prodGroup
                orderby prod.CategoryID
                select new { Category = prod.CategoryID, ProductName = prod.Name };

            var resultInnerJoinQuery = categories.Join(products, u => u.ID, d => d.CategoryID, (u, d) =>
                d
            ).Select(p => p).OrderBy(a=>a.CategoryID).Select(aa=>new { Category = aa.CategoryID, ProductName = aa.Name });
           

            int totalItems = 0;

            Console.WriteLine("GroupJoin3:");
            foreach (var item in resultInnerJoinQuery)
            {
                totalItems++;
                Console.WriteLine("   {0}:{1}", item.ProductName, item.Category);
            }

            Console.WriteLine("GroupJoin3: {0} items in 1 group", totalItems);
            Console.WriteLine(Environment.NewLine);
        }

        public void LeftOuterJoin()
        {

            var leftOuterQuery =
               from category in categories
               join prod in products on category.ID equals prod.CategoryID into prodGroup
               select prodGroup.DefaultIfEmpty(new Product() { Name = "Nothing!", CategoryID = category.ID });

            var resultInnerJoinQuery = categories.GroupJoin(products, u => u.ID, d => d.CategoryID, (u, d) => new
            {
                category = u,
                prodGroup = d
            }).Select(o => o.prodGroup.DefaultIfEmpty(new Product()
            {
                Name = "Nothing!",
                CategoryID = o.category.ID
            }));
            int totalItems = 0;

            Console.WriteLine("Left Outer Join:");


            foreach (var prodGrouping in resultInnerJoinQuery)
            {
                Console.WriteLine("Group:");
                foreach (var item in prodGrouping)
                {
                    totalItems++;
                    Console.WriteLine("  {0,-10}{1}", item.Name, item.CategoryID);
                }
            }
            Console.WriteLine("LeftOuterJoin: {0} items in {1} groups", totalItems, leftOuterQuery.Count());
            Console.WriteLine(Environment.NewLine);
        }

        public void LeftOuterJoin2()
        {

            var leftOuterQuery2 =
               from category in categories
               join prod in products on category.ID equals prod.CategoryID into prodGroup
               from item in prodGroup.DefaultIfEmpty()
               select new { Name = item == null ? "Nothing!" : item.Name, CategoryID = category.ID };



            Console.WriteLine("LeftOuterJoin2: {0} items in 1 group", leftOuterQuery2.Count());
            int totalItems = 0;

            Console.WriteLine("Left Outer Join 2:");

            foreach (var item in leftOuterQuery2)
            {
                totalItems++;
                Console.WriteLine("{0,-10}{1}", item.Name, item.CategoryID);
            }
            Console.WriteLine("LeftOuterJoin2: {0} items in 1 group", totalItems);
        }
    }
}
