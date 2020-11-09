//----------------------------------------------------------------------------
// <copyright file="Program.cs" company="Binaware">
//   Copyright (c) Binaware s.r.o. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------

namespace LedEcoKatalog
{
  using System;
  using System.IO;

  using Microsoft.AspNetCore.Hosting;
  using Microsoft.Extensions.Configuration;
  using Microsoft.Extensions.Hosting;

  using Serilog;
  using Serilog.Core;

  public class Program
  {
    #region Public Methods

    public static void Main(string[] args)
    {
      try
      {
        CreateHostBuilder(args).Build().Run();
      }
      catch (Exception exception)
      {
        try
        {
          LogStartupException(exception);
        }
        catch (Exception)
        {
          throw exception;
        }
      }
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
      Host.CreateDefaultBuilder(args)
        .UseSerilog((hostBuilderContext, loggerConfiguration) => loggerConfiguration.ReadFrom.Configuration(hostBuilderContext.Configuration))
        .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });

    #endregion

    #region Public Methods

    private static void LogStartupException(Exception exception)
    {
      if (!(Log.Logger is Logger))
      {
        var currentDirectory = Directory.GetCurrentDirectory();
        var settingsFileName = $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json";

        if (!File.Exists(Path.Combine(currentDirectory, settingsFileName)))
        {
          settingsFileName = $"appsettings.json";
        }

        var configuration = new ConfigurationBuilder()
          .SetBasePath(currentDirectory)
          .AddJsonFile(settingsFileName)
          .Build();

        Log.Logger = new LoggerConfiguration()
          .ReadFrom.Configuration(configuration)
          .CreateLogger();
      }

      Log.Fatal(exception, "Application failed to start.");

      Log.CloseAndFlush();
    }

    #endregion
  }
}
