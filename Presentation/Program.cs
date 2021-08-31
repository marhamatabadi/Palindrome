using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domaın.Abstracts;
using Domaın.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Presentation
{
    class Program
    {
        static Task Main(string[] args)
        {
            Console.WriteLine("Welcome to PALINDROME finder app.");
            using IHost host = CreateHostBuilder(args).Build();
            using IServiceScope serviceScope = host.Services.CreateScope();
            IServiceProvider provider = serviceScope.ServiceProvider;

            var textProcessorServıce = provider.GetRequiredService<ITextProcessorServıce>();


           var result= FindPalindromes(textProcessorServıce);
            foreach (var item in result)
                Console.WriteLine(item);
            return host.RunAsync();
        }
        static IHostBuilder CreateHostBuilder(string[] args) =>
          Host.CreateDefaultBuilder(args)
              .ConfigureServices((_, services) =>
                  services.AddScoped<ITextProcessorServıce, TextProcessorServıce>());

        static IEnumerable<string> FindPalindromes(ITextProcessorServıce textProcessorServıce)
        {
            Console.WriteLine("Please enter a word/for exit press ctrl+c");
            var word = Console.ReadLine();

            try
            {
               return textProcessorServıce.FindPalindromes(word);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"error : {ex.Message}");
                return new string[0];
            }

        }
    }
}
