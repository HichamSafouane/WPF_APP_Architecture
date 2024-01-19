// Decompiled with JetBrains decompiler
// Type: Microsoft.Practices.Prism.Modularity.ModuleCatalog
// Assembly: Microsoft.Practices.Prism.Composition, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ADF29BAF-B8A8-43AD-B62C-39144F40570E
// Assembly location: C:\SubVersion\MLD\Main\IFace\Bin\Debug\Microsoft.Practices.Prism.Composition.dll

using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Resources;

namespace Microsoft.Practices.Prism.Modularity
{
  /// <summary>
  /// The <see cref="T:Microsoft.Practices.Prism.Modularity.ModuleCatalog"/> holds information about the modules that can be used by the
  ///             application. Each module is described in a <see cref="T:Microsoft.Practices.Prism.Modularity.ModuleInfo"/> class, that records the
  ///             name, type and location of the module.
  /// 
  ///             It also verifies that the <see cref="T:Microsoft.Practices.Prism.Modularity.ModuleCatalog"/> is internally valid. That means that
  ///             it does not have:
  /// 
  /// <list>
  /// 
  /// <item>
  /// Circular dependencies
  /// </item>
  /// 
  /// <item>
  /// Missing dependencies
  /// </item>
  /// 
  /// <item>
  /// Invalid dependencies, such as a Module that's loaded at startup that depends on a module
  ///                     that might need to be retrieved.
  /// 
  /// </item>
  /// 
  /// </list>
  /// 
  ///             The <see cref="T:Microsoft.Practices.Prism.Modularity.ModuleCatalog"/> also serves as a baseclass for more specialized Catalogs .
  /// 
  /// </summary>
  [ContentProperty("Items")]
  public class ModuleCatalog : IModuleCatalog
  {
    private readonly ModuleCatalog.ModuleCatalogItemCollection items;
    private bool isLoaded;

    /// <summary>
    /// Gets the items in the <see cref="T:Microsoft.Practices.Prism.Modularity.ModuleCatalog"/>. This property is mainly used to add <see cref="T:Microsoft.Practices.Prism.Modularity.ModuleInfoGroup"/>s or
    ///             <see cref="T:Microsoft.Practices.Prism.Modularity.ModuleInfo"/>s through XAML.
    /// 
    /// </summary>
    /// 
    /// <value>
    /// The items in the catalog.
    /// </value>
    public Collection<IModuleCatalogItem> Items
    {
      get
      {
        return (Collection<IModuleCatalogItem>) this.items;
      }
    }

    /// <summary>
    /// Gets all the <see cref="T:Microsoft.Practices.Prism.Modularity.ModuleInfo"/> classes that are in the <see cref="T:Microsoft.Practices.Prism.Modularity.ModuleCatalog"/>, regardless
    ///             if they are within a <see cref="T:Microsoft.Practices.Prism.Modularity.ModuleInfoGroup"/> or not.
    /// 
    /// </summary>
    /// 
    /// <value>
    /// The modules.
    /// </value>
    public virtual IEnumerable<ModuleInfo> Modules
    {
      get
      {
        return Enumerable.Union<ModuleInfo>(this.GrouplessModules, Enumerable.SelectMany<ModuleInfoGroup, ModuleInfo>(this.Groups, (Func<ModuleInfoGroup, IEnumerable<ModuleInfo>>) (g => (IEnumerable<ModuleInfo>) g)));
      }
    }

    /// <summary>
    /// Gets the <see cref="T:Microsoft.Practices.Prism.Modularity.ModuleInfoGroup"/>s that have been added to the <see cref="T:Microsoft.Practices.Prism.Modularity.ModuleCatalog"/>.
    /// 
    /// </summary>
    /// 
    /// <value>
    /// The groups.
    /// </value>
    public IEnumerable<ModuleInfoGroup> Groups
    {
      get
      {
        return Enumerable.OfType<ModuleInfoGroup>((IEnumerable) this.Items);
      }
    }

