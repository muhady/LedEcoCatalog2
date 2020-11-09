//----------------------------------------------------------------------------
// <copyright file="HttpError.cs" company="Binaware">
//   Copyright (c) Binaware s.r.o. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------

namespace LedEcoKatalog
{
  public class HttpError
  {
    #region Public Properties

    public int Code { get; set; }

    public string Title { get; set; }

    public string Message { get; set; }

    #endregion
  }
}
