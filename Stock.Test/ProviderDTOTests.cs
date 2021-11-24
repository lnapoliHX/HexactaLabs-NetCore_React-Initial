using NUnit.Framework;
using System;
using System.ComponentModel.DataAnnotations;

namespace Stock.Test
{
    class ProviderDTOTests
    {
        [Test]
        public void When_ProviderDTOCreated_Expect_NamePropertyIsRequired()
        {
            var className = "Stock.Api.DTOs.ProviderDTO, Stock.Api";
            var property = "Name";
            var attribute = typeof(RequiredAttribute);

            Assert.True(HasAttribute(className, property, attribute));
        }

        [Test]
        public void When_ProviderDTOCreated_Expect_PhonePropertyIsRequired()
        {
            var className = "Stock.Api.DTOs.ProviderDTO, Stock.Api";
            var property = "Phone";
            var attribute = typeof(RequiredAttribute);

            Assert.True(HasAttribute(className, property, attribute));
        }

        [Test]
        public void When_ProviderDTOCreated_Expect_EmailPropertyIsRequired()
        {
            var className = "Stock.Api.DTOs.ProviderDTO, Stock.Api";
            var property = "Email";
            var attribute = typeof(RequiredAttribute);

            Assert.True(HasAttribute(className, property, attribute));
        }

        private static bool HasAttribute(string className, string propertyName, Type attribute)
        {
            var property = Type.GetType(className).GetProperty(propertyName);

            return Attribute.IsDefined(property, attribute);
        }
    }
}
