using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
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
            services.AddMvc();
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            var service = app.ApplicationServices.GetService(typeof(IOptions<AppSettings>));
            var settings = ((IOptions<AppSettings>)service).Value;

            if (settings.CustomQuotes != null && settings.CustomQuotes.Any())
            {
                Quote.Quotes = settings.CustomQuotes.Select(x => x.Quote).ToList();
                Quote.Authors = settings.CustomQuotes.Select(x => x.Author).ToList();
            }
            else
            {
                var quoteFilePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{Path.DirectorySeparatorChar}data{Path.DirectorySeparatorChar}quotes.txt");
                Quote.Quotes = File.Exists(quoteFilePath) ? File.ReadAllLines(quoteFilePath).Select(System.Net.WebUtility.HtmlDecode).ToList() : new List<string>();
                var authorFilePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{Path.DirectorySeparatorChar}data{Path.DirectorySeparatorChar}authors.txt");
                Quote.Authors = File.Exists(authorFilePath) ? File.ReadAllLines(authorFilePath).Select(System.Net.WebUtility.HtmlDecode).ToList() : new List<string>();
            }
        }
    }
}