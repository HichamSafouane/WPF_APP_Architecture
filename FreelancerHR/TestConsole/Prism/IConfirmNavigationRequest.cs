// Decompiled with JetBrains decompiler
// Type: Microsoft.Practices.Prism.Regions.IConfirmNavigationRequest
// Assembly: Microsoft.Practices.Prism.Composition, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ADF29BAF-B8A8-43AD-B62C-39144F40570E
// Assembly location: C:\SubVersion\MLD\Main\IFace\Bin\Debug\Microsoft.Practices.Prism.Composition.dll

using System;

namespace Microsoft.Practices.Prism.Regions
{
  /// <summary>
  /// Provides a way for objects involved in navigation to determine if a navigation request should continue.
  /// 
  /// </summary>
  public interface IConfirmNavigationRequest : INavigationAware
  {
    /// <summary>
    /// Determines whether this instance accepts being navigated away from.
    /// 
    /// </summary>
    /// <param name="navigationContext">The navigation context.</param><param name="continuationCallback">The callback to indicate when navigation can proceed.</param>
    /// <remarks>
    /// Implementors of this method do not need to invoke the callback before this method is completed,
    ///             but they must ensure the callback is eventually invoked.
    /// 
    /// </remarks>
    void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback);
  }
}
