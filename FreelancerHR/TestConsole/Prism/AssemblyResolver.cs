// Decompiled with JetBrains decompiler
// Type: Microsoft.Practices.Prism.Modularity.AssemblyResolver
// Assembly: Microsoft.Practices.Prism.Composition, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ADF29BAF-B8A8-43AD-B62C-39144F40570E
// Assembly location: C:\SubVersion\MLD\Main\IFace\Bin\Debug\Microsoft.Practices.Prism.Composition.dll

using Microsoft.Practices.Prism.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Microsoft.Practices.Prism.Modularity
{
  /// <summary>
  /// Handles AppDomain's AssemblyResolve event to be able to load assemblies dynamically in
  ///             the LoadFrom context, but be able to reference the type from assemblies loaded in the Load context.
  /// 
  /// </summary>
  public class AssemblyResolver : IAssemblyResolver, IDisposable
  {
    private readonly List<AssemblyResolver.AssemblyInfo> registeredAssemblies = new List<AssemblyResolver.AssemblyInfo>();
    private bool handlesAssemblyResolve;

    /// <summary>
    /// Registers the specified assembly and resolves the types in it when the AppDomain requests for it.
    /// 
    /// </summary>
    /// <param name="assemblyFilePath">The path to the assemly to load in the LoadFrom context.</param>
    /// <remarks>
    /// This method does not load the assembly immediately, but lazily until someone requests a <see cref="T:System.Type"/>
    ///             declared in the assembly.
    /// </remarks>
    public void LoadAssemblyFrom(string assemblyFilePath)
    {
      if (!this.handlesAssemblyResolve)
      {
        AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(this.CurrentDomain_AssemblyResolve);
        this.handlesAssemblyResolve = true;
      }
      Uri fileUri = AssemblyResolver.GetFileUri(assemblyFilePath);
      if (fileUri == (Uri) null)
        throw new ArgumentException(Resources.InvalidArgumentAssemblyUri, "assemblyFilePath");
      if (!File.Exists(fileUri.LocalPath))
        throw new FileNotFoundException();
      AssemblyName assemblyName = AssemblyName.GetAssemblyName(fileUri.LocalPath);
      if (Enumerable.FirstOrDefault<AssemblyResolver.AssemblyInfo>((IEnumerable<AssemblyResolver.AssemblyInfo>) this.registeredAssemblies, (Func<AssemblyResolver.AssemblyInfo, bool>) (a => assemblyName == a.AssemblyName)) != null)
        return;
      this.registeredAssemblies.Add(new AssemblyResolver.AssemblyInfo()
      {
        AssemblyName = assemblyName,
        AssemblyUri = fileUri
      });
    }

    private static Uri GetFileUri(string filePath)
    {
      Uri result;
      if (string.IsNullOrEmpty(filePath) || !Uri.TryCreate(filePath, UriKind.Absolute, out result) || !result.IsFile)
        return (Uri) null;
      return result;
    }

    [SuppressMessage("Microsoft.Reliability", "CA2001:AvoidCallingProblematicMethods", MessageId = "System.Reflection.Assembly.LoadFrom")]
    private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
    {
      AssemblyName assemblyName = new AssemblyName(args.Name);
      AssemblyResolver.AssemblyInfo assemblyInfo = Enumerable.FirstOrDefault<AssemblyResolver.AssemblyInfo>((IEnumerable<AssemblyResolver.AssemblyInfo>) this.registeredAssemblies, (Func<AssemblyResolver.AssemblyInfo, bool>) (a => AssemblyName.ReferenceMatchesDefinition(assemblyName, a.AssemblyName)));
      if (assemblyInfo == null)
        return (Assembly) null;
      if (assemblyInfo.Assembly == (Assembly) null)
        assemblyInfo.Assembly = Assembly.LoadFrom(assemblyInfo.AssemblyUri.LocalPath);
      return assemblyInfo.Assembly;
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// 
    /// </summary>
    /// 
    /// <remarks>
    /// Calls <see cref="M:Microsoft.Practices.Prism.Modularity.AssemblyResolver.Dispose(System.Boolean)"/>
    /// </remarks>
    /// .
    ///             <filterpriority>2</filterpriority>
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    /// <summary>
    /// Disposes the associated <see cref="T:Microsoft.Practices.Prism.Modularity.AssemblyResolver"/>.
    /// 
    /// </summary>
    /// <param name="disposing">When <see langword="true"/>, it is being called from the Dispose method.</param>
    protected virtual void Dispose(bool disposing)
    {
      if (!this.handlesAssemblyResolve)
        return;
      AppDomain.CurrentDomain.AssemblyResolve -= new ResolveEventHandler(this.CurrentDomain_AssemblyResolve);
      this.handlesAssemblyResolve = false;
    }

    private class AssemblyInfo
    {
      public AssemblyName AssemblyName { get; set; }

      public Uri AssemblyUri { get; set; }

      public Assembly Assembly { get; set; }
    }
  }
}
