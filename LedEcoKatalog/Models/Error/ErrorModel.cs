//----------------------------------------------------------------------------
// <copyright file="ErrorModel.cs" company="Binaware">
//   Copyright (c) Binaware s.r.o. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------

namespace LedEcoKatalog.Models
{
  using System;
  using System.Diagnostics;

  using Microsoft.AspNetCore.Diagnostics;
  using Microsoft.AspNetCore.Http;
  using Microsoft.AspNetCore.Http.Features;

  public class ErrorModel
  {
    #region Public Constructors

    public ErrorModel(HttpContext httpContext)
    {
      RequestId = Activity.Current?.Id ?? httpContext?.TraceIdentifier;

      if (httpContext != null)
      {
        if (httpContext.Response != null)
        {
          StatusCode = httpContext.Response.StatusCode;
        }

        var exceptionHandlerPathFeature = httpContext.Features.Get<IExceptionHandlerPathFeature>();
        if (exceptionHandlerPathFeature != null)
        {
          Error = exceptionHandlerPathFeature.Error;
          Path = exceptionHandlerPathFeature.Path;
        }
        else
        {
          var exceptionHandlerFeature = httpContext.Features.Get<IExceptionHandlerFeature>();
          if (exceptionHandlerFeature != null)
          {
            Error = exceptionHandlerFeature.Error;
          }
        }
      }

      if (Error != null)
      {
        //// Title = Resources.Views.Error.Error.Error_Default_Title;
        Title = Settings.GetHttpError(StatusCode)?.Title;
        Message = Error.Message;
        Details = Settings.ShowErrorDetails ? Error.ToString() : null;
      }
      else
      {
        // (Title, Message) = ResourceHelper.GetStatusResources(StatusCode);
        var httpError = Settings.GetHttpError(StatusCode);
        Title = httpError?.Title;
        Message = httpError?.Message;
      }

      if (string.IsNullOrEmpty(Path) && httpContext?.Features is IHttpRequestFeature requestFeature)
      {
        Path = requestFeature.RawTarget;
      }
    }

    #endregion

    #region Public Properties

    public string RequestId { get; set; }

    public int StatusCode { get; set; }

    public Exception Error { get; set; }

    public string Path { get; set; }

    public string Title { get; set; }

    public string Message { get; set; }

    public string Details { get; set; }

    #endregion
  }
}
