// Decompiled with JetBrains decompiler
// Type: Microsoft.Practices.Prism.Modularity.DirectoryModuleCatalog
// Assembly: Microsoft.Practices.Prism.Composition, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ADF29BAF-B8A8-43AD-B62C-39144F40570E
// Assembly location: C:\SubVersion\MLD\Main\IFace\Bin\Debug\Microsoft.Practices.Prism.Composition.dll

using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Security.Policy;

namespace Microsoft.Practices.Prism.Modularity
{
  /// <summary>
  /// Represets a catalog created from a directory on disk.
  /// 
  /// </summary>
  /// 
  /// <remarks>
  /// The directory catalog will scan the contents of a directory, locating classes that implement
  ///             <see cref="T:Microsoft.Practices.Prism.Modularity.IModule"/> and add them to the catalog based on contents in their associated <see cref="T:Microsoft.Practices.Prism.Modularity.ModuleAttribute"/>.
  ///             Assemblies are loaded into a new application domain with ReflectionOnlyLoad.  The application domain is destroyed
  ///             once the assemblies have been discovered.
  /// 
  ///             The diretory catalog does not continue to monitor the directory after it has created the initialze catalog.
  /// 
  /// </remarks>
  public class DirectoryModuleCatalog : ModuleCatalog
  {
    /// <summary>
    /// Directory containing modules to search for.
    /// 
    /// </summary>
    public string ModulePath { get; set; }

