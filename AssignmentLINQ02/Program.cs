using System;
using System.Collections.Generic;
using System.Linq;



namespace AssignmentLINQ02
{
    class Program
    {
        static void Main()
        {
            // Assume Products & Customers are already loaded
            List<Product> products = GetProducts();
            List<Customer> customers = GetCustomers();

            #region Q1
            var top3Expensive = products
                .OrderByDescending(p => p.UnitPrice)
                .Take(3);
            #endregion

            #region Q2
            var page2 = products
                .Skip(5)
                .Take(5);
            #endregion

            #region Q3
            var lessThan25 = products
                .OrderBy(p => p.UnitPrice)
                .TakeWhile(p => p.UnitPrice < 25);
            #endregion

            #region Q4
            bool allSeafoodInStock = products
                .Where(p => p.Category == "Seafood")
                .All(p => p.UnitsInStock > 0);
            #endregion

            #region Q5
            int[] ids = { 3, 9, 13, 18 };
            bool contains9 = ids.Contains(9);
            #endregion

            #region Q6
            var groupedWithCount = products
                .GroupBy(p => p.Category)
                .Select(g => new
                {
                    Category = g.Key,
                    Count = g.Count()
                });
            #endregion

            #region Q7
            var groupedNames = products
                .GroupBy(p => p.Category)
                .Select(g => new
                {
                    Category = g.Key,
                    Names = g.Select(p => p.ProductName)
                });
            #endregion

            #region Q8
            var categoriesMoreThan3 = products
                .GroupBy(p => p.Category)
                .Where(g => g.Count() > 3)
                .Select(g => g.Key);
            #endregion

            #region Q9
            var customersByCountry =
                from c in customers
                group c by c.Country into g
                select new
                {
                    Country = g.Key,
                    Count = g.Count(),
                    TotalOrderValue = g.Sum(c => c.Orders.Sum(o => o.Total))
                };
            #endregion

            #region Q10
            var totalUnits = products.Sum(p => p.UnitsInStock);
            #endregion

            #region Q11
            var cheapest = products.Min(p => p.UnitPrice);
            var mostExpensive = products.Max(p => p.UnitPrice);
            #endregion

            #region Q12
            var distinctCategories = products
                .Select(p => p.Category)
                .Distinct();
            #endregion

            #region Q13
            int[] setA = { 1, 3, 5, 7, 9, 11, 13 };
            int[] setB = { 3, 6, 9, 12, 15, 13 };

            var inANotB = setA.Except(setB);
            #endregion

            #region Q14
            string[] list1 = { "Germany", "France", "UK", "Spain" };
            string[] list2 = { "france", "SPAIN", "Italy" };

            var result = list1
                .Where(x => !list2
                .Any(y => y.Equals(x, StringComparison.OrdinalIgnoreCase)));
            #endregion

            #region Q15
            var dict = products.ToDictionary(p => p.ProductID);
            var product18 = dict.ContainsKey(18) ? dict[18] : null;
            #endregion

            #region Q16
            var firstAbove50 = products
                .First(p => p.UnitPrice > 50);
            #endregion

            #region Q17
            var firstAbove500 = products
                .FirstOrDefault(p => p.UnitPrice > 500);
            #endregion

            #region Q18
            var table7 = Enumerable.Range(1, 10)
                .Select(x => $"7 x {x} = {7 * x}");
            #endregion

            #region Q19
            var evens = Enumerable.Range(1, 30)
                .Where(x => x % 2 == 0);
            #endregion

            #region Q20
            var concat = products.Take(3).Select(p => p.ProductName)
                .Concat(customers.Take(3).Select(c => c.CompanyName));
            #endregion

            #region Q21
            var paired = products.Zip(customers,
                (p, c) => $"{p.ProductName} sold to {c.CompanyName}");
            #endregion
        }

        // Dummy classes
        public class Product
        {
            public int ProductID { get; set; }
            public string ProductName { get; set; }
            public string Category { get; set; }
            public decimal UnitPrice { get; set; }
            public int UnitsInStock { get; set; }
        }

        public class Customer
        {
            public string CompanyName { get; set; }
            public string Country { get; set; }
            public List<Order> Orders { get; set; }
        }

        public class Order
        {
            public decimal Total { get; set; }
        }

        // Dummy data methods
        static List<Product> GetProducts() => new List<Product>();
        static List<Customer> GetCustomers() => new List<Customer>();
    }
}