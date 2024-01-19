// Decompiled with JetBrains decompiler
// Type: System.Collections.ObjectModel.Collection`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 255ABCDF-D9D6-4E3D-BAD4-F74D4CE3D7A8
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Collections.ObjectModel
{
  /// <summary>
  /// Provides the base class for a generic collection.
  /// </summary>
  /// <typeparam name="T">The type of elements in the collection.</typeparam>
  [DebuggerDisplay("Count = {Count}")]
  [ComVisible(false)]
  [DebuggerTypeProxy(typeof (Mscorlib_CollectionDebugView<>))]
  [__DynamicallyInvokable]
  [Serializable]
  public class Collection<T> : IList<T>, ICollection<T>, IList, ICollection, IReadOnlyList<T>, IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable
  {
    private IList<T> items;
    [NonSerialized]
    private object _syncRoot;

    /// <summary>
    /// Gets the number of elements actually contained in the <see cref="T:System.Collections.ObjectModel.Collection`1"/>.
    /// </summary>
    /// 
    /// <returns>
    /// The number of elements actually contained in the <see cref="T:System.Collections.ObjectModel.Collection`1"/>.
    /// </returns>
    [__DynamicallyInvokable]
    public int Count
    {
      [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries"), __DynamicallyInvokable] get
      {
        return this.items.Count;
      }
    }

    /// <summary>
    /// Gets a <see cref="T:System.Collections.Generic.IList`1"/> wrapper around the <see cref="T:System.Collections.ObjectModel.Collection`1"/>.
    /// </summary>
    /// 
    /// <returns>
    /// A <see cref="T:System.Collections.Generic.IList`1"/> wrapper around the <see cref="T:System.Collections.ObjectModel.Collection`1"/>.
    /// </returns>
    [__DynamicallyInvokable]
    protected IList<T> Items
    {
      [__DynamicallyInvokable, TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] get
      {
        return this.items;
      }
    }

    /// <summary>
    /// Gets or sets the element at the specified index.
    /// </summary>
    /// 
    /// <returns>
    /// The element at the specified index.
    /// </returns>
    /// <param name="index">The zero-based index of the element to get or set.</param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is less than zero.-or-<paramref name="index"/> is equal to or greater than <see cref="P:System.Collections.ObjectModel.Collection`1.Count"/>. </exception>
    [__DynamicallyInvokable]
    public T this[int index]
    {
      [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries"), __DynamicallyInvokable] get
      {
        return this.items[index];
      }
      [__DynamicallyInvokable] set
      {
        if (this.items.IsReadOnly)
          ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
        if (index < 0 || index >= this.items.Count)
          ThrowHelper.ThrowArgumentOutOfRangeException();
        this.SetItem(index, value);
      }
    }

    [__DynamicallyInvokable]
    bool ICollection<T>.IsReadOnly
    {
      [__DynamicallyInvokable] get
      {
        return this.items.IsReadOnly;
      }
    }

    [__DynamicallyInvokable]
    bool ICollection.IsSynchronized
    {
      [__DynamicallyInvokable] get
      {
        return false;
      }
    }

    [__DynamicallyInvokable]
    object ICollection.SyncRoot
    {
      [__DynamicallyInvokable] get
      {
        if (this._syncRoot == null)
        {
          ICollection collection = this.items as ICollection;
          if (collection != null)
            this._syncRoot = collection.SyncRoot;
          else
            Interlocked.CompareExchange<object>(ref this._syncRoot, new object(), (object) null);
        }
        return this._syncRoot;
      }
    }

    [__DynamicallyInvokable]
    object IList.this[int index]
    {
      [__DynamicallyInvokable] get
      {
        return (object) this.items[index];
      }
      [__DynamicallyInvokable] set
      {
        ThrowHelper.IfNullAndNullsAreIllegalThenThrow<T>(value, ExceptionArgument.value);
        try
        {
          this[index] = (T) value;
        }
        catch (InvalidCastException ex)
        {
          ThrowHelper.ThrowWrongValueTypeArgumentException(value, typeof (T));
        }
      }
    }

    [__DynamicallyInvokable]
    bool IList.IsReadOnly
    {
      [__DynamicallyInvokable] get
      {
        return this.items.IsReadOnly;
      }
    }

    [__DynamicallyInvokable]
    bool IList.IsFixedSize
    {
      [__DynamicallyInvokable] get
      {
        IList list = this.items as IList;
        if (list != null)
          return list.IsFixedSize;
        return this.items.IsReadOnly;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:System.Collections.ObjectModel.Collection`1"/> class that is empty.
    /// </summary>
    [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    [__DynamicallyInvokable]
    public Collection()
    {
      this.items = (IList<T>) new List<T>();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:System.Collections.ObjectModel.Collection`1"/> class as a wrapper for the specified list.
    /// </summary>
    /// <param name="list">The list that is wrapped by the new collection.</param><exception cref="T:System.ArgumentNullException"><paramref name="list"/> is null.</exception>
    [__DynamicallyInvokable]
    public Collection(IList<T> list)
    {
      if (list == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.list);
      this.items = list;
    }

    /// <summary>
    /// Adds an object to the end of the <see cref="T:System.Collections.ObjectModel.Collection`1"/>.
    /// </summary>
    /// <param name="item">The object to be added to the end of the <see cref="T:System.Collections.ObjectModel.Collection`1"/>. The value can be null for reference types.</param>
    [__DynamicallyInvokable]
    public void Add(T item)
    {
      if (this.items.IsReadOnly)
        ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
      this.InsertItem(this.items.Count, item);
    }

    /// <summary>
    /// Removes all elements from the <see cref="T:System.Collections.ObjectModel.Collection`1"/>.
    /// </summary>
    [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    [__DynamicallyInvokable]
    public void Clear()
    {
      if (this.items.IsReadOnly)
        ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
      this.ClearItems();
    }

    /// <summary>
    /// Copies the entire <see cref="T:System.Collections.ObjectModel.Collection`1"/> to a compatible one-dimensional <see cref="T:System.Array"/>, starting at the specified index of the target array.
    /// </summary>
    /// <param name="array">The one-dimensional <see cref="T:System.Array"/> that is the destination of the elements copied from <see cref="T:System.Collections.ObjectModel.Collection`1"/>. The <see cref="T:System.Array"/> must have zero-based indexing.</param><param name="index">The zero-based index in <paramref name="array"/> at which copying begins.</param><exception cref="T:System.ArgumentNullException"><paramref name="array"/> is null.</exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is less than zero.</exception><exception cref="T:System.ArgumentException">The number of elements in the source <see cref="T:System.Collections.ObjectModel.Collection`1"/> is greater than the available space from <paramref name="index"/> to the end of the destination <paramref name="array"/>.</exception>
    [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    [__DynamicallyInvokable]
    public void CopyTo(T[] array, int index)
    {
      this.items.CopyTo(array, index);
    }

    /// <summary>
    /// Determines whether an element is in the <see cref="T:System.Collections.ObjectModel.Collection`1"/>.
    /// </summary>
    /// 
    /// <returns>
    /// true if <paramref name="item"/> is found in the <see cref="T:System.Collections.ObjectModel.Collection`1"/>; otherwise, false.
    /// </returns>
    /// <param name="item">The object to locate in the <see cref="T:System.Collections.ObjectModel.Collection`1"/>. The value can be null for reference types.</param>
    [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    [__DynamicallyInvokable]
    public bool Contains(T item)
    {
      return this.items.Contains(item);
    }

    /// <summary>
    /// Returns an enumerator that iterates through the <see cref="T:System.Collections.ObjectModel.Collection`1"/>.
    /// </summary>
    /// 
    /// <returns>
    /// An <see cref="T:System.Collections.Generic.IEnumerator`1"/> for the <see cref="T:System.Collections.ObjectModel.Collection`1"/>.
    /// </returns>
    [__DynamicallyInvokable]
    public IEnumerator<T> GetEnumerator()
    {
      return this.items.GetEnumerator();
    }

    /// <summary>
    /// Searches for the specified object and returns the zero-based index of the first occurrence within the entire <see cref="T:System.Collections.ObjectModel.Collection`1"/>.
    /// </summary>
    /// 
    /// <returns>
    /// The zero-based index of the first occurrence of <paramref name="item"/> within the entire <see cref="T:System.Collections.ObjectModel.Collection`1"/>, if found; otherwise, -1.
    /// </returns>
    /// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.List`1"/>. The value can be null for reference types.</param>
    [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    [__DynamicallyInvokable]
    public int IndexOf(T item)
    {
      return this.items.IndexOf(item);
    }

    /// <summary>
    /// Inserts an element into the <see cref="T:System.Collections.ObjectModel.Collection`1"/> at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index at which <paramref name="item"/> should be inserted.</param><param name="item">The object to insert. The value can be null for reference types.</param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is less than zero.-or-<paramref name="index"/> is greater than <see cref="P:System.Collections.ObjectModel.Collection`1.Count"/>.</exception>
    [__DynamicallyInvokable]
    public void Insert(int index, T item)
    {
      if (this.items.IsReadOnly)
        ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
      if (index < 0 || index > this.items.Count)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_ListInsert);
      this.InsertItem(index, item);
    }

    /// <summary>
    /// Removes the first occurrence of a specific object from the <see cref="T:System.Collections.ObjectModel.Collection`1"/>.
    /// </summary>
    /// 
    /// <returns>
    /// true if <paramref name="item"/> is successfully removed; otherwise, false.  This method also returns false if <paramref name="item"/> was not found in the original <see cref="T:System.Collections.ObjectModel.Collection`1"/>.
    /// </returns>
    /// <param name="item">The object to remove from the <see cref="T:System.Collections.ObjectModel.Collection`1"/>. The value can be null for reference types.</param>
    [__DynamicallyInvokable]
    public bool Remove(T item)
    {
      if (this.items.IsReadOnly)
        ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
      int index = this.items.IndexOf(item);
      if (index < 0)
        return false;
      this.RemoveItem(index);
      return true;
    }

    /// <summary>
    /// Removes the element at the specified index of the <see cref="T:System.Collections.ObjectModel.Collection`1"/>.
    /// </summary>
    /// <param name="index">The zero-based index of the element to remove.</param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is less than zero.-or-<paramref name="index"/> is equal to or greater than <see cref="P:System.Collections.ObjectModel.Collection`1.Count"/>.</exception>
    [__DynamicallyInvokable]
    public void RemoveAt(int index)
    {
      if (this.items.IsReadOnly)
        ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
      if (index < 0 || index >= this.items.Count)
        ThrowHelper.ThrowArgumentOutOfRangeException();
      this.RemoveItem(index);
    }

    /// <summary>
    /// Removes all elements from the <see cref="T:System.Collections.ObjectModel.Collection`1"/>.
    /// </summary>
    [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    [__DynamicallyInvokable]
    protected virtual void ClearItems()
    {
      this.items.Clear();
    }

    /// <summary>
    /// Inserts an element into the <see cref="T:System.Collections.ObjectModel.Collection`1"/> at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index at which <paramref name="item"/> should be inserted.</param><param name="item">The object to insert. The value can be null for reference types.</param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is less than zero.-or-<paramref name="index"/> is greater than <see cref="P:System.Collections.ObjectModel.Collection`1.Count"/>.</exception>
    [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    [__DynamicallyInvokable]
    protected virtual void InsertItem(int index, T item)
    {
      this.items.Insert(index, item);
    }

    /// <summary>
    /// Removes the element at the specified index of the <see cref="T:System.Collections.ObjectModel.Collection`1"/>.
    /// </summary>
    /// <param name="index">The zero-based index of the element to remove.</param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is less than zero.-or-<paramref name="index"/> is equal to or greater than <see cref="P:System.Collections.ObjectModel.Collection`1.Count"/>.</exception>
    [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    [__DynamicallyInvokable]
    protected virtual void RemoveItem(int index)
    {
      this.items.RemoveAt(index);
    }

    /// <summary>
    /// Replaces the element at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index of the element to replace.</param><param name="item">The new value for the element at the specified index. The value can be null for reference types.</param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is less than zero.-or-<paramref name="index"/> is greater than <see cref="P:System.Collections.ObjectModel.Collection`1.Count"/>.</exception>
    [__DynamicallyInvokable]
    protected virtual void SetItem(int index, T item)
    {
      this.items[index] = item;
    }

    [__DynamicallyInvokable]
    IEnumerator IEnumerable.GetEnumerator()
    {
      return this.items.GetEnumerator();
    }

    [__DynamicallyInvokable]
    void ICollection.CopyTo(Array array, int index)
    {
      if (array == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
      if (array.Rank != 1)
        ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RankMultiDimNotSupported);
      if (array.GetLowerBound(0) != 0)
        ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_NonZeroLowerBound);
      if (index < 0)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
      if (array.Length - index < this.Count)
        ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
      T[] array1 = array as T[];
      if (array1 != null)
      {
        this.items.CopyTo(array1, index);
      }
      else
      {
        Type elementType = array.GetType().GetElementType();
        Type c = typeof (T);
        if (!elementType.IsAssignableFrom(c) && !c.IsAssignableFrom(elementType))
          ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
        object[] objArray = array as object[];
        if (objArray == null)
          ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
        int count = this.items.Count;
        try
        {
          for (int index1 = 0; index1 < count; ++index1)
            objArray[index++] = (object) this.items[index1];
        }
        catch (ArrayTypeMismatchException ex)
        {
          ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
        }
      }
    }

    [__DynamicallyInvokable]
    int IList.Add(object value)
    {
      if (this.items.IsReadOnly)
        ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
      ThrowHelper.IfNullAndNullsAreIllegalThenThrow<T>(value, ExceptionArgument.value);
      try
      {
        this.Add((T) value);
      }
      catch (InvalidCastException ex)
      {
        ThrowHelper.ThrowWrongValueTypeArgumentException(value, typeof (T));
      }
      return this.Count - 1;
    }

    [__DynamicallyInvokable]
    bool IList.Contains(object value)
    {
      if (Collection<T>.IsCompatibleObject(value))
        return this.Contains((T) value);
      return false;
    }

    [__DynamicallyInvokable]
    int IList.IndexOf(object value)
    {
      if (Collection<T>.IsCompatibleObject(value))
        return this.IndexOf((T) value);
      return -1;
    }

    [__DynamicallyInvokable]
    void IList.Insert(int index, object value)
    {
      if (this.items.IsReadOnly)
        ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
      ThrowHelper.IfNullAndNullsAreIllegalThenThrow<T>(value, ExceptionArgument.value);
      try
      {
        this.Insert(index, (T) value);
      }
      catch (InvalidCastException ex)
      {
        ThrowHelper.ThrowWrongValueTypeArgumentException(value, typeof (T));
      }
    }

    [__DynamicallyInvokable]
    void IList.Remove(object value)
    {
      if (this.items.IsReadOnly)
        ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
      if (!Collection<T>.IsCompatibleObject(value))
        return;
      this.Remove((T) value);
    }

    private static bool IsCompatibleObject(object value)
    {
      if (value is T)
        return true;
      if (value == null)
        return (object) default (T) == null;
      return false;
    }
  }
}
