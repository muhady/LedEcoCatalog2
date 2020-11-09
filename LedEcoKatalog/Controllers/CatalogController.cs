//----------------------------------------------------------------------------
// <copyright file="CatalogController.cs" company="Binaware">
//   Copyright (c) Binaware s.r.o. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------

namespace LedEcoKatalog.Controllers
{
  using System.Threading.Tasks;

  using LedEcoKatalog.Data;
  using LedEcoKatalog.Models;

  using Microsoft.AspNetCore.Mvc;
  using Microsoft.Extensions.Logging;

  public class CatalogController : AppDataController<CatalogController>
  {
    #region Public Constructors

    public CatalogController(KatalogDbContext dataContext, ILogger<CatalogController> logger)
      : base(dataContext, logger)
    {
    }

    #endregion

    #region Public Methods

    [HttpPost]
    public Task<IActionResult> Index(CatalogModel model)
    {
      return ExecuteActionAsync(model);
    }

    #endregion
  }
}
