// Decompiled with JetBrains decompiler
// Type: Microsoft.Practices.Prism.Regions.RegionViewRegistry
// Assembly: Microsoft.Practices.Prism.Composition, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ADF29BAF-B8A8-43AD-B62C-39144F40570E
// Assembly location: C:\SubVersion\MLD\Main\IFace\Bin\Debug\Microsoft.Practices.Prism.Composition.dll

using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Properties;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace Microsoft.Practices.Prism.Regions
{
  /// <summary>
  /// Defines a registry for the content of the regions used on View Discovery composition.
  /// 
  /// </summary>
  public class RegionViewRegistry : IRegionViewRegistry
  {
    private readonly ListDictionary<string, Func<object>> registeredContent = new ListDictionary<string, Func<object>>();
    private readonly WeakDelegatesManager contentRegisteredListeners = new WeakDelegatesManager();
    private readonly IServiceLocator locator;

    /// <summary>
    /// Occurs whenever a new view is registered.
    /// 
    /// </summary>
    public event EventHandler<ViewRegisteredEventArgs> ContentRegistered
    {
      add
      {
        this.contentRegisteredListeners.AddListener((Delegate) value);
      }
      remove
      {
        this.contentRegisteredListeners.RemoveListener((Delegate) value);
      }
    }

    /// <summary>
    /// Creates a new instance of the <see cref="T:Microsoft.Practices.Prism.Regions.RegionViewRegistry"/> class.
    /// 
    /// </summary>
    /// <param name="locator"><see cref="T:Microsoft.Practices.ServiceLocation.IServiceLocator"/> used to create the instance of the views from its <see cref="T:System.Type"/>.</param>
    public RegionViewRegistry(IServiceLocator locator)
    {
      this.locator = locator;
    }

    /// <summary>
    /// Returns the contents registered for a region.
    /// 
    /// </summary>
    /// <param name="regionName">Name of the region which content is being requested.</param>
    /// <returns>
    /// Collection of contents registered for the region.
    /// </returns>
    public IEnumerable<object> GetContents(string regionName)
    {
      List<object> list = new List<object>();
      foreach (Func<object> func in (IEnumerable<Func<object>>) this.registeredContent[regionName])
        list.Add(func());
      return (IEnumerable<object>) list;
    }

    /// <summary>
    /// Registers a content type with a region name.
    /// 
    /// </summary>
    /// <param name="regionName">Region name to which the <paramref name="viewType"/> will be registered.</param><param name="viewType">Content type to be registered for the <paramref name="regionName"/>.</param>
    public void RegisterViewWithRegion(string regionName, System.Type viewType)
    {
      this.RegisterViewWithRegion(regionName, (Func<object>) (() => this.CreateInstance(viewType)));
    }

    /// <summary>
    /// Registers a delegate that can be used to retrieve the content associated with a region name.
    /// 
    /// </summary>
    /// <param name="regionName">Region name to which the <paramref name="getContentDelegate"/> will be registered.</param><param name="getContentDelegate">Delegate used to retrieve the content associated with the <paramref name="regionName"/>.</param>
    public void RegisterViewWithRegion(string regionName, Func<object> getContentDelegate)
    {
      this.registeredContent.Add(regionName, getContentDelegate);
      this.OnContentRegistered(new ViewRegisteredEventArgs(regionName, getContentDelegate));
    }

    /// <summary>
    /// Creates an instance of a registered view <see cref="T:System.Type"/>.
    /// 
    /// </summary>
    /// <param name="type">Type of the registered view.</param>
    /// <returns>
    /// Instance of the registered view.
    /// </returns>
    protected virtual object CreateInstance(System.Type type)
    {
      return this.locator.GetInstance((System.Type) type);
    }

    private void OnContentRegistered(ViewRegisteredEventArgs e)
    {
      try
      {
        this.contentRegisteredListeners.Raise((object) this, (object) e);
      }
      catch (TargetInvocationException ex)
      {
        Exception exception = ex.InnerException == null ? ExceptionExtensions.GetRootException((Exception) ex) : ExceptionExtensions.GetRootException(ex.InnerException);
        throw new ViewRegistrationException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Resources.OnViewRegisteredException, new object[2]
        {
          (object) e.RegionName,
          (object) exception
        }), ex.InnerException);
      }
    }
  }
}
