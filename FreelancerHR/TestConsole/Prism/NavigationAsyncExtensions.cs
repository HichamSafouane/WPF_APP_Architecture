// Decompiled with JetBrains decompiler
// Type: Microsoft.Practices.Prism.Regions.NavigationAsyncExtensions
// Assembly: Microsoft.Practices.Prism.Composition, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ADF29BAF-B8A8-43AD-B62C-39144F40570E
// Assembly location: C:\SubVersion\MLD\Main\IFace\Bin\Debug\Microsoft.Practices.Prism.Composition.dll

using System;

namespace Microsoft.Practices.Prism.Regions
{
  /// <summary>
  /// Provides additional methods to the <see cref="T:Microsoft.Practices.Prism.Regions.INavigateAsync"/> interface.
  /// 
  /// </summary>
  public static class NavigationAsyncExtensions
  {
    /// <summary>
    /// Initiates navigation to the target specified by the <paramref name="target"/>.
    /// 
    /// </summary>
    /// <param name="navigation">The navigation object.</param><param name="target">The navigation target</param>
    public static void RequestNavigate(this INavigateAsync navigation, string target)
    {
      NavigationAsyncExtensions.RequestNavigate(navigation, target, (Action<NavigationResult>) (nr => {}));
    }

    /// <summary>
    /// Initiates navigation to the target specified by the <paramref name="target"/>.
    /// 
    /// </summary>
    /// <param name="navigation">The navigation object.</param><param name="target">The navigation target</param><param name="navigationCallback">The callback executed when the navigation request is completed.</param>
    public static void RequestNavigate(this INavigateAsync navigation, string target, Action<NavigationResult> navigationCallback)
    {
      if (navigation == null)
        throw new ArgumentNullException("navigation");
      if (target == null)
        throw new ArgumentNullException("target");
      Uri target1 = new Uri(target, UriKind.RelativeOrAbsolute);
      navigation.RequestNavigate(target1, navigationCallback);
    }

    /// <summary>
    /// Initiates navigation to the target specified by the <see cref="T:System.Uri"/>.
    /// 
    /// </summary>
    /// <param name="navigation">The navigation object.</param><param name="target">The navigation target</param>
    public static void RequestNavigate(this INavigateAsync navigation, Uri target)
    {
      if (navigation == null)
        throw new ArgumentNullException("navigation");
      navigation.RequestNavigate(target, (Action<NavigationResult>) (nr => {}));
    }

    /// <summary>
    /// Initiates navigation to the target specified by the <paramref name="target"/>.
    /// 
    /// </summary>
    /// <param name="navigation">The navigation object.</param><param name="target">The navigation target</param><param name="navigationCallback">The callback executed when the navigation request is completed.</param><param name="navigationParameters">An instance of NavigationParameters, which holds a collection of object parameters.</param>
    public static void RequestNavigate(this INavigateAsync navigation, string target, Action<NavigationResult> navigationCallback, NavigationParameters navigationParameters)
    {
      if (navigation == null)
        throw new ArgumentNullException("navigation");
      if (target == null)
        throw new ArgumentNullException("target");
      Uri target1 = new Uri(target, UriKind.RelativeOrAbsolute);
      navigation.RequestNavigate(target1, navigationCallback, navigationParameters);
    }

    /// <summary>
    /// Initiates navigation to the target specified by the <paramref name="target"/>.
    /// 
    /// </summary>
    /// <param name="navigation">The navigation object.</param><param name="target">A Uri that represents the target where the region will navigate.</param><param name="navigationParameters">An instance of NavigationParameters, which holds a collection of object parameters.</param>
    public static void RequestNavigate(this INavigateAsync navigation, Uri target, NavigationParameters navigationParameters)
    {
      if (navigation == null)
        throw new ArgumentNullException("navigation");
      navigation.RequestNavigate(target, (Action<NavigationResult>) (nr => {}), navigationParameters);
    }

    /// <summary>
    /// Initiates navigation to the target specified by the <paramref name="target"/>.
    /// 
    /// </summary>
    /// <param name="navigation">The navigation object.</param><param name="target">A string that represents the target where the region will navigate.</param><param name="navigationParameters">An instance of NavigationParameters, which holds a collection of object parameters.</param>
    public static void RequestNavigate(this INavigateAsync navigation, string target, NavigationParameters navigationParameters)
    {
      if (navigation == null)
        throw new ArgumentNullException("navigation");
      if (target == null)
        throw new ArgumentNullException("target");
      navigation.RequestNavigate(new Uri(target, UriKind.RelativeOrAbsolute), (Action<NavigationResult>) (nr => {}), navigationParameters);
    }
  }
}
