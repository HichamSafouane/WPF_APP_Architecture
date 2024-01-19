// Decompiled with JetBrains decompiler
// Type: Microsoft.Practices.Prism.Regions.RegionNavigationService
// Assembly: Microsoft.Practices.Prism.Composition, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ADF29BAF-B8A8-43AD-B62C-39144F40570E
// Assembly location: C:\SubVersion\MLD\Main\IFace\Bin\Debug\Microsoft.Practices.Prism.Composition.dll

using Microsoft.Practices.Prism.Properties;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows;

namespace Microsoft.Practices.Prism.Regions
{
  /// <summary>
  /// Provides navigation for regions.
  /// 
  /// </summary>
  public class RegionNavigationService : IRegionNavigationService, INavigateAsync
  {
    private readonly IServiceLocator serviceLocator;
    private readonly IRegionNavigationContentLoader regionNavigationContentLoader;
    private IRegionNavigationJournal journal;
    private NavigationContext currentNavigationContext;

    /// <summary>
    /// Gets or sets the region.
    /// 
    /// </summary>
    /// 
    /// <value>
    /// The region.
    /// </value>
    public IRegion Region { get; set; }

    /// <summary>
    /// Gets the journal.
    /// 
    /// </summary>
    /// 
    /// <value>
    /// The journal.
    /// </value>
    public IRegionNavigationJournal Journal
    {
      get
      {
        return this.journal;
      }
    }

    /// <summary>
    /// Raised when the region is about to be navigated to content.
    /// 
    /// </summary>
    public event EventHandler<RegionNavigationEventArgs> Navigating;

    /// <summary>
    /// Raised when the region is navigated to content.
    /// 
    /// </summary>
    public event EventHandler<RegionNavigationEventArgs> Navigated;

    /// <summary>
    /// Raised when a navigation request fails.
    /// 
    /// </summary>
    public event EventHandler<RegionNavigationFailedEventArgs> NavigationFailed;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Microsoft.Practices.Prism.Regions.RegionNavigationService"/> class.
    /// 
    /// </summary>
    /// <param name="serviceLocator">The service locator.</param><param name="regionNavigationContentLoader">The navigation target handler.</param><param name="journal">The journal.</param>
    public RegionNavigationService(IServiceLocator serviceLocator, IRegionNavigationContentLoader regionNavigationContentLoader, IRegionNavigationJournal journal)
    {
      if (serviceLocator == null)
        throw new ArgumentNullException("serviceLocator");
      if (regionNavigationContentLoader == null)
        throw new ArgumentNullException("regionNavigationContentLoader");
      if (journal == null)
        throw new ArgumentNullException("journal");
      this.serviceLocator = serviceLocator;
      this.regionNavigationContentLoader = regionNavigationContentLoader;
      this.journal = journal;
      this.journal.NavigationTarget = (INavigateAsync) this;
    }

    private void RaiseNavigating(NavigationContext navigationContext)
    {
      if (this.Navigating == null)
        return;
      this.Navigating((object) this, new RegionNavigationEventArgs(navigationContext));
    }

    private void RaiseNavigated(NavigationContext navigationContext)
    {
      if (this.Navigated == null)
        return;
      this.Navigated((object) this, new RegionNavigationEventArgs(navigationContext));
    }

    private void RaiseNavigationFailed(NavigationContext navigationContext, Exception error)
    {
      if (this.NavigationFailed == null)
        return;
      this.NavigationFailed((object) this, new RegionNavigationFailedEventArgs(navigationContext, error));
    }

