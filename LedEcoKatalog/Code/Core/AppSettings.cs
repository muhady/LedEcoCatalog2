//----------------------------------------------------------------------------
// <copyright file="AppSettings.cs" company="Binaware">
//   Copyright (c) Binaware s.r.o. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------

namespace LedEcoKatalog
{
  using System.Collections.Generic;
  using System.Globalization;
  using System.Linq;

  using Microsoft.AspNetCore.Builder;
  using Microsoft.AspNetCore.Hosting;
  using Microsoft.Extensions.Configuration;
  using Microsoft.Extensions.Hosting;

  public class AppSettings
  {
    #region Public Properties

    public static string AppSettingsSectionName { get; set; } = "AppSettings";

    public static IConfiguration Configuration { get; private set; }

    public static IApplicationBuilder Application { get; private set; }

    public static IWebHostEnvironment Environment { get; private set; }

    public static string ContentRootPath { get; private set; }

    public static string WebRootPath { get; private set; }

    public static bool IsDevelopment { get; private set; }

    #endregion

    #region Public Methods

    public static void Initialize(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public static void Initialize(IApplicationBuilder application, IWebHostEnvironment environment)
    {
      Application = application;
      Environment = environment;

      if (environment != null)
      {
        ContentRootPath = environment.ContentRootPath;
        WebRootPath = environment.WebRootPath;
        IsDevelopment = environment.IsDevelopment();
      }
    }

    public static void Initialize(IConfiguration configuration, IApplicationBuilder application, IWebHostEnvironment environment)
    {
      Initialize(configuration);
      Initialize(application, environment);
    }

    #endregion

    #region Protected Methods

    protected static T GetValue<T>(string section, string name, T defaultValue = default)
    {
      return Configuration.GetValue($"{section}:{name}", defaultValue);
    }

    protected static T GetValue<T>(string name, T defaultValue = default)
    {
      return GetValue(AppSettingsSectionName, name, defaultValue);
    }

    protected static List<T> GetList<T>(string section, string name, bool shouldCreate = false)
    {
      return Configuration.GetSection($"{section}:{name}").Get<List<T>>() ?? (shouldCreate ? new List<T>() : null);
    }

    protected static List<T> GetList<T>(string name, bool shouldCreate = false)
    {
      return GetList<T>(AppSettingsSectionName, name, shouldCreate);
    }

    protected static CultureInfo GetCulture(string section, string name, CultureInfo defaultValue = null)
    {
      var setting = GetValue(section, name, string.Empty);
      return string.IsNullOrWhiteSpace(setting) ? defaultValue ?? CultureInfo.CurrentCulture : new CultureInfo(setting);
    }

    protected static CultureInfo GetCulture(string name, CultureInfo defaultValue = null)
    {
      return GetCulture(AppSettingsSectionName, name, defaultValue);
    }

    protected static List<CultureInfo> GetCultures(string section, string name, bool shouldCreate = false)
    {
      return GetList<string>(section, name, shouldCreate)?.Select(c => new CultureInfo(c)).ToList();
    }

    protected static List<CultureInfo> GetCultures(string name, bool shouldCreate = false)
    {
      return GetCultures(AppSettingsSectionName, name, shouldCreate);
    }

    /*
    protected static string GetPath(string name, string defaultValue = "")
    {
      var path = GetString(name, defaultValue);
      return Path.IsPathRooted(path) ? path : Path.Combine(ContentRootPath, path);
    }
    */

    #endregion
  }
}
