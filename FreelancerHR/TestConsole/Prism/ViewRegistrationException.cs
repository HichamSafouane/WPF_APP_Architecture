// Decompiled with JetBrains decompiler
// Type: Microsoft.Practices.Prism.Regions.ViewRegistrationException
// Assembly: Microsoft.Practices.Prism.Composition, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ADF29BAF-B8A8-43AD-B62C-39144F40570E
// Assembly location: C:\SubVersion\MLD\Main\IFace\Bin\Debug\Microsoft.Practices.Prism.Composition.dll

using System;
using System.Runtime.Serialization;

namespace Microsoft.Practices.Prism.Regions
{
  /// <summary>
  /// Exception that's thrown when something goes wrong while Registering a View with a region name in the <see cref="T:Microsoft.Practices.Prism.Regions.RegionViewRegistry"/> class.
  /// 
  /// </summary>
  /// 
  /// <summary>
  /// Exception that's thrown when something goes wrong while Registering a View with a region name in the <see cref="T:Microsoft.Practices.Prism.Regions.RegionViewRegistry"/> class.
  /// 
  /// </summary>
  [Serializable]
  public class ViewRegistrationException : Exception
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Microsoft.Practices.Prism.Regions.ViewRegistrationException"/> class.
    /// 
    /// </summary>
    public ViewRegistrationException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Microsoft.Practices.Prism.Regions.ViewRegistrationException"/> class.
    /// 
    /// </summary>
    /// <param name="message">The exception message.</param>
    public ViewRegistrationException(string message)
      : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Microsoft.Practices.Prism.Regions.ViewRegistrationException"/> class.
    /// 
    /// </summary>
    /// <param name="message">The exception message.</param><param name="inner">The inner exception.</param>
    public ViewRegistrationException(string message, Exception inner)
      : base(message, inner)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Microsoft.Practices.Prism.Regions.ViewRegistrationException"/> class with serialized data.
    /// 
    /// </summary>
    /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the serialized
    ///             object data about the exception being thrown.</param><param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination.</param>
    protected ViewRegistrationException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
