// Decompiled with JetBrains decompiler
// Type: Microsoft.Practices.Prism.Regions.INavigationAware
// Assembly: Microsoft.Practices.Prism.Composition, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ADF29BAF-B8A8-43AD-B62C-39144F40570E
// Assembly location: C:\SubVersion\MLD\Main\IFace\Bin\Debug\Microsoft.Practices.Prism.Composition.dll

namespace Microsoft.Practices.Prism.Regions
{
  /// <summary>
  /// Provides a way for objects involved in navigation to be notified of navigation activities.
  /// 
  /// </summary>
  public interface INavigationAware
  {
    /// <summary>
    /// Called when the implementer has been navigated to.
    /// 
    /// </summary>
    /// <param name="navigationContext">The navigation context.</param>
    void OnNavigatedTo(NavigationContext navigationContext);

    /// <summary>
    /// Called to determine if this instance can handle the navigation request.
    /// 
    /// </summary>
    /// <param name="navigationContext">The navigation context.</param>
    /// <returns>
    /// <see langword="true"/> if this instance accepts the navigation request; otherwise, <see langword="false"/>.
    /// 
    /// </returns>
    bool IsNavigationTarget(NavigationContext navigationContext);

    /// <summary>
    /// Called when the implementer is being navigated away from.
    /// 
    /// </summary>
    /// <param name="navigationContext">The navigation context.</param>
    void OnNavigatedFrom(NavigationContext navigationContext);
  }
}
