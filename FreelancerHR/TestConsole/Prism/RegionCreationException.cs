// Decompiled with JetBrains decompiler
// Type: Microsoft.Practices.Prism.Regions.Behaviors.RegionCreationException
// Assembly: Microsoft.Practices.Prism.Composition, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ADF29BAF-B8A8-43AD-B62C-39144F40570E
// Assembly location: C:\SubVersion\MLD\Main\IFace\Bin\Debug\Microsoft.Practices.Prism.Composition.dll

using System;
using System.Runtime.Serialization;

namespace Microsoft.Practices.Prism.Regions.Behaviors
{
  /// <summary>
  /// Represents errors that occured during region creation.
  /// 
  /// </summary>
  /// 
  /// <summary/>
  [Serializable]
  public class RegionCreationException : Exception
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Microsoft.Practices.Prism.Regions.Behaviors.RegionCreationException"/>
    /// </summary>
    public RegionCreationException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Microsoft.Practices.Prism.Regions.Behaviors.RegionCreationException"/> class with a specified error message.
    /// 
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public RegionCreationException(string message)
      : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Microsoft.Practices.Prism.Regions.Behaviors.RegionCreationException"/> class with a specified error message and a reference
    ///             to the inner exception that is the cause of this exception.
    /// 
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param><param name="inner">The exception that is the cause of the current exception, or a null reference
    ///             (Nothing in Visual Basic) if no inner exception is specified.</param>
    public RegionCreationException(string message, Exception inner)
      : base(message, inner)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Microsoft.Practices.Prism.Regions.Behaviors.RegionCreationException"/> class with serialized data.
    /// 
    /// </summary>
    /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param><param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination.</param>
    protected RegionCreationException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
