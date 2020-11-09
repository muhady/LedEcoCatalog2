//----------------------------------------------------------------------------
// <copyright file="AppModel.cs" company="Binaware">
//   Copyright (c) Binaware s.r.o. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------

namespace LedEcoKatalog.Models
{
  using System;
  using System.Threading.Tasks;

  using LedEcoKatalog.Controllers;

  using Microsoft.AspNetCore.Mvc;

  public class AppModel
  {
    #region Public Properties

    public AppController Controller { get; private set; }

    public IActionResult Result { get; set; }

    #endregion

    #region Public Methods

    public void InitializeContext(AppController controller/*, ActionResultType expectedResultType = ActionResultType.View*/)
    {
      Controller = controller ?? throw new ArgumentNullException(nameof(controller));
      //// this.expectedResultType = expectedResultType;

      controller?.ModelState?.Remove(nameof(Controller));

      /*
      if (string.IsNullOrWhiteSpace(ReturnUrl))
      {
        if (controller.Request?.Headers?.TryGetValue("Referer", out StringValues referer) == true && referer.Count > 0)
        {
          var returnUrl = referer.First();
          if (!string.IsNullOrWhiteSpace(returnUrl))
          {
            ReturnUrl = returnUrl;
          }
        }
      }
      */
    }

    public virtual void Initialize()
    {
    }

    public virtual Task InitializeAsync()
    {
      Initialize();

      return Task.CompletedTask;
    }

    public virtual void Execute()
    {
    }

    public virtual Task ExecuteAsync()
    {
      Execute();

      return Task.CompletedTask;
    }

    public virtual void OnCreatingViewResult()
    {
    }

    public virtual Task OnCreatingViewResultAsync()
    {
      OnCreatingViewResult();

      return Task.CompletedTask;
    }

    public virtual string GetViewName()
    {
      return null;
    }

    #endregion
  }
}
