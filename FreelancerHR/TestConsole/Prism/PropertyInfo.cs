// Decompiled with JetBrains decompiler
// Type: System.Reflection.PropertyInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 255ABCDF-D9D6-4E3D-BAD4-F74D4CE3D7A8
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Reflection
{
  /// <summary>
  /// Discovers the attributes of a property and provides access to property metadata.
  /// </summary>
  [ComVisible(true)]
  [ClassInterface(ClassInterfaceType.None)]
  [ComDefaultInterface(typeof (_PropertyInfo))]
  [__DynamicallyInvokable]
  [Serializable]
  [PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
  public abstract class PropertyInfo : MemberInfo, _PropertyInfo
  {
    /// <summary>
    /// Gets a <see cref="T:System.Reflection.MemberTypes"/> value indicating that this member is a property.
    /// </summary>
    /// 
    /// <returns>
    /// A <see cref="T:System.Reflection.MemberTypes"/> value indicating that this member is a property.
    /// </returns>
    public override MemberTypes MemberType
    {
      get
      {
        return MemberTypes.Property;
      }
    }

    /// <summary>
    /// Gets the type of this property.
    /// </summary>
    /// 
    /// <returns>
    /// The type of this property.
    /// </returns>
    [__DynamicallyInvokable]
    public abstract Type PropertyType { [__DynamicallyInvokable] get; }

    /// <summary>
    /// Gets the attributes for this property.
    /// </summary>
    /// 
    /// <returns>
    /// Attributes of this property.
    /// </returns>
    [__DynamicallyInvokable]
    public abstract PropertyAttributes Attributes { [__DynamicallyInvokable] get; }

    /// <summary>
    /// Gets a value indicating whether the property can be read.
    /// </summary>
    /// 
    /// <returns>
    /// true if this property can be read; otherwise, false.
    /// </returns>
    [__DynamicallyInvokable]
    public abstract bool CanRead { [__DynamicallyInvokable] get; }

    /// <summary>
    /// Gets a value indicating whether the property can be written to.
    /// </summary>
    /// 
    /// <returns>
    /// true if this property can be written to; otherwise, false.
    /// </returns>
    [__DynamicallyInvokable]
    public abstract bool CanWrite { [__DynamicallyInvokable] get; }

    /// <summary>
    /// Gets the get accessor for this property.
    /// </summary>
    /// 
    /// <returns>
    /// The get accessor for this property.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual MethodInfo GetMethod
    {
      [__DynamicallyInvokable, TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] get
      {
        return this.GetGetMethod(true);
      }
    }

    /// <summary>
    /// Gets the set accessor for this property.
    /// </summary>
    /// 
    /// <returns>
    /// The set accessor for this property.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual MethodInfo SetMethod
    {
      [__DynamicallyInvokable, TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] get
      {
        return this.GetSetMethod(true);
      }
    }

    /// <summary>
    /// Gets a value indicating whether the property is the special name.
    /// </summary>
    /// 
    /// <returns>
    /// true if this property is the special name; otherwise, false.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsSpecialName
    {
      [__DynamicallyInvokable] get
      {
        return (this.Attributes & PropertyAttributes.SpecialName) != PropertyAttributes.None;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:System.Reflection.PropertyInfo"/> class.
    /// </summary>
    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    protected PropertyInfo()
    {
    }

    /// <summary>
    /// Indicates whether two <see cref="T:System.Reflection.PropertyInfo"/> objects are equal.
    /// </summary>
    /// 
    /// <returns>
    /// true if <paramref name="left"/> is equal to <paramref name="right"/>; otherwise, false.
    /// </returns>
    /// <param name="left">The first object to compare.</param><param name="right">The second object to compare.</param>
    [__DynamicallyInvokable]
    public static bool operator ==(PropertyInfo left, PropertyInfo right)
    {
      if (object.ReferenceEquals((object) left, (object) right))
        return true;
      if (left == null || right == null || (left is RuntimePropertyInfo || right is RuntimePropertyInfo))
        return false;
      return left.Equals((object) right);
    }

    /// <summary>
    /// Indicates whether two <see cref="T:System.Reflection.PropertyInfo"/> objects are not equal.
    /// </summary>
    /// 
    /// <returns>
    /// true if <paramref name="left"/> is not equal to <paramref name="right"/>; otherwise, false.
    /// </returns>
    /// <param name="left">The first object to compare.</param><param name="right">The second object to compare.</param>
    [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    [__DynamicallyInvokable]
    public static bool operator !=(PropertyInfo left, PropertyInfo right)
    {
      return !(left == right);
    }

    /// <summary>
    /// Returns a value that indicates whether this instance is equal to a specified object.
    /// </summary>
    /// 
    /// <returns>
    /// true if <paramref name="obj"/> equals the type and value of this instance; otherwise, false.
    /// </returns>
    /// <param name="obj">An object to compare with this instance, or null.</param>
    [__DynamicallyInvokable]
    public override bool Equals(object obj)
    {
      return base.Equals(obj);
    }

    /// <summary>
    /// Returns the hash code for this instance.
    /// </summary>
    /// 
    /// <returns>
    /// A 32-bit signed integer hash code.
    /// </returns>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

    /// <summary>
    /// Returns a literal value associated with the property by a compiler.
    /// </summary>
    /// 
    /// <returns>
    /// An <see cref="T:System.Object"/> that contains the literal value associated with the property. If the literal value is a class type with an element value of zero, the return value is null.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">The Constant table in unmanaged metadata does not contain a constant value for the current property.</exception><exception cref="T:System.FormatException">The type of the value is not one of the types permitted by the Common Language Specification (CLS). See the ECMA Partition II specification, Metadata. </exception>
    [__DynamicallyInvokable]
    public virtual object GetConstantValue()
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Returns a literal value associated with the property by a compiler.
    /// </summary>
    /// 
    /// <returns>
    /// An <see cref="T:System.Object"/> that contains the literal value associated with the property. If the literal value is a class type with an element value of zero, the return value is null.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">The Constant table in unmanaged metadata does not contain a constant value for the current property.</exception><exception cref="T:System.FormatException">The type of the value is not one of the types permitted by the Common Language Specification (CLS). See the ECMA Partition II specification, Metadata Logical Format: Other Structures, Element Types used in Signatures. </exception>
    public virtual object GetRawConstantValue()
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// When overridden in a derived class, sets the property value for a specified object that has the specified binding, index, and culture-specific information.
    /// </summary>
    /// <param name="obj">The object whose property value will be set. </param><param name="value">The new property value. </param><param name="invokeAttr">A bitwise combination of the following enumeration members that specify the invocation attribute: InvokeMethod, CreateInstance, Static, GetField, SetField, GetProperty, or SetProperty. You must specify a suitable invocation attribute. For example, to invoke a static member, set the Static flag. </param><param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of <see cref="T:System.Reflection.MemberInfo"/> objects through reflection. If <paramref name="binder"/> is null, the default binder is used. </param><param name="index">Optional index values for indexed properties. This value should be null for non-indexed properties. </param><param name="culture">The culture for which the resource is to be localized. If the resource is not localized for this culture, the <see cref="P:System.Globalization.CultureInfo.Parent"/> property will be called successively in search of a match. If this value is null, the culture-specific information is obtained from the <see cref="P:System.Globalization.CultureInfo.CurrentUICulture"/> property. </param><exception cref="T:System.ArgumentException">The <paramref name="index"/> array does not contain the type of arguments needed.-or- The property's set accessor is not found. </exception><exception cref="T:System.Reflection.TargetException">The object does not match the target type, or a property is an instance property but <paramref name="obj"/> is null. </exception><exception cref="T:System.Reflection.TargetParameterCountException">The number of parameters in <paramref name="index"/> does not match the number of parameters the indexed property takes. </exception><exception cref="T:System.MethodAccessException">There was an illegal attempt to access a private or protected method inside a class. </exception><exception cref="T:System.Reflection.TargetInvocationException">An error occurred while setting the property value. For example, an index value specified for an indexed property is out of range. The <see cref="P:System.Exception.InnerException"/> property indicates the reason for the error.</exception>
    public abstract void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture);

    /// <summary>
    /// Returns an array whose elements reflect the public and, if specified, non-public get, set, and other accessors of the property reflected by the current instance.
    /// </summary>
    /// 
    /// <returns>
    /// An array of <see cref="T:System.Reflection.MethodInfo"/> objects whose elements reflect the get, set, and other accessors of the property reflected by the current instance. If <paramref name="nonPublic"/> is true, this array contains public and non-public get, set, and other accessors. If <paramref name="nonPublic"/> is false, this array contains only public get, set, and other accessors. If no accessors with the specified visibility are found, this method returns an array with zero (0) elements.
    /// </returns>
    /// <param name="nonPublic">Indicates whether non-public methods should be returned in the MethodInfo array. true if non-public methods are to be included; otherwise, false. </param>
    [__DynamicallyInvokable]
    public abstract MethodInfo[] GetAccessors(bool nonPublic);

    /// <summary>
    /// When overridden in a derived class, returns the public or non-public get accessor for this property.
    /// </summary>
    /// 
    /// <returns>
    /// A MethodInfo object representing the get accessor for this property, if <paramref name="nonPublic"/> is true. Returns null if <paramref name="nonPublic"/> is false and the get accessor is non-public, or if <paramref name="nonPublic"/> is true but no get accessors exist.
    /// </returns>
    /// <param name="nonPublic">Indicates whether a non-public get accessor should be returned. true if a non-public accessor is to be returned; otherwise, false. </param><exception cref="T:System.Security.SecurityException">The requested method is non-public and the caller does not have <see cref="T:System.Security.Permissions.ReflectionPermission"/> to reflect on this non-public method. </exception>
    [__DynamicallyInvokable]
    public abstract MethodInfo GetGetMethod(bool nonPublic);

    /// <summary>
    /// When overridden in a derived class, returns the set accessor for this property.
    /// </summary>
    /// 
    /// <returns>
    /// Value Condition A <see cref="T:System.Reflection.MethodInfo"/> object representing the Set method for this property. The set accessor is public.-or- <paramref name="nonPublic"/> is true and the set accessor is non-public. null<paramref name="nonPublic"/> is true, but the property is read-only.-or- <paramref name="nonPublic"/> is false and the set accessor is non-public.-or- There is no set accessor.
    /// </returns>
    /// <param name="nonPublic">Indicates whether the accessor should be returned if it is non-public. true if a non-public accessor is to be returned; otherwise, false. </param><exception cref="T:System.Security.SecurityException">The requested method is non-public and the caller does not have <see cref="T:System.Security.Permissions.ReflectionPermission"/> to reflect on this non-public method. </exception>
    [__DynamicallyInvokable]
    public abstract MethodInfo GetSetMethod(bool nonPublic);

    /// <summary>
    /// When overridden in a derived class, returns an array of all the index parameters for the property.
    /// </summary>
    /// 
    /// <returns>
    /// An array of type ParameterInfo containing the parameters for the indexes. If the property is not indexed, the array has 0 (zero) elements.
    /// </returns>
    [__DynamicallyInvokable]
    public abstract ParameterInfo[] GetIndexParameters();

    /// <summary>
    /// Returns the property value of a specified object.
    /// </summary>
    /// 
    /// <returns>
    /// The property value of the specified object.
    /// </returns>
    /// <param name="obj">The object whose property value will be returned.</param>
    [DebuggerHidden]
    [DebuggerStepThrough]
    [__DynamicallyInvokable]
    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    public object GetValue(object obj)
    {
      return this.GetValue(obj, (object[]) null);
    }

    /// <summary>
    /// Returns the property value of a specified object with optional index values for indexed properties.
    /// </summary>
    /// 
    /// <returns>
    /// The property value of the specified object.
    /// </returns>
    /// <param name="obj">The object whose property value will be returned. </param><param name="index">Optional index values for indexed properties. This value should be null for non-indexed properties. </param><exception cref="T:System.ArgumentException">The <paramref name="index"/> array does not contain the type of arguments needed.-or- The property's get accessor is not found. </exception><exception cref="T:System.Reflection.TargetException">The object does not match the target type, or a property is an instance property but <paramref name="obj"/> is null. </exception><exception cref="T:System.Reflection.TargetParameterCountException">The number of parameters in <paramref name="index"/> does not match the number of parameters the indexed property takes. </exception><exception cref="T:System.MethodAccessException">There was an illegal attempt to access a private or protected method inside a class. </exception><exception cref="T:System.Reflection.TargetInvocationException">An error occurred while retrieving the property value. For example, an index value specified for an indexed property is out of range. The <see cref="P:System.Exception.InnerException"/> property indicates the reason for the error.</exception>
    [DebuggerStepThrough]
    [DebuggerHidden]
    [__DynamicallyInvokable]
    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    public virtual object GetValue(object obj, object[] index)
    {
      return this.GetValue(obj, BindingFlags.Default, (Binder) null, index, (CultureInfo) null);
    }

    /// <summary>
    /// When overridden in a derived class, returns the property value of a specified object that has the specified binding, index, and culture-specific information.
    /// </summary>
    /// 
    /// <returns>
    /// The property value of the specified object.
    /// </returns>
    /// <param name="obj">The object whose property value will be returned. </param><param name="invokeAttr">A bitwise combination of the following enumeration members that specify the invocation attribute: InvokeMethod, CreateInstance, Static, GetField, SetField, GetProperty, and SetProperty. You must specify a suitable invocation attribute. For example, to invoke a static member, set the Static flag. </param><param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of <see cref="T:System.Reflection.MemberInfo"/> objects through reflection. If <paramref name="binder"/> is null, the default binder is used. </param><param name="index">Optional index values for indexed properties. This value should be null for non-indexed properties. </param><param name="culture">The culture for which the resource is to be localized. If the resource is not localized for this culture, the <see cref="P:System.Globalization.CultureInfo.Parent"/> property will be called successively in search of a match. If this value is null, the culture-specific information is obtained from the <see cref="P:System.Globalization.CultureInfo.CurrentUICulture"/> property. </param><exception cref="T:System.ArgumentException">The <paramref name="index"/> array does not contain the type of arguments needed.-or- The property's get accessor is not found. </exception><exception cref="T:System.Reflection.TargetException">The object does not match the target type, or a property is an instance property but <paramref name="obj"/> is null. </exception><exception cref="T:System.Reflection.TargetParameterCountException">The number of parameters in <paramref name="index"/> does not match the number of parameters the indexed property takes. </exception><exception cref="T:System.MethodAccessException">There was an illegal attempt to access a private or protected method inside a class. </exception><exception cref="T:System.Reflection.TargetInvocationException">An error occurred while retrieving the property value. For example, an index value specified for an indexed property is out of range. The <see cref="P:System.Exception.InnerException"/> property indicates the reason for the error.</exception>
    public abstract object GetValue(object obj, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture);

    /// <summary>
    /// Sets the property value of a specified object.
    /// </summary>
    /// <param name="obj">The object whose property value will be set.</param><param name="value">The new property value.</param>
    [DebuggerStepThrough]
    [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    [DebuggerHidden]
    [__DynamicallyInvokable]
    public void SetValue(object obj, object value)
    {
      this.SetValue(obj, value, (object[]) null);
    }

    /// <summary>
    /// Sets the property value of a specified object with optional index values for index properties.
    /// </summary>
    /// <param name="obj">The object whose property value will be set. </param><param name="value">The new property value. </param><param name="index">Optional index values for indexed properties. This value should be null for non-indexed properties. </param><exception cref="T:System.ArgumentException">The <paramref name="index"/> array does not contain the type of arguments needed.-or- The property's set accessor is not found. </exception><exception cref="T:System.Reflection.TargetException">The object does not match the target type, or a property is an instance property but <paramref name="obj"/> is null. </exception><exception cref="T:System.Reflection.TargetParameterCountException">The number of parameters in <paramref name="index"/> does not match the number of parameters the indexed property takes. </exception><exception cref="T:System.MethodAccessException">There was an illegal attempt to access a private or protected method inside a class. </exception><exception cref="T:System.Reflection.TargetInvocationException">An error occurred while setting the property value. For example, an index value specified for an indexed property is out of range. The <see cref="P:System.Exception.InnerException"/> property indicates the reason for the error.</exception>
    [DebuggerStepThrough]
    [DebuggerHidden]
    [__DynamicallyInvokable]
    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    public virtual void SetValue(object obj, object value, object[] index)
    {
      this.SetValue(obj, value, BindingFlags.Default, (Binder) null, index, (CultureInfo) null);
    }

    /// <summary>
    /// Returns an array of types representing the required custom modifiers of the property.
    /// </summary>
    /// 
    /// <returns>
    /// An array of <see cref="T:System.Type"/> objects that identify the required custom modifiers of the current property, such as <see cref="T:System.Runtime.CompilerServices.IsConst"/> or <see cref="T:System.Runtime.CompilerServices.IsImplicitlyDereferenced"/>.
    /// </returns>
    public virtual Type[] GetRequiredCustomModifiers()
    {
      return new Type[0];
    }

    /// <summary>
    /// Returns an array of types representing the optional custom modifiers of the property.
    /// </summary>
    /// 
    /// <returns>
    /// An array of <see cref="T:System.Type"/> objects that identify the optional custom modifiers of the current property, such as <see cref="T:System.Runtime.CompilerServices.IsConst"/> or <see cref="T:System.Runtime.CompilerServices.IsImplicitlyDereferenced"/>.
    /// </returns>
    public virtual Type[] GetOptionalCustomModifiers()
    {
      return new Type[0];
    }

    /// <summary>
    /// Returns an array whose elements reflect the public get, set, and other accessors of the property reflected by the current instance.
    /// </summary>
    /// 
    /// <returns>
    /// An array of <see cref="T:System.Reflection.MethodInfo"/> objects that reflect the public get, set, and other accessors of the property reflected by the current instance, if found; otherwise, this method returns an array with zero (0) elements.
    /// </returns>
    [__DynamicallyInvokable]
    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    public MethodInfo[] GetAccessors()
    {
      return this.GetAccessors(false);
    }

    /// <summary>
    /// Returns the public get accessor for this property.
    /// </summary>
    /// 
    /// <returns>
    /// A MethodInfo object representing the public get accessor for this property, or null if the get accessor is non-public or does not exist.
    /// </returns>
    [__DynamicallyInvokable]
    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    public MethodInfo GetGetMethod()
    {
      return this.GetGetMethod(false);
    }

    /// <summary>
    /// Returns the public set accessor for this property.
    /// </summary>
    /// 
    /// <returns>
    /// The MethodInfo object representing the Set method for this property if the set accessor is public, or null if the set accessor is not public.
    /// </returns>
    [__DynamicallyInvokable]
    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    public MethodInfo GetSetMethod()
    {
      return this.GetSetMethod(false);
    }

    Type _PropertyInfo.GetType()
    {
      return this.GetType();
    }

    void _PropertyInfo.GetTypeInfoCount(out uint pcTInfo)
    {
      throw new NotImplementedException();
    }

    void _PropertyInfo.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
    {
      throw new NotImplementedException();
    }

    void _PropertyInfo.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
    {
      throw new NotImplementedException();
    }

    void _PropertyInfo.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
    {
      throw new NotImplementedException();
    }
  }
}