    /// <summary>
    /// Initiates navigation to the specified target.
    /// 
    /// </summary>
    /// <param name="target">The target.</param><param name="navigationCallback">A callback to execute when the navigation request is completed.</param>
    [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Exception is marshalled to callback")]
    public void RequestNavigate(Uri target, Action<NavigationResult> navigationCallback)
    {
      this.RequestNavigate(target, navigationCallback, (NavigationParameters) null);
    }

    /// <summary>
    /// Initiates navigation to the specified target.
    /// 
    /// </summary>
    /// <param name="target">The target.</param><param name="navigationCallback">A callback to execute when the navigation request is completed.</param><param name="navigationParameters">The navigation parameters specific to the navigation request.</param>
    public void RequestNavigate(Uri target, Action<NavigationResult> navigationCallback, NavigationParameters navigationParameters)
    {
      if (navigationCallback == null)
        throw new ArgumentNullException("navigationCallback");
      try
      {
        this.DoNavigate(target, navigationCallback, navigationParameters);
      }
      catch (Exception ex)
      {
        this.NotifyNavigationFailed(new NavigationContext((IRegionNavigationService) this, target), navigationCallback, ex);
      }
    }

    private void DoNavigate(Uri source, Action<NavigationResult> navigationCallback, NavigationParameters navigationParameters)
    {
      if (source == (Uri) null)
        throw new ArgumentNullException("source");
      if (this.Region == null)
        throw new InvalidOperationException(Resources.NavigationServiceHasNoRegion);
      this.currentNavigationContext = new NavigationContext((IRegionNavigationService) this, source, navigationParameters);
      this.RequestCanNavigateFromOnCurrentlyActiveView(this.currentNavigationContext, navigationCallback, Enumerable.ToArray<object>((IEnumerable<object>) this.Region.ActiveViews), 0);
    }

    private void RequestCanNavigateFromOnCurrentlyActiveView(NavigationContext navigationContext, Action<NavigationResult> navigationCallback, object[] activeViews, int currentViewIndex)
    {
      if (currentViewIndex < activeViews.Length)
      {
        IConfirmNavigationRequest navigationRequest = activeViews[currentViewIndex] as IConfirmNavigationRequest;
        if (navigationRequest != null)
          navigationRequest.ConfirmNavigationRequest(navigationContext, (Action<bool>) (canNavigate =>
          {
            if (this.currentNavigationContext == navigationContext && canNavigate)
              this.RequestCanNavigateFromOnCurrentlyActiveViewModel(navigationContext, navigationCallback, activeViews, currentViewIndex);
            else
              this.NotifyNavigationFailed(navigationContext, navigationCallback, (Exception) null);
          }));
        else
          this.RequestCanNavigateFromOnCurrentlyActiveViewModel(navigationContext, navigationCallback, activeViews, currentViewIndex);
      }
      else
        this.ExecuteNavigation(navigationContext, activeViews, navigationCallback);
    }

    private void RequestCanNavigateFromOnCurrentlyActiveViewModel(NavigationContext navigationContext, Action<NavigationResult> navigationCallback, object[] activeViews, int currentViewIndex)
    {
      FrameworkElement frameworkElement = activeViews[currentViewIndex] as FrameworkElement;
      if (frameworkElement != null)
      {
        IConfirmNavigationRequest navigationRequest = frameworkElement.DataContext as IConfirmNavigationRequest;
        if (navigationRequest != null)
        {
          navigationRequest.ConfirmNavigationRequest(navigationContext, (Action<bool>) (canNavigate =>
          {
            if (this.currentNavigationContext == navigationContext && canNavigate)
              this.RequestCanNavigateFromOnCurrentlyActiveView(navigationContext, navigationCallback, activeViews, currentViewIndex + 1);
            else
              this.NotifyNavigationFailed(navigationContext, navigationCallback, (Exception) null);
          }));
          return;
        }
      }
      this.RequestCanNavigateFromOnCurrentlyActiveView(navigationContext, navigationCallback, activeViews, currentViewIndex + 1);
    }

    [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Exception is marshalled to callback")]
    private void ExecuteNavigation(NavigationContext navigationContext, object[] activeViews, Action<NavigationResult> navigationCallback)
    {
      try
      {
        RegionNavigationService.NotifyActiveViewsNavigatingFrom(navigationContext, activeViews);
        object view = this.regionNavigationContentLoader.LoadContent(this.Region, navigationContext);
        this.RaiseNavigating(navigationContext);
        this.Region.Activate(view);
        IRegionNavigationJournalEntry instance = this.serviceLocator.GetInstance<IRegionNavigationJournalEntry>();
        instance.Uri = navigationContext.Uri;
        instance.Parameters = navigationContext.Parameters;
        this.journal.RecordNavigation(instance);
        RegionNavigationService.InvokeOnNavigationAwareElement(view, (Action<INavigationAware>) (n => n.OnNavigatedTo(navigationContext)));
        navigationCallback(new NavigationResult(navigationContext, new bool?(true)));
        this.RaiseNavigated(navigationContext);
      }
      catch (Exception ex)
      {
        this.NotifyNavigationFailed(navigationContext, navigationCallback, ex);
      }
    }

    private void NotifyNavigationFailed(NavigationContext navigationContext, Action<NavigationResult> navigationCallback, Exception e)
    {
      NavigationResult navigationResult = e != null ? new NavigationResult(navigationContext, e) : new NavigationResult(navigationContext, new bool?(false));
      navigationCallback(navigationResult);
      this.RaiseNavigationFailed(navigationContext, e);
    }

    private static void NotifyActiveViewsNavigatingFrom(NavigationContext navigationContext, object[] activeViews)
    {
      RegionNavigationService.InvokeOnNavigationAwareElements((IEnumerable<object>) activeViews, (Action<INavigationAware>) (n => n.OnNavigatedFrom(navigationContext)));
    }

    private static void InvokeOnNavigationAwareElements(IEnumerable<object> items, Action<INavigationAware> invocation)
    {
      foreach (object obj in items)
        RegionNavigationService.InvokeOnNavigationAwareElement(obj, invocation);
    }

    private static void InvokeOnNavigationAwareElement(object item, Action<INavigationAware> invocation)
    {
      INavigationAware navigationAware1 = item as INavigationAware;
      if (navigationAware1 != null)
        invocation(navigationAware1);
      FrameworkElement frameworkElement = item as FrameworkElement;
      if (frameworkElement == null)
        return;
      INavigationAware navigationAware2 = frameworkElement.DataContext as INavigationAware;
      if (navigationAware2 != null)
        invocation(navigationAware2);
    }
  }
}
