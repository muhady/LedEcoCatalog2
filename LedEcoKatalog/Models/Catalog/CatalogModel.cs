//----------------------------------------------------------------------------
// <copyright file="CatalogModel.cs" company="Binaware">
//   Copyright (c) Binaware s.r.o. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------

namespace LedEcoKatalog.Models
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Reflection.Metadata;
  using System.Threading.Tasks;

  using LedEcoKatalog.Data;

  using Microsoft.AspNetCore.Mvc.Rendering;
  using Microsoft.EntityFrameworkCore;

  public class CatalogModel : AppDataModel
  {
    #region Public Properties

    public int Scope { get; set; }

    public int PriceLevel { get; set; }

    public string Language { get; set; }

    public string Layout { get; set; }

    public string NameOfCatalog { get; set; }

    #endregion

    #region Public Properties

    public int InitialPages { get; set; }

    public string PriceLevelText { get; set; }

    public string CatalogName { get; set; }

    public List<CatalogContentt> Content { get; set; }

    public List<CatalogSection4t> Section4 { get; set; }

    public string LegendContent { get; set; }

    public List<CatalogSection1t> Sec1 { get; set; }

    public List<CatalogSection2t> Sec2 { get; set; }

    public List<CatalogSection2pict> Section2Picts { get; set; }

    public List<CatalogSection2pic3rdt> Section2Pic3Rdts { get; set; }

    public List<CatalogSection3t> Section3 { get; set; }

    public string[] FosaliLevels { get; set; }

    public int EndPages { get; set; }

    public string DisclaimerContent { get; set; }

    #endregion

    #region Public Properties

    /*
    public string ContentTitle { get; set; }

    public string LegendTitle { get; set; }

    public string LegendDescription { get; set; }
    */

    public CatalogLanguage CatalogLanguage { get; set; }

    #endregion

    #region Public Methods

    public async override Task ExecuteAsync()
    {
      var scope = Scope;
      var priceLevel = PriceLevel;
      var language = Language;
      var visual = Layout;
      var nameOfCatalog = NameOfCatalog;

      /*
      InitialPages = int.Parse(_configuration[visual + ":InitialPages"]);
      EndPages = int.Parse(_configuration[visual + ":FinalPages"]);
      */
      if (Settings.CatalogSettings.TryGetValue(visual, out CatalogSettings catalogSettings))
      {
        InitialPages = catalogSettings.InitialPages;
        EndPages = catalogSettings.FinalPages;
      }

      // LegendContent = System.IO.File.ReadAllText(@"wwwroot\legend\" + visual + @"\" + language + @"\legend.html");
      LegendContent = ResourceHelper.GetLegend(visual, language);

      // DisclaimerContent = System.IO.File.ReadAllText(@"wwwroot\disclaimer\" + visual + @"\" + language + @"\disclaimer.html");
      DisclaimerContent = ResourceHelper.GetDisclaimer(visual, language);

      // FosaliLevels = _configuration.GetSection("Layout_Fosali").Get<string[]>();
      FosaliLevels = Settings.FosaliLayouts.Select(i => i.ToString()).ToArray();

      //// string priceleveltext;

      if (priceLevel == -1)
      {
        PriceLevelText = "Bez cien";
      }
      else
      {
        PriceLevelText = DataContext.PriceLevel.First(e => e.IntIdpriceList == priceLevel).StrTextId.ToString();
      }

      Content = await DataContext.CatalogContentt
          .FromSqlRaw($"catalogcontent {scope}, {language}")
          .ToListAsync();

      // var catalogname = "";
      if (nameOfCatalog == null)
      {
        CatalogName = Content.OrderBy(e => e.Page).First().CategoryName;
        //// catalogname = DataContext.Hierarchy.First(e => e.Idlevel == int.Parse(scope)).Názov;
      }
      else
      {
        CatalogName = nameOfCatalog;
      }

      Sec1 = await DataContext.CatalogSection1t
          .FromSqlRaw($"catalogsection1 {scope}, {language}")
          .ToListAsync();

      Sec2 = await DataContext.CatalogSection2t
        .FromSqlRaw($"catalogsection2 {scope}, {language}, {priceLevel}")
        .ToListAsync();

      Section2Picts = await DataContext.CatalogSection2pict
        .FromSqlRaw($"catalogsection2pic {scope}, {language}")
        .ToListAsync();

      Section2Pic3Rdts = await DataContext.CatalogSection2pic3rdt
        .FromSqlRaw($"catalogsection2pic3rd {scope}, {language}")
        .ToListAsync();

      Section3 = await DataContext.CatalogSection3t
        .FromSqlRaw($"catalogsection3 {scope}, {language}, {priceLevel}")
        .ToListAsync();

      var fosaliENproperties = 0;
      if (visual == "Fosali")
      {
        fosaliENproperties = 1;
      }
      else
      {
        fosaliENproperties = 0;
      }

      Section4 = await DataContext.CatalogSection4t
        .FromSqlRaw($"catalogsection4 {scope}, {language},{fosaliENproperties} ")
        .ToListAsync();

      if (visual == "Fosali")
      {
        language = Settings.DefaultCatalogLanguage;
      }

      // var catalogLanguage = Settings.GetCatalogLanguage(language);
      // ContentTitle = catalogLanguage?.ContentTitle;
      // LegendTitle = catalogLanguage?.LegendTitle;
      // LegendDescription = catalogLanguage?.LegendDescription;
      CatalogLanguage = Settings.GetCatalogLanguage(language);
    }

    /*
    public override async Task OnCreatingViewResultAsync()
    {
    }
    */

    #endregion
  }
}
