//----------------------------------------------------------------------------
// <copyright file="WebContentHelper.cs" company="Binaware">
//   Copyright (c) Binaware s.r.o. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------

namespace LedEcoKatalog
{
  using System.IO;

  public static class WebContentHelper
  {
    #region Public Fields

    // private const string FrontCoverPagePathFormat = "frontpages\\{0}\\{1}-{0}-predna-obalka-0{2}";

    private const string FrontCoverPagePathFormat = "{0}\\CoverPage";
    private const string CoverPageExtension = ".html";

    private static readonly string RootPath = AppSettings.WebRootPath;
    private static readonly string CoverPagesPath = Path.Combine(RootPath, Settings.CoverPagesFolderPath);
    private static readonly string FrontPagesPath = Path.Combine(CoverPagesPath, "frontpages");

    #endregion

    #region Public Methods

    /*
    public static string GetFrontCoverContent(string layoutCode, string languageCode, string defaultValue = null)
    {
      return GetFileContent(Path.Combine(CoverPagesPath, string.Format(FrontCoverPagePathFormat, layoutCode, languageCode, CoverPageExtension)), defaultValue);
    }
    */
    public static string GetFrontCoverContent(string layoutCode, string languageCode, string defaultValue = null)
    {
      return GetLocalizedFileContent(Path.Combine(FrontPagesPath, string.Format(FrontCoverPagePathFormat, layoutCode)), languageCode, CoverPageExtension, defaultValue);
    }

    #endregion

    #region Private Methods

    private static string GetLocalizedFileContent(string fileNameBase, string languageCode, string extension, string defaultValue)
    {
      var fileName = $"{fileNameBase}.{languageCode}{extension}";

      if (File.Exists(fileName))
      {
        return File.ReadAllText(fileName);
      }

      fileName = $"{fileNameBase}{extension}";

      if (File.Exists(fileName))
      {
        return File.ReadAllText(fileName);
      }

      return defaultValue;
    }

    #endregion
  }
}
