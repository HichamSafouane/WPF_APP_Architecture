// Decompiled with JetBrains decompiler
// Type: System.IO.File
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 255ABCDF-D9D6-4E3D-BAD4-F74D4CE3D7A8
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.AccessControl;
using System.Security.Permissions;
using System.Text;

namespace System.IO
{
  /// <summary>
  /// Provides static methods for the creation, copying, deletion, moving, and opening of files, and aids in the creation of <see cref="T:System.IO.FileStream"/> objects.
  /// </summary>
  /// <filterpriority>1</filterpriority>
  [ComVisible(true)]
  public static class File
  {
    private const int GetFileExInfoStandard = 0;
    private const int ERROR_INVALID_PARAMETER = 87;
    private const int ERROR_ACCESS_DENIED = 5;

    /// <summary>
    /// Opens an existing UTF-8 encoded text file for reading.
    /// </summary>
    /// 
    /// <returns>
    /// A <see cref="T:System.IO.StreamReader"/> on the specified path.
    /// </returns>
    /// <param name="path">The file to be opened for reading. </param><exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception><exception cref="T:System.ArgumentException"><paramref name="path"/> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars"/>. </exception><exception cref="T:System.ArgumentNullException"><paramref name="path"/> is null. </exception><exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception><exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, (for example, it is on an unmapped drive). </exception><exception cref="T:System.IO.FileNotFoundException">The file specified in <paramref name="path"/> was not found. </exception><exception cref="T:System.NotSupportedException"><paramref name="path"/> is in an invalid format. </exception><filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
    public static StreamReader OpenText(string path)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      return new StreamReader(path);
    }

