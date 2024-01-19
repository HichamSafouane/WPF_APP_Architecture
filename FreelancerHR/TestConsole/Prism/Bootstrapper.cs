// Decompiled with JetBrains decompiler
// Type: Microsoft.Practices.Prism.Bootstrapper
// Assembly: Microsoft.Practices.Prism.Composition, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ADF29BAF-B8A8-43AD-B62C-39144F40570E
// Assembly location: C:\SubVersion\MLD\Main\IFace\Bin\Debug\Microsoft.Practices.Prism.Composition.dll

using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.Regions.Behaviors;
using Microsoft.Practices.ServiceLocation;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Microsoft.Practices.Prism
{
  /// <summary>
  /// Base class that provides a basic bootstrapping sequence and hooks
  ///             that specific implementations can override
  /// 
  /// </summary>
  /// 
  /// <remarks>
  /// This class must be overridden to provide application specific configuration.
  /// 
  /// </remarks>
  public abstract class Bootstrapper
  {
    /// <summary>
    /// Gets the <see cref="T:Microsoft.Practices.Prism.Logging.ILoggerFacade"/> for the application.
    /// 
    /// </summary>
    /// 
    /// <value>
    /// A <see cref="T:Microsoft.Practices.Prism.Logging.ILoggerFacade"/> instance.
    /// </value>
    protected ILoggerFacade Logger { get; set; }

    /// <summary>
    /// Gets the default <see cref="T:Microsoft.Practices.Prism.Modularity.IModuleCatalog"/> for the application.
    /// 
    /// </summary>
    /// 
    /// <value>
    /// The default <see cref="T:Microsoft.Practices.Prism.Modularity.IModuleCatalog"/> instance.
    /// </value>
    protected IModuleCatalog ModuleCatalog { get; set; }

    /// <summary>
    /// Gets the shell user interface
    /// 
    /// </summary>
    /// 
    /// <value>
    /// The shell user interface.
    /// </value>
    protected DependencyObject Shell { get; set; }

    /// <summary>
    /// Create the <see cref="T:Microsoft.Practices.Prism.Logging.ILoggerFacade"/> used by the bootstrapper.
    /// 
    /// </summary>
    /// 
    /// <remarks>
    /// The base implementation returns a new TextLogger.
    /// 
    /// </remarks>
    [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "The Logger is added to the container which will dispose it when the container goes out of scope.")]
    protected virtual ILoggerFacade CreateLogger()
    {
      return (ILoggerFacade) new TextLogger();
    }

    /// <summary>
    /// Runs the bootstrapper process.
    /// 
    /// </summary>
    public void Run()
    {
      this.Run(true);
    }

    /// <summary>
    /// Creates the <see cref="T:Microsoft.Practices.Prism.Modularity.IModuleCatalog"/> used by Prism.
    /// 
    /// </summary>
    /// 
    /// <remarks>
    /// The base implementation returns a new ModuleCatalog.
    /// 
    /// </remarks>
    protected virtual IModuleCatalog CreateModuleCatalog()
    {
      return (IModuleCatalog) new Microsoft.Practices.Prism.Modularity.ModuleCatalog();
    }

    /// <summary>
    /// Configures the <see cref="T:Microsoft.Practices.Prism.Modularity.IModuleCatalog"/> used by Prism.
    /// 
    /// </summary>
    protected virtual void ConfigureModuleCatalog()
    {
    }

    /// <summary>
    /// Registers the <see cref="T:System.Type"/>s of the Exceptions that are not considered
    ///             root exceptions by the <see cref="T:Microsoft.Practices.Prism.ExceptionExtensions"/>.
    /// 
    /// </summary>
    protected virtual void RegisterFrameworkExceptionTypes()
    {
      ExceptionExtensions.RegisterFrameworkExceptionType(typeof (ActivationException));
    }

    /// <summary>
    /// Initializes the modules. May be overwritten in a derived class to use a custom Modules Catalog
    /// 
    /// </summary>
    protected virtual void InitializeModules()
    {
      ServiceLocator.Current.GetInstance<IModuleManager>().Run();
    }

    /// <summary>
    /// Configures the default region adapter mappings to use in the application, in order
    ///             to adapt UI controls defined in XAML to use a region and register it automatically.
    ///             May be overwritten in a derived class to add specific mappings required by the application.
    /// 
    /// </summary>
    /// 
    /// <returns>
    /// The <see cref="T:Microsoft.Practices.Prism.Regions.RegionAdapterMappings"/> instance containing all the mappings.
    /// </returns>
    protected virtual RegionAdapterMappings ConfigureRegionAdapterMappings()
    {
      RegionAdapterMappings instance = ServiceLocator.Current.GetInstance<RegionAdapterMappings>();
      if (instance != null)
      {
        instance.RegisterMapping(typeof (Selector), (IRegionAdapter) ServiceLocator.Current.GetInstance<SelectorRegionAdapter>());
        instance.RegisterMapping(typeof (ItemsControl), (IRegionAdapter) ServiceLocator.Current.GetInstance<ItemsControlRegionAdapter>());
        instance.RegisterMapping(typeof (ContentControl), (IRegionAdapter) ServiceLocator.Current.GetInstance<ContentControlRegionAdapter>());
      }
      return instance;
    }

    /// <summary>
    /// Configures the <see cref="T:Microsoft.Practices.Prism.Regions.IRegionBehaviorFactory"/>.
    ///             This will be the list of default behaviors that will be added to a region.
    /// 
    /// </summary>
    protected virtual IRegionBehaviorFactory ConfigureDefaultRegionBehaviors()
    {
      IRegionBehaviorFactory instance = ServiceLocator.Current.GetInstance<IRegionBehaviorFactory>();
      if (instance != null)
      {
        instance.AddIfMissing("ContextToDependencyObject", typeof (BindRegionContextToDependencyObjectBehavior));
        instance.AddIfMissing("ActiveAware", typeof (RegionActiveAwareBehavior));
        instance.AddIfMissing(SyncRegionContextWithHostBehavior.BehaviorKey, typeof (SyncRegionContextWithHostBehavior));
        instance.AddIfMissing(RegionManagerRegistrationBehavior.BehaviorKey, typeof (RegionManagerRegistrationBehavior));
        instance.AddIfMissing("RegionMemberLifetimeBehavior", typeof (RegionMemberLifetimeBehavior));
        instance.AddIfMissing("ClearChildViews", typeof (ClearChildViewsRegionBehavior));
        instance.AddIfMissing("AutoPopulate", typeof (AutoPopulateRegionBehavior));
      }
      return instance;
    }

    /// <summary>
    /// Initializes the shell.
    /// 
    /// </summary>
    protected virtual void InitializeShell()
    {
    }

    /// <summary>
    /// Run the bootstrapper process.
    /// 
    /// </summary>
    /// <param name="runWithDefaultConfiguration">If <see langword="true"/>, registers default
    ///             Prism Library services in the container. This is the default behavior.</param>
    public abstract void Run(bool runWithDefaultConfiguration);

    /// <summary>
    /// Creates the shell or main window of the application.
    /// 
    /// </summary>
    /// 
    /// <returns>
    /// The shell of the application.
    /// </returns>
    /// 
    /// <remarks>
    /// If the returned instance is a <see cref="T:System.Windows.DependencyObject"/>, the
    ///             <see cref="T:Microsoft.Practices.Prism.Bootstrapper"/> will attach the default <see cref="T:Microsoft.Practices.Prism.Regions.IRegionManager"/> of
    ///             the application in its <see cref="F:Microsoft.Practices.Prism.Regions.RegionManager.RegionManagerProperty"/> attached property
    ///             in order to be able to add regions by using the <see cref="F:Microsoft.Practices.Prism.Regions.RegionManager.RegionNameProperty"/>
    ///             attached property from XAML.
    /// 
    /// </remarks>
    protected abstract DependencyObject CreateShell();

    /// <summary>
    /// Configures the LocatorProvider for the <see cref="T:Microsoft.Practices.ServiceLocation.ServiceLocator"/>.
    /// 
    /// </summary>
    protected abstract void ConfigureServiceLocator();
  }
}
