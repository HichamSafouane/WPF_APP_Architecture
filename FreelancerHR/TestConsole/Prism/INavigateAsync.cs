// Decompiled with JetBrains decompiler
// Type: Microsoft.Practices.Prism.Regions.INavigateAsync
// Assembly: Microsoft.Practices.Prism.Composition, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ADF29BAF-B8A8-43AD-B62C-39144F40570E
// Assembly location: C:\SubVersion\MLD\Main\IFace\Bin\Debug\Microsoft.Practices.Prism.Composition.dll

using System;

namespace Microsoft.Practices.Prism.Regions
{
  /// <summary>
  /// Provides methods to perform navigation.
  /// 
  /// </summary>
  /// 
  /// <remarks>
  /// Convenience overloads for the methods in this interface can be found as extension methods on the
  ///             <see cref="T:Microsoft.Practices.Prism.Regions.NavigationAsyncExtensions"/> class.
  /// 
  /// </remarks>
  public interface INavigateAsync
  {
    /// <summary>
    /// Initiates navigation to the target specified by the <see cref="T:System.Uri"/>.
    /// 
    /// </summary>
    /// <param name="target">The navigation target</param><param name="navigationCallback">The callback executed when the navigation request is completed.</param>
    /// <remarks>
    /// Convenience overloads for this method can be found as extension methods on the
    ///             <see cref="T:Microsoft.Practices.Prism.Regions.NavigationAsyncExtensions"/> class.
    /// 
    /// </remarks>
    void RequestNavigate(Uri target, Action<NavigationResult> navigationCallback);

    /// <summary>
    /// Initiates navigation to the target specified by the <see cref="T:System.Uri"/>.
    /// 
    /// </summary>
    /// <param name="target">The navigation target</param><param name="navigationCallback">The callback executed when the navigation request is completed.</param><param name="navigationParameters">The navigation parameters specific to the navigation request.</param>
    /// <remarks>
    /// Convenience overloads for this method can be found as extension methods on the
    ///             <see cref="T:Microsoft.Practices.Prism.Regions.NavigationAsyncExtensions"/> class.
    /// 
    /// </remarks>
    void RequestNavigate(Uri target, Action<NavigationResult> navigationCallback, NavigationParameters navigationParameters);
  }
}