    /// <summary>
    /// Drives the main logic of building the child domain and searching for the assemblies.
    /// 
    /// </summary>
    protected override void InnerLoad()
    {
      if (string.IsNullOrEmpty(this.ModulePath))
        throw new InvalidOperationException(Resources.ModulePathCannotBeNullOrEmpty);
      if (!Directory.Exists(this.ModulePath))
        throw new InvalidOperationException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Resources.DirectoryNotFound, new object[1]
        {
          (object) this.ModulePath
        }));
      AppDomain domain = this.BuildChildDomain(AppDomain.CurrentDomain);
      try
      {
        List<string> list = new List<string>();
        IEnumerable<string> collection = Enumerable.Select<Assembly, string>(Enumerable.Where<Assembly>(Enumerable.Cast<Assembly>((IEnumerable) AppDomain.CurrentDomain.GetAssemblies()), (Func<Assembly, bool>) (assembly => !(assembly is AssemblyBuilder) && assembly.GetType().FullName != "System.Reflection.Emit.InternalAssemblyBuilder" && !string.IsNullOrEmpty(assembly.Location))), (Func<Assembly, string>) (assembly => assembly.Location));
        list.AddRange(collection);
        System.Type type = typeof (DirectoryModuleCatalog.InnerModuleInfoLoader);
        if (!(type.Assembly != (Assembly) null))
          return;
        DirectoryModuleCatalog.InnerModuleInfoLoader moduleInfoLoader = (DirectoryModuleCatalog.InnerModuleInfoLoader) domain.CreateInstanceFrom(type.Assembly.Location, type.FullName).Unwrap();
        moduleInfoLoader.LoadAssemblies((IEnumerable<string>) list);
        CollectionExtensions.AddRange<IModuleCatalogItem>(this.Items, (IEnumerable<IModuleCatalogItem>) moduleInfoLoader.GetModuleInfos(this.ModulePath));
      }
      finally
      {
        AppDomain.Unload(domain);
      }
    }

    /// <summary>
    /// Creates a new child domain and copies the evidence from a parent domain.
    /// 
    /// </summary>
    /// <param name="parentDomain">The parent domain.</param>
    /// <returns>
    /// The new child domain.
    /// </returns>
    /// 
    /// <remarks>
    /// Grabs the <paramref name="parentDomain"/> evidence and uses it to construct the new
    ///             <see cref="T:System.AppDomain"/> because in a ClickOnce execution environment, creating an
    ///             <see cref="T:System.AppDomain"/> will by default pick up the partial trust environment of
    ///             the AppLaunch.exe, which was the root executable. The AppLaunch.exe does a
    ///             create domain and applies the evidence from the ClickOnce manifests to
    ///             create the domain that the application is actually executing in. This will
    ///             need to be Full Trust for Prism applications.
    /// 
    /// </remarks>
    /// <exception cref="T:System.ArgumentNullException">An <see cref="T:System.ArgumentNullException"/> is thrown if <paramref name="parentDomain"/> is null.</exception>
    protected virtual AppDomain BuildChildDomain(AppDomain parentDomain)
    {
      if (parentDomain == null)
        throw new ArgumentNullException("parentDomain");
      return AppDomain.CreateDomain("DiscoveryRegion", new Evidence(parentDomain.Evidence), parentDomain.SetupInformation);
    }

    private class InnerModuleInfoLoader : MarshalByRefObject
    {
      [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
      internal ModuleInfo[] GetModuleInfos(string path)
      {
        DirectoryInfo directory = new DirectoryInfo(path);
        ResolveEventHandler resolveEventHandler = (ResolveEventHandler) ((sender, args) => DirectoryModuleCatalog.InnerModuleInfoLoader.OnReflectionOnlyResolve(args, directory));
        AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += resolveEventHandler;
        System.Type type = Enumerable.First<Assembly>((IEnumerable<Assembly>) AppDomain.CurrentDomain.ReflectionOnlyGetAssemblies(), (Func<Assembly, bool>) (asm => asm.FullName == typeof (IModule).Assembly.FullName)).GetType(typeof (IModule).FullName);
        ModuleInfo[] moduleInfoArray = Enumerable.ToArray<ModuleInfo>(DirectoryModuleCatalog.InnerModuleInfoLoader.GetNotAllreadyLoadedModuleInfos(directory, type));
        AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve -= resolveEventHandler;
        return moduleInfoArray;
      }

      private static IEnumerable<ModuleInfo> GetNotAllreadyLoadedModuleInfos(DirectoryInfo directory, System.Type IModuleType)
      {
        List<FileInfo> list = new List<FileInfo>();
        Assembly[] alreadyLoadedAssemblies = AppDomain.CurrentDomain.ReflectionOnlyGetAssemblies();
        foreach (FileInfo fileInfo in Enumerable.Where<FileInfo>((IEnumerable<FileInfo>) directory.GetFiles("*.dll"), (Func<FileInfo, bool>) (file => Enumerable.FirstOrDefault<Assembly>((IEnumerable<Assembly>) alreadyLoadedAssemblies, (Func<Assembly, bool>) (assembly => string.Compare(Path.GetFileName(assembly.Location), file.Name, StringComparison.OrdinalIgnoreCase) == 0)) == (Assembly) null)))
        {
          try
          {
            Assembly.ReflectionOnlyLoadFrom(fileInfo.FullName);
            list.Add(fileInfo);
          }
          catch (BadImageFormatException ex)
          {
          }
        }
        return Enumerable.SelectMany<FileInfo, ModuleInfo>((IEnumerable<FileInfo>) list, (Func<FileInfo, IEnumerable<ModuleInfo>>) (file => Enumerable.Select<System.Type, ModuleInfo>(Enumerable.Where<System.Type>(Enumerable.Where<System.Type>(Enumerable.Where<System.Type>((IEnumerable<System.Type>) Assembly.ReflectionOnlyLoadFrom(file.FullName).GetExportedTypes(), new Func<System.Type, bool>(IModuleType.IsAssignableFrom)), (Func<System.Type, bool>) (t => t != IModuleType)), (Func<System.Type, bool>) (t => !t.IsAbstract)), (Func<System.Type, ModuleInfo>) (type => DirectoryModuleCatalog.InnerModuleInfoLoader.CreateModuleInfo(type)))));
      }

      private static Assembly OnReflectionOnlyResolve(ResolveEventArgs args, DirectoryInfo directory)
      {
        Assembly assembly = Enumerable.FirstOrDefault<Assembly>((IEnumerable<Assembly>) AppDomain.CurrentDomain.ReflectionOnlyGetAssemblies(), (Func<Assembly, bool>) (asm => string.Equals(asm.FullName, args.Name, StringComparison.OrdinalIgnoreCase)));
        if (assembly != (Assembly) null)
          return assembly;
        AssemblyName assemblyName = new AssemblyName(args.Name);
        string str = Path.Combine(directory.FullName, assemblyName.Name + ".dll");
        if (File.Exists(str))
          return Assembly.ReflectionOnlyLoadFrom(str);
        return Assembly.ReflectionOnlyLoad(args.Name);
      }

      [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
      internal void LoadAssemblies(IEnumerable<string> assemblies)
      {
        foreach (string assemblyFile in assemblies)
        {
          try
          {
            Assembly.ReflectionOnlyLoadFrom(assemblyFile);
          }
          catch (FileNotFoundException ex)
          {
          }
        }
      }

      private static ModuleInfo CreateModuleInfo(System.Type type)
      {
        string name = type.Name;
        List<string> list = new List<string>();
        bool flag = false;
        CustomAttributeData customAttributeData1 = Enumerable.FirstOrDefault<CustomAttributeData>((IEnumerable<CustomAttributeData>) CustomAttributeData.GetCustomAttributes((MemberInfo) type), (Func<CustomAttributeData, bool>) (cad => cad.Constructor.DeclaringType.FullName == typeof (ModuleAttribute).FullName));
        CustomAttributeTypedArgument typedValue;
        if (customAttributeData1 != null)
        {
          foreach (CustomAttributeNamedArgument attributeNamedArgument in (IEnumerable<CustomAttributeNamedArgument>) customAttributeData1.NamedArguments)
          {
            switch (attributeNamedArgument.MemberInfo.Name)
            {
              case "ModuleName":
                typedValue = attributeNamedArgument.TypedValue;
                name = (string) typedValue.Value;
                break;
              case "OnDemand":
                typedValue = attributeNamedArgument.TypedValue;
                flag = (bool) typedValue.Value;
                break;
              case "StartupLoaded":
                typedValue = attributeNamedArgument.TypedValue;
                flag = !(bool) typedValue.Value;
                break;
            }
          }
        }
        foreach (CustomAttributeData customAttributeData2 in Enumerable.Where<CustomAttributeData>((IEnumerable<CustomAttributeData>) CustomAttributeData.GetCustomAttributes((MemberInfo) type), (Func<CustomAttributeData, bool>) (cad => cad.Constructor.DeclaringType.FullName == typeof (ModuleDependencyAttribute).FullName)))
          list.Add((string) customAttributeData2.ConstructorArguments[0].Value);
        ModuleInfo moduleInfo = new ModuleInfo(name, type.AssemblyQualifiedName)
        {
          InitializationMode = flag ? InitializationMode.OnDemand : InitializationMode.WhenAvailable,
          Ref = type.Assembly.CodeBase
        };
        CollectionExtensions.AddRange<string>(moduleInfo.DependsOn, (IEnumerable<string>) list);
        return moduleInfo;
      }
    }
  }
}
