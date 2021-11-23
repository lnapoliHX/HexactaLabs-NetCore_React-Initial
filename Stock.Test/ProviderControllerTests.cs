using NUnit.Framework;
using Stock.Api.DTOs;
using System;

namespace Stock.Test
{
    public class ProviderControllerTests
    {
        [Test]
        public void When_ProviderControllerCreated_Expect_PostMethodWithParameter()
        {
            var className = "Stock.Api.Controllers.ProviderController, Stock.Api";
            var methodName = "CreateProvider";
            var providerDTOParameter = "Stock.Api.DTOs.ProviderDTO, Stock.Api";
            var parametersType = new Type[] { Type.GetType(providerDTOParameter) };

            Assert.True(HasMethod(className, methodName, parametersType));
        }

        [Test]
        public void When_ProviderControllerCreated_Expect_GetMethodWhitParameter()
        {
            var className = "Stock.Api.Controllers.ProviderController, Stock.Api";
            var methodName = "GetById";
            var parametersType = new Type[] { typeof(string) };

            Assert.True(HasMethod(className, methodName, parametersType));
        }

        [Test]
        public void When_ProviderControllerCreated_Expect_GetMethodWhitoutParameter()
        {
            var className = "Stock.Api.Controllers.ProviderController, Stock.Api";
            var methodName = "GetAll";

            Assert.True(HasMethod(className, methodName));
        }

        [Test]
        public void When_ProviderControllerCreated_Expect_PutMethodWithTwoParameters()
        {
            var className = "Stock.Api.Controllers.ProviderController, Stock.Api";
            var methodName = "UpdateProvider";
            var providerDTOParameter = "Stock.Api.DTOs.ProviderDTO, Stock.Api";
            var parametersType = new Type[] { typeof(string), Type.GetType(providerDTOParameter) };

            Assert.True(HasMethod(className, methodName, parametersType));
        }

        [Test]
        public void When_ProviderControllerCreated_Expect_DeleteMethodWhitParameter()
        {
            var className = "Stock.Api.Controllers.ProviderController, Stock.Api";
            var methodName = "Delete";
            var parametersType = new Type[] { typeof(string) };

            Assert.True(HasMethod(className, methodName, parametersType));
        }

        [Test]
        public void When_ProviderControllerCreated_Expect_SearchMethodWithParameter()
        {
            var className = "Stock.Api.Controllers.ProviderController, Stock.Api";
            var methodName = "SearchProvider";
            var providerSearchDTOParameter = "Stock.Api.DTOs.ProviderSearchDTO, Stock.Api";
            var parametersType = new Type[] { Type.GetType(providerSearchDTOParameter) };

            Assert.True(HasMethod(className, methodName, parametersType));
        }

        private bool? HasMethod(string className, string methodName)
        {
            return Type.GetType(className).GetMethod(methodName, new Type[] { }) != null;
        }

        private bool? HasMethod(string className, string methodName, Type[] types)
        {
            return Type.GetType(className).GetMethod(methodName, types) != null;
        }
    }
}