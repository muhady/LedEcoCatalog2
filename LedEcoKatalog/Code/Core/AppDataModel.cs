//----------------------------------------------------------------------------
// <copyright file="AppDataModel.cs" company="Binaware">
//   Copyright (c) Binaware s.r.o. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------

namespace LedEcoKatalog.Models
{
  using LedEcoKatalog.Controllers;
  using LedEcoKatalog.Data;

  public class AppDataModel : AppModel
  {
    #region Protected Properties

    protected KatalogDbContext DataContext { get; private set; }

    #endregion

    #region Public Methods

    public void InitializeContext(AppDataController controller, KatalogDbContext dataContext)
    {
      InitializeContext(controller);

      DataContext = dataContext;
    }

    #endregion
  }
}
