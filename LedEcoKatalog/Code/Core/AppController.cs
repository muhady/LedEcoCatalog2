//----------------------------------------------------------------------------
// <copyright file="AppController.cs" company="Binaware">
//   Copyright (c) Binaware s.r.o. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------

namespace LedEcoKatalog.Controllers
{
  using System;
  using System.Threading.Tasks;

  using LedEcoKatalog.Models;

  using Microsoft.AspNetCore.Mvc;
  using Microsoft.Extensions.Logging;

  public class AppController : Controller
  {
    #region Public Constructors

    public AppController()
    {
    }

    public AppController(ILogger logger)
    {
      Logger = logger;
    }

    #endregion

    #region Protected Properties

    protected ILogger Logger { get; }

    #endregion

    #region Public Methods

    public static string GetName(string controller)
    {
      const string Controller = nameof(Controller);
      return controller.EndsWith(Controller) ? controller.Substring(0, controller.Length - Controller.Length) : controller;
    }

    #endregion

    #region Protected Methods

    protected virtual IActionResult ExecuteAction<TModel>(/*ResponseType? expectedResponseType = null*/)
      where TModel : AppModel, new()
      => ExecuteAction(default(TModel), null/*, expectedResponseType*/);

    protected virtual IActionResult ExecuteAction<TModel>(TModel model/*, ResponseType? expectedResponseType = null*/)
      where TModel : AppModel, new()
      => ExecuteAction(model, null/*, expectedResponseType*/);

    protected virtual IActionResult ExecuteAction<TModel>(Action<TModel> action/*, ResponseType? expectedResponseType = null*/)
      where TModel : AppModel, new()
      => ExecuteAction(default, action/*, expectedResponseType*/);

    protected virtual IActionResult ExecuteAction<TModel>(TModel model, Action<TModel> action/*, ResponseType? expectedResponseType = null*/)
      where TModel : AppModel, new()
    {
      //// ExpectedResponseType = expectedResponseType;

      if (model == null)
      {
        model = new TModel();
      }

      InitializeModel(model);

      model.Initialize();

      if (model.Result != null)
      {
        return model.Result;
      }

      if (action != null)
      {
        action(model);
      }
      else
      {
        model.Execute();
      }

      if (model.Result != null)
      {
        return model.Result;
      }

      model.OnCreatingViewResult();

      return CreateResult(model/*, expectedResponseType*/);
    }

    protected virtual Task<IActionResult> ExecuteActionAsync<TModel>(/*ResponseType? expectedResultType = null*/)
      where TModel : AppModel, new()
      => ExecuteActionAsync(default(TModel), null/*, expectedResultType*/);

    protected virtual Task<IActionResult> ExecuteActionAsync<TModel>(TModel model/*, ResponseType? expectedResultType = null*/)
      where TModel : AppModel, new()
      => ExecuteActionAsync(model, null/*, expectedResultType*/);

    protected virtual Task<IActionResult> ExecuteActionAsync<TModel>(Func<TModel, Task> action/*, ResponseType? expectedResultType = null*/)
      where TModel : AppModel, new()
      => ExecuteActionAsync(default, action/*, expectedResultType*/);

    protected virtual async Task<IActionResult> ExecuteActionAsync<TModel>(TModel model, Func<TModel, Task> action/*, ResponseType? expectedResultType = null*/)
      where TModel : AppModel, new()
    {
      //// ExpectedResponseType = expectedResultType;

      if (model == null)
      {
        model = new TModel();
      }

      InitializeModel(model);

      await model.InitializeAsync();

      if (model.Result != null)
      {
        return model.Result;
      }

      Task task;

      if (action != null)
      {
        task = action(model);
      }
      else
      {
        task = model.ExecuteAsync();
      }

      if (task != null)
      {
        await task;
      }

      if (model.Result != null)
      {
        return model.Result;
      }

      task = model.OnCreatingViewResultAsync();

      if (task != null)
      {
        await task;
      }

      return CreateResult(model/*, expectedResultType*/);
    }

    protected virtual void InitializeModel<TModel>(TModel model)
      where TModel : AppModel, new()
    {
      model?.InitializeContext(this);
    }

    protected virtual string GetViewName<TModel>(TModel model/*, ResponseType expectedResultType = ResponseType.View*/)
      where TModel : AppModel
    {
      return model?.GetViewName();
    }

    protected virtual IActionResult CreateResult<TModel>(TModel model/*, ResponseType? expectedResponseType*/)
      where TModel : AppModel
    {
      // expectedResponseType ??= HttpRequestHelper.GetExpectedResponseType(HttpContext?.Request) ?? ResponseType.View;
      // var viewName = GetViewName(model, expectedResponseType.Value);
      // return expectedResponseType == ResponseType.PartialView || expectedResponseType == ResponseType.PartialPanel || expectedResponseType == ResponseType.PartialWindow ? (IActionResult)PartialView(viewName, model) : View(viewName, model);
      var viewName = GetViewName(model/*, expectedResponseType.Value*/);
      return View(viewName, model);
    }

    #endregion
  }
}
