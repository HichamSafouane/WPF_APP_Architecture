// Decompiled with JetBrains decompiler
// Type: Microsoft.Practices.Prism.Regions.NavigationContext
// Assembly: Microsoft.Practices.Prism.Composition, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ADF29BAF-B8A8-43AD-B62C-39144F40570E
// Assembly location: C:\SubVersion\MLD\Main\IFace\Bin\Debug\Microsoft.Practices.Prism.Composition.dll

using Microsoft.Practices.Prism;
using System;
using System.Collections.Generic;

namespace Microsoft.Practices.Prism.Regions
{
  /// <summary>
  /// Encapsulates information about a navigation request.
  /// 
  /// </summary>
  public class NavigationContext
  {
    /// <summary>
    /// Gets the region navigation service.
    /// 
    /// </summary>
    /// 
    /// <value>
    /// The navigation service.
    /// </value>
    public IRegionNavigationService NavigationService { get; private set; }

    /// <summary>
    /// Gets the navigation URI.
    /// 
    /// </summary>
    /// 
    /// <value>
    /// The navigation URI.
    /// </value>
    public Uri Uri { get; private set; }

    /// <summary>
    /// Gets the <see cref="T:Microsoft.Practices.Prism.Regions.NavigationParameters"/> extracted from the URI and the object parameters passed in navigation.
    /// 
    /// </summary>
    /// 
    /// <value>
    /// The URI query.
    /// </value>
    public NavigationParameters Parameters { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Microsoft.Practices.Prism.Regions.NavigationContext"/> class for a region name and a
    ///             <see cref="P:Microsoft.Practices.Prism.Regions.NavigationContext.Uri"/>.
    /// 
    /// </summary>
    /// <param name="navigationService">The navigation service.</param><param name="uri">The Uri.</param>
    public NavigationContext(IRegionNavigationService navigationService, Uri uri)
      : this(navigationService, uri, (NavigationParameters) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Microsoft.Practices.Prism.Regions.NavigationContext"/> class for a region name and a
    ///             <see cref="P:Microsoft.Practices.Prism.Regions.NavigationContext.Uri"/>.
    /// 
    /// </summary>
    /// <param name="navigationService">The navigation service.</param><param name="navigationParameters">The navigation parameters.</param><param name="uri">The Uri.</param>
    public NavigationContext(IRegionNavigationService navigationService, Uri uri, NavigationParameters navigationParameters)
    {
      this.NavigationService = navigationService;
      this.Uri = uri;
      this.Parameters = uri != (Uri) null ? UriParsingHelper.ParseQuery(uri) : (NavigationParameters) null;
      this.GetNavigationParameters(navigationParameters);
    }

    private void GetNavigationParameters(NavigationParameters navigationParameters)
    {
      if (this.Parameters == null || this.NavigationService == null || this.NavigationService.Region == null)
      {
        this.Parameters = new NavigationParameters();
      }
      else
      {
        if (navigationParameters == null)
          return;
        foreach (KeyValuePair<string, object> keyValuePair in navigationParameters)
          this.Parameters.Add(keyValuePair.Key, keyValuePair.Value);
      }
    }
  }
}
