//----------------------------------------------------------------------------
// <copyright file="AppController{TController}.cs" company="Binaware">
//   Copyright (c) Binaware s.r.o. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------

namespace LedEcoKatalog.Controllers
{
  using Microsoft.Extensions.Logging;

  public class AppController<TController> : AppController
    where TController : AppController<TController>
  {
    #region Public Fields

    public static readonly string Name = GetName(typeof(TController).Name);

    #endregion

    #region Public Constructors

    public AppController()
    {
    }

    public AppController(ILogger<TController> logger)
      : base(logger)
    {
    }

    #endregion
  }
}
