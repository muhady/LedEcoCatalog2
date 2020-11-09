//----------------------------------------------------------------------------
// <copyright file="AppDataController{TController}.cs" company="Binaware">
//   Copyright (c) Binaware s.r.o. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------

namespace LedEcoKatalog.Controllers
{
  using LedEcoKatalog.Data;

  using Microsoft.Extensions.Logging;

  public class AppDataController<TController> : AppDataController
    where TController : AppDataController<TController>
  {
    #region Public Fields

    public static readonly string Name = GetName(typeof(TController).Name);

    #endregion

    #region Public Constructors

    public AppDataController(KatalogDbContext dataContext, ILogger<TController> logger = null)
      : base(dataContext, logger)
    {
    }

    #endregion
  }
}
