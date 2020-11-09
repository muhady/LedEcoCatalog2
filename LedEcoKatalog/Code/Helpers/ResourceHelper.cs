//----------------------------------------------------------------------------
// <copyright file="ResourceHelper.cs" company="Binaware">
//   Copyright (c) Binaware s.r.o. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------

namespace LedEcoKatalog
{
  using System.IO;

  public static class ResourceHelper
  {
    #region Public Fields

    private const string DisclaimerFileName = "disclaimer.html";
    private const string LegendFileName = "legend.html";

    private static readonly string ResourcesDirectory = Path.Combine(AppSettings.ContentRootPath, "Resources");
    private static readonly string DisclaimersPath = Path.Combine(ResourcesDirectory, "Disclaimers");
    private static readonly string LegendsPath = Path.Combine(ResourcesDirectory, "Legends");

    #endregion

    #region Public Methods

    public static string GetDisclaimer(string name, string language, string defaultValue = null)
    {
      return GetFileContent(Path.Combine(DisclaimersPath, name, language, DisclaimerFileName), defaultValue);
    }

    public static string GetLegend(string name, string language, string defaultValue = null)
    {
      return GetFileContent(Path.Combine(LegendsPath, name, language, LegendFileName), defaultValue);
    }

    #endregion

    #region Private Methods

    private static string GetFileContent(string fileName, string defaultValue)
    {
      if (File.Exists(fileName))
      {
        return File.ReadAllText(fileName);
      }

      return defaultValue;
    }

    #endregion
  }
}
