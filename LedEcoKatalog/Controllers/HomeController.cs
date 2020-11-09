//----------------------------------------------------------------------------
// <copyright file="HomeController.cs" company="Binaware">
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

  public class HomeController : AppDataController<HomeController>
  {
    #region Public Constructors

    public HomeController(KatalogDbContext dataContext, ILogger<HomeController> logger)
      : base(dataContext, logger)
    {
    }

    #endregion

    #region Public Methods

    public Task<IActionResult> Index()
    {
      return ExecuteActionAsync<HomeModel>();
    }

    #endregion
  }
}
