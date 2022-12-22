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
    public class CustomerControllerTests
    {
        private CustomersController _sut;
        
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        [Category("Moq")]
        public void BeAbleToBeConstructedUsingMoq()
        {
            // Arrange
            var mockCustomerService = new Mock<ICustomerService>();
            // Act
            _sut = new CustomersController(mockCustomerService.Object);
            // Assert
            Assert.That(_sut, Is.InstanceOf<CustomersController>());
        }

        [Test]
        [Category("Create Tests")]
        public void GivenNewCustomer_PostCustomer_AddsNewCustomerToTable()
        {
            var mockCustomerService = new Mock<ICustomerService>();

            var originalCustomerDTO = new CustomerDTO[]
            {
                new CustomerDTO()
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
                }
            };
            var originalCustomer = new Customer
            {
                CustomerId = "TESTS",
            };
            mockCustomerService.Setup(cs => cs.GetCustomerById("TESTS")).Returns(originalCustomer);
            mockCustomerService.Setup(cs => cs.GetCustomerDTOById("TESTS")).Returns(originalCustomerDTO[0]);
            mockCustomerService.Setup(cs => cs.CreateCustomer(originalCustomer));

            _sut = new CustomersController(mockCustomerService.Object);

            var result = _sut.PostMultipleCustomer(originalCustomerDTO);

            //mockCustomerService.Verify(
            //    cs => cs.CreateCustomer(originalCustomer),
            //    Times.Once);
            Assert.That(result.Result.Value[0].CustomerId, Is.EqualTo("TESTS"));
        }

        [Test]
        [Category("Read Tests")]
        public void GivenIdIsInDatabase_GetCustomer_ReturnsCustomerDetails()
        {
            var mockCustomerService = new Mock<ICustomerService>();
            
            var originalCustomer = new CustomerDTO
            {
                CustomerId = "TESTS",
                ContactName = "Testing Man"
            };

            mockCustomerService.Setup(cs => cs.DoesCustomerExist("TESTS")).Returns(true);
            mockCustomerService.Setup(cs => cs.GetCustomerDTOById("TESTS")).Returns(originalCustomer);

            _sut = new CustomersController(mockCustomerService.Object);

            var result = _sut.GetCustomer("TESTS");

            mockCustomerService.Verify(
                cs => cs.GetCustomerDTOById("TESTS"),
                Times.Once);

            Assert.That(result.Result.Value.ContactName, Is.EqualTo("Testing Man"));
        }

        [Test]
        [Category("Delete Tests")]
        public void GivenIdIsInDatabase_DeleteCustomer_RemovesCustomerFromDB()
        {
            var mockCustomerService = new Mock<ICustomerService>();

            var originalCustomer = new Customer
            {
                CustomerId = "ROCK"
            };
            mockCustomerService.Setup(
                cs => cs.GetCustomerById("ROCK"))
                    .Returns(originalCustomer);

            _sut = new CustomersController(mockCustomerService.Object);

            var result = _sut.DeleteCustomer("ROCK").Result.ToString();

            Assert.That(result, Does.Contain("204"));
        }
    }
}