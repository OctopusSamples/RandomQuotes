using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RandomQuotes.Models;

namespace RandomQuotes
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            var quoteFilePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{Path.DirectorySeparatorChar}data{Path.DirectorySeparatorChar}quotes.txt");
            Quote.Quotes = File.Exists(quoteFilePath) ? File.ReadAllLines(quoteFilePath).Select(System.Net.WebUtility.HtmlDecode).ToList() : new List<string>();
            var authorFilePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{Path.DirectorySeparatorChar}data{Path.DirectorySeparatorChar}authors.txt");
            Quote.Authors = File.Exists(authorFilePath) ? File.ReadAllLines(authorFilePath).Select(System.Net.WebUtility.HtmlDecode).ToList() : new List<string>();
        }
    }
}