//----------------------------------------------------------------------------
// <copyright file="Settings.cs" company="Binaware">
//   Copyright (c) Binaware s.r.o. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------

namespace LedEcoKatalog
{
  using System.Collections.Generic;
  using System.Linq;

  using Microsoft.AspNetCore.Builder;
  using Microsoft.AspNetCore.Hosting;
  using Microsoft.Extensions.Configuration;

  public class Settings : AppSettings
  {
    #region Public Properties (Settings)

    public static Dictionary<string, CatalogSettings> CatalogSettings { get; private set; }

    public static Dictionary<string, CatalogLayout> CatalogLayouts { get; private set; }

    public static Dictionary<string, CatalogLanguage> CatalogLanguages { get; private set; }

    public static string AppLanguageCode { get; private set; }

    public static string FosaliLanguageCode { get; private set; }

    public static List<int> FosaliLayouts { get; private set; }

    public static Dictionary<int, HttpError> HttpErrors { get; private set; }

    public static bool ShowErrorDetails { get; private set; }

    public static bool UseRequestLogging { get; private set; }

    #endregion

    #region Public Properties (ConnectionStrings)

    public static string KatalogDbConnectionString { get; private set; }

    #endregion

    #region Public Methods

    public static new void Initialize(IConfiguration configuration, IApplicationBuilder application, IWebHostEnvironment environment)
    {
      AppSettings.Initialize(configuration, application, environment);

      //// AppSettings

      CatalogSettings = GetList<CatalogSettings>(nameof(CatalogSettings), true).ToDictionary(s => s.Name);

      CatalogLayouts = GetList<CatalogLayout>(nameof(CatalogLayouts), true).ToDictionary(s => s.Code);

      CatalogLanguages = GetList<CatalogLanguage>(nameof(CatalogLanguages), true).ToDictionary(s => s.Name);

      AppLanguageCode = GetValue(nameof(AppLanguageCode), "SK");

      FosaliLanguageCode = GetValue(nameof(FosaliLanguageCode), "EN");

      FosaliLayouts = GetList<int>(nameof(FosaliLayouts), true);

      HttpErrors = GetList<HttpError>(nameof(HttpErrors), true).ToDictionary(s => s.Code);

      ShowErrorDetails = GetValue(nameof(ShowErrorDetails), false);

      UseRequestLogging = GetValue(nameof(UseRequestLogging), false);
    }

    public static CatalogLanguage GetCatalogLanguage(string language)
    {
      if (language != null && CatalogLanguages.TryGetValue(language, out CatalogLanguage catalogLanguage))
      {
        return catalogLanguage;
      }

      return CatalogLanguages.Values.FirstOrDefault();
    }

    public static HttpError GetHttpError(int code)
    {
      if (HttpErrors.TryGetValue(code, out HttpError httpError))
      {
        return httpError;
      }

      if (code != 0 && HttpErrors.TryGetValue(0, out httpError))
      {
        return httpError;
      }

      return HttpErrors.Values.FirstOrDefault();
    }

    #endregion
  }
}
