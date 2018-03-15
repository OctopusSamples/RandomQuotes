using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using RandomQuotes.Data.Data;

namespace RandomQuotes
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment HostingEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<QuoteContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            var mvcBuilder = services.AddMvc();

            var moduleAssemblies = GetModuleAssemblies();

            foreach (var assembly in moduleAssemblies)
            {
                mvcBuilder.AddApplicationPart(assembly.Item2);
            }

            var appSettingsConfig = Configuration.GetSection("AppSettings");
            appSettingsConfig["Modules"] = moduleAssemblies.Any() ? string.Join(",", moduleAssemblies.Select(x => x.Item1)) : "";
            services.Configure<AppSettings>(appSettingsConfig);

            foreach (var assembly in moduleAssemblies)
            {
                services.Configure<RazorViewEngineOptions>(options =>
                {
                    options.FileProviders.Add(new EmbeddedFileProvider(assembly.Item2));
                });
            }
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
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private IList<(string, Assembly)> GetModuleAssemblies()
        {
            var moduleAssemblies = new List<(string, Assembly)>();

            // Thanks to Thien Nguyen for the code snippet to scan a modules directory for assemblies.
            // Source: https://www.codeproject.com/Articles/1109475/Modular-Web-Application-with-ASP-NET-Core
            var moduleRootFolder = new DirectoryInfo(Path.Combine(HostingEnvironment.ContentRootPath, "Modules"));
            var moduleFolders = moduleRootFolder.GetDirectories();
            
            foreach (var moduleFolder in moduleFolders)
            {
                var binFolder = new DirectoryInfo(Path.Combine(moduleFolder.FullName, "bin"));
                if (!binFolder.Exists)
                {
                    continue;
                }

                foreach (var file in binFolder.GetFileSystemInfos("*.dll", SearchOption.AllDirectories))
                {
                    Assembly assembly;
                    try
                    {
                        assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(file.FullName);
                    }
                    catch (FileLoadException ex)
                    {
                        if (ex.Message == "Assembly with same name is already loaded")
                        {
                            // Get loaded assembly
                            assembly = Assembly.Load(new AssemblyName(Path.GetFileNameWithoutExtension(file.Name)));
                        }
                        else
                        {
                            throw;
                        }
                    }

                    var assemblyInfo = (Name: moduleFolder.Name, Assembly: assembly);
                    moduleAssemblies.Add(assemblyInfo);
                }
            }

            return moduleAssemblies;
        }
    }
}