    /// <summary>
    /// Creates or opens a file for writing UTF-8 encoded text.
    /// </summary>
    /// 
    /// <returns>
    /// A <see cref="T:System.IO.StreamWriter"/> that writes to the specified file using UTF-8 encoding.
    /// </returns>
    /// <param name="path">The file to be opened for writing. </param><exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception><exception cref="T:System.ArgumentException"><paramref name="path"/> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars"/>. </exception><exception cref="T:System.ArgumentNullException"><paramref name="path"/> is null. </exception><exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception><exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception><exception cref="T:System.NotSupportedException"><paramref name="path"/> is in an invalid format. </exception><filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
    public static StreamWriter CreateText(string path)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      return new StreamWriter(path, false);
    }

    /// <summary>
    /// Creates a <see cref="T:System.IO.StreamWriter"/> that appends UTF-8 encoded text to an existing file.
    /// </summary>
    /// 
    /// <returns>
    /// A StreamWriter that appends UTF-8 encoded text to an existing file.
    /// </returns>
    /// <param name="path">The path to the file to append to. </param><exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception><exception cref="T:System.ArgumentException"><paramref name="path"/> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars"/>. </exception><exception cref="T:System.ArgumentNullException"><paramref name="path"/> is null. </exception><exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception><exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception><exception cref="T:System.NotSupportedException"><paramref name="path"/> is in an invalid format. </exception><filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
    public static StreamWriter AppendText(string path)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      return new StreamWriter(path, true);
    }

    /// <summary>
    /// Copies an existing file to a new file. Overwriting a file of the same name is not allowed.
    /// </summary>
    /// <param name="sourceFileName">The file to copy. </param><param name="destFileName">The name of the destination file. This cannot be a directory or an existing file. </param><exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception><exception cref="T:System.ArgumentException"><paramref name="sourceFileName"/> or <paramref name="destFileName"/> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars"/>.-or- <paramref name="sourceFileName"/> or <paramref name="destFileName"/> specifies a directory. </exception><exception cref="T:System.ArgumentNullException"><paramref name="sourceFileName"/> or <paramref name="destFileName"/> is null. </exception><exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception><exception cref="T:System.IO.DirectoryNotFoundException">The path specified in <paramref name="sourceFileName"/> or <paramref name="destFileName"/> is invalid (for example, it is on an unmapped drive). </exception><exception cref="T:System.IO.FileNotFoundException"><paramref name="sourceFileName"/> was not found. </exception><exception cref="T:System.IO.IOException"><paramref name="destFileName"/> exists.-or- An I/O error has occurred. </exception><exception cref="T:System.NotSupportedException"><paramref name="sourceFileName"/> or <paramref name="destFileName"/> is in an invalid format. </exception><filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
    public static void Copy(string sourceFileName, string destFileName)
    {
      if (sourceFileName == null)
        throw new ArgumentNullException("sourceFileName", Environment.GetResourceString("ArgumentNull_FileName"));
      if (destFileName == null)
        throw new ArgumentNullException("destFileName", Environment.GetResourceString("ArgumentNull_FileName"));
      if (sourceFileName.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyFileName"), "sourceFileName");
      if (destFileName.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyFileName"), "destFileName");
      File.InternalCopy(sourceFileName, destFileName, false, true);
    }

    /// <summary>
    /// Copies an existing file to a new file. Overwriting a file of the same name is allowed.
    /// </summary>
    /// <param name="sourceFileName">The file to copy. </param><param name="destFileName">The name of the destination file. This cannot be a directory. </param><param name="overwrite">true if the destination file can be overwritten; otherwise, false. </param><exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. -or-<paramref name="destFileName"/> is read-only.</exception><exception cref="T:System.ArgumentException"><paramref name="sourceFileName"/> or <paramref name="destFileName"/> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars"/>.-or- <paramref name="sourceFileName"/> or <paramref name="destFileName"/> specifies a directory. </exception><exception cref="T:System.ArgumentNullException"><paramref name="sourceFileName"/> or <paramref name="destFileName"/> is null. </exception><exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception><exception cref="T:System.IO.DirectoryNotFoundException">The path specified in <paramref name="sourceFileName"/> or <paramref name="destFileName"/> is invalid (for example, it is on an unmapped drive). </exception><exception cref="T:System.IO.FileNotFoundException"><paramref name="sourceFileName"/> was not found. </exception><exception cref="T:System.IO.IOException"><paramref name="destFileName"/> exists and <paramref name="overwrite"/> is false.-or- An I/O error has occurred. </exception><exception cref="T:System.NotSupportedException"><paramref name="sourceFileName"/> or <paramref name="destFileName"/> is in an invalid format. </exception><filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
    public static void Copy(string sourceFileName, string destFileName, bool overwrite)
    {
      if (sourceFileName == null)
        throw new ArgumentNullException("sourceFileName", Environment.GetResourceString("ArgumentNull_FileName"));
      if (destFileName == null)
        throw new ArgumentNullException("destFileName", Environment.GetResourceString("ArgumentNull_FileName"));
      if (sourceFileName.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyFileName"), "sourceFileName");
      if (destFileName.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyFileName"), "destFileName");
      File.InternalCopy(sourceFileName, destFileName, overwrite, true);
    }

    [SecurityCritical]
    internal static void UnsafeCopy(string sourceFileName, string destFileName, bool overwrite)
    {
      if (sourceFileName == null)
        throw new ArgumentNullException("sourceFileName", Environment.GetResourceString("ArgumentNull_FileName"));
      if (destFileName == null)
        throw new ArgumentNullException("destFileName", Environment.GetResourceString("ArgumentNull_FileName"));
      if (sourceFileName.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyFileName"), "sourceFileName");
      if (destFileName.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyFileName"), "destFileName");
      File.InternalCopy(sourceFileName, destFileName, overwrite, false);
    }

    [SecuritySafeCritical]
    internal static string InternalCopy(string sourceFileName, string destFileName, bool overwrite, bool checkHost)
    {
      string fullPathInternal1 = Path.GetFullPathInternal(sourceFileName);
      string fullPathInternal2 = Path.GetFullPathInternal(destFileName);
      new FileIOPermission(FileIOPermissionAccess.Read, new string[1]
      {
        fullPathInternal1
      }, 0 != 0, 0 != 0).Demand();
      new FileIOPermission(FileIOPermissionAccess.Write, new string[1]
      {
        fullPathInternal2
      }, 0 != 0, 0 != 0).Demand();
      if (!Win32Native.CopyFile(fullPathInternal1, fullPathInternal2, !overwrite))
      {
        int lastWin32Error = Marshal.GetLastWin32Error();
        string maybeFullPath = destFileName;
        if (lastWin32Error != 80)
        {
          using (SafeFileHandle file = Win32Native.UnsafeCreateFile(fullPathInternal1, int.MinValue, FileShare.Read, (Win32Native.SECURITY_ATTRIBUTES) null, FileMode.Open, 0, IntPtr.Zero))
          {
            if (file.IsInvalid)
              maybeFullPath = sourceFileName;
          }
          if (lastWin32Error == 5 && Directory.InternalExists(fullPathInternal2))
            throw new IOException(Environment.GetResourceString("Arg_FileIsDirectory_Name", (object) destFileName), 5, fullPathInternal2);
        }
        __Error.WinIOError(lastWin32Error, maybeFullPath);
      }
      return fullPathInternal2;
    }

    /// <summary>
    /// Creates or overwrites a file in the specified path.
    /// </summary>
    /// 
    /// <returns>
    /// A <see cref="T:System.IO.FileStream"/> that provides read/write access to the file specified in <paramref name="path"/>.
    /// </returns>
    /// <param name="path">The path and name of the file to create. </param><exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.-or- <paramref name="path"/> specified a file that is read-only. </exception><exception cref="T:System.ArgumentException"><paramref name="path"/> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars"/>. </exception><exception cref="T:System.ArgumentNullException"><paramref name="path"/> is null. </exception><exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception><exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception><exception cref="T:System.IO.IOException">An I/O error occurred while creating the file. </exception><exception cref="T:System.NotSupportedException"><paramref name="path"/> is in an invalid format. </exception><filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    public static FileStream Create(string path)
    {
      return File.Create(path, 4096);
    }

    /// <summary>
    /// Creates or overwrites the specified file.
    /// </summary>
    /// 
    /// <returns>
    /// A <see cref="T:System.IO.FileStream"/> with the specified buffer size that provides read/write access to the file specified in <paramref name="path"/>.
    /// </returns>
    /// <param name="path">The name of the file. </param><param name="bufferSize">The number of bytes buffered for reads and writes to the file. </param><exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.-or- <paramref name="path"/> specified a file that is read-only. </exception><exception cref="T:System.ArgumentException"><paramref name="path"/> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars"/>. </exception><exception cref="T:System.ArgumentNullException"><paramref name="path"/> is null. </exception><exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception><exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception><exception cref="T:System.IO.IOException">An I/O error occurred while creating the file. </exception><exception cref="T:System.NotSupportedException"><paramref name="path"/> is in an invalid format. </exception><filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
    public static FileStream Create(string path, int bufferSize)
    {
      return new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.None, bufferSize);
    }

    /// <summary>
    /// Creates or overwrites the specified file, specifying a buffer size and a <see cref="T:System.IO.FileOptions"/> value that describes how to create or overwrite the file.
    /// </summary>
    /// 
    /// <returns>
    /// A new file with the specified buffer size.
    /// </returns>
    /// <param name="path">The name of the file. </param><param name="bufferSize">The number of bytes buffered for reads and writes to the file. </param><param name="options">One of the <see cref="T:System.IO.FileOptions"/> values that describes how to create or overwrite the file.</param><exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.-or- <paramref name="path"/> specified a file that is read-only. -or-<see cref="F:System.IO.FileOptions.Encrypted"/> is specified for <paramref name="options"/> and file encryption is not supported on the current platform.</exception><exception cref="T:System.ArgumentException"><paramref name="path"/> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars"/>. </exception><exception cref="T:System.ArgumentNullException"><paramref name="path"/> is null. </exception><exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception><exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive. </exception><exception cref="T:System.IO.IOException">An I/O error occurred while creating the file. </exception><exception cref="T:System.NotSupportedException"><paramref name="path"/> is in an invalid format. </exception><exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.-or- <paramref name="path"/> specified a file that is read-only. </exception><exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.-or- <paramref name="path"/> specified a file that is read-only. </exception>
    public static FileStream Create(string path, int bufferSize, FileOptions options)
    {
      return new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.None, bufferSize, options);
    }

    /// <summary>
    /// Creates or overwrites the specified file with the specified buffer size, file options, and file security.
    /// </summary>
    /// 
    /// <returns>
    /// A new file with the specified buffer size, file options, and file security.
    /// </returns>
    /// <param name="path">The name of the file. </param><param name="bufferSize">The number of bytes buffered for reads and writes to the file. </param><param name="options">One of the <see cref="T:System.IO.FileOptions"/> values that describes how to create or overwrite the file.</param><param name="fileSecurity">One of the <see cref="T:System.Security.AccessControl.FileSecurity"/> values that determines the access control and audit security for the file.</param><exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.-or- <paramref name="path"/> specified a file that is read-only.-or-<see cref="F:System.IO.FileOptions.Encrypted"/> is specified for <paramref name="options"/> and file encryption is not supported on the current platform. </exception><exception cref="T:System.ArgumentException"><paramref name="path"/> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars"/>. </exception><exception cref="T:System.ArgumentNullException"><paramref name="path"/> is null. </exception><exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception><exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception><exception cref="T:System.IO.IOException">An I/O error occurred while creating the file. </exception><exception cref="T:System.NotSupportedException"><paramref name="path"/> is in an invalid format. </exception><exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.-or- <paramref name="path"/> specified a file that is read-only. </exception><exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.-or- <paramref name="path"/> specified a file that is read-only. </exception>
    public static FileStream Create(string path, int bufferSize, FileOptions options, FileSecurity fileSecurity)
    {
      return new FileStream(path, FileMode.Create, FileSystemRights.Read | FileSystemRights.Write, FileShare.None, bufferSize, options, fileSecurity);
    }

    /// <summary>
    /// Deletes the specified file.
    /// </summary>
    /// <param name="path">The name of the file to be deleted. Wildcard characters are not supported.</param><exception cref="T:System.ArgumentException"><paramref name="path"/> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars"/>. </exception><exception cref="T:System.ArgumentNullException"><paramref name="path"/> is null. </exception><exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception><exception cref="T:System.IO.IOException">The specified file is in use. -or-There is an open handle on the file, and the operating system is Windows XP or earlier. This open handle can result from enumerating directories and files. For more information, see How to: Enumerate Directories and Files.</exception><exception cref="T:System.NotSupportedException"><paramref name="path"/> is in an invalid format. </exception><exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception><exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.-or- <paramref name="path"/> is a directory.-or- <paramref name="path"/> specified a read-only file. </exception><filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
    [SecuritySafeCritical]
    public static void Delete(string path)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      File.InternalDelete(path, true);
    }

    [SecurityCritical]
    internal static void UnsafeDelete(string path)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      File.InternalDelete(path, false);
    }

    [SecurityCritical]
    internal static void InternalDelete(string path, bool checkHost)
    {
      string fullPathInternal = Path.GetFullPathInternal(path);
      new FileIOPermission(FileIOPermissionAccess.Write, new string[1]
      {
        fullPathInternal
      }, 0 != 0, 0 != 0).Demand();
      if (Win32Native.DeleteFile(fullPathInternal))
        return;
      int lastWin32Error = Marshal.GetLastWin32Error();
      if (lastWin32Error == 2)
        return;
      __Error.WinIOError(lastWin32Error, fullPathInternal);
    }

    /// <summary>
    /// Decrypts a file that was encrypted by the current account using the <see cref="M:System.IO.File.Encrypt(System.String)"/> method.
    /// </summary>
    /// <param name="path">A path that describes a file to decrypt.</param><exception cref="T:System.ArgumentException">The <paramref name="path"/> parameter is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars"/>.</exception><exception cref="T:System.ArgumentNullException">The <paramref name="path"/> parameter is null.</exception><exception cref="T:System.IO.DriveNotFoundException">An invalid drive was specified. </exception><exception cref="T:System.IO.FileNotFoundException">The file described by the <paramref name="path"/> parameter could not be found.</exception><exception cref="T:System.IO.IOException">An I/O error occurred while opening the file. For example, the encrypted file is already open. -or-This operation is not supported on the current platform.</exception><exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception><exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception><exception cref="T:System.NotSupportedException">The file system is not NTFS.</exception><exception cref="T:System.UnauthorizedAccessException">The <paramref name="path"/> parameter specified a file that is read-only.-or- This operation is not supported on the current platform.-or- The <paramref name="path"/> parameter specified a directory.-or- The caller does not have the required permission.</exception><filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
    [SecuritySafeCritical]
    public static void Decrypt(string path)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      string fullPathInternal = Path.GetFullPathInternal(path);
      new FileIOPermission(FileIOPermissionAccess.Read | FileIOPermissionAccess.Write, new string[1]
      {
        fullPathInternal
      }, 0 != 0, 0 != 0).Demand();
      if (Win32Native.DecryptFile(fullPathInternal, 0))
        return;
      int lastWin32Error = Marshal.GetLastWin32Error();
      if (lastWin32Error == 5 && !string.Equals("NTFS", new DriveInfo(Path.GetPathRoot(fullPathInternal)).DriveFormat))
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_EncryptionNeedsNTFS"));
      __Error.WinIOError(lastWin32Error, fullPathInternal);
    }

    /// <summary>
    /// Encrypts a file so that only the account used to encrypt the file can decrypt it.
    /// </summary>
    /// <param name="path">A path that describes a file to encrypt.</param><exception cref="T:System.ArgumentException">The <paramref name="path"/> parameter is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars"/>.</exception><exception cref="T:System.ArgumentNullException">The <paramref name="path"/> parameter is null.</exception><exception cref="T:System.IO.DriveNotFoundException">An invalid drive was specified. </exception><exception cref="T:System.IO.FileNotFoundException">The file described by the <paramref name="path"/> parameter could not be found.</exception><exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.-or-This operation is not supported on the current platform.</exception><exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception><exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception><exception cref="T:System.NotSupportedException">The file system is not NTFS.</exception><exception cref="T:System.UnauthorizedAccessException">The <paramref name="path"/> parameter specified a file that is read-only.-or- This operation is not supported on the current platform.-or- The <paramref name="path"/> parameter specified a directory.-or- The caller does not have the required permission.</exception><filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
    [SecuritySafeCritical]
    public static void Encrypt(string path)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      string fullPathInternal = Path.GetFullPathInternal(path);
      new FileIOPermission(FileIOPermissionAccess.Read | FileIOPermissionAccess.Write, new string[1]
      {
        fullPathInternal
      }, 0 != 0, 0 != 0).Demand();
      if (Win32Native.EncryptFile(fullPathInternal))
        return;
      int lastWin32Error = Marshal.GetLastWin32Error();
      if (lastWin32Error == 5 && !string.Equals("NTFS", new DriveInfo(Path.GetPathRoot(fullPathInternal)).DriveFormat))
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_EncryptionNeedsNTFS"));
      __Error.WinIOError(lastWin32Error, fullPathInternal);
    }

    /// <summary>
    /// Determines whether the specified file exists.
    /// </summary>
    /// 
    /// <returns>
    /// true if the caller has the required permissions and <paramref name="path"/> contains the name of an existing file; otherwise, false. This method also returns false if <paramref name="path"/> is null, an invalid path, or a zero-length string. If the caller does not have sufficient permissions to read the specified file, no exception is thrown and the method returns false regardless of the existence of <paramref name="path"/>.
    /// </returns>
    /// <param name="path">The file to check. </param><filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
    [SecuritySafeCritical]
    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    public static bool Exists(string path)
    {
      return File.InternalExistsHelper(path, true);
    }

    [SecurityCritical]
    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    internal static bool UnsafeExists(string path)
    {
      return File.InternalExistsHelper(path, false);
    }

    [SecurityCritical]
    private static bool InternalExistsHelper(string path, bool checkHost)
    {
      try
      {
        if (path == null || path.Length == 0)
          return false;
        path = Path.GetFullPathInternal(path);
        if (path.Length > 0 && Path.IsDirectorySeparator(path[path.Length - 1]))
          return false;
        new FileIOPermission(FileIOPermissionAccess.Read, new string[1]
        {
          path
        }, 0 != 0, 0 != 0).Demand();
        return File.InternalExists(path);
      }
      catch (ArgumentException ex)
      {
      }
      catch (NotSupportedException ex)
      {
      }
      catch (SecurityException ex)
      {
      }
      catch (IOException ex)
      {
      }
      catch (UnauthorizedAccessException ex)
      {
      }
      return false;
    }

    [SecurityCritical]
    internal static bool InternalExists(string path)
    {
      Win32Native.WIN32_FILE_ATTRIBUTE_DATA data = new Win32Native.WIN32_FILE_ATTRIBUTE_DATA();
      if (File.FillAttributeInfo(path, ref data, false, true) == 0 && data.fileAttributes != -1)
        return (data.fileAttributes & 16) == 0;
      return false;
    }

    /// <summary>
    /// Opens a <see cref="T:System.IO.FileStream"/> on the specified path with read/write access.
    /// </summary>
    /// 
    /// <returns>
    /// A <see cref="T:System.IO.FileStream"/> opened in the specified mode and path, with read/write access and not shared.
    /// </returns>
    /// <param name="path">The file to open. </param><param name="mode">A <see cref="T:System.IO.FileMode"/> value that specifies whether a file is created if one does not exist, and determines whether the contents of existing files are retained or overwritten. </param><exception cref="T:System.ArgumentException"><paramref name="path"/> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars"/>. </exception><exception cref="T:System.ArgumentNullException"><paramref name="path"/> is null. </exception><exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception><exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, (for example, it is on an unmapped drive). </exception><exception cref="T:System.IO.IOException">An I/O error occurred while opening the file. </exception><exception cref="T:System.UnauthorizedAccessException"><paramref name="path"/> specified a file that is read-only.-or- This operation is not supported on the current platform.-or- <paramref name="path"/> specified a directory.-or- The caller does not have the required permission. -or-<paramref name="mode"/> is <see cref="F:System.IO.FileMode.Create"/> and the specified file is a hidden file.</exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="mode"/> specified an invalid value. </exception><exception cref="T:System.IO.FileNotFoundException">The file specified in <paramref name="path"/> was not found. </exception><exception cref="T:System.NotSupportedException"><paramref name="path"/> is in an invalid format. </exception><filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
    public static FileStream Open(string path, FileMode mode)
    {
      return File.Open(path, mode, mode == FileMode.Append ? FileAccess.Write : FileAccess.ReadWrite, FileShare.None);
    }

    /// <summary>
    /// Opens a <see cref="T:System.IO.FileStream"/> on the specified path, with the specified mode and access.
    /// </summary>
    /// 
    /// <returns>
    /// An unshared <see cref="T:System.IO.FileStream"/> that provides access to the specified file, with the specified mode and access.
    /// </returns>
    /// <param name="path">The file to open. </param><param name="mode">A <see cref="T:System.IO.FileMode"/> value that specifies whether a file is created if one does not exist, and determines whether the contents of existing files are retained or overwritten. </param><param name="access">A <see cref="T:System.IO.FileAccess"/> value that specifies the operations that can be performed on the file. </param><exception cref="T:System.ArgumentException"><paramref name="path"/> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars"/>.-or- <paramref name="access"/> specified Read and <paramref name="mode"/> specified Create, CreateNew, Truncate, or Append. </exception><exception cref="T:System.ArgumentNullException"><paramref name="path"/> is null. </exception><exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception><exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, (for example, it is on an unmapped drive). </exception><exception cref="T:System.IO.IOException">An I/O error occurred while opening the file. </exception><exception cref="T:System.UnauthorizedAccessException"><paramref name="path"/> specified a file that is read-only and <paramref name="access"/> is not Read.-or- <paramref name="path"/> specified a directory.-or- The caller does not have the required permission. -or-<paramref name="mode"/> is <see cref="F:System.IO.FileMode.Create"/> and the specified file is a hidden file.</exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="mode"/> or <paramref name="access"/> specified an invalid value. </exception><exception cref="T:System.IO.FileNotFoundException">The file specified in <paramref name="path"/> was not found. </exception><exception cref="T:System.NotSupportedException"><paramref name="path"/> is in an invalid format. </exception><filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    public static FileStream Open(string path, FileMode mode, FileAccess access)
    {
      return File.Open(path, mode, access, FileShare.None);
    }

    /// <summary>
    /// Opens a <see cref="T:System.IO.FileStream"/> on the specified path, having the specified mode with read, write, or read/write access and the specified sharing option.
    /// </summary>
    /// 
    /// <returns>
    /// A <see cref="T:System.IO.FileStream"/> on the specified path, having the specified mode with read, write, or read/write access and the specified sharing option.
    /// </returns>
    /// <param name="path">The file to open. </param><param name="mode">A <see cref="T:System.IO.FileMode"/> value that specifies whether a file is created if one does not exist, and determines whether the contents of existing files are retained or overwritten. </param><param name="access">A <see cref="T:System.IO.FileAccess"/> value that specifies the operations that can be performed on the file. </param><param name="share">A <see cref="T:System.IO.FileShare"/> value specifying the type of access other threads have to the file. </param><exception cref="T:System.ArgumentException"><paramref name="path"/> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars"/>.-or- <paramref name="access"/> specified Read and <paramref name="mode"/> specified Create, CreateNew, Truncate, or Append. </exception><exception cref="T:System.ArgumentNullException"><paramref name="path"/> is null. </exception><exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception><exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, (for example, it is on an unmapped drive). </exception><exception cref="T:System.IO.IOException">An I/O error occurred while opening the file. </exception><exception cref="T:System.UnauthorizedAccessException"><paramref name="path"/> specified a file that is read-only and <paramref name="access"/> is not Read.-or- <paramref name="path"/> specified a directory.-or- The caller does not have the required permission. -or-<paramref name="mode"/> is <see cref="F:System.IO.FileMode.Create"/> and the specified file is a hidden file.</exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="mode"/>, <paramref name="access"/>, or <paramref name="share"/> specified an invalid value. </exception><exception cref="T:System.IO.FileNotFoundException">The file specified in <paramref name="path"/> was not found. </exception><exception cref="T:System.NotSupportedException"><paramref name="path"/> is in an invalid format. </exception><filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
    public static FileStream Open(string path, FileMode mode, FileAccess access, FileShare share)
    {
      return new FileStream(path, mode, access, share);
    }

    /// <summary>
    /// Sets the date and time the file was created.
    /// </summary>
    /// <param name="path">The file for which to set the creation date and time information. </param><param name="creationTime">A <see cref="T:System.DateTime"/> containing the value to set for the creation date and time of <paramref name="path"/>. This value is expressed in local time. </param><exception cref="T:System.IO.FileNotFoundException">The specified path was not found. </exception><exception cref="T:System.ArgumentException"><paramref name="path"/> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars"/>. </exception><exception cref="T:System.ArgumentNullException"><paramref name="path"/> is null. </exception><exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception><exception cref="T:System.IO.IOException">An I/O error occurred while performing the operation. </exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="creationTime"/> specifies a value outside the range of dates, times, or both permitted for this operation. </exception><exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception><exception cref="T:System.NotSupportedException"><paramref name="path"/> is in an invalid format. </exception><filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
    public static void SetCreationTime(string path, DateTime creationTime)
    {
      File.SetCreationTimeUtc(path, creationTime.ToUniversalTime());
    }

    /// <summary>
    /// Sets the date and time, in coordinated universal time (UTC), that the file was created.
    /// </summary>
    /// <param name="path">The file for which to set the creation date and time information. </param><param name="creationTimeUtc">A <see cref="T:System.DateTime"/> containing the value to set for the creation date and time of <paramref name="path"/>. This value is expressed in UTC time. </param><exception cref="T:System.IO.FileNotFoundException">The specified path was not found. </exception><exception cref="T:System.ArgumentException"><paramref name="path"/> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars"/>. </exception><exception cref="T:System.ArgumentNullException"><paramref name="path"/> is null. </exception><exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception><exception cref="T:System.IO.IOException">An I/O error occurred while performing the operation. </exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="creationTime"/> specifies a value outside the range of dates, times, or both permitted for this operation. </exception><exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception><exception cref="T:System.NotSupportedException"><paramref name="path"/> is in an invalid format. </exception><filterpriority>2</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
    [SecuritySafeCritical]
    public static unsafe void SetCreationTimeUtc(string path, DateTime creationTimeUtc)
    {
      SafeFileHandle handle;
      using (File.OpenFile(path, FileAccess.Write, out handle))
      {
        Win32Native.FILE_TIME fileTime = new Win32Native.FILE_TIME(creationTimeUtc.ToFileTimeUtc());
        if (Win32Native.SetFileTime(handle, &fileTime, (Win32Native.FILE_TIME*) null, (Win32Native.FILE_TIME*) null))
          return;
        __Error.WinIOError(Marshal.GetLastWin32Error(), path);
      }
    }

    /// <summary>
    /// Returns the creation date and time of the specified file or directory.
    /// </summary>
    /// 
    /// <returns>
    /// A <see cref="T:System.DateTime"/> structure set to the creation date and time for the specified file or directory. This value is expressed in local time.
    /// </returns>
    /// <param name="path">The file or directory for which to obtain creation date and time information. </param><exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception><exception cref="T:System.ArgumentException"><paramref name="path"/> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars"/>. </exception><exception cref="T:System.ArgumentNullException"><paramref name="path"/> is null. </exception><exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception><exception cref="T:System.NotSupportedException"><paramref name="path"/> is in an invalid format. </exception><filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
    [SecuritySafeCritical]
    public static DateTime GetCreationTime(string path)
    {
      return File.InternalGetCreationTimeUtc(path, true).ToLocalTime();
    }

    /// <summary>
    /// Returns the creation date and time, in coordinated universal time (UTC), of the specified file or directory.
    /// </summary>
    /// 
    /// <returns>
    /// A <see cref="T:System.DateTime"/> structure set to the creation date and time for the specified file or directory. This value is expressed in UTC time.
    /// </returns>
    /// <param name="path">The file or directory for which to obtain creation date and time information. </param><exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception><exception cref="T:System.ArgumentException"><paramref name="path"/> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars"/>. </exception><exception cref="T:System.ArgumentNullException"><paramref name="path"/> is null. </exception><exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception><exception cref="T:System.NotSupportedException"><paramref name="path"/> is in an invalid format. </exception><filterpriority>2</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
    [SecuritySafeCritical]
    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    public static DateTime GetCreationTimeUtc(string path)
    {
      return File.InternalGetCreationTimeUtc(path, false);
    }

    [SecurityCritical]
    private static DateTime InternalGetCreationTimeUtc(string path, bool checkHost)
    {
      string fullPathInternal = Path.GetFullPathInternal(path);
      new FileIOPermission(FileIOPermissionAccess.Read, new string[1]
      {
        fullPathInternal
      }, 0 != 0, 0 != 0).Demand();
      Win32Native.WIN32_FILE_ATTRIBUTE_DATA data = new Win32Native.WIN32_FILE_ATTRIBUTE_DATA();
      int errorCode = File.FillAttributeInfo(fullPathInternal, ref data, false, false);
      if (errorCode != 0)
        __Error.WinIOError(errorCode, fullPathInternal);
      return DateTime.FromFileTimeUtc((long) data.ftCreationTimeHigh << 32 | (long) data.ftCreationTimeLow);
    }

    /// <summary>
    /// Sets the date and time the specified file was last accessed.
    /// </summary>
    /// <param name="path">The file for which to set the access date and time information. </param><param name="lastAccessTime">A <see cref="T:System.DateTime"/> containing the value to set for the last access date and time of <paramref name="path"/>. This value is expressed in local time. </param><exception cref="T:System.ArgumentException"><paramref name="path"/> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars"/>. </exception><exception cref="T:System.ArgumentNullException"><paramref name="path"/> is null. </exception><exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception><exception cref="T:System.IO.FileNotFoundException">The specified path was not found. </exception><exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception><exception cref="T:System.NotSupportedException"><paramref name="path"/> is in an invalid format. </exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="lastAccessTime"/> specifies a value outside the range of dates or times permitted for this operation.</exception><filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
    public static void SetLastAccessTime(string path, DateTime lastAccessTime)
    {
      File.SetLastAccessTimeUtc(path, lastAccessTime.ToUniversalTime());
    }

    /// <summary>
    /// Sets the date and time, in coordinated universal time (UTC), that the specified file was last accessed.
    /// </summary>
    /// <param name="path">The file for which to set the access date and time information. </param><param name="lastAccessTimeUtc">A <see cref="T:System.DateTime"/> containing the value to set for the last access date and time of <paramref name="path"/>. This value is expressed in UTC time. </param><exception cref="T:System.ArgumentException"><paramref name="path"/> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars"/>. </exception><exception cref="T:System.ArgumentNullException"><paramref name="path"/> is null. </exception><exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception><exception cref="T:System.IO.FileNotFoundException">The specified path was not found. </exception><exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception><exception cref="T:System.NotSupportedException"><paramref name="path"/> is in an invalid format. </exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="lastAccessTimeUtc"/> specifies a value outside the range of dates or times permitted for this operation.</exception><filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
    [SecuritySafeCritical]
    public static unsafe void SetLastAccessTimeUtc(string path, DateTime lastAccessTimeUtc)
    {
      SafeFileHandle handle;
      using (File.OpenFile(path, FileAccess.Write, out handle))
      {
        Win32Native.FILE_TIME fileTime = new Win32Native.FILE_TIME(lastAccessTimeUtc.ToFileTimeUtc());
        if (Win32Native.SetFileTime(handle, (Win32Native.FILE_TIME*) null, &fileTime, (Win32Native.FILE_TIME*) null))
          return;
        __Error.WinIOError(Marshal.GetLastWin32Error(), path);
      }
    }

    /// <summary>
    /// Returns the date and time the specified file or directory was last accessed.
    /// </summary>
    /// 
    /// <returns>
    /// A <see cref="T:System.DateTime"/> structure set to the date and time that the specified file or directory was last accessed. This value is expressed in local time.
    /// </returns>
    /// <param name="path">The file or directory for which to obtain access date and time information. </param><exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception><exception cref="T:System.ArgumentException"><paramref name="path"/> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars"/>. </exception><exception cref="T:System.ArgumentNullException"><paramref name="path"/> is null. </exception><exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception><exception cref="T:System.NotSupportedException"><paramref name="path"/> is in an invalid format. </exception><filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
    [SecuritySafeCritical]
    public static DateTime GetLastAccessTime(string path)
    {
      return File.InternalGetLastAccessTimeUtc(path, true).ToLocalTime();
    }

    /// <summary>
    /// Returns the date and time, in coordinated universal time (UTC), that the specified file or directory was last accessed.
    /// </summary>
    /// 
    /// <returns>
    /// A <see cref="T:System.DateTime"/> structure set to the date and time that the specified file or directory was last accessed. This value is expressed in UTC time.
    /// </returns>
    /// <param name="path">The file or directory for which to obtain access date and time information. </param><exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception><exception cref="T:System.ArgumentException"><paramref name="path"/> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars"/>. </exception><exception cref="T:System.ArgumentNullException"><paramref name="path"/> is null. </exception><exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception><exception cref="T:System.NotSupportedException"><paramref name="path"/> is in an invalid format. </exception><filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
    [SecuritySafeCritical]
    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    public static DateTime GetLastAccessTimeUtc(string path)
    {
      return File.InternalGetLastAccessTimeUtc(path, false);
    }

    [SecurityCritical]
    private static DateTime InternalGetLastAccessTimeUtc(string path, bool checkHost)
    {
      string fullPathInternal = Path.GetFullPathInternal(path);
      new FileIOPermission(FileIOPermissionAccess.Read, new string[1]
      {
        fullPathInternal
      }, 0 != 0, 0 != 0).Demand();
      Win32Native.WIN32_FILE_ATTRIBUTE_DATA data = new Win32Native.WIN32_FILE_ATTRIBUTE_DATA();
      int errorCode = File.FillAttributeInfo(fullPathInternal, ref data, false, false);
      if (errorCode != 0)
        __Error.WinIOError(errorCode, fullPathInternal);
      return DateTime.FromFileTimeUtc((long) data.ftLastAccessTimeHigh << 32 | (long) data.ftLastAccessTimeLow);
    }

    /// <summary>
    /// Sets the date and time that the specified file was last written to.
    /// </summary>
    /// <param name="path">The file for which to set the date and time information. </param><param name="lastWriteTime">A <see cref="T:System.DateTime"/> containing the value to set for the last write date and time of <paramref name="path"/>. This value is expressed in local time. </param><exception cref="T:System.ArgumentException"><paramref name="path"/> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars"/>. </exception><exception cref="T:System.ArgumentNullException"><paramref name="path"/> is null. </exception><exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception><exception cref="T:System.IO.FileNotFoundException">The specified path was not found. </exception><exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception><exception cref="T:System.NotSupportedException"><paramref name="path"/> is in an invalid format. </exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="lastWriteTime"/> specifies a value outside the range of dates or times permitted for this operation.</exception><filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
    public static void SetLastWriteTime(string path, DateTime lastWriteTime)
    {
      File.SetLastWriteTimeUtc(path, lastWriteTime.ToUniversalTime());
    }

    /// <summary>
    /// Sets the date and time, in coordinated universal time (UTC), that the specified file was last written to.
    /// </summary>
    /// <param name="path">The file for which to set the date and time information. </param><param name="lastWriteTimeUtc">A <see cref="T:System.DateTime"/> containing the value to set for the last write date and time of <paramref name="path"/>. This value is expressed in UTC time. </param><exception cref="T:System.ArgumentException"><paramref name="path"/> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars"/>. </exception><exception cref="T:System.ArgumentNullException"><paramref name="path"/> is null. </exception><exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception><exception cref="T:System.IO.FileNotFoundException">The specified path was not found. </exception><exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception><exception cref="T:System.NotSupportedException"><paramref name="path"/> is in an invalid format. </exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="lastWriteTimeUtc"/> specifies a value outside the range of dates or times permitted for this operation.</exception><filterpriority>2</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
    [SecuritySafeCritical]
    public static unsafe void SetLastWriteTimeUtc(string path, DateTime lastWriteTimeUtc)
    {
      SafeFileHandle handle;
      using (File.OpenFile(path, FileAccess.Write, out handle))
      {
        Win32Native.FILE_TIME fileTime = new Win32Native.FILE_TIME(lastWriteTimeUtc.ToFileTimeUtc());
        if (Win32Native.SetFileTime(handle, (Win32Native.FILE_TIME*) null, (Win32Native.FILE_TIME*) null, &fileTime))
          return;
        __Error.WinIOError(Marshal.GetLastWin32Error(), path);
      }
    }

    /// <summary>
    /// Returns the date and time the specified file or directory was last written to.
    /// </summary>
    /// 
    /// <returns>
    /// A <see cref="T:System.DateTime"/> structure set to the date and time that the specified file or directory was last written to. This value is expressed in local time.
    /// </returns>
    /// <param name="path">The file or directory for which to obtain write date and time information. </param><exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception><exception cref="T:System.ArgumentException"><paramref name="path"/> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars"/>. </exception><exception cref="T:System.ArgumentNullException"><paramref name="path"/> is null. </exception><exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception><exception cref="T:System.NotSupportedException"><paramref name="path"/> is in an invalid format. </exception><filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
    [SecuritySafeCritical]
    public static DateTime GetLastWriteTime(string path)
    {
      return File.InternalGetLastWriteTimeUtc(path, true).ToLocalTime();
    }

    /// <summary>
    /// Returns the date and time, in coordinated universal time (UTC), that the specified file or directory was last written to.
    /// </summary>
    /// 
    /// <returns>
    /// A <see cref="T:System.DateTime"/> structure set to the date and time that the specified file or directory was last written to. This value is expressed in UTC time.
    /// </returns>
    /// <param name="path">The file or directory for which to obtain write date and time information. </param><exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception><exception cref="T:System.ArgumentException"><paramref name="path"/> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars"/>. </exception><exception cref="T:System.ArgumentNullException"><paramref name="path"/> is null. </exception><exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception><exception cref="T:System.NotSupportedException"><paramref name="path"/> is in an invalid format. </exception><filterpriority>2</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
    [SecuritySafeCritical]
    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    public static DateTime GetLastWriteTimeUtc(string path)
    {
      return File.InternalGetLastWriteTimeUtc(path, false);
    }

    [SecurityCritical]
    private static DateTime InternalGetLastWriteTimeUtc(string path, bool checkHost)
    {
      string fullPathInternal = Path.GetFullPathInternal(path);
      new FileIOPermission(FileIOPermissionAccess.Read, new string[1]
      {
        fullPathInternal
      }, 0 != 0, 0 != 0).Demand();
      Win32Native.WIN32_FILE_ATTRIBUTE_DATA data = new Win32Native.WIN32_FILE_ATTRIBUTE_DATA();
      int errorCode = File.FillAttributeInfo(fullPathInternal, ref data, false, false);
      if (errorCode != 0)
        __Error.WinIOError(errorCode, fullPathInternal);
      return DateTime.FromFileTimeUtc((long) data.ftLastWriteTimeHigh << 32 | (long) data.ftLastWriteTimeLow);
    }

    /// <summary>
    /// Gets the <see cref="T:System.IO.FileAttributes"/> of the file on the path.
    /// </summary>
    /// 
    /// <returns>
    /// The <see cref="T:System.IO.FileAttributes"/> of the file on the path.
    /// </returns>
    /// <param name="path">The path to the file. </param><exception cref="T:System.ArgumentException"><paramref name="path"/> is empty, contains only white spaces, or contains invalid characters. </exception><exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception><exception cref="T:System.NotSupportedException"><paramref name="path"/> is in an invalid format. </exception><exception cref="T:System.IO.FileNotFoundException"><paramref name="path"/> represents a file and is invalid, such as being on an unmapped drive, or the file cannot be found. </exception><exception cref="T:System.IO.DirectoryNotFoundException"><paramref name="path"/> represents a directory and is invalid, such as being on an unmapped drive, or the directory cannot be found.</exception><exception cref="T:System.IO.IOException">This file is being used by another process.</exception><exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception><filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
    [SecuritySafeCritical]
    public static FileAttributes GetAttributes(string path)
    {
      string fullPathInternal = Path.GetFullPathInternal(path);
      new FileIOPermission(FileIOPermissionAccess.Read, new string[1]
      {
        fullPathInternal
      }, 0 != 0, 0 != 0).Demand();
      Win32Native.WIN32_FILE_ATTRIBUTE_DATA data = new Win32Native.WIN32_FILE_ATTRIBUTE_DATA();
      int errorCode = File.FillAttributeInfo(fullPathInternal, ref data, false, true);
      if (errorCode != 0)
        __Error.WinIOError(errorCode, fullPathInternal);
      return (FileAttributes) data.fileAttributes;
    }

    /// <summary>
    /// Sets the specified <see cref="T:System.IO.FileAttributes"/> of the file on the specified path.
    /// </summary>
    /// <param name="path">The path to the file. </param><param name="fileAttributes">A bitwise combination of the enumeration values. </param><exception cref="T:System.ArgumentException"><paramref name="path"/> is empty, contains only white spaces, contains invalid characters, or the file attribute is invalid. </exception><exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception><exception cref="T:System.NotSupportedException"><paramref name="path"/> is in an invalid format. </exception><exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, (for example, it is on an unmapped drive). </exception><exception cref="T:System.IO.FileNotFoundException">The file cannot be found.</exception><exception cref="T:System.UnauthorizedAccessException"><paramref name="path"/> specified a file that is read-only.-or- This operation is not supported on the current platform.-or- <paramref name="path"/> specified a directory.-or- The caller does not have the required permission.</exception><filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
    [SecuritySafeCritical]
    public static void SetAttributes(string path, FileAttributes fileAttributes)
    {
      string fullPathInternal = Path.GetFullPathInternal(path);
      new FileIOPermission(FileIOPermissionAccess.Write, new string[1]
      {
        fullPathInternal
      }, 0 != 0, 0 != 0).Demand();
      if (Win32Native.SetFileAttributes(fullPathInternal, (int) fileAttributes))
        return;
      int lastWin32Error = Marshal.GetLastWin32Error();
      if (lastWin32Error == 87)
        throw new ArgumentException(Environment.GetResourceString("Arg_InvalidFileAttrs"));
      __Error.WinIOError(lastWin32Error, fullPathInternal);
    }

    /// <summary>
    /// Gets a <see cref="T:System.Security.AccessControl.FileSecurity"/> object that encapsulates the access control list (ACL) entries for a specified file.
    /// </summary>
    /// 
    /// <returns>
    /// A <see cref="T:System.Security.AccessControl.FileSecurity"/> object that encapsulates the access control rules for the file described by the <paramref name="path"/> parameter.
    /// </returns>
    /// <param name="path">The path to a file containing a <see cref="T:System.Security.AccessControl.FileSecurity"/> object that describes the file's access control list (ACL) information.</param><exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.</exception><exception cref="T:System.Runtime.InteropServices.SEHException">The <paramref name="path"/> parameter is null.</exception><exception cref="T:System.SystemException">The file could not be found.</exception><exception cref="T:System.UnauthorizedAccessException">The <paramref name="path"/> parameter specified a file that is read-only.-or- This operation is not supported on the current platform.-or- The <paramref name="path"/> parameter specified a directory.-or- The caller does not have the required permission.</exception><filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    public static FileSecurity GetAccessControl(string path)
    {
      return File.GetAccessControl(path, AccessControlSections.Access | AccessControlSections.Owner | AccessControlSections.Group);
    }

    /// <summary>
    /// Gets a <see cref="T:System.Security.AccessControl.FileSecurity"/> object that encapsulates the specified type of access control list (ACL) entries for a particular file.
    /// </summary>
    /// 
    /// <returns>
    /// A <see cref="T:System.Security.AccessControl.FileSecurity"/> object that encapsulates the access control rules for the file described by the <paramref name="path"/> parameter.
    /// </returns>
    /// <param name="path">The path to a file containing a <see cref="T:System.Security.AccessControl.FileSecurity"/> object that describes the file's access control list (ACL) information.</param><param name="includeSections">One of the <see cref="T:System.Security.AccessControl.AccessControlSections"/> values that specifies the type of access control list (ACL) information to receive.</param><exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.</exception><exception cref="T:System.Runtime.InteropServices.SEHException">The <paramref name="path"/> parameter is null.</exception><exception cref="T:System.SystemException">The file could not be found.</exception><exception cref="T:System.UnauthorizedAccessException">The <paramref name="path"/> parameter specified a file that is read-only.-or- This operation is not supported on the current platform.-or- The <paramref name="path"/> parameter specified a directory.-or- The caller does not have the required permission.</exception><filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
    public static FileSecurity GetAccessControl(string path, AccessControlSections includeSections)
    {
      return new FileSecurity(path, includeSections);
    }

    /// <summary>
    /// Applies access control list (ACL) entries described by a <see cref="T:System.Security.AccessControl.FileSecurity"/> object to the specified file.
    /// </summary>
    /// <param name="path">A file to add or remove access control list (ACL) entries from.</param><param name="fileSecurity">A <see cref="T:System.Security.AccessControl.FileSecurity"/> object that describes an ACL entry to apply to the file described by the <paramref name="path"/> parameter.</param><exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.</exception><exception cref="T:System.Runtime.InteropServices.SEHException">The <paramref name="path"/> parameter is null.</exception><exception cref="T:System.SystemException">The file could not be found.</exception><exception cref="T:System.UnauthorizedAccessException">The <paramref name="path"/> parameter specified a file that is read-only.-or- This operation is not supported on the current platform.-or- The <paramref name="path"/> parameter specified a directory.-or- The caller does not have the required permission.</exception><exception cref="T:System.ArgumentNullException">The <paramref name="fileSecurity"/> parameter is null.</exception><filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
    [SecuritySafeCritical]
    public static void SetAccessControl(string path, FileSecurity fileSecurity)
    {
      if (fileSecurity == null)
        throw new ArgumentNullException("fileSecurity");
      string fullPathInternal = Path.GetFullPathInternal(path);
      fileSecurity.Persist(fullPathInternal);
    }

    /// <summary>
    /// Opens an existing file for reading.
    /// </summary>
    /// 
    /// <returns>
    /// A read-only <see cref="T:System.IO.FileStream"/> on the specified path.
    /// </returns>
    /// <param name="path">The file to be opened for reading. </param><exception cref="T:System.ArgumentException"><paramref name="path"/> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars"/>. </exception><exception cref="T:System.ArgumentNullException"><paramref name="path"/> is null. </exception><exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception><exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, (for example, it is on an unmapped drive). </exception><exception cref="T:System.UnauthorizedAccessException"><paramref name="path"/> specified a directory.-or- The caller does not have the required permission. </exception><exception cref="T:System.IO.FileNotFoundException">The file specified in <paramref name="path"/> was not found. </exception><exception cref="T:System.NotSupportedException"><paramref name="path"/> is in an invalid format. </exception><filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
    public static FileStream OpenRead(string path)
    {
      return new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
    }

    /// <summary>
    /// Opens an existing file or creates a new file for writing.
    /// </summary>
    /// 
    /// <returns>
    /// An unshared <see cref="T:System.IO.FileStream"/> object on the specified path with <see cref="F:System.IO.FileAccess.Write"/> access.
    /// </returns>
    /// <param name="path">The file to be opened for writing. </param><exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.-or- <paramref name="path"/> specified a read-only file or directory. </exception><exception cref="T:System.ArgumentException"><paramref name="path"/> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars"/>. </exception><exception cref="T:System.ArgumentNullException"><paramref name="path"/> is null. </exception><exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception><exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, (for example, it is on an unmapped drive). </exception><exception cref="T:System.IO.FileNotFoundException">The file specified in <paramref name="path"/> was not found. </exception><exception cref="T:System.NotSupportedException"><paramref name="path"/> is in an invalid format. </exception><filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
    public static FileStream OpenWrite(string path)
    {
      return new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
    }

    /// <summary>
    /// Opens a text file, reads all lines of the file, and then closes the file.
    /// </summary>
    /// 
    /// <returns>
    /// A string containing all lines of the file.
    /// </returns>
    /// <param name="path">The file to open for reading. </param><exception cref="T:System.ArgumentException"><paramref name="path"/> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars"/>. </exception><exception cref="T:System.ArgumentNullException"><paramref name="path"/> is null. </exception><exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception><exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception><exception cref="T:System.IO.IOException">An I/O error occurred while opening the file. </exception><exception cref="T:System.UnauthorizedAccessException"><paramref name="path"/> specified a file that is read-only.-or- This operation is not supported on the current platform.-or- <paramref name="path"/> specified a directory.-or- The caller does not have the required permission. </exception><exception cref="T:System.IO.FileNotFoundException">The file specified in <paramref name="path"/> was not found. </exception><exception cref="T:System.NotSupportedException"><paramref name="path"/> is in an invalid format. </exception><exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception><filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
    [SecuritySafeCritical]
    public static string ReadAllText(string path)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      if (path.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
      return File.InternalReadAllText(path, Encoding.UTF8, true);
    }

    /// <summary>
    /// Opens a file, reads all lines of the file with the specified encoding, and then closes the file.
    /// </summary>
    /// 
    /// <returns>
    /// A string containing all lines of the file.
    /// </returns>
    /// <param name="path">The file to open for reading. </param><param name="encoding">The encoding applied to the contents of the file. </param><exception cref="T:System.ArgumentException"><paramref name="path"/> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars"/>. </exception><exception cref="T:System.ArgumentNullException"><paramref name="path"/> is null. </exception><exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception><exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception><exception cref="T:System.IO.IOException">An I/O error occurred while opening the file. </exception><exception cref="T:System.UnauthorizedAccessException"><paramref name="path"/> specified a file that is read-only.-or- This operation is not supported on the current platform.-or- <paramref name="path"/> specified a directory.-or- The caller does not have the required permission. </exception><exception cref="T:System.IO.FileNotFoundException">The file specified in <paramref name="path"/> was not found. </exception><exception cref="T:System.NotSupportedException"><paramref name="path"/> is in an invalid format. </exception><exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception><filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
    [SecuritySafeCritical]
    public static string ReadAllText(string path, Encoding encoding)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      if (encoding == null)
        throw new ArgumentNullException("encoding");
      if (path.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
      return File.InternalReadAllText(path, encoding, true);
    }

    [SecurityCritical]
    internal static string UnsafeReadAllText(string path)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      if (path.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
      return File.InternalReadAllText(path, Encoding.UTF8, false);
    }

    /// <summary>
    /// Creates a new file, writes the specified string to the file, and then closes the file. If the target file already exists, it is overwritten.
    /// </summary>
    /// <param name="path">The file to write to. </param><param name="contents">The string to write to the file. </param><exception cref="T:System.ArgumentException"><paramref name="path"/> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars"/>. </exception><exception cref="T:System.ArgumentNullException"><paramref name="path"/> is null or <paramref name="contents"/> is empty.  </exception><exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception><exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception><exception cref="T:System.IO.IOException">An I/O error occurred while opening the file. </exception><exception cref="T:System.UnauthorizedAccessException"><paramref name="path"/> specified a file that is read-only.-or- This operation is not supported on the current platform.-or- <paramref name="path"/> specified a directory.-or- The caller does not have the required permission. </exception><exception cref="T:System.NotSupportedException"><paramref name="path"/> is in an invalid format. </exception><exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception><filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
    [SecuritySafeCritical]
    public static void WriteAllText(string path, string contents)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      if (path.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
      File.InternalWriteAllText(path, contents, StreamWriter.UTF8NoBOM, true);
    }

    /// <summary>
    /// Creates a new file, writes the specified string to the file using the specified encoding, and then closes the file. If the target file already exists, it is overwritten.
    /// </summary>
    /// <param name="path">The file to write to. </param><param name="contents">The string to write to the file. </param><param name="encoding">The encoding to apply to the string.</param><exception cref="T:System.ArgumentException"><paramref name="path"/> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars"/>. </exception><exception cref="T:System.ArgumentNullException"><paramref name="path"/> is null or <paramref name="contents"/> is empty. </exception><exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception><exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception><exception cref="T:System.IO.IOException">An I/O error occurred while opening the file. </exception><exception cref="T:System.UnauthorizedAccessException"><paramref name="path"/> specified a file that is read-only.-or- This operation is not supported on the current platform.-or- <paramref name="path"/> specified a directory.-or- The caller does not have the required permission. </exception><exception cref="T:System.NotSupportedException"><paramref name="path"/> is in an invalid format. </exception><exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception><filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
    [SecuritySafeCritical]
    public static void WriteAllText(string path, string contents, Encoding encoding)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      if (encoding == null)
        throw new ArgumentNullException("encoding");
      if (path.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
      File.InternalWriteAllText(path, contents, encoding, true);
    }

    [SecurityCritical]
    internal static void UnsafeWriteAllText(string path, string contents)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      if (path.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
      File.InternalWriteAllText(path, contents, StreamWriter.UTF8NoBOM, false);
    }

    /// <summary>
    /// Opens a binary file, reads the contents of the file into a byte array, and then closes the file.
    /// </summary>
    /// 
    /// <returns>
    /// A byte array containing the contents of the file.
    /// </returns>
    /// <param name="path">The file to open for reading. </param><exception cref="T:System.ArgumentException"><paramref name="path"/> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars"/>. </exception><exception cref="T:System.ArgumentNullException"><paramref name="path"/> is null. </exception><exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception><exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception><exception cref="T:System.IO.IOException">An I/O error occurred while opening the file. </exception><exception cref="T:System.UnauthorizedAccessException">This operation is not supported on the current platform.-or- <paramref name="path"/> specified a directory.-or- The caller does not have the required permission. </exception><exception cref="T:System.IO.FileNotFoundException">The file specified in <paramref name="path"/> was not found. </exception><exception cref="T:System.NotSupportedException"><paramref name="path"/> is in an invalid format. </exception><exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception><filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
    [SecuritySafeCritical]
    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    public static byte[] ReadAllBytes(string path)
    {
      return File.InternalReadAllBytes(path, true);
    }

    [SecurityCritical]
    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    internal static byte[] UnsafeReadAllBytes(string path)
    {
      return File.InternalReadAllBytes(path, false);
    }

    [SecurityCritical]
    private static byte[] InternalReadAllBytes(string path, bool checkHost)
    {
      byte[] buffer;
      using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, FileOptions.None, Path.GetFileName(path), false, false, checkHost))
      {
        int offset = 0;
        long length = fileStream.Length;
        if (length > (long) int.MaxValue)
          throw new IOException(Environment.GetResourceString("IO.IO_FileTooLong2GB"));
        int count = (int) length;
        buffer = new byte[count];
        while (count > 0)
        {
          int num = fileStream.Read(buffer, offset, count);
          if (num == 0)
            __Error.EndOfFile();
          offset += num;
          count -= num;
        }
      }
      return buffer;
    }

    /// <summary>
    /// Creates a new file, writes the specified byte array to the file, and then closes the file. If the target file already exists, it is overwritten.
    /// </summary>
    /// <param name="path">The file to write to. </param><param name="bytes">The bytes to write to the file. </param><exception cref="T:System.ArgumentException"><paramref name="path"/> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars"/>. </exception><exception cref="T:System.ArgumentNullException"><paramref name="path"/> is null or the byte array is empty. </exception><exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception><exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception><exception cref="T:System.IO.IOException">An I/O error occurred while opening the file. </exception><exception cref="T:System.UnauthorizedAccessException"><paramref name="path"/> specified a file that is read-only.-or- This operation is not supported on the current platform.-or- <paramref name="path"/> specified a directory.-or- The caller does not have the required permission. </exception><exception cref="T:System.NotSupportedException"><paramref name="path"/> is in an invalid format. </exception><exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception><filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
    [SecuritySafeCritical]
    public static void WriteAllBytes(string path, byte[] bytes)
    {
      if (path == null)
        throw new ArgumentNullException("path", Environment.GetResourceString("ArgumentNull_Path"));
      if (path.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
      if (bytes == null)
        throw new ArgumentNullException("bytes");
      File.InternalWriteAllBytes(path, bytes, true);
    }

    [SecurityCritical]
    internal static void UnsafeWriteAllBytes(string path, byte[] bytes)
    {
      if (path == null)
        throw new ArgumentNullException("path", Environment.GetResourceString("ArgumentNull_Path"));
      if (path.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
      if (bytes == null)
        throw new ArgumentNullException("bytes");
      File.InternalWriteAllBytes(path, bytes, false);
    }

    /// <summary>
    /// Opens a text file, reads all lines of the file, and then closes the file.
    /// </summary>
    /// 
    /// <returns>
    /// A string array containing all lines of the file.
    /// </returns>
    /// <param name="path">The file to open for reading. </param><exception cref="T:System.ArgumentException"><paramref name="path"/> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars"/>. </exception><exception cref="T:System.ArgumentNullException"><paramref name="path"/> is null. </exception><exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception><exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception><exception cref="T:System.IO.IOException">An I/O error occurred while opening the file. </exception><exception cref="T:System.UnauthorizedAccessException"><paramref name="path"/> specified a file that is read-only.-or- This operation is not supported on the current platform.-or- <paramref name="path"/> specified a directory.-or- The caller does not have the required permission. </exception><exception cref="T:System.IO.FileNotFoundException">The file specified in <paramref name="path"/> was not found. </exception><exception cref="T:System.NotSupportedException"><paramref name="path"/> is in an invalid format. </exception><exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception><filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
    public static string[] ReadAllLines(string path)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      if (path.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
      return File.InternalReadAllLines(path, Encoding.UTF8);
    }

    /// <summary>
    /// Opens a file, reads all lines of the file with the specified encoding, and then closes the file.
    /// </summary>
    /// 
    /// <returns>
    /// A string array containing all lines of the file.
    /// </returns>
    /// <param name="path">The file to open for reading. </param><param name="encoding">The encoding applied to the contents of the file. </param><exception cref="T:System.ArgumentException"><paramref name="path"/> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars"/>. </exception><exception cref="T:System.ArgumentNullException"><paramref name="path"/> is null. </exception><exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception><exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception><exception cref="T:System.IO.IOException">An I/O error occurred while opening the file. </exception><exception cref="T:System.UnauthorizedAccessException"><paramref name="path"/> specified a file that is read-only.-or- This operation is not supported on the current platform.-or- <paramref name="path"/> specified a directory.-or- The caller does not have the required permission. </exception><exception cref="T:System.IO.FileNotFoundException">The file specified in <paramref name="path"/> was not found. </exception><exception cref="T:System.NotSupportedException"><paramref name="path"/> is in an invalid format. </exception><exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception><filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
    public static string[] ReadAllLines(string path, Encoding encoding)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      if (encoding == null)
        throw new ArgumentNullException("encoding");
      if (path.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
      return File.InternalReadAllLines(path, encoding);
    }

    /// <summary>
    /// Reads the lines of a file.
    /// </summary>
    /// 
    /// <returns>
    /// All the lines of the file, or the lines that are the result of a query.
    /// </returns>
    /// <param name="path">The file to read.</param><exception cref="T:System.ArgumentException"><paramref name="path"/> is a zero-length string, contains only white space, or contains one or more invalid characters defined by the <see cref="M:System.IO.Path.GetInvalidPathChars"/> method.</exception><exception cref="T:System.ArgumentNullException"><paramref name="path"/> is null.</exception><exception cref="T:System.IO.DirectoryNotFoundException"><paramref name="path"/> is invalid (for example, it is on an unmapped drive).</exception><exception cref="T:System.IO.FileNotFoundException">The file specified by <paramref name="path"/> was not found.</exception><exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.</exception><exception cref="T:System.IO.PathTooLongException"><paramref name="path"/> exceeds the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters.</exception><exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception><exception cref="T:System.UnauthorizedAccessException"><paramref name="path"/> specifies a file that is read-only.-or-This operation is not supported on the current platform.-or-<paramref name="path"/> is a directory.-or-The caller does not have the required permission.</exception>
    public static IEnumerable<string> ReadLines(string path)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      if (path.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"), "path");
      return (IEnumerable<string>) ReadLinesIterator.CreateIterator(path, Encoding.UTF8);
    }

    /// <summary>
    /// Read the lines of a file that has a specified encoding.
    /// </summary>
    /// 
    /// <returns>
    /// All the lines of the file, or the lines that are the result of a query.
    /// </returns>
    /// <param name="path">The file to read.</param><param name="encoding">The encoding that is applied to the contents of the file. </param><exception cref="T:System.ArgumentException"><paramref name="path"/> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by the <see cref="M:System.IO.Path.GetInvalidPathChars"/> method.</exception><exception cref="T:System.ArgumentNullException"><paramref name="path"/> is null.</exception><exception cref="T:System.IO.DirectoryNotFoundException"><paramref name="path"/> is invalid (for example, it is on an unmapped drive).</exception><exception cref="T:System.IO.FileNotFoundException">The file specified by <paramref name="path"/> was not found.</exception><exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.</exception><exception cref="T:System.IO.PathTooLongException"><paramref name="path"/> exceeds the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters.</exception><exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception><exception cref="T:System.UnauthorizedAccessException"><paramref name="path"/> specifies a file that is read-only.-or-This operation is not supported on the current platform.-or-<paramref name="path"/> is a directory.-or-The caller does not have the required permission.</exception>
    public static IEnumerable<string> ReadLines(string path, Encoding encoding)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      if (encoding == null)
        throw new ArgumentNullException("encoding");
      if (path.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"), "path");
      return (IEnumerable<string>) ReadLinesIterator.CreateIterator(path, encoding);
    }

    /// <summary>
    /// Creates a new file, write the specified string array to the file, and then closes the file.
    /// </summary>
    /// <param name="path">The file to write to. </param><param name="contents">The string array to write to the file. </param><exception cref="T:System.ArgumentException"><paramref name="path"/> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars"/>. </exception><exception cref="T:System.ArgumentNullException">Either <paramref name="path"/> or <paramref name="contents"/> is null.  </exception><exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception><exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception><exception cref="T:System.IO.IOException">An I/O error occurred while opening the file. </exception><exception cref="T:System.UnauthorizedAccessException"><paramref name="path"/> specified a file that is read-only.-or- This operation is not supported on the current platform.-or- <paramref name="path"/> specified a directory.-or- The caller does not have the required permission. </exception><exception cref="T:System.NotSupportedException"><paramref name="path"/> is in an invalid format. </exception><exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception><filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
    public static void WriteAllLines(string path, string[] contents)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      if (contents == null)
        throw new ArgumentNullException("contents");
      if (path.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
      File.InternalWriteAllLines((TextWriter) new StreamWriter(path, false, StreamWriter.UTF8NoBOM), (IEnumerable<string>) contents);
    }

    /// <summary>
    /// Creates a new file, writes the specified string array to the file by using the specified encoding, and then closes the file.
    /// </summary>
    /// <param name="path">The file to write to. </param><param name="contents">The string array to write to the file. </param><param name="encoding">An <see cref="T:System.Text.Encoding"/> object that represents the character encoding applied to the string array.</param><exception cref="T:System.ArgumentException"><paramref name="path"/> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars"/>. </exception><exception cref="T:System.ArgumentNullException">Either <paramref name="path"/> or <paramref name="contents"/> is null.  </exception><exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception><exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception><exception cref="T:System.IO.IOException">An I/O error occurred while opening the file. </exception><exception cref="T:System.UnauthorizedAccessException"><paramref name="path"/> specified a file that is read-only.-or- This operation is not supported on the current platform.-or- <paramref name="path"/> specified a directory.-or- The caller does not have the required permission. </exception><exception cref="T:System.NotSupportedException"><paramref name="path"/> is in an invalid format. </exception><exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception><filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
    public static void WriteAllLines(string path, string[] contents, Encoding encoding)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      if (contents == null)
        throw new ArgumentNullException("contents");
      if (encoding == null)
        throw new ArgumentNullException("encoding");
      if (path.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
      File.InternalWriteAllLines((TextWriter) new StreamWriter(path, false, encoding), (IEnumerable<string>) contents);
    }

    /// <summary>
    /// Creates a new file, writes a collection of strings to the file, and then closes the file.
    /// </summary>
    /// <param name="path">The file to write to.</param><param name="contents">The lines to write to the file.</param><exception cref="T:System.ArgumentException"><paramref name="path"/> is a zero-length string, contains only white space, or contains one or more invalid characters defined by the <see cref="M:System.IO.Path.GetInvalidPathChars"/> method.</exception><exception cref="T:System.ArgumentNullException">Either<paramref name=" path "/>or <paramref name="contents"/> is null.</exception><exception cref="T:System.IO.DirectoryNotFoundException"><paramref name="path"/> is invalid (for example, it is on an unmapped drive).</exception><exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.</exception><exception cref="T:System.IO.PathTooLongException"><paramref name="path"/> exceeds the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters.</exception><exception cref="T:System.NotSupportedException"><paramref name="path"/> is in an invalid format.</exception><exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception><exception cref="T:System.UnauthorizedAccessException"><paramref name="path"/> specifies a file that is read-only.-or-This operation is not supported on the current platform.-or-<paramref name="path"/> is a directory.-or-The caller does not have the required permission.</exception>
    public static void WriteAllLines(string path, IEnumerable<string> contents)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      if (contents == null)
        throw new ArgumentNullException("contents");
      if (path.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
      File.InternalWriteAllLines((TextWriter) new StreamWriter(path, false, StreamWriter.UTF8NoBOM), contents);
    }

    /// <summary>
    /// Creates a new file by using the specified encoding, writes a collection of strings to the file, and then closes the file.
    /// </summary>
    /// <param name="path">The file to write to.</param><param name="contents">The lines to write to the file.</param><param name="encoding">The character encoding to use.</param><exception cref="T:System.ArgumentException"><paramref name="path"/> is a zero-length string, contains only white space, or contains one or more invalid characters defined by the <see cref="M:System.IO.Path.GetInvalidPathChars"/> method.</exception><exception cref="T:System.ArgumentNullException">Either<paramref name=" path"/>,<paramref name=" contents"/>, or <paramref name="encoding"/> is null.</exception><exception cref="T:System.IO.DirectoryNotFoundException"><paramref name="path"/> is invalid (for example, it is on an unmapped drive).</exception><exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.</exception><exception cref="T:System.IO.PathTooLongException"><paramref name="path"/> exceeds the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters.</exception><exception cref="T:System.NotSupportedException"><paramref name="path"/> is in an invalid format.</exception><exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception><exception cref="T:System.UnauthorizedAccessException"><paramref name="path"/> specifies a file that is read-only.-or-This operation is not supported on the current platform.-or-<paramref name="path"/> is a directory.-or-The caller does not have the required permission.</exception>
    public static void WriteAllLines(string path, IEnumerable<string> contents, Encoding encoding)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      if (contents == null)
        throw new ArgumentNullException("contents");
      if (encoding == null)
        throw new ArgumentNullException("encoding");
      if (path.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
      File.InternalWriteAllLines((TextWriter) new StreamWriter(path, false, encoding), contents);
    }

    /// <summary>
    /// Opens a file, appends the specified string to the file, and then closes the file. If the file does not exist, this method creates a file, writes the specified string to the file, then closes the file.
    /// </summary>
    /// <param name="path">The file to append the specified string to. </param><param name="contents">The string to append to the file. </param><exception cref="T:System.ArgumentException"><paramref name="path"/> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars"/>. </exception><exception cref="T:System.ArgumentNullException"><paramref name="path"/> is null. </exception><exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception><exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception><exception cref="T:System.IO.IOException">An I/O error occurred while opening the file. </exception><exception cref="T:System.UnauthorizedAccessException"><paramref name="path"/> specified a file that is read-only.-or- This operation is not supported on the current platform.-or- <paramref name="path"/> specified a directory.-or- The caller does not have the required permission. </exception><exception cref="T:System.IO.FileNotFoundException">The file specified in <paramref name="path"/> was not found. </exception><exception cref="T:System.NotSupportedException"><paramref name="path"/> is in an invalid format. </exception><exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception><filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
    public static void AppendAllText(string path, string contents)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      if (path.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
      File.InternalAppendAllText(path, contents, StreamWriter.UTF8NoBOM);
    }

    /// <summary>
    /// Appends the specified string to the file, creating the file if it does not already exist.
    /// </summary>
    /// <param name="path">The file to append the specified string to. </param><param name="contents">The string to append to the file. </param><param name="encoding">The character encoding to use. </param><exception cref="T:System.ArgumentException"><paramref name="path"/> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars"/>. </exception><exception cref="T:System.ArgumentNullException"><paramref name="path"/> is null. </exception><exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception><exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception><exception cref="T:System.IO.IOException">An I/O error occurred while opening the file. </exception><exception cref="T:System.UnauthorizedAccessException"><paramref name="path"/> specified a file that is read-only.-or- This operation is not supported on the current platform.-or- <paramref name="path"/> specified a directory.-or- The caller does not have the required permission. </exception><exception cref="T:System.IO.FileNotFoundException">The file specified in <paramref name="path"/> was not found. </exception><exception cref="T:System.NotSupportedException"><paramref name="path"/> is in an invalid format. </exception><exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception><filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
    public static void AppendAllText(string path, string contents, Encoding encoding)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      if (encoding == null)
        throw new ArgumentNullException("encoding");
      if (path.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
      File.InternalAppendAllText(path, contents, encoding);
    }

    /// <summary>
    /// Appends lines to a file, and then closes the file.
    /// </summary>
    /// <param name="path">The file to append the lines to. The file is created if it does not already exist.</param><param name="contents">The lines to append to the file.</param><exception cref="T:System.ArgumentException"><paramref name="path"/> is a zero-length string, contains only white space, or contains one more invalid characters defined by the <see cref="M:System.IO.Path.GetInvalidPathChars"/> method.</exception><exception cref="T:System.ArgumentNullException">Either<paramref name=" path "/>or <paramref name="contents"/> is null.</exception><exception cref="T:System.IO.DirectoryNotFoundException"><paramref name="path"/> is invalid (for example, it is on an unmapped drive).</exception><exception cref="T:System.IO.FileNotFoundException">The file specified by <paramref name="path"/> was not found.</exception><exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.</exception><exception cref="T:System.IO.PathTooLongException"><paramref name="path"/> exceeds the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters.</exception><exception cref="T:System.NotSupportedException"><paramref name="path"/> is in an invalid format.</exception><exception cref="T:System.Security.SecurityException">The caller does not have permission to write to the file.</exception><exception cref="T:System.UnauthorizedAccessException"><paramref name="path"/> specifies a file that is read-only.-or-This operation is not supported on the current platform.-or-<paramref name="path"/> is a directory.</exception>
    public static void AppendAllLines(string path, IEnumerable<string> contents)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      if (contents == null)
        throw new ArgumentNullException("contents");
      if (path.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
      File.InternalWriteAllLines((TextWriter) new StreamWriter(path, true, StreamWriter.UTF8NoBOM), contents);
    }

    /// <summary>
    /// Appends lines to a file by using a specified encoding, and then closes the file.
    /// </summary>
    /// <param name="path">The file to append the lines to.</param><param name="contents">The lines to append to the file.</param><param name="encoding">The character encoding to use.</param><exception cref="T:System.ArgumentException"><paramref name="path"/> is a zero-length string, contains only white space, or contains one more invalid characters defined by the <see cref="M:System.IO.Path.GetInvalidPathChars"/> method.</exception><exception cref="T:System.ArgumentNullException">Either<paramref name=" path"/>, <paramref name="contents"/>, or <paramref name="encoding"/> is null.</exception><exception cref="T:System.IO.DirectoryNotFoundException"><paramref name="path"/> is invalid (for example, it is on an unmapped drive).</exception><exception cref="T:System.IO.FileNotFoundException">The file specified by <paramref name="path"/> was not found.</exception><exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.</exception><exception cref="T:System.IO.PathTooLongException"><paramref name="path"/> exceeds the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters.</exception><exception cref="T:System.NotSupportedException"><paramref name="path"/> is in an invalid format.</exception><exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception><exception cref="T:System.UnauthorizedAccessException"><paramref name="path"/> specifies a file that is read-only.-or-This operation is not supported on the current platform.-or-<paramref name="path"/> is a directory.-or-The caller does not have the required permission.</exception>
    public static void AppendAllLines(string path, IEnumerable<string> contents, Encoding encoding)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      if (contents == null)
        throw new ArgumentNullException("contents");
      if (encoding == null)
        throw new ArgumentNullException("encoding");
      if (path.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
      File.InternalWriteAllLines((TextWriter) new StreamWriter(path, true, encoding), contents);
    }

    /// <summary>
    /// Moves a specified file to a new location, providing the option to specify a new file name.
    /// </summary>
    /// <param name="sourceFileName">The name of the file to move. </param><param name="destFileName">The new path for the file. </param><exception cref="T:System.IO.IOException">The destination file already exists.-or-<paramref name="sourceFileName"/> was not found. </exception><exception cref="T:System.ArgumentNullException"><paramref name="sourceFileName"/> or <paramref name="destFileName"/> is null. </exception><exception cref="T:System.ArgumentException"><paramref name="sourceFileName"/> or <paramref name="destFileName"/> is a zero-length string, contains only white space, or contains invalid characters as defined in <see cref="F:System.IO.Path.InvalidPathChars"/>. </exception><exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception><exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception><exception cref="T:System.IO.DirectoryNotFoundException">The path specified in <paramref name="sourceFileName"/> or <paramref name="destFileName"/> is invalid, (for example, it is on an unmapped drive). </exception><exception cref="T:System.NotSupportedException"><paramref name="sourceFileName"/> or <paramref name="destFileName"/> is in an invalid format. </exception><filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
    [SecuritySafeCritical]
    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    public static void Move(string sourceFileName, string destFileName)
    {
      File.InternalMove(sourceFileName, destFileName, true);
    }

    [SecurityCritical]
    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    internal static void UnsafeMove(string sourceFileName, string destFileName)
    {
      File.InternalMove(sourceFileName, destFileName, false);
    }

    [SecurityCritical]
    private static void InternalMove(string sourceFileName, string destFileName, bool checkHost)
    {
      if (sourceFileName == null)
        throw new ArgumentNullException("sourceFileName", Environment.GetResourceString("ArgumentNull_FileName"));
      if (destFileName == null)
        throw new ArgumentNullException("destFileName", Environment.GetResourceString("ArgumentNull_FileName"));
      if (sourceFileName.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyFileName"), "sourceFileName");
      if (destFileName.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyFileName"), "destFileName");
      string fullPathInternal1 = Path.GetFullPathInternal(sourceFileName);
      string fullPathInternal2 = Path.GetFullPathInternal(destFileName);
      new FileIOPermission(FileIOPermissionAccess.Read | FileIOPermissionAccess.Write, new string[1]
      {
        fullPathInternal1
      }, 0 != 0, 0 != 0).Demand();
      new FileIOPermission(FileIOPermissionAccess.Write, new string[1]
      {
        fullPathInternal2
      }, 0 != 0, 0 != 0).Demand();
      if (!File.InternalExists(fullPathInternal1))
        __Error.WinIOError(2, fullPathInternal1);
      if (Win32Native.MoveFile(fullPathInternal1, fullPathInternal2))
        return;
      __Error.WinIOError();
    }

    /// <summary>
    /// Replaces the contents of a specified file with the contents of another file, deleting the original file, and creating a backup of the replaced file.
    /// </summary>
    /// <param name="sourceFileName">The name of a file that replaces the file specified by <paramref name="destinationFileName"/>.</param><param name="destinationFileName">The name of the file being replaced.</param><param name="destinationBackupFileName">The name of the backup file.</param><exception cref="T:System.ArgumentException">The path described by the <paramref name="destinationFileName"/> parameter was not of a legal form.-or-The path described by the <paramref name="destinationBackupFileName"/> parameter was not of a legal form.</exception><exception cref="T:System.ArgumentNullException">The <paramref name="destinationFileName"/> parameter is null.</exception><exception cref="T:System.IO.DriveNotFoundException">An invalid drive was specified. </exception><exception cref="T:System.IO.FileNotFoundException">The file described by the current <see cref="T:System.IO.FileInfo"/> object could not be found.-or-The file described by the <paramref name="destinationBackupFileName"/> parameter could not be found. </exception><exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.- or -The <paramref name="sourceFileName"/> and <paramref name="destinationFileName"/> parameters specify the same file.</exception><exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception><exception cref="T:System.PlatformNotSupportedException">The operating system is Windows 98 Second Edition or earlier and the files system is not NTFS.</exception><exception cref="T:System.UnauthorizedAccessException">The <paramref name="sourceFileName"/> or <paramref name="destinationFileName"/> parameter specifies a file that is read-only.-or- This operation is not supported on the current platform.-or- Source or destination parameters specify a directory instead of a file.-or- The caller does not have the required permission.</exception><filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
    public static void Replace(string sourceFileName, string destinationFileName, string destinationBackupFileName)
    {
      if (sourceFileName == null)
        throw new ArgumentNullException("sourceFileName");
      if (destinationFileName == null)
        throw new ArgumentNullException("destinationFileName");
      File.InternalReplace(sourceFileName, destinationFileName, destinationBackupFileName, false);
    }

    /// <summary>
    /// Replaces the contents of a specified file with the contents of another file, deleting the original file, and creating a backup of the replaced file and optionally ignores merge errors.
    /// </summary>
    /// <param name="sourceFileName">The name of a file that replaces the file specified by <paramref name="destinationFileName"/>.</param><param name="destinationFileName">The name of the file being replaced.</param><param name="destinationBackupFileName">The name of the backup file.</param><param name="ignoreMetadataErrors">true to ignore merge errors (such as attributes and access control lists (ACLs)) from the replaced file to the replacement file; otherwise, false. </param><exception cref="T:System.ArgumentException">The path described by the <paramref name="destinationFileName"/> parameter was not of a legal form.-or-The path described by the <paramref name="destinationBackupFileName"/> parameter was not of a legal form.</exception><exception cref="T:System.ArgumentNullException">The <paramref name="destinationFileName"/> parameter is null.</exception><exception cref="T:System.IO.DriveNotFoundException">An invalid drive was specified. </exception><exception cref="T:System.IO.FileNotFoundException">The file described by the current <see cref="T:System.IO.FileInfo"/> object could not be found.-or-The file described by the <paramref name="destinationBackupFileName"/> parameter could not be found. </exception><exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.- or -The <paramref name="sourceFileName"/> and <paramref name="destinationFileName"/> parameters specify the same file.</exception><exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception><exception cref="T:System.PlatformNotSupportedException">The operating system is Windows 98 Second Edition or earlier and the files system is not NTFS.</exception><exception cref="T:System.UnauthorizedAccessException">The <paramref name="sourceFileName"/> or <paramref name="destinationFileName"/> parameter specifies a file that is read-only.-or- This operation is not supported on the current platform.-or- Source or destination parameters specify a directory instead of a file.-or- The caller does not have the required permission.</exception><filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
    public static void Replace(string sourceFileName, string destinationFileName, string destinationBackupFileName, bool ignoreMetadataErrors)
    {
      if (sourceFileName == null)
        throw new ArgumentNullException("sourceFileName");
      if (destinationFileName == null)
        throw new ArgumentNullException("destinationFileName");
      File.InternalReplace(sourceFileName, destinationFileName, destinationBackupFileName, ignoreMetadataErrors);
    }

    [SecurityCritical]
    internal static int FillAttributeInfo(string path, ref Win32Native.WIN32_FILE_ATTRIBUTE_DATA data, bool tryagain, bool returnErrorOnNotFound)
    {
      int num = 0;
      if (tryagain)
      {
        Win32Native.WIN32_FIND_DATA wiN32FindData = new Win32Native.WIN32_FIND_DATA();
        string fileName = path.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
        int newMode = Win32Native.SetErrorMode(1);
        try
        {
          bool flag = false;
          SafeFindHandle firstFile = Win32Native.FindFirstFile(fileName, wiN32FindData);
          try
          {
            if (firstFile.IsInvalid)
            {
              flag = true;
              num = Marshal.GetLastWin32Error();
              switch (num)
              {
                case 2:
                case 3:
                case 21:
                  if (!returnErrorOnNotFound)
                  {
                    num = 0;
                    data.fileAttributes = -1;
                    break;
                  }
                  break;
              }
              return num;
            }
          }
          finally
          {
            try
            {
              firstFile.Close();
            }
            catch
            {
              if (!flag)
                __Error.WinIOError();
            }
          }
        }
        finally
        {
          Win32Native.SetErrorMode(newMode);
        }
        data.PopulateFrom(wiN32FindData);
      }
      else
      {
        bool flag = false;
        int newMode = Win32Native.SetErrorMode(1);
        try
        {
          flag = Win32Native.GetFileAttributesEx(path, 0, ref data);
        }
        finally
        {
          Win32Native.SetErrorMode(newMode);
        }
        if (!flag)
        {
          num = Marshal.GetLastWin32Error();
          switch (num)
          {
            case 2:
            case 3:
            case 21:
              if (!returnErrorOnNotFound)
              {
                num = 0;
                data.fileAttributes = -1;
                break;
              }
              break;
            default:
              return File.FillAttributeInfo(path, ref data, true, returnErrorOnNotFound);
          }
        }
      }
      return num;
    }

    [SecurityCritical]
    private static string InternalReadAllText(string path, Encoding encoding, bool checkHost)
    {
      using (StreamReader streamReader = new StreamReader(path, encoding, true, 1024, checkHost))
        return streamReader.ReadToEnd();
    }

    [SecurityCritical]
    private static void InternalWriteAllText(string path, string contents, Encoding encoding, bool checkHost)
    {
      using (StreamWriter streamWriter = new StreamWriter(path, false, encoding, 1024, checkHost))
        streamWriter.Write(contents);
    }

    [SecurityCritical]
    private static void InternalWriteAllBytes(string path, byte[] bytes, bool checkHost)
    {
      using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Read, 4096, FileOptions.None, Path.GetFileName(path), false, false, checkHost))
        fileStream.Write(bytes, 0, bytes.Length);
    }

    private static string[] InternalReadAllLines(string path, Encoding encoding)
    {
      List<string> list = new List<string>();
      using (StreamReader streamReader = new StreamReader(path, encoding))
      {
        string str;
        while ((str = streamReader.ReadLine()) != null)
          list.Add(str);
      }
      return list.ToArray();
    }

    private static void InternalWriteAllLines(TextWriter writer, IEnumerable<string> contents)
    {
      using (writer)
      {
        foreach (string str in contents)
          writer.WriteLine(str);
      }
    }

    private static void InternalAppendAllText(string path, string contents, Encoding encoding)
    {
      using (StreamWriter streamWriter = new StreamWriter(path, true, encoding))
        streamWriter.Write(contents);
    }

    [SecuritySafeCritical]
    private static void InternalReplace(string sourceFileName, string destinationFileName, string destinationBackupFileName, bool ignoreMetadataErrors)
    {
      string fullPathInternal1 = Path.GetFullPathInternal(sourceFileName);
      string fullPathInternal2 = Path.GetFullPathInternal(destinationFileName);
      string str = (string) null;
      if (destinationBackupFileName != null)
        str = Path.GetFullPathInternal(destinationBackupFileName);
      FileIOPermission fileIoPermission = new FileIOPermission(FileIOPermissionAccess.Read | FileIOPermissionAccess.Write, new string[2]
      {
        fullPathInternal1,
        fullPathInternal2
      });
      if (destinationBackupFileName != null)
        fileIoPermission.AddPathList(FileIOPermissionAccess.Write, str);
      fileIoPermission.Demand();
      int dwReplaceFlags = 1;
      if (ignoreMetadataErrors)
        dwReplaceFlags |= 2;
      if (Win32Native.ReplaceFile(fullPathInternal2, fullPathInternal1, str, dwReplaceFlags, IntPtr.Zero, IntPtr.Zero))
        return;
      __Error.WinIOError();
    }

    [SecurityCritical]
    private static FileStream OpenFile(string path, FileAccess access, out SafeFileHandle handle)
    {
      FileStream fileStream = new FileStream(path, FileMode.Open, access, FileShare.ReadWrite, 1);
      handle = fileStream.SafeFileHandle;
      if (handle.IsInvalid)
      {
        int errorCode = Marshal.GetLastWin32Error();
        string fullPathInternal = Path.GetFullPathInternal(path);
        if (errorCode == 3 && fullPathInternal.Equals(Directory.GetDirectoryRoot(fullPathInternal)))
          errorCode = 5;
        __Error.WinIOError(errorCode, path);
      }
      return fileStream;
    }
  }
}
