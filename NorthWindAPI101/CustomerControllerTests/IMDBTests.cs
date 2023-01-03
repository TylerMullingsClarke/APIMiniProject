using NorthWindAPI101;
using NorthWindAPI101.Controllers;
using NorthWindAPI101.Models;
using NorthWindAPI101.Models.DTO;
using NorthWindAPI101.Models.Services;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Moq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace NorthwindAPITests
{
    public class IMDBTests
    {
        private NorthwindContext _context;
        private CustomerService _sut;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var options = new DbContextOptionsBuilder<NorthwindContext>()
                .UseInMemoryDatabase("Example_DB")
                .Options;
            _context = new NorthwindContext(options);
            _sut = new CustomerService(_context);

            _context.Customers.Add(
                new Customer
                {
                    CustomerId = "WINDR",
                    ContactName = "Philip Windridge",
                    CompanyName = "Sparta Global",
                    City = "Birmingham"
                });
            _context.Customers.Add(
                new Customer
                {
                    CustomerId = "TOZER",
                    ContactName = "Laura Tozer",
                    CompanyName = "Sparta Global",
                    City = "London"
                });
            _context.SaveChanges();
        }

        [TearDown]
        public void TearDown()
        {
            var cust = _context.Customers.Find("TESTS");
            _context.Customers.Remove(cust);
            _context.SaveChanges();
        }

        [Test]
        [Category("Create Tests")]
        public void GivenNewCustomer_CreateCustomer_AddsNewCustomerToTable()
        {

            var originalCustomer = new Customer
            {
                CustomerId = "TESTS",
                CompanyName = "TESTS",
                City = "TESTS",
                ContactName = "TESTS",
                ContactTitle = "TESTS",
                Country = "TESTS",
                Address = "TESTS",
                Fax = "TESTS",
                Phone = "TESTS",
                Region = "TESTS",
                PostalCode = "TESTS"
            };
            _sut.CreateCustomer(originalCustomer);
            var result = _context.Customers.Find("TESTS");
            Assert.That(result.ContactName, Is.EqualTo("TESTS"));
        }

        [Test]
        [Category("Delete Tests")]
        public void GivenNewCustomer_RemoveCustomer_AddsNewCustomerToTable()
        {

            var originalCustomer = new Customer
            {
                CustomerId = "TESTS",
                CompanyName = "TESTS",
                City = "TESTS",
                ContactName = "TESTS",
                ContactTitle = "TESTS",
                Country = "TESTS",
                Address = "TESTS",
                Fax = "TESTS",
                Phone = "TESTS",
                Region = "TESTS",
                PostalCode = "TESTS"
            };
            _context.Customers.Add(originalCustomer);
            _context.SaveChanges();
            var prevCount = _context.Customers.Count();

            _sut.RemoveCustomer(originalCustomer);
            var postCount = _context.Customers.Count();

            Assert.That(postCount+1, Is.EqualTo(prevCount));

            _context.Customers.Add(originalCustomer);
        }
    }
}
