using Domaın.Abstracts;
using Domaın.Services;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;

namespace TestProject
{
    [TestFixture]
    public class TestTextService
    {
        protected ITextProcessorServıce TextProcessorServıce { get; set; }
        protected Mock<ITextProcessorServıce> MockedTextProcessorServıce => new Mock<ITextProcessorServıce>();
        private IServiceProvider ServicesProvider { get; set; }
        [SetUp]
        public void Setup()
        {
            // Configure DI container
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            ServicesProvider = services.BuildServiceProvider();

            // Use DI to get instances of ITextProcessorServıce
            TextProcessorServıce = ServicesProvider.GetService<ITextProcessorServıce>();
        }

        [Test]
        [TestCase("aab",new string[] { "a,a,b","aa,b"})]
        [TestCase("geeks",new string[] { "g,e,e,k,s","g,ee,k,s"})]
        public void FindPalindromes(string input, IEnumerable<string> expectedResult)
        {
            // Prepare
            // if we need to test another service related to FindPalindromes results in TextProcessorServıce 
            // we can use MockedTextProcessorServıce like this :
            // MockedTextProcessorServıce.Setup(x => x.FindPalindromes(input)).Returns(expectedResult);

            // Act - This will call the real FindPalindromes method
            IEnumerable<string> result = TextProcessorServıce.FindPalindromes(input);

            // Check
            Assert.AreEqual(expectedResult, result);
        }


        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ITextProcessorServıce,TextProcessorServıce>();
        }
    }
}