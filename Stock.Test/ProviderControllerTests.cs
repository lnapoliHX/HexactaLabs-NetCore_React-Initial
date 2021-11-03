using NUnit.Framework;
using Stock.Api.DTOs;
using System;

namespace Stock.Test
{
    public class ProviderControllerTests
    {
        [Test]
        public void When_ProvierControllerCreated_Expect_PostMethodWithParameter()
        {
            var className = "Stock.Api.Controllers.ProviderController, Stock.Api";
            var methodName = "Post";
            var providerDTOParameter = "Stock.Api.DTOs.ProviderDTO, Stock.Api";
            var parametersType = new Type[] { Type.GetType(providerDTOParameter) };

            Assert.True(HasMethod(className, methodName, parametersType));
        }

        [Test]
        public void When_ProviderControllerCreated_Expect_GetMethodWhitParameter()
        {
            var className = "Stock.Api.Controllers.ProviderController, Stock.Api";
            var methodName = "Get";
            var parametersType = new Type[] { typeof(string) };

            Assert.True(HasMethod(className, methodName, parametersType));
        }

        [Test]
        public void When_ProviderControllerCreated_Expect_GetMethodWhitoutParameter()
        {
            var className = "Stock.Api.Controllers.ProviderController, Stock.Api";
            var methodName = "Get";

            Assert.True(HasMethod(className, methodName));
        }

        [Test]
        public void When_ProviderControllerCreated_Expect_PutMethodWithTwoParameters()
        {
            var className = "Stock.Api.Controllers.ProviderController, Stock.Api";
            var methodName = "Put";
            var providerDTOParameter = "Stock.Api.DTOs.ProviderDTO, Stock.Api";
            var parametersType = new Type[] { typeof(string), Type.GetType(providerDTOParameter) };

            Assert.True(HasMethod(className, methodName, parametersType));
        }

        [Test]
        public void When_ProviderControllerCreated_Expect_DeleteMethodWhitParameter()
        {
            var className = "Stock.Api.Controllers.ProviderController, Stock.Api";
            var methodName = "Get";
            var parametersType = new Type[] { typeof(string) };

            Assert.True(HasMethod(className, methodName, parametersType));
        }

        [Test]
        public void When_ProviderControllerCreated_Expect_SearchMethodWithParameter()
        {
            var className = "Stock.Api.Controllers.ProviderController, Stock.Api";
            var methodName = "Search";
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