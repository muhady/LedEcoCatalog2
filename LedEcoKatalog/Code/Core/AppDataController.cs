//----------------------------------------------------------------------------
// <copyright file="AppDataController.cs" company="Binaware">
//   Copyright (c) Binaware s.r.o. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------

namespace LedEcoKatalog.Controllers
{
  using LedEcoKatalog.Data;
  using LedEcoKatalog.Models;

  using Microsoft.Extensions.Logging;

  public class AppDataController : AppController
  {
    #region Public Constructors

    public AppDataController(KatalogDbContext dataContext, ILogger logger = null)
      : base(logger)
    {
      DataContext = dataContext;
    }

    #endregion

    #region Protected Properties

    protected KatalogDbContext DataContext { get; }

    #endregion

    #region Protected Methods

    protected override void InitializeModel<TModel>(TModel model)
    {
      if (model is AppDataModel dataModel)
      {
        dataModel.InitializeContext(this, DataContext);
      }
      else
      {
        model?.InitializeContext(this);
      }
    }

    #endregion
  }
}
