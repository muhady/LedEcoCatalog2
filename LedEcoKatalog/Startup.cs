//----------------------------------------------------------------------------
// <copyright file="Startup.cs" company="Binaware">
//   Copyright (c) Binaware s.r.o. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------

namespace LedEcoKatalog
{
  using LedEcoKatalog.Data;

  using Microsoft.AspNetCore.Builder;
  using Microsoft.AspNetCore.Hosting;
  using Microsoft.EntityFrameworkCore;
  using Microsoft.Extensions.Configuration;
  using Microsoft.Extensions.DependencyInjection;
  using Microsoft.Extensions.Hosting;
  using Microsoft.Extensions.Logging;

  using Serilog;
  using Serilog.Context;

  public class Startup
  {
    #region Public Constructors

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    #endregion

    #region Public Properties

    public IConfiguration Configuration { get; }

    #endregion

    #region Public Methods

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      Log.Information("Application is starting...");

      services.AddDbContext<KatalogDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("KatalogDbConnectionString")));

      services.AddControllersWithViews();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
    {
      Settings.Initialize(Configuration, app, env);

      if (env.IsDevelopment())
      {
        app.UseExceptionHandler("/Error/Index");
        app.UseStatusCodePagesWithReExecute("/Error/Index");
        //// app.UseDeveloperExceptionPage();
        app.UseDatabaseErrorPage();
      }
      else
      {
        app.UseExceptionHandler("/Error/Index");
        app.UseStatusCodePagesWithReExecute("/Error/Index");

        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }

      app.UseHttpsRedirection();
      app.UseStaticFiles();

      if (Settings.UseRequestLogging)
      {
        app.Use(async (ctx, next) =>
        {
          using (LogContext.PushProperty("RemoteIpAddress", ctx?.Connection?.RemoteIpAddress?.ToString()))
          {
            await next();
          }
        });

        app.UseSerilogRequestLogging();
      }

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
      });

      logger.LogInformation("Application has started.");
    }

    #endregion
  }
}
