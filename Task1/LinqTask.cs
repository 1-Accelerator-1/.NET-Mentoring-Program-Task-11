using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Task1.DoNotChange;

namespace Task1
{
    public static class LinqTask
    {
        public static IEnumerable<Customer> Linq1(IEnumerable<Customer> customers, decimal limit)
        {
            var selectedCustomers = customers.Where(customer => customer.Orders.Sum(order => order.Total) > limit);
            return selectedCustomers;
        }

        public static IEnumerable<(Customer customer, IEnumerable<Supplier> suppliers)> Linq2(
            IEnumerable<Customer> customers,
            IEnumerable<Supplier> suppliers
        )
        {
            var selectedCustomersAndSuppliers = customers.Select(customer => (customer, suppliers.Where(supplier => supplier.Country == customer.Country && supplier.City == customer.City)));
            return selectedCustomersAndSuppliers;
        }

        public static IEnumerable<(Customer customer, IEnumerable<Supplier> suppliers)> Linq2UsingGroup(
            IEnumerable<Customer> customers,
            IEnumerable<Supplier> suppliers
        )
        {
            var selectedCustomersAndSuppliers = suppliers.GroupBy(supplier => new Customer { Country = supplier.Country, City = supplier.City })
                .Select(custAndSupp => (custAndSupp.Key, custAndSupp.AsEnumerable()));
            return selectedCustomersAndSuppliers;
        }

        public static IEnumerable<Customer> Linq3(IEnumerable<Customer> customers, decimal limit)
        {
            var selectedCustomers = customers.Where(customer => customer.Orders.Any(order => order.Total > limit));
            return selectedCustomers;
        }

        public static IEnumerable<(Customer customer, DateTime dateOfEntry)> Linq4(
            IEnumerable<Customer> customers
        )
        {
            var selectedCustomers = customers.Where(customer => customer.Orders.FirstOrDefault() != null).Select(customer => (customer, customer.Orders.First().OrderDate));
            return selectedCustomers;
        }

        public static IEnumerable<(Customer customer, DateTime dateOfEntry)> Linq5(
            IEnumerable<Customer> customers
        )
        {
            var selectedCustomers = customers.Where(customer => customer.Orders.FirstOrDefault() != null).Select(customer => (customer, customer.Orders.First().OrderDate));
            var finalCustomers = selectedCustomers.OrderBy(customer => customer.OrderDate.Year)
                .ThenBy(customer => customer.OrderDate.Month)
                .ThenByDescending(customer => customer.customer.Orders.Sum(order => order.Total))
                .ThenBy(customer => customer.customer.CustomerID);
            return finalCustomers;
        }

        public static IEnumerable<Customer> Linq6(IEnumerable<Customer> customers)
        {
            var selectedCustomers = customers.Where(customer => (customer.PostalCode.Where(c => !char.IsDigit(c)).Count() != 0) || 
                (customer.Region == null) || !Regex.IsMatch(customer.Phone, @"\(.*?\)"));
            return selectedCustomers;
        }

        public static IEnumerable<Linq7CategoryGroup> Linq7(IEnumerable<Product> products)
        {
            /* example of Linq7result

             category - Beverages
	            UnitsInStock - 39
		            price - 18.0000
		            price - 19.0000
	            UnitsInStock - 17
		            price - 18.0000
		            price - 19.0000
             */

            //var selectedProducts = products.GroupBy(product => product.Category).GroupBy(product => product.GroupBy(prod => prod.UnitsInStock))
            //    .Select(group => group);

            //var selectedProducts = products.GroupBy(product => new Linq7UnitsInStockGroup { UnitsInStock = product.UnitsInStock, Prices = products.Where(prod => prod.UnitsInStock == product.UnitsInStock).Select(prod => prod.UnitPrice) })
            //    .Select(product => new Linq7CategoryGroup { Category = product.Key, UnitsInStockGroup = pr });

            return null;
        }

        public static IEnumerable<(decimal category, IEnumerable<Product> products)> Linq8(
            IEnumerable<Product> products,
            decimal cheap,
            decimal middle,
            decimal expensive
        )
        {
            throw new NotImplementedException();
        }

        public static IEnumerable<(string city, int averageIncome, int averageIntensity)> Linq9(
            IEnumerable<Customer> customers
        )
        {
            var average = customers.Select(customer => customer.City).Select(city => (city: city,
            averageIncome: (int)Math.Round(customers.Where(cust => cust.City == city).Average(cust => cust.Orders.Sum(order => order.Total))),
            averageIntensity: (int)Math.Round(customers.Where(cust => cust.City == city).Average(cust => cust.Orders.Count()))));

            return average;
        }

        public static string Linq10(IEnumerable<Supplier> suppliers)
        {
            var uniqueCountries = suppliers.Select(supplier => supplier.Country).Distinct()
                .OrderBy(country => country.Length).ThenBy(country => country);
            var uniqueCountriesLine = string.Join(string.Empty, uniqueCountries);

            return uniqueCountriesLine;
        }
    }
}