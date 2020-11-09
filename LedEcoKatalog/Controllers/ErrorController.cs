//----------------------------------------------------------------------------
// <copyright file="ErrorController.cs" company="Binaware">
//   Copyright (c) Binaware s.r.o. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------

namespace LedEcoKatalog.Controllers
{
  using LedEcoKatalog.Models;

  using Microsoft.AspNetCore.Mvc;
  using Microsoft.Extensions.Logging;

  public class ErrorController : AppController<ErrorController>
  {
    #region Public Constructors

    public ErrorController(ILogger<ErrorController> logger)
      : base(logger)
    {
    }

    #endregion

    #region Public Methods

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Index()
    {
      return View(new ErrorModel(HttpContext));
    }

    #endregion
  }
}