    /// <summary>
    /// Gets or sets a value that remembers whether the <see cref="T:Microsoft.Practices.Prism.Modularity.ModuleCatalog"/> has been validated already.
    /// 
    /// </summary>
    protected bool Validated { get; set; }

    /// <summary>
    /// Returns the list of <see cref="T:Microsoft.Practices.Prism.Modularity.ModuleInfo"/>s that are not contained within any <see cref="T:Microsoft.Practices.Prism.Modularity.ModuleInfoGroup"/>.
    /// 
    /// </summary>
    /// 
    /// <value>
    /// The groupless modules.
    /// </value>
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Groupless")]
    protected IEnumerable<ModuleInfo> GrouplessModules
    {
      get
      {
        return Enumerable.OfType<ModuleInfo>((IEnumerable) this.Items);
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Microsoft.Practices.Prism.Modularity.ModuleCatalog"/> class.
    /// 
    /// </summary>
    public ModuleCatalog()
    {
      this.items = new ModuleCatalog.ModuleCatalogItemCollection();
      this.items.CollectionChanged += new NotifyCollectionChangedEventHandler(this.ItemsCollectionChanged);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Microsoft.Practices.Prism.Modularity.ModuleCatalog"/> class while providing an
    ///             initial list of <see cref="T:Microsoft.Practices.Prism.Modularity.ModuleInfo"/>s.
    /// 
    /// </summary>
    /// <param name="modules">The initial list of modules.</param>
    public ModuleCatalog(IEnumerable<ModuleInfo> modules)
      : this()
    {
      if (modules == null)
        throw new ArgumentNullException("modules");
      foreach (IModuleCatalogItem moduleCatalogItem in modules)
        this.Items.Add(moduleCatalogItem);
    }

    /// <summary>
    /// Creates a <see cref="T:Microsoft.Practices.Prism.Modularity.ModuleCatalog"/> from XAML.
    /// 
    /// </summary>
    /// <param name="xamlStream"><see cref="T:System.IO.Stream"/> that contains the XAML declaration of the catalog.</param>
    /// <returns>
    /// An instance of <see cref="T:Microsoft.Practices.Prism.Modularity.ModuleCatalog"/> built from the XAML.
    /// </returns>
    public static ModuleCatalog CreateFromXaml(Stream xamlStream)
    {
      if (xamlStream == null)
        throw new ArgumentNullException("xamlStream");
      return XamlReader.Load(xamlStream) as ModuleCatalog;
    }

    /// <summary>
    /// Creates a <see cref="T:Microsoft.Practices.Prism.Modularity.ModuleCatalog"/> from a XAML included as an Application Resource.
    /// 
    /// </summary>
    /// <param name="builderResourceUri">Relative <see cref="T:System.Uri"/> that identifies the XAML included as an Application Resource.</param>
    /// <returns>
    /// An instance of <see cref="T:Microsoft.Practices.Prism.Modularity.ModuleCatalog"/> build from the XAML.
    /// </returns>
    public static ModuleCatalog CreateFromXaml(Uri builderResourceUri)
    {
      StreamResourceInfo resourceStream = Application.GetResourceStream(builderResourceUri);
      if (resourceStream != null && resourceStream.Stream != null)
        return ModuleCatalog.CreateFromXaml(resourceStream.Stream);
      return (ModuleCatalog) null;
    }

    /// <summary>
    /// Loads the catalog if necessary.
    /// 
    /// </summary>
    public void Load()
    {
      this.isLoaded = true;
      this.InnerLoad();
    }

    /// <summary>
    /// Return the list of <see cref="T:Microsoft.Practices.Prism.Modularity.ModuleInfo"/>s that <paramref name="moduleInfo"/> depends on.
    /// 
    /// </summary>
    /// 
    /// <remarks>
    /// If  the <see cref="T:Microsoft.Practices.Prism.Modularity.ModuleCatalog"/> was not yet validated, this method will call <see cref="M:Microsoft.Practices.Prism.Modularity.ModuleCatalog.Validate"/>.
    /// 
    /// </remarks>
    /// <param name="moduleInfo">The <see cref="T:Microsoft.Practices.Prism.Modularity.ModuleInfo"/> to get the </param>
    /// <returns>
    /// An enumeration of <see cref="T:Microsoft.Practices.Prism.Modularity.ModuleInfo"/> that <paramref name="moduleInfo"/> depends on.
    /// </returns>
    public virtual IEnumerable<ModuleInfo> GetDependentModules(ModuleInfo moduleInfo)
    {
      this.EnsureCatalogValidated();
      return this.GetDependentModulesInner(moduleInfo);
    }

    /// <summary>
    /// Returns a list of <see cref="T:Microsoft.Practices.Prism.Modularity.ModuleInfo"/>s that contain both the <see cref="T:Microsoft.Practices.Prism.Modularity.ModuleInfo"/>s in
    ///             <paramref name="modules"/>, but also all the modules they depend on.
    /// 
    /// </summary>
    /// <param name="modules">The modules to get the dependencies for.</param>
    /// <returns>
    /// A list of <see cref="T:Microsoft.Practices.Prism.Modularity.ModuleInfo"/> that contains both all <see cref="T:Microsoft.Practices.Prism.Modularity.ModuleInfo"/>s in <paramref name="modules"/>
    ///             but also all the <see cref="T:Microsoft.Practices.Prism.Modularity.ModuleInfo"/> they depend on.
    /// 
    /// </returns>
    public virtual IEnumerable<ModuleInfo> CompleteListWithDependencies(IEnumerable<ModuleInfo> modules)
    {
      if (modules == null)
        throw new ArgumentNullException("modules");
      this.EnsureCatalogValidated();
      List<ModuleInfo> list1 = new List<ModuleInfo>();
      List<ModuleInfo> list2 = Enumerable.ToList<ModuleInfo>(modules);
      while (list2.Count > 0)
      {
        ModuleInfo moduleInfo1 = list2[0];
        foreach (ModuleInfo moduleInfo2 in this.GetDependentModules(moduleInfo1))
        {
          if (!list1.Contains(moduleInfo2) && !list2.Contains(moduleInfo2))
            list2.Add(moduleInfo2);
        }
        list2.RemoveAt(0);
        list1.Add(moduleInfo1);
      }
      return this.Sort((IEnumerable<ModuleInfo>) list1);
    }

    /// <summary>
    /// Validates the <see cref="T:Microsoft.Practices.Prism.Modularity.ModuleCatalog"/>.
    /// 
    /// </summary>
    /// <exception cref="T:Microsoft.Practices.Prism.Modularity.ModularityException">When validation of the <see cref="T:Microsoft.Practices.Prism.Modularity.ModuleCatalog"/> fails.</exception>
    public virtual void Validate()
    {
      this.ValidateUniqueModules();
      this.ValidateDependencyGraph();
      this.ValidateCrossGroupDependencies();
      this.ValidateDependenciesInitializationMode();
      this.Validated = true;
    }

    /// <summary>
    /// Adds a <see cref="T:Microsoft.Practices.Prism.Modularity.ModuleInfo"/> to the <see cref="T:Microsoft.Practices.Prism.Modularity.ModuleCatalog"/>.
    /// 
    /// </summary>
    /// <param name="moduleInfo">The <see cref="T:Microsoft.Practices.Prism.Modularity.ModuleInfo"/> to add.</param>
    /// <returns>
    /// The <see cref="T:Microsoft.Practices.Prism.Modularity.ModuleCatalog"/> for easily adding multiple modules.
    /// </returns>
    public virtual void AddModule(ModuleInfo moduleInfo)
    {
      this.Items.Add((IModuleCatalogItem) moduleInfo);
    }

    /// <summary>
    /// Adds a groupless <see cref="T:Microsoft.Practices.Prism.Modularity.ModuleInfo"/> to the catalog.
    /// 
    /// </summary>
    /// <param name="moduleType"><see cref="T:System.Type"/> of the module to be added.</param><param name="dependsOn">Collection of module names (<see cref="P:Microsoft.Practices.Prism.Modularity.ModuleInfo.ModuleName"/>) of the modules on which the module to be added logically depends on.</param>
    /// <returns>
    /// The same <see cref="T:Microsoft.Practices.Prism.Modularity.ModuleCatalog"/> instance with the added module.
    /// </returns>
    public ModuleCatalog AddModule(Type moduleType, params string[] dependsOn)
    {
      return this.AddModule(moduleType, InitializationMode.WhenAvailable, dependsOn);
    }

    /// <summary>
    /// Adds a groupless <see cref="T:Microsoft.Practices.Prism.Modularity.ModuleInfo"/> to the catalog.
    /// 
    /// </summary>
    /// <param name="moduleType"><see cref="T:System.Type"/> of the module to be added.</param><param name="initializationMode">Stage on which the module to be added will be initialized.</param><param name="dependsOn">Collection of module names (<see cref="P:Microsoft.Practices.Prism.Modularity.ModuleInfo.ModuleName"/>) of the modules on which the module to be added logically depends on.</param>
    /// <returns>
    /// The same <see cref="T:Microsoft.Practices.Prism.Modularity.ModuleCatalog"/> instance with the added module.
    /// </returns>
    public ModuleCatalog AddModule(Type moduleType, InitializationMode initializationMode, params string[] dependsOn)
    {
      if (moduleType == (Type) null)
        throw new ArgumentNullException("moduleType");
      return this.AddModule(moduleType.Name, moduleType.AssemblyQualifiedName, initializationMode, dependsOn);
    }

    /// <summary>
    /// Adds a groupless <see cref="T:Microsoft.Practices.Prism.Modularity.ModuleInfo"/> to the catalog.
    /// 
    /// </summary>
    /// <param name="moduleName">Name of the module to be added.</param><param name="moduleType"><see cref="T:System.Type"/> of the module to be added.</param><param name="dependsOn">Collection of module names (<see cref="P:Microsoft.Practices.Prism.Modularity.ModuleInfo.ModuleName"/>) of the modules on which the module to be added logically depends on.</param>
    /// <returns>
    /// The same <see cref="T:Microsoft.Practices.Prism.Modularity.ModuleCatalog"/> instance with the added module.
    /// </returns>
    public ModuleCatalog AddModule(string moduleName, string moduleType, params string[] dependsOn)
    {
      return this.AddModule(moduleName, moduleType, InitializationMode.WhenAvailable, dependsOn);
    }

    /// <summary>
    /// Adds a groupless <see cref="T:Microsoft.Practices.Prism.Modularity.ModuleInfo"/> to the catalog.
    /// 
    /// </summary>
    /// <param name="moduleName">Name of the module to be added.</param><param name="moduleType"><see cref="T:System.Type"/> of the module to be added.</param><param name="initializationMode">Stage on which the module to be added will be initialized.</param><param name="dependsOn">Collection of module names (<see cref="P:Microsoft.Practices.Prism.Modularity.ModuleInfo.ModuleName"/>) of the modules on which the module to be added logically depends on.</param>
    /// <returns>
    /// The same <see cref="T:Microsoft.Practices.Prism.Modularity.ModuleCatalog"/> instance with the added module.
    /// </returns>
    public ModuleCatalog AddModule(string moduleName, string moduleType, InitializationMode initializationMode, params string[] dependsOn)
    {
      return this.AddModule(moduleName, moduleType, (string) null, initializationMode, dependsOn);
    }

    /// <summary>
    /// Adds a groupless <see cref="T:Microsoft.Practices.Prism.Modularity.ModuleInfo"/> to the catalog.
    /// 
    /// </summary>
    /// <param name="moduleName">Name of the module to be added.</param><param name="moduleType"><see cref="T:System.Type"/> of the module to be added.</param><param name="refValue">Reference to the location of the module to be added assembly.</param><param name="initializationMode">Stage on which the module to be added will be initialized.</param><param name="dependsOn">Collection of module names (<see cref="P:Microsoft.Practices.Prism.Modularity.ModuleInfo.ModuleName"/>) of the modules on which the module to be added logically depends on.</param>
    /// <returns>
    /// The same <see cref="T:Microsoft.Practices.Prism.Modularity.ModuleCatalog"/> instance with the added module.
    /// </returns>
    public ModuleCatalog AddModule(string moduleName, string moduleType, string refValue, InitializationMode initializationMode, params string[] dependsOn)
    {
      if (moduleName == null)
        throw new ArgumentNullException("moduleName");
      if (moduleType == null)
        throw new ArgumentNullException("moduleType");
      ModuleInfo moduleInfo = new ModuleInfo(moduleName, moduleType);
      CollectionExtensions.AddRange<string>(moduleInfo.DependsOn, (IEnumerable<string>) dependsOn);
      moduleInfo.InitializationMode = initializationMode;
      moduleInfo.Ref = refValue;
      this.Items.Add((IModuleCatalogItem) moduleInfo);
      return this;
    }

    /// <summary>
    /// Initializes the catalog, which may load and validate the modules.
    /// 
    /// </summary>
    /// <exception cref="T:Microsoft.Practices.Prism.Modularity.ModularityException">When validation of the <see cref="T:Microsoft.Practices.Prism.Modularity.ModuleCatalog"/> fails, because this method calls <see cref="M:Microsoft.Practices.Prism.Modularity.ModuleCatalog.Validate"/>.</exception>
    public virtual void Initialize()
    {
      if (!this.isLoaded)
        this.Load();
      this.Validate();
    }

    /// <summary>
    /// Creates and adds a <see cref="T:Microsoft.Practices.Prism.Modularity.ModuleInfoGroup"/> to the catalog.
    /// 
    /// </summary>
    /// <param name="initializationMode">Stage on which the module group to be added will be initialized.</param><param name="refValue">Reference to the location of the module group to be added.</param><param name="moduleInfos">Collection of <see cref="T:Microsoft.Practices.Prism.Modularity.ModuleInfo"/> included in the group.</param>
    /// <returns>
    /// <see cref="T:Microsoft.Practices.Prism.Modularity.ModuleCatalog"/> with the added module group.
    /// </returns>
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Infos")]
    public virtual ModuleCatalog AddGroup(InitializationMode initializationMode, string refValue, params ModuleInfo[] moduleInfos)
    {
      if (moduleInfos == null)
        throw new ArgumentNullException("moduleInfos");
      ModuleInfoGroup moduleInfoGroup = new ModuleInfoGroup();
      moduleInfoGroup.InitializationMode = initializationMode;
      moduleInfoGroup.Ref = refValue;
      foreach (ModuleInfo moduleInfo in moduleInfos)
        moduleInfoGroup.Add(moduleInfo);
      this.items.Add((IModuleCatalogItem) moduleInfoGroup);
      return this;
    }

    /// <summary>
    /// Checks for cyclic dependencies, by calling the dependencysolver.
    /// 
    /// </summary>
    /// <param name="modules">the.</param>
    /// <returns/>
    protected static string[] SolveDependencies(IEnumerable<ModuleInfo> modules)
    {
      if (modules == null)
        throw new ArgumentNullException("modules");
      ModuleDependencySolver dependencySolver = new ModuleDependencySolver();
      foreach (ModuleInfo moduleInfo in modules)
      {
        dependencySolver.AddModule(moduleInfo.ModuleName);
        if (moduleInfo.DependsOn != null)
        {
          foreach (string dependentModule in moduleInfo.DependsOn)
            dependencySolver.AddDependency(moduleInfo.ModuleName, dependentModule);
        }
      }
      if (dependencySolver.ModuleCount > 0)
        return dependencySolver.Solve();
      return new string[0];
    }

    /// <summary>
    /// Ensures that all the dependencies within <paramref name="modules"/> refer to <see cref="T:Microsoft.Practices.Prism.Modularity.ModuleInfo"/>s
    ///             within that list.
    /// 
    /// </summary>
    /// <param name="modules">The modules to validate modules for.</param><exception cref="T:Microsoft.Practices.Prism.Modularity.ModularityException">Throws if a <see cref="T:Microsoft.Practices.Prism.Modularity.ModuleInfo"/> in <paramref name="modules"/> depends on a module that's
    ///             not in <paramref name="modules"/>.
    ///             </exception><exception cref="T:System.ArgumentNullException">Throws if <paramref name="modules"/> is <see langword="null"/>.</exception>
    protected static void ValidateDependencies(IEnumerable<ModuleInfo> modules)
    {
      if (modules == null)
        throw new ArgumentNullException("modules");
      List<string> list = Enumerable.ToList<string>(Enumerable.Select<ModuleInfo, string>(modules, (Func<ModuleInfo, string>) (m => m.ModuleName)));
      foreach (ModuleInfo moduleInfo in modules)
      {
        if (moduleInfo.DependsOn != null && Enumerable.Any<string>(Enumerable.Except<string>((IEnumerable<string>) moduleInfo.DependsOn, (IEnumerable<string>) list)))
          throw new ModularityException(moduleInfo.ModuleName, string.Format((IFormatProvider) CultureInfo.CurrentCulture, Resources.ModuleDependenciesNotMetInGroup, new object[1]
          {
            (object) moduleInfo.ModuleName
          }));
      }
    }

    /// <summary>
    /// Does the actual work of loading the catalog.  The base implementation does nothing.
    /// 
    /// </summary>
    protected virtual void InnerLoad()
    {
    }

    /// <summary>
    /// Sorts a list of <see cref="T:Microsoft.Practices.Prism.Modularity.ModuleInfo"/>s. This method is called by <see cref="M:Microsoft.Practices.Prism.Modularity.ModuleCatalog.CompleteListWithDependencies(System.Collections.Generic.IEnumerable{Microsoft.Practices.Prism.Modularity.ModuleInfo})"/>
    ///             to return a sorted list.
    /// 
    /// </summary>
    /// <param name="modules">The <see cref="T:Microsoft.Practices.Prism.Modularity.ModuleInfo"/>s to sort.</param>
    /// <returns>
    /// Sorted list of <see cref="T:Microsoft.Practices.Prism.Modularity.ModuleInfo"/>s
    /// </returns>
    protected virtual IEnumerable<ModuleInfo> Sort(IEnumerable<ModuleInfo> modules)
    {
      foreach (string str in ModuleCatalog.SolveDependencies(modules))
      {
        string moduleName = str;
        yield return Enumerable.First<ModuleInfo>(modules, (Func<ModuleInfo, bool>) (m => m.ModuleName == moduleName));
      }
    }

    /// <summary>
    /// Makes sure all modules have an Unique name.
    /// 
    /// </summary>
    /// <exception cref="T:Microsoft.Practices.Prism.Modularity.DuplicateModuleException">Thrown if the names of one or more modules are not unique.
    ///             </exception>
    protected virtual void ValidateUniqueModules()
    {
      List<string> moduleNames = Enumerable.ToList<string>(Enumerable.Select<ModuleInfo, string>(this.Modules, (Func<ModuleInfo, string>) (m => m.ModuleName)));
      string moduleName = Enumerable.FirstOrDefault<string>((IEnumerable<string>) moduleNames, (Func<string, bool>) (m => Enumerable.Count<string>((IEnumerable<string>) moduleNames, (Func<string, bool>) (m2 => m2 == m)) > 1));
      if (moduleName != null)
        throw new DuplicateModuleException(moduleName, string.Format((IFormatProvider) CultureInfo.CurrentCulture, Resources.DuplicatedModule, new object[1]
        {
          (object) moduleName
        }));
    }

    /// <summary>
    /// Ensures that there are no cyclic dependencies.
    /// 
    /// </summary>
    protected virtual void ValidateDependencyGraph()
    {
      ModuleCatalog.SolveDependencies(this.Modules);
    }

    /// <summary>
    /// Ensures that there are no dependencies between modules on different groups.
    /// 
    /// </summary>
    /// 
    /// <remarks>
    /// A groupless module can only depend on other groupless modules.
    ///             A module within a group can depend on other modules within the same group and/or on groupless modules.
    /// 
    /// </remarks>
    protected virtual void ValidateCrossGroupDependencies()
    {
      ModuleCatalog.ValidateDependencies(this.GrouplessModules);
      foreach (IEnumerable<ModuleInfo> second in this.Groups)
        ModuleCatalog.ValidateDependencies(Enumerable.Union<ModuleInfo>(this.GrouplessModules, second));
    }

    /// <summary>
    /// Ensures that there are no modules marked to be loaded <see cref="F:Microsoft.Practices.Prism.Modularity.InitializationMode.WhenAvailable"/>
    ///             depending on modules loaded <see cref="F:Microsoft.Practices.Prism.Modularity.InitializationMode.OnDemand"/>
    /// </summary>
    protected virtual void ValidateDependenciesInitializationMode()
    {
      ModuleInfo moduleInfo = Enumerable.FirstOrDefault<ModuleInfo>(this.Modules, (Func<ModuleInfo, bool>) (m => m.InitializationMode == InitializationMode.WhenAvailable && Enumerable.Any<ModuleInfo>(this.GetDependentModulesInner(m), (Func<ModuleInfo, bool>) (dependency => dependency.InitializationMode == InitializationMode.OnDemand))));
      if (moduleInfo != null)
        throw new ModularityException(moduleInfo.ModuleName, string.Format((IFormatProvider) CultureInfo.CurrentCulture, Resources.StartupModuleDependsOnAnOnDemandModule, new object[1]
        {
          (object) moduleInfo.ModuleName
        }));
    }

    /// <summary>
    /// Returns the <see cref="T:Microsoft.Practices.Prism.Modularity.ModuleInfo"/> on which the received module dependens on.
    /// 
    /// </summary>
    /// <param name="moduleInfo">Module whose dependant modules are requested.</param>
    /// <returns>
    /// Collection of <see cref="T:Microsoft.Practices.Prism.Modularity.ModuleInfo"/> dependants of <paramref name="moduleInfo"/>.
    /// </returns>
    protected virtual IEnumerable<ModuleInfo> GetDependentModulesInner(ModuleInfo moduleInfo)
    {
      return Enumerable.Where<ModuleInfo>(this.Modules, (Func<ModuleInfo, bool>) (dependantModule => moduleInfo.DependsOn.Contains(dependantModule.ModuleName)));
    }

    /// <summary>
    /// Ensures that the catalog is validated.
    /// 
    /// </summary>
    protected virtual void EnsureCatalogValidated()
    {
      if (this.Validated)
        return;
      this.Validate();
    }

    private void ItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      if (!this.Validated)
        return;
      this.EnsureCatalogValidated();
    }

    private class ModuleCatalogItemCollection : Collection<IModuleCatalogItem>, INotifyCollectionChanged
    {
      public event NotifyCollectionChangedEventHandler CollectionChanged;

      protected override void InsertItem(int index, IModuleCatalogItem item)
      {
        this.InsertItem(index, item);
        this.OnNotifyCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, (object) item, index));
      }

      protected void OnNotifyCollectionChanged(NotifyCollectionChangedEventArgs eventArgs)
      {
        if (this.CollectionChanged == null)
          return;
        this.CollectionChanged((object) this, eventArgs);
      }
    }
  }
}
