// @copyright Copyright © 2024 Reactional Music Group AB. All rights reserved.
namespace Reactional.Core
{

  using System;
  using System.Runtime.InteropServices;
  using System.Collections.Generic;
  using System.Text;
  using System.Diagnostics;

  /// <summary>
  ///   Contains all native declarations.
  /// </summary>
  public class Native
  {

    //
    // *** Constants ***
    //

#if UNITY_IOS && !UNITY_EDITOR
    /// <summary>
    ///   In Unity IOS builds native code is baked.
    /// </summary>
    public const string DLL_NAME = "__Internal";
#else
    /// <summary>
    ///   The native code.
    /// </summary>
    public const string DLL_NAME = "ge_timeline";
#endif

    /// <summary>
    ///   Generic error.
    /// </summary>
    public const int Error = -1;

    /// <summary>
    ///   Out of memory.
    /// </summary>
    public const int ErrorNoMem = -2;

    /// <summary>
    ///   Entity not found.
    /// </summary>
    public const int ErrorNoEnt = -3;

    /// <summary>
    ///   Integer overflow.
    /// </summary>
    public const int ErrorOverflow = -4;

    /// <summary>
    ///   Duplicate.
    /// </summary>
    public const int ErrorDup = -5;

    /// <summary>
    ///   Busy.
    /// </summary>
    public const int ErrorBusy = -6;

    /// <summary>
    ///   Unsupported operation.
    /// </summary>
    public const int ErrorUnsupported = -7;

    /// <summary>
    ///   Queue full.
    /// </summary>
    public const int ErrorFull = -8;

    /// <summary>
    ///   Invalid value.
    /// </summary>
    public const int ErrorValue = -20;

    /// <summary>
    ///   Invalid value type.
    /// </summary>
    public const int ErrorValueType = -21;

    /// <summary>
    ///   Value conversion failed.
    /// </summary>
    public const int ErrorValueConversion = -22;

    /// <summary>
    ///   The component ID is invalid.
    /// </summary>
    public const int ErrorInvalidID = -30;

    /// <summary>
    ///   Error on read or write operation.
    /// </summary>
    public const int ErrorInvalidIO = -31;

    /// <summary>
    ///   Serialization or deserialization error.
    /// </summary>
    public const int ErrorSerial = -40;

    /// <summary>
    ///   Not enough bytes to complete OSC operation.
    /// </summary>
    public const int ErrorOSCSize = -50;

    /// <summary>
    ///   Invalid OSC type.
    /// </summary>
    public const int ErrorOSCType = -51;

    /// <summary>
    ///   Invalid OSC type.
    /// </summary>
    public const int ErrorOSCTypetag = -52;

    /// <summary>
    ///   Invalid OSC address.
    /// </summary>
    public const int ErrorOSCAddress = -53;

    /// <summary>
    ///   OSC data overflow.
    /// </summary>
    public const int ErrorOSCOverflow = -54;

    /// <summary>
    ///   OSC message was not handled.
    /// </summary>
    public const int ErrorOSCUnhandled = -55;

    /// <summary>
    ///   OSC values are invalid.
    /// </summary>
    public const int ErrorOSCValues = -56;

    /// <summary>
    ///   You may have found a bug!
    /// </summary>
    public const int ErrorBug = -666;

    /// <summary>
    ///   Used by the 's', 'b' and 'S' types.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct PointerValue
    {

      /// <summary>
      ///   The size of the unmanaged data, for strings this does not include the zero terminator.
      /// </summary>
      public Int32 size;

      /// <summary>
      ///   A pointer to the unmanaged memory.
      /// </summary>
      public IntPtr ptr;

    };

    /// <summary>
    ///   Used to transfer MIDI data over OSC.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct MIDIValue
    {

      /// <summary>
      ///   The status byte.
      /// </summary>
      public byte status;

      /// <summary>
      ///   The first data byte.
      /// </summary>
      public byte data1;

      /// <summary>
      ///   The second data byte.
      /// </summary>
      public byte data2;

      /// <summary>
      ///   Padding.
      /// </summary>
      public byte pad;

    };

    /// <summary>
    ///   Used to encode/decode OSC values.
    /// </summary>
    [System.Runtime.InteropServices.StructLayout(LayoutKind.Explicit)]
    public struct OSCValue
    {

      /// <summary>
      ///   Used by type 'i'.
      /// </summary>
      [System.Runtime.InteropServices.FieldOffset(0)]
      public Int32 i;

      /// <summary>
      ///   Used by type 'f'.
      /// </summary>
      [System.Runtime.InteropServices.FieldOffset(0)]
      public Single f;

      /// <summary>
      ///   Used by type 's'.
      /// </summary>
      [System.Runtime.InteropServices.FieldOffset(0)]
      public PointerValue s;

      /// <summary>
      ///   Used by type 'b'.
      /// </summary>
      [System.Runtime.InteropServices.FieldOffset(0)]
      public PointerValue b;

      /// <summary>
      ///   Used by type 'h'.
      /// </summary>
      [System.Runtime.InteropServices.FieldOffset(0)]
      public Int64 h;

      /// <summary>
      ///   Used by type 't'.
      /// </summary>
      [System.Runtime.InteropServices.FieldOffset(0)]
      public UInt64 t;

      /// <summary>
      ///   Used by type 'd'.
      /// </summary>
      [System.Runtime.InteropServices.FieldOffset(0)]
      public Double d;

      /// <summary>
      ///   Used by type 'S'.
      /// </summary>
      [System.Runtime.InteropServices.FieldOffset(0)]
      public PointerValue S;

      /// <summary>
      ///   Used by type 'c'.
      /// </summary>
      [System.Runtime.InteropServices.FieldOffset(0)]
      public byte c;

      /// <summary>
      ///   Used by type 'r'.
      /// </summary>
      [System.Runtime.InteropServices.FieldOffset(0)]
      public UInt32 r;

      /// <summary>
      ///   Used by type 'm'.
      /// </summary>
      [System.Runtime.InteropServices.FieldOffset(0)]
      public MIDIValue m;

      /// <summary>
      ///   Used by type 'T' and 'F'.
      /// </summary>
      [System.Runtime.InteropServices.FieldOffset(0)]
      public byte TF;

    };

    //
    // *** Utils
    //

    /// <summary>
    ///   The callback used by the unmanaged code.
    /// </summary>
    /// <param name="logger">
    ///   A pointer to the logger that issued the message.
    /// </param>
    /// <param name="message">
    ///   A pointer to the string message.
    /// </param>
    /// <param name="size">
    ///   The length of the message excluding the zero terminator.
    /// </param>
    public delegate void reactional_log_callback_func(IntPtr message, int size);

    /// <summary>
    ///   Get an error string from an error code.
    /// </summary>
    /// <param name="err_code">
    ///   The error code.
    /// </param>
    /// <returns>
    ///   A pointer to a human readable error string.
    /// </returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr reactional_string_error(int err_code);

    /// <summary>
    ///   Set the log callback function.
    /// </summary>
    /// <param name="cb">
    ///   The callback function.
    /// </param>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern void reactional_set_log_callback(reactional_log_callback_func cb);

    /// <summary>
    ///   Set the log level.
    /// </summary>
    /// <param name="level">
    ///   The log level.
    /// </param>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern void reactional_set_log_level(int level);

    /// <summary>
    ///   Get the version of the library.
    /// </summary>
    /// <returns>
    ///   A pointer to the version string formatted as "major.minor.patch".
    /// </returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr reactional_get_version();

    /// <summary>
    ///   Get the Git revision of the library.
    /// </summary>
    /// <returns>
    ///   A pointer to the Git reversion string.
    /// </returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr reactional_get_git_revision();

    /// <summary>
    ///   Get the build type of the library.
    /// </summary>
    /// <returns>
    ///   A pointer to the build type string, something like "Debug" or "Release".
    /// </returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr reactional_get_build_type();

    //
    // *** Setup ***
    //

    /// <summary>
    ///   Create a new Reactional Engine instance.
    /// </summary>
    /// <returns>
    ///   A pointer to the instance on success or NULL on error.
    /// </returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr reactional_new();

    /// <summary>
    ///   Add a Reactional Track from a file.
    /// </summary>
    /// <param name="engine">
    ///   The engine.
    /// </param>
    /// <param name="path">
    ///   The path to the file.
    /// </param>
    /// <param name="key">
    ///   The decryption key.
    /// </param>
    /// <param name="key_size">
    ///   The size of the decryption key.
    /// </param>
    /// <returns>
    ///   A Track ID on success or a negative error code.
    /// </returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern int reactional_add_track_from_path
    (
      IntPtr engine,
      [MarshalAs(UnmanagedType.LPStr)] string path,
      byte[] key,
      int key_size
    );

    /// <summary>
    ///   Add a Reactional Track from a track format string.
    /// </summary>
    /// <param name="engine">
    ///   The engine.
    /// </param>
    /// <param name="json_str">
    ///   Reactional Track as a JSON formatted string.
    /// </param>
    /// <param name="key">
    ///   The decryption key.
    /// </param>
    /// <param name="key_size">
    ///   The size of the decryption key.
    /// </param>
    /// <returns>
    ///   A Track ID on success or a negative error code.
    /// </returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern int reactional_add_track_from_string
    (
      IntPtr engine,
      [MarshalAs(UnmanagedType.LPStr)] string json_str,
      int size,
      byte[] key,
      int key_size
    );

    /// <summary>
    ///   Free a Reactional Engine instance.
    /// </summary>
    /// <param name="engine">
    ///   The instance to free.
    /// </param>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern void reactional_free
    (
      IntPtr engine
    );

    /// <summary>
    ///   Reset the engine.
    /// </summary>
    /// <param name="engine">
    ///   The engine.
    /// </param>
    /// <param name="id">
    ///   The track ID.
    /// </param>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern void reactional_reset
    (
      IntPtr engine
    );

    /// <summary>
    ///   Reset the Reactional track to be processed.
    /// </summary>
    /// <param name="engine">
    ///   The engine.
    /// </param>
    /// <param name="id">
    ///   The track ID.
    /// </param>
    /// <returns>
    ///   0 on success or a negative error code.
    /// </returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern int reactional_reset_track
    (
      IntPtr engine,
      int id
    );

    /// <summary>
    ///   Set the Reactional track to be processed.
    /// </summary>
    /// <param name="engine">
    ///   The engine.
    /// </param>
    /// <param name="id">
    ///   The track ID.
    /// </param>
    /// <returns>
    ///   0 on success or a negative error code.
    /// </returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern int reactional_set_track
    (
      IntPtr engine,
      int id
    );

    /// <summary>
    ///   Set the Reactional theme to be processed.
    /// </summary>
    /// <param name="engine">
    ///   The engine.
    /// </param>
    /// <param name="id">
    ///   The track ID.
    /// </param>
    /// <returns>
    ///   0 on success or a negative error code.
    /// </returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern int reactional_set_theme
    (
      IntPtr engine,
      int id
    );

    /// <summary>
    ///   Unset the previously set Reactional theme.
    /// </summary>
    /// <param name="engine">
    ///   The engine.
    /// </param>
    /// <returns>
    ///   0 on success or a negative error code.
    /// </returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern int reactional_unset_theme
    (
      IntPtr engine
    );

    /// <summary>
    ///   Get the ID of the current Reactional Track.
    /// </summary>
    /// <param name="engine">
    ///   The engine.
    /// </param>
    /// <returns>
    ///   The track ID or a negative error code if no track is currently set.
    /// </returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern int reactional_get_track
    (
      IntPtr engine
    );

    /// <summary>
    ///   Unset the previously set Reactional track.
    /// </summary>
    /// <param name="engine">
    ///   The engine.
    /// </param>
    /// <returns>
    ///   0 on success or a negative error code.
    /// </returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern int reactional_unset_track
    (
      IntPtr engine
    );

    /// <summary>
    ///   Get the ID of the current Reactional Theme.
    /// </summary>
    /// <param name="engine">
    ///   The engine.
    /// </param>
    /// <returns>
    ///   The track ID or a negative error code if no theme is currently set.
    /// </returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern int reactional_get_theme
    (
      IntPtr engine
    );

    /// <summary>
    ///   Remove a track from the engine.
    /// </summary>
    /// <param name="engine">
    ///   The engine.
    /// </param>
    /// <param name="id">
    ///   The track ID.
    /// </param>
    /// <returns>
    ///   0 on success or a negative error code.
    /// </returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern int reactional_remove_track
    (
      IntPtr engine,
      int id
    );

    /// <summary>
    ///   Get the number of tracks added to the Engine.
    /// </summary>
    /// <param name="engine">
    ///   The engine.
    /// </param>
    /// <returns>
    ///   The number of tracks added to the Engine.
    /// </returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern int reactional_get_num_tracks
    (
      IntPtr engine
    );

    //
    // *** Introspection ***
    //

    /// <summary>
    ///   Get the number of parameters the track has.
    /// </summary>
    /// <param name="engine">
    ///   The engine.
    /// </param>
    /// <param name="id">
    ///   The track ID.
    /// </param>
    /// <returns>
    ///   The number of parameters.
    /// </returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern int reactional_get_num_params(IntPtr engine, int id);

    /// <summary>
    ///   Get the the type a parameter is.
    /// </summary>
    /// <param name="engine">
    ///   The engine.
    /// </param>
    /// <param name="id">
    ///   The track ID.
    /// </param>
    /// <param name="param_index">
    ///   The index of the parameter.
    /// </param>
    /// <returns>
    ///   The parameter type.
    /// </returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern int reactional_get_param_type(IntPtr engine, int id, int param_index);

    /// <summary>
    ///   Get the the name of a parameter.
    /// </summary>
    /// <param name="engine">
    ///   The engine.
    /// </param>
    /// <param name="id">
    ///   The track ID.
    /// </param>
    /// <param name="param_index">
    ///   The index of the parameter.
    /// </param>
    /// <returns>
    ///   A pointer to the parameter name.
    /// </returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr reactional_get_param_name(IntPtr engine, int id, int param_index);

    /// <summary>
    ///   Find a parameter from name.
    /// </summary>
    /// <param name="engine">
    ///   The engine.
    /// </param>
    /// <param name="id">
    ///   The track ID.
    /// </param>
    /// <param name="param_name">
    ///   The name of the parameter to find.
    /// </param>
    /// <returns>
    ///   The index of the parameter or a negative value if the parameter name was not found.
    /// </returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern int reactional_find_param(IntPtr engine, int id, [MarshalAs(UnmanagedType.LPStr)] string param_name);

    /// <summary>
    ///   Get a boolean value from a parameter.
    /// </summary>
    /// <param name="engine">
    ///   The engine.
    /// </param>
    /// <param name="id">
    ///   The track ID.
    /// </param>
    /// <param name="param_index">
    ///   The index of the parameter.
    /// </param>
    /// <param name="value">
    ///   Store the value here.
    /// </param>
    /// <returns>
    ///   0 on success or a negative error code on failure.
    /// </returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern int reactional_get_param_bool(IntPtr engine, int id, int param_index, ref bool value);

    /// <summary>
    ///   Set a boolean value for a parameter.
    /// </summary>
    /// <param name="engine">
    ///   The engine.
    /// </param>
    /// <param name="id">
    ///   The track ID.
    /// </param>
    /// <param name="param_index">
    ///   The index of the parameter.
    /// </param>
    /// <param name="value">
    ///   The new value.
    /// </param>
    /// <returns>
    ///   0 on sucess or a negative error code on failure.
    /// </returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern int reactional_set_param_bool(IntPtr engine, int id, int param_index, bool value);

    /// <summary>
    ///   Get an integer value from a parameter.
    /// </summary>
    /// <param name="engine">
    ///   The engine.
    /// </param>
    /// <param name="id">
    ///   The track ID.
    /// </param>
    /// <param name="param_index">
    ///   The index of the parameter.
    /// </param>
    /// <param name="value">
    ///   Store the value here.
    /// </param>
    /// <returns>
    ///   0 on success or a negative error code on failure.
    /// </returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern int reactional_get_param_int(IntPtr engine, int id, int param_index, ref long value);

    /// <summary>
    ///   Set an integer value for a parameter.
    /// </summary>
    /// <param name="engine">
    ///   The engine.
    /// </param>
    /// <param name="id">
    ///   The track ID.
    /// </param>
    /// <param name="param_index">
    ///   The index of the parameter.
    /// </param>
    /// <param name="value">
    ///   The new value.
    /// </param>
    /// <returns>
    ///   0 on sucess or a negative error code on failure.
    /// </returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern int reactional_set_param_int(IntPtr engine, int id, int param_index, long value);

    /// <summary>
    ///   Get a floating point value from a parameter.
    /// </summary>
    /// <param name="engine">
    ///   The engine.
    /// </param>
    /// <param name="id">
    ///   The track ID.
    /// </param>
    /// <param name="param_index">
    ///   The index of the parameter.
    /// </param>
    /// <param name="value">
    ///   Store the value here.
    /// </param>
    /// <returns>
    ///   0 on success or a negative error code on failure.
    /// </returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern int reactional_get_param_float(IntPtr engine, int id, int param_index, ref double value);

    /// <summary>
    ///   Set a floating point value for a parameter.
    /// </summary>
    /// <param name="engine">
    ///   The engine.
    /// </param>
    /// <param name="id">
    ///   The track ID.
    /// </param>
    /// <param name="param_index">
    ///   The index of the parameter.
    /// </param>
    /// <param name="value">
    ///   The new value.
    /// </param>
    /// <returns>
    ///   0 on sucess or a negative error code on failure.
    /// </returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern int reactional_set_param_float(IntPtr engine, int id, int param_index, double value);

    /// <summary>
    ///   Get a string value from a parameter.
    /// </summary>
    /// <param name="engine">
    ///   The engine.
    /// </param>
    /// <param name="id">
    ///   The track ID.
    /// </param>
    /// <param name="param_index">
    ///   The index of the parameter.
    /// </param>
    /// <param name="value">
    ///   Store the value here.
    /// </param>
    /// <returns>
    ///   0 on success or a negative error code on failure.
    /// </returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern int reactional_get_param_string(IntPtr engine, int id, int param_index, IntPtr value, int n);

    /// <summary>
    ///   Set a string value for a parameter.
    /// </summary>
    /// <param name="engine">
    ///   The engine.
    /// </param>
    /// <param name="id">
    ///   The track ID.
    /// </param>
    /// <param name="param_index">
    ///   The index of the parameter.
    /// </param>
    /// <param name="value">
    ///   The new value.
    /// </param>
    /// <returns>
    ///   0 on sucess or a negative error code on failure.
    /// </returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern int reactional_set_param_string(IntPtr engine, int id, int param_index, [MarshalAs(UnmanagedType.LPStr)] string value);

    /// <summary>
    ///   Trigger a parameter.
    /// </summary>
    /// <param name="engine">
    ///   The engine.
    /// </param>
    /// <param name="id">
    ///   The track ID.
    /// </param>
    /// <param name="param_index">
    ///   The index of the parameter.
    /// </param>
    /// <returns>
    ///   0 on success or a negative error code on failure.
    /// </returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern int reactional_param_trig(IntPtr engine, int id, int param_index);

    //
    // *** Controls
    //

    /// <summary>
    ///   Get the number of controls the engine has.
    /// </summary>
    /// <param name="engine">
    ///   The engine.
    /// </param>
    /// <param name="id">
    ///   The track ID.
    /// </param>
    /// <returns>
    ///   The number of controls.
    /// </returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern int reactional_get_num_controls(IntPtr engine, int id);

    /// <summary>
    ///   Get the the name of a control.
    /// </summary>
    /// <param name="engine">
    ///   The engine.
    /// </param>
    /// <param name="id">
    ///   The track ID.
    /// </param>
    /// <param name="control_index">
    ///   The index of the control.
    /// </param>
    /// <returns>
    ///   A pointer to the control name.
    /// </returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr reactional_get_control_name(IntPtr engine, int id, int control_index);

    /// <summary>
    ///   Get the the description of a control.
    /// </summary>
    /// <param name="engine">
    ///   The engine.
    /// </param>
    /// <param name="id">
    ///   The track ID.
    /// </param>
    /// <param name="control_index">
    ///   The index of the control.
    /// </param>
    /// <returns>
    ///   A pointer to the control name.
    /// </returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr reactional_get_control_description(IntPtr engine, int id, int control_index);

    /// <summary>
    ///   Find a control from name.
    /// </summary>
    /// <param name="engine">
    ///   The engine.
    /// </param>
    /// <param name="id">
    ///   The track ID.
    /// </param>
    /// <param name="control_name">
    ///   The name of the control to find.
    /// </param>
    /// <returns>
    ///   The index of the control or a negative value if the control name was not found.
    /// </returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern int reactional_find_control(IntPtr engine, int id, [MarshalAs(UnmanagedType.LPStr)] string control_name);

    /// <summary>
    ///   Get the value of a control (normalized 0 to 1).
    /// </summary>
    /// <param name="engine">
    ///   The engine.
    /// </param>
    /// <param name="id">
    ///   The track ID.
    /// </param>
    /// <param name="control_index">
    ///   The index of the control.
    /// </param>
    /// <param name="value">
    ///   The value of the control.
    /// </param>
    /// <returns>
    ///   The value of the control (normalized 0 to 1).
    ///   If the control index is out of bounds the function returns 0.
    /// </returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern double reactional_get_control_value(IntPtr engine, int id, int control_index);

    /// <summary>
    ///   Set the value of a control (normalized 0 to 1).
    /// </summary>
    /// <param name="engine">
    ///   The engine.
    /// </param>
    /// <param name="id">
    ///   The track ID.
    /// </param>
    /// <param name="control_index">
    ///   The index of the control.
    /// </param>
    /// <param name="value">
    ///   The new value, will be clamped between 0 and 1.
    /// </param>
    /// <returns>
    ///   0 on sucess or a negative error code if the control index is out of bounds.
    /// </returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern int reactional_set_control_value(IntPtr engine, int id, int control_index, double value);

    /// <summary>
    ///   Set the control value array.
    /// </summary>
    /// <param name="engine">
    ///   The engine.
    /// </param>
    /// <param name="id">
    ///   The track ID.
    /// </param>
    /// <param name="control_index">
    ///   The index of the control.
    /// </param>
    /// <param name="values">
    ///   The values to set.
    /// </param>
    /// <param name="num_values">
    ///   The number of values to set.
    /// </param>
    /// <returns>
    ///   0 on sucess or a negative error code if the control index is out of bounds.
    /// </returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern int reactional_set_control_value_array(IntPtr engine, int id, int control_index, double[] values, int num_values);

    /// <summary>
    ///   Get the control value array.
    /// </summary>
    /// <param name="engine">
    ///   The engine.
    /// </param>
    /// <param name="id">
    ///   The track ID.
    /// </param>
    /// <param name="control_index">
    ///   The index of the control.
    /// </param>
    /// <param name="values">
    ///   The values to get.
    /// </param>
    /// <param name="num_values">
    ///   The number of values to get.
    /// </param>
    /// <returns>
    ///   0 on sucess or a negative error code if the control index is out of bounds.
    /// </returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern int reactional_get_control_value_array(IntPtr engine, int id, int control_index, ref double[] values, int num_values);

    /// <summary>
    ///   Get the length of the control value array.
    /// </summary>
    /// <param name="engine">
    ///   The engine.
    /// </param>
    /// <param name="id">
    ///   The track ID.
    /// </param>
    /// <param name="control_index">
    ///   The index of the control.
    /// </param>
    /// <returns>
    ///   The size of the value array or a negative error code.
    /// </returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern int reactional_get_control_value_array_size(IntPtr engine, int id, int control_index);

    /// <summary>
    ///   Get the the type of a control.
    /// </summary>
    /// <param name="engine">
    ///   The engine.
    /// </param>
    /// <param name="id">
    ///   The track ID.
    /// </param>
    /// <param name="control_index">
    ///   The index of the control.
    /// </param>
    /// <returns>
    ///   A pointer to the control type.
    /// </returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr reactional_get_control_type(IntPtr engine, int id, int control_index);

    /// <summary>
    ///   Get the the level of a control.
    /// </summary>
    /// <param name="engine">
    ///   The engine.
    /// </param>
    /// <param name="id">
    ///   The track ID.
    /// </param>
    /// <param name="control_index">
    ///   The index of the control.
    /// </param>
    /// <returns>
    ///   A pointer to the control level.
    /// </returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr reactional_get_control_level(IntPtr engine, int id, int control_index);

    //
    // *** Main thread processing.
    //

    /// <summary>
    ///   Process the engine.
    /// </summary>
    /// <param name="engine">
    ///   The engine.
    /// </param>
    /// <param name="time">
    ///   The current system time in microseconds or a negative value to let the
    ///   unmanaged code query the system.
    /// </param>
    /// <returns>
    ///   0 on sucess or a negative error code on failure.
    /// </returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern int reactional_process(IntPtr engine, long time);


    //
    // *** Result, optionally in other thread.
    //

    /// <summary>
    ///   Render an audio stream in a planar layout.
    ///   <para>
    ///     In a planar layout each buffer represents a channel.
    ///   </para>
    /// </summary>
    /// <param name="engine">
    ///   The engine.
    /// </param>
    /// <param name="sampleRate">
    ///   The sample rate.
    /// </param>
    /// <param name="numFrames">
    ///   The number of frames (i.e samples) to render.
    /// </param>
    /// <param name="numChannels">
    ///   The number of channels to render (i.e the number of buffers).
    /// </param>
    /// <param name="buffers">
    ///   An array of float arrays.
    /// </param>
    /// <returns>
    ///   0 on sucess or a negative error code on failure.
    /// </returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern int reactional_render_planar(IntPtr engine, double sampleRate, int numFrames, int numChannels, float[][] buffers);

    /// <summary>
    ///   Render an audio stream in an interleaved layout.
    ///   <para>
    ///     In an interleaved layout a single buffer holds a sample for all
    ///     channel in sequence before the next sample. Example for 3 channels [c0, c1, c2, c0, c1, c2, ...].
    ///   </para>
    /// </summary>
    /// <param name="engine">
    ///   The engine.
    /// </param>
    /// <param name="sampleRate">
    ///   The sample rate.
    /// </param>
    /// <param name="numFrames">
    ///   The number of frames (i.e samples) to render.
    /// </param>
    /// <param name="numChannels">
    ///   The number of channels to render (i.e the number of buffers).
    /// </param>
    /// <param name="buffer">
    ///   An array of floats.
    /// </param>
    /// <returns>
    ///   0 on sucess or a negative error code on failure.
    /// </returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern int reactional_render_interleaved(IntPtr engine, double sampleRate, int numFrames, int numChannels, float[] buffer);

    /// <summary>
    ///   Push an OSC event to the track.
    /// </summary>
    /// <param name="engine">
    ///   The engine.
    /// </param>
    /// <param name="id">
    ///   The track ID.
    /// </param>
    /// <param name="microbeats">
    ///   The absolute offset in microbeats for scheduling the event.
    /// </param>
    /// <param name="osc">
    ///   The OSC data.
    /// </param>
    /// <param name="size">
    ///   Read at most this many bytes from the OSC data.
    /// </param>
    /// <returns>
    ///   The number of available events on sucess or a negative error code on failure.
    /// </returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern int reactional_event_push(IntPtr engine, int id, Int64 microbeats, byte[] osc, int size);

    /// <summary>
    ///   Syncronize memory with the processing and retrieve the number of available events.
    /// </summary>
    /// <param name="engine">
    ///   The engine.
    /// </param>
    /// <param name="id">
    ///   The track ID.
    /// </param>
    /// <param name="startBeat">
    ///   The starting beat, used to get a relative beat offset for an
    ///   event by subtracting this value.
    /// </param>
    /// <returns>
    ///   The number of available events on sucess or a negative error code on failure.
    /// </returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern int reactional_event_poll_begin(IntPtr engine, int id, ref Int64 startBeat);

    /// <summary>
    ///   Poll OSC data.
    /// </summary>
    /// <param name="engine">
    ///   The engine.
    /// </param>
    /// <param name="id">
    ///   The track ID.
    /// </param>
    /// <param name="index">
    ///   The index of the event to poll where 0 is the nearest in time.
    ///   Make sure this value does not exceed the number of available
    ///   events - 1.
    /// </param>
    /// <param name="size">
    ///   Store the size of the OSC data here.
    /// </param>
    /// <returns>
    ///   A pointer to the OSC data on sucess or NULL on failure.
    /// </returns>
    /// <remarks>
    ///   This function should only be called after a successful call to
    ///   <see cref="reactional_event_poll_begin"/>.
    /// </remarks>
    /// <remarks>
    ///   The pointer may become invalid after a call to reactional_event_poll_end.
    ///   <see cref="reactional_event_poll_end"/>.
    /// </remarks>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr reactional_event_poll(IntPtr engine, int id, int index, ref int size);

    /// <summary>
    ///   Poll a pointer to an event struct.
    ///   
    /// </summary>
    /// <param name="engine">
    ///   The engine.
    /// </param>
    /// <param name="index">
    ///   The index of the event to poll where 0 is the nearest in time.
    ///   Make sure this value does not exceed the number of available
    ///   events - 1.
    /// </param>
    /// <returns>
    ///   A pointer to the event struct on sucess or NULL on failure.
    /// </returns>
    /// <remarks>
    ///   This function should only be called after a successful call to
    ///   <see cref="reactional_event_poll_begin"/>.
    /// </remarks>
    /// <remarks>
    ///   The pointer may become invalid after a call to reactional_event_poll_end.
    ///   <see cref="reactional_event_poll_end"/>.
    /// </remarks>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr reactional_event_poll_struct(IntPtr engine, int index);

    /// <summary>
    ///   End polling by letting the processing thread know how many events we polled.
    /// </summary>
    /// <param name="engine">
    ///   The engine.
    /// </param>
    /// <param name="id">
    ///   The track ID.
    /// </param>
    /// <param name="numEvents">
    ///   The number of events we polled (or want to discard).
    ///   Make sure this value does not exceed the number of available events.
    /// </param>
    /// <returns>
    ///   The number of ended on sucess or a negative error code on failure.
    /// </returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern int reactional_event_poll_end(IntPtr engine, int id, int numEvents);

    /// <summary>
    ///   Encodes an OSC message from the OSC value unions.
    /// </summary>
    /// <param name="data">The OSC data.</param>
    /// <param name="size">Read at most this many bytes from data.</param>
    /// <param name="address">The address.</param>
    /// <param name="addressSize">Read at most this many bytes from the address.</param>
    /// <param name="typetag">The typetag.</param>
    /// <param name="typetagSize">Read at most this many bytes from the typetag.</param>
    /// <param name="values">An array of value unions to encode.</param>
    /// <param name="numValues">The number of successfully encoded values.</param>
    /// <returns>The number of encoded bytes</returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern Int32 reactional_osc_message_encode
    (
      IntPtr data,
      Int32 size,
      [MarshalAs(UnmanagedType.LPStr)] string address,
      Int32 addressSize,
      [MarshalAs(UnmanagedType.LPStr)] string typetag,
      Int32 typetagSize,
      [MarshalAs(UnmanagedType.LPArray)]
      IntPtr values,
      ref Int32 numValues
    );

    /// <summary>
    ///   Decodes an OSC message from to OSC value unions.
    /// </summary>
    /// <param name="data">The OSC data.</param>
    /// <param name="size">Read at most this many bytes from data.</param>
    /// <param name="address">The address.</param>
    /// <param name="addressSize">Read at most this many bytes from the address.</param>
    /// <param name="typetag">The typetag.</param>
    /// <param name="typetagSize">Read at most this many bytes from the typetag.</param>
    /// <param name="maxValues">Store at most this many values.</param>
    /// <param name="values">An array of value unions to decode.</param>
    /// <param name="numValues">The number of successfully decoded values.</param>
    /// <returns>The number of decoded bytes</returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern Int32 reactional_osc_message_decode
    (
      IntPtr data,
      Int32 size,
      ref IntPtr address,
      ref Int32 addressSize,
      ref IntPtr typetag,
      ref Int32 typetagSize,
      Int32 maxValues,
      IntPtr values,
      ref Int32 numValues
    );

    /// <summary>Convert time to beats.</summary>
    /// <param name="engine"> The engine to do the conversion for.</param>
    /// <param name="id"> The track ID.</param>
    /// <param name="time">The time in seconds.</param>
    /// <returns>The number of beats for the specified time.</returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern Int64 reactional_get_next_quant_beat(IntPtr engine, int id, Int64 quant, Int64 phase);

    /// <summary>Convert time to beats.</summary>
    /// <param name="engine"> The engine to do the conversion for.</param>
    /// <param name="id"> The track ID.</param>
    /// <param name="time">The time in seconds.</param>
    /// <returns>The number of beats for the specified time.</returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern double reactional_get_beats_from_time(IntPtr engine, int id, double time);

    /// <summary>Convert audio frames to beats.</summary>
    /// <param name="engine">The engine to do the conversion for.</param>
    /// <param name="id"> The track ID.</param>
    /// <param name="frames">The number of audio frames.</param>
    /// <returns>The number of beats for the specified number of audio frames.</returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern double reactional_get_beats_from_frames(IntPtr engine, int id, double frames);

    /// <summary>Convert beats to time.</summary>
    /// <param name="engine">The engine to do the conversion for.</param>
    /// <param name="id"> The track ID.</param>
    /// <param name="beats">The beats.</param>
    /// <returns>The time for the specified number of beats.</returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern double reactional_get_time_from_beats(IntPtr engine, int id, double beats);

    /// <summary>Convert audo frames to time.</summary>
    /// <param name="engine">The engine to do the conversion for.</param>
    /// <param name="id"> The track ID.</param>
    /// <param name="frames">The number of audio frames.</param>
    /// <returns>The time for the specified number of audio frames.</returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern double reactional_get_time_from_frames(IntPtr engine, int id, double frames);

    /// <summary>Convert beats to audio frames.</summary>
    /// <param name="engine">The engine to do the conversion for.</param>
    /// <param name="id"> The track ID.</param>
    /// <param name="beats">The beats.</param>
    /// <returns>The number of audio frames from the specified number of beats.</returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern double reactional_get_frames_from_beats(IntPtr engine, int id, double beats);

    /// <summary>Convert time to audio frames.</summary>
    /// <param name="engine">The engine to do the conversion for.</param>
    /// <param name="id"> The track ID.</param>
    /// <param name="time">The time in seconds.</param>
    /// <returns>The number of audio frames from the specified time.</returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern double reactional_get_frames_from_time(IntPtr engine, int id, double time);

    //
    // *** Assets ***
    //

    /// <summary>Get the number of assets the track requires.</summary>
    /// <param name="engine">The engine.</param>
    /// <param name="id"> The track ID.</param>
    /// <returns>The number of assets the engine requires.</returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern int reactional_get_num_assets(IntPtr engine, int id);

    /// <summary>Get the ID of an asset.</summary>
    /// <param name="engine">The engine.</param>
    /// <param name="id"> The track ID.</param>
    /// <param name="index">The index of the asset.</param>
    /// <returns>The ID or NULL if index is out of bounds.</returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr reactional_get_asset_id(IntPtr engine, int id, int index);

    /// <summary>Get the type of an asset.</summary>
    /// <param name="engine">The engine.</param>
    /// <param name="id"> The track ID.</param>
    /// <param name="index">The index of the asset.</param>
    /// <returns>The type or NULL if index is out of bounds.</returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr reactional_get_asset_type(IntPtr engine, int id, int index);

    /// <summary>Get the type of an asset.</summary>
    /// <param name="engine">The engine.</param>
    /// <param name="id"> The track ID.</param>
    /// <param name="index">The index of the asset.</param>
    /// <returns>The type or NULL if index is out of bounds.</returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr reactional_get_asset_uri(IntPtr engine, int id, int index);

    /// <summary>Set the data for an asset.</summary>
    /// <param name="engine">The engine.</param>
    /// <param name="id"> The track ID.</param>
    /// <param name="asset_id">The ID of the asset.</param>
    /// <param name="asset_type">The type of data to set.</param>
    /// <param name="asset_data">The data.</param>
    /// <param name="asset_size">The data size in bytes.</param>
    /// <returns>0 on success or a negative error code on failure.</returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern int reactional_set_asset_data(
      IntPtr engine,
      int id,
      [MarshalAs(UnmanagedType.LPStr)] string asset_id,
      [MarshalAs(UnmanagedType.LPStr)] string asset_type,
      IntPtr asset_data,
      int asset_size,
      IntPtr key,
      int key_size
    );

    /// <summary>Get the number of stingers the engine has.</summary>
    /// <param name="engine">The engine.</param>
    /// <param name="id"> The track ID.</param>
    /// <returns>The number of stingers the engine has.</returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern int reactional_get_num_stingers(IntPtr engine, int id);

    /// <summary>Start a stinger.</summary>
    /// <param name="engine">The engine.</param>
    /// <param name="id"> The track ID.</param>
    /// <param name="stinger_index">The index of the stinger.</param>
    /// <param name="start_offset">Start the stinger on this microbeat.</param>
    /// <param name="behaviour">How to act when a stinger is re-triggered.</param>
    /// <returns>0 on success or a negative error code on failure.</returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern int reactional_stinger_start(IntPtr engine, int id, int stinger_index, Int64 start_offset, int behaviour);

    /// <summary>Get the number of states.</summary>
    /// <param name="engine">The engine.</param>
    /// <param name="id"> The track ID.</param>
    /// <returns>The number of states.</returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern int reactional_get_num_states(IntPtr engine, int id);

    /// <summary>Find a state from a name.</summary>
    /// <param name="engine">The engine.</param>
    /// <param name="id"> The track ID.</param>
    /// <param name="name"> The name to search for.</param>
    /// <returns>The state index or a negative error code on failure.</returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern int reactional_find_state(IntPtr engine, int id, [MarshalAs(UnmanagedType.LPStr)] string name);

    /// <summary>Set a state.</summary>
    /// <param name="engine">The engine.</param>
    /// <param name="id"> The track ID.</param>
    /// <param name="index"> The state index to set.</param>
    /// <param name="lag_multiplier"> A lag multiplier for the control values. If < 0 the default will be used.</param>
    /// <returns>0 on success or a negative error code on failure.</returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern int reactional_set_state(IntPtr engine, int id, int index, double lag_multiplier);

    /// <summary>Get the number of snapshots.</summary>
    /// <param name="engine">The engine.</param>
    /// <param name="id"> The track ID.</param>
    /// <returns>The number of snapshots.</returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern int reactional_get_num_snapshots(IntPtr engine, int id);

    /// <summary>Find a snapshot from a name.</summary>
    /// <param name="engine">The engine.</param>
    /// <param name="id"> The track ID.</param>
    /// <param name="name"> The name to search for.</param>
    /// <returns>The snapshot index or a negative error code on failure.</returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern int reactional_find_snapshot(IntPtr engine, int id, [MarshalAs(UnmanagedType.LPStr)] string name);

    /// <summary>Set a snapshot.</summary>
    /// <param name="engine">The engine.</param>
    /// <param name="id"> The track ID.</param>
    /// <param name="index"> The snapshot index to set.</param>
    /// <param name="lag_multiplier"> A lag multiplier for the control values. If < 0 the default will be used.</param>
    /// <returns>0 on success or a negative error code on failure.</returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern int reactional_set_snapshot(IntPtr engine, int id, int index, double lag_multiplier);

    /// <summary>Fade the current track amplitude.</summary>
    /// <param name="engine">The engine.</param>
    /// <param name="target">The target amplitude.</param>
    /// <param name="beat_offset">The microbeat to start the fade on.</param>
    /// <param name="time_duration">The duration of the fade in microseconds.</param>
    /// <param name="stop_on_finish">Stop the track when the fade has finished.</param>
    /// <returns>0 on success or a negative error code on failure.</returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern int reactional_track_fade(IntPtr engine, float target, Int64 beat_offset, Int64 time_duration, bool stop_on_finish);

    /// <summary>Fade the current theme amplitude.</summary>
    /// <param name="engine">The engine.</param>
    /// <param name="target">The target amplitude.</param>
    /// <param name="beat_offset">The microbeat to start the fade on.</param>
    /// <param name="time_duration">The duration of the fade in microseconds.</param>
    /// <param name="stop_on_finish">Stop the theme when the fade has finished.</param>
    /// <returns>0 on success or a negative error code on failure.</returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern int reactional_theme_fade(IntPtr engine, float target, Int64 beat_offset, Int64 time_duration, bool stop_on_finish);

    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern int reactional_get_channel_amp(IntPtr engine, int track_id, int channel_index, ref double amp);

    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern int reactional_set_channel_amp(IntPtr engine, int track_id, int channel_index, double amp);

    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern int reactional_validate_track(
      byte[] encrypted_json,
      int size,
      byte[] key,
      int key_size,
      StringBuilder buffer,
      int buffer_size
    );

    /// <summary>Get the number of parts in a track.</summary>
    /// <param name="engine">The engine.</param>
    /// <param name="id"> The track ID.</param>
    /// <returns>The number of parts in the track on success or a negative error code on failure.</returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern int reactional_get_num_parts(IntPtr engine, int id);

    /// <summary>Get the current part index at runtime.</summary>
    /// <param name="engine">The engine.</param>
    /// <param name="id"> The track ID.</param>
    /// <returns>The current part index on success or a negative error code on failure.</returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern int reactional_get_current_part(IntPtr engine, int id);

    /// <summary>Get the name of a part in a track.</summary>
    /// <param name="engine">The engine.</param>
    /// <param name="id"> The track ID.</param>
    /// <param name="part_index"> The section index.</param>
    /// <returns>The part name on success or NULL on failure.</returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr reactional_get_part_name(IntPtr engine, int id, int part_index);

    /// <summary>Get the offset of a part in a track.</summary>
    /// <param name="engine">The engine.</param>
    /// <param name="id"> The track ID.</param>
    /// <param name="part_index"> The section index.</param>
    /// <returns>The part offset on success or a negative error code on failure.</returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern Int64 reactional_get_part_offset(IntPtr engine, int id, int part_index);

    /// <summary>Get the duration of a part in a track.</summary>
    /// <param name="engine">The engine.</param>
    /// <param name="id"> The track ID.</param>
    /// <param name="part_index"> The section index.</param>
    /// <returns>The part duration on success or a negative error code on failure.</returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern Int64 reactional_get_part_duration(IntPtr engine, int id, int part_index);

    /// <summary>Get the number of bars in a track.</summary>
    /// <param name="engine">The engine.</param>
    /// <param name="id"> The track ID.</param>
    /// <returns>The number of bars in the track on success or a negative error code on failure.</returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern int reactional_get_num_bars(IntPtr engine, int id);

    /// <summary>Get the current bar index at runtime.</summary>
    /// <param name="engine">The engine.</param>
    /// <param name="id"> The track ID.</param>
    /// <returns>The current bar index on success or a negative error code on failure.</returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern int reactional_get_current_bar(IntPtr engine, int id);

    /// <summary>Get the offset of a bar in a track.</summary>
    /// <param name="engine">The engine.</param>
    /// <param name="id"> The track ID.</param>
    /// <param name="bar_index"> The bar index.</param>
    /// <returns>The bar offset on success or a negative error code on failure.</returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern Int64 reactional_get_bar_offset(IntPtr engine, int id, int bar_index);

    /// <summary>Get the duration of a bar in a track.</summary>
    /// <param name="engine">The engine.</param>
    /// <param name="id"> The track ID.</param>
    /// <param name="bar_index"> The bar index.</param>
    /// <returns>The bar duration on success or a negative error code on failure.</returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern Int64 reactional_get_bar_duration(IntPtr engine, int id, int bar_index);

    /// <summary>Get member data from an event struct returned from <see cref="reactional_event_poll_struct"/>.</summary>
    /// <param name="ev">The event pointer.</param>
    /// <returns>The member value of the event struct.</returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern int reactional_evstruct_get_type(IntPtr ev);

    /// <summary>Get member data from an event struct returned from <see cref="reactional_event_poll_struct"/>.</summary>
    /// <param name="ev">The event pointer.</param>
    /// <returns>The member value of the event struct.</returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern Int64 reactional_evstruct_get_offset(IntPtr ev);

    /// <summary>Get member data from an event struct returned from <see cref="reactional_event_poll_struct"/>.</summary>
    /// <param name="ev">The event pointer.</param>
    /// <returns>The member value of the event struct.</returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern Int64 reactional_evstruct_get_duration(IntPtr ev);

    /// <summary>Get member data from an event struct returned from <see cref="reactional_event_poll_struct"/>.</summary>
    /// <param name="ev">The event pointer.</param>
    /// <returns>The member value of the event struct.</returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern int reactional_evstruct_get_lane_index(IntPtr ev);

    /// <summary>Get member data from an event struct returned from <see cref="reactional_event_poll_struct"/>.</summary>
    /// <param name="ev">The event pointer.</param>
    /// <returns>The member value of the event struct.</returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern int reactional_evstruct_get_sink_index(IntPtr ev);

    /// <summary>Get member data from an event struct returned from <see cref="reactional_event_poll_struct"/>.</summary>
    /// <param name="ev">The event pointer.</param>
    /// <returns>The member value of the event struct.</returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern int reactional_evstruct_get_output_index(IntPtr ev);

    /// <summary>Get member data from an event struct returned from <see cref="reactional_event_poll_struct"/>.</summary>
    /// <param name="ev">The event pointer.</param>
    /// <returns>The member value of the event struct.</returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern int reactional_evstruct_get_priority(IntPtr ev);

    /// <summary>Get member data from an event struct returned from <see cref="reactional_event_poll_struct"/>.</summary>
    /// <param name="ev">The event pointer.</param>
    /// <returns>The member value of the event struct.</returns>
    [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool reactional_evstruct_get_is_theme(IntPtr ev);

    /// <summary>
    ///   Helper to stringify a pointer.
    /// </summary>
    /// <param name="pointer">The pointer pointing to a string.</param>
    /// <returns>The string</returns>
    public static string StringifyPointer(IntPtr pointer)
    {
      return pointer != IntPtr.Zero ? Marshal.PtrToStringAnsi(pointer) : "";
    }

  };

  //
  // *** Engine ***
  //

  /// <summary>
  ///   Base class for track exceptions.
  /// </summary>
  class EngineException : Exception
  {

    /// <summary>
    ///   Basic constructor.
    /// </summary>
    public EngineException()
    {
    }

    /// <summary>
    ///   Basic constructor.
    /// </summary>
    /// <param name="message">The message.</param>
    public EngineException(string message)
    : base(message)
    {
    }

    /// <summary>
    ///   Basic constructor.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <param name="inner">The inner exception.</param>
    public EngineException(string message, Exception inner)
    : base(message, inner)
    {
    }

  };

  /// <summary>
  ///   Thrown if the underlying cpointer is NULL.
  /// </summary>
  class EngineNullException : EngineException
  {

    /// <summary>
    ///   Basic constructor.
    /// </summary>
    public EngineNullException()
    : base("The internal C pointer is NULL.")
    {
    }

    /// <summary>
    ///   Basic constructor.
    /// </summary>
    /// <param name="inner">The inner exception.</param>
    public EngineNullException(Exception inner)
    : base("The internal C pointer is NULL.", inner)
    {
    }

  };

  /// <summary>
  ///   Thrown if the underlying C function returns a C-specific error.
  /// </summary>
  class EngineErrorException : EngineException
  {

    /// <summary>
    ///   The error code.
    /// </summary>
    public int Error = 0;

    /// <summary>
    ///   Create a track error exception providing the C error code.
    /// </summary>
    /// <param name="error">The C error code.</param>
    public EngineErrorException(int error)
    : base(Native.StringifyPointer(Native.reactional_string_error(error)))
    {
      Error = error;
    }

    /// <summary>
    ///   Create a track error exception providing the C error code.
    /// </summary>
    /// <param name="error">The C error code.</param>
    public EngineErrorException(Int64 error)
    : base(Native.StringifyPointer(Native.reactional_string_error((int)error)))
    {
      Error = (int)error;
    }

    /// <summary>
    ///   Basic constructor.
    /// </summary>
    /// <param name="error">The C error code.</param>
    /// <param name="inner">The inner exception.</param>
    public EngineErrorException(int error, Exception inner)
        : base(Native.StringifyPointer(Native.reactional_string_error(error)), inner)
    {
      Error = error;
    }

  };

  /// <summary>
  ///   This is a reactional engine.
  /// </summary>
  public class Engine
  {

    //
    // *** Internal use only ***
    //

    /// <summary>
    ///   The pointer to the unmanaged track.
    /// </summary>
    protected IntPtr _cpointer;

    /// <summary>
    ///   Checks that the C pointer is non-NULL or throw exception <see cref="EngineNullException"/>.
    /// </summary>
    /// <exception cref="EngineNullException">Throws an exception if the pointer is NULL.</exception>
    protected void CheckPointer()
    {
      if (_cpointer == IntPtr.Zero)
        throw new EngineNullException();
    }

    protected static void InternalLogCallback(IntPtr message, int size)
    {
      if (LogCallback != null)
        LogCallback(Marshal.PtrToStringAnsi(message));
    }

    /// <summary>
    ///   The log callback.
    /// </summary>
    protected static LogCallbackDelegate _LogCallback;

    //
    // *** Types ***
    //

    /// <summary>
    ///   The parameter types.
    /// </summary>
    public enum ParamType
    {

      /// <summary>
      ///   Boolean.
      /// </summary>
      Bool,

      /// <summary>
      ///   Signed 64-bit integer.
      /// </summary>
      Int,

      /// <summary>
      ///   A double precision floating point.
      /// </summary>
      Float,

      /// <summary>
      ///   A string.
      /// </summary>
      String,

      /// <summary>
      ///   A trigger.
      /// </summary>
      Trigger

    };

    //
    // *** Enumerations ***
    //

    /// <summary>
    ///   Used to set log level.
    /// </summary>
    public enum LogLevel
    {

      /// <summary>
      ///   No Logging.
      /// </summary>
      None,

      /// <summary>
      ///   Critical.
      /// </summary>
      Critical,

      /// <summary>
      ///   Errors.
      /// </summary>
      Errors,

      /// <summary>
      ///   Warnings.
      /// </summary>
      Warnings,

      /// <summary>
      ///   Info.
      /// </summary>
      Info,

      /// <summary>
      ///   Debug.
      /// </summary>
      Debug

    };

    //
    // *** Non-function members ***
    //

    /// <summary>
    ///   A delegate used for log callbacks.
    /// </summary>
    /// <param name="message">
    ///   The logged message.
    /// </param>
    public delegate void LogCallbackDelegate(string message);

    /// <summary>
    ///   The log callback.
    /// </summary>
    public static LogCallbackDelegate LogCallback
    {
      get => _LogCallback;
      set
      {
        _LogCallback = value;
        if (value != null)
          Native.reactional_set_log_callback(InternalLogCallback);
        else
          Native.reactional_set_log_callback(null);
      }
    }

    //
    // *** Constructor/destructor ***
    //

    /// <summary>
    ///   Create a new engine instance.
    /// </summary>
    /// <exception cref="EngineErrorException">Thrown if the track fails to load.</exception>
    public Engine()
    {
      _cpointer = Native.reactional_new();
      if (_cpointer == IntPtr.Zero)
        throw new EngineErrorException(Native.ErrorNoMem);
    }

    /// <summary>
    ///   Default destructor.
    /// </summary>
    ~Engine()
    {
      Native.reactional_free(_cpointer);
    }

    //
    // *** Tracks/Themes ***
    //

    /// <summary>
    ///   Reset the engine.
    /// </summary>
    public void Reset()
    {
      CheckPointer();
      Native.reactional_reset(_cpointer);
    }

    /// <summary>
    ///   Reset the engine.
    /// </summary>
    /// <param name="path">
    ///   An absolute path to a track.
    /// </param>
    /// <returns>
    ///   0 on success or a negative error code.
    /// </returns>
    public int ResetTrack(int id)
    {
      CheckPointer();
      return Native.reactional_reset_track(_cpointer, id);
    }

    /// <summary>
    ///   Add a track from a file path.
    /// </summary>
    /// <param name="path">
    ///   An absolute path to a track.
    /// </param>
    /// <returns>
    ///   The track ID or a negative error code.
    /// </returns>
    public int AddTrackFromPath(string path, byte[] key = null)
    {
      CheckPointer();
      if (key != null)
        return Native.reactional_add_track_from_path(_cpointer, path, key, key.Length);
      return Native.reactional_add_track_from_path(_cpointer, path, null, -1);
    }

    /// <summary>
    ///   Add a track using a JSON string.
    /// </summary>
    /// <param name="json_str">
    ///   An absolute string to a track.
    /// </param>
    /// <returns>
    ///   The track ID or a negative error code.
    /// </returns>
    public int AddTrackFromString(string json_str, byte[] key = null)
    {
      CheckPointer();
      if (key != null)
        return Native.reactional_add_track_from_string(_cpointer, json_str, json_str.Length, key, key.Length);
      return Native.reactional_add_track_from_string(_cpointer, json_str, json_str.Length, null, -1);
    }

    /// <summary>
    ///   Set the track that will be processed by the Engine.
    /// </summary>
    /// <param name="id">
    ///   The ID of the track.
    /// </param>
    /// <returns>
    ///   0 on success or a negative error code.
    /// </returns>
    public int SetTrack(int id)
    {
      CheckPointer();
      return Native.reactional_set_track(_cpointer, id);
    }

    /// <summary>
    ///   Set the theme that will be processed by the Engine.
    /// </summary>
    /// <param name="id">
    ///   The ID of the track that will be used as a theme.
    /// </param>
    /// <returns>
    ///   0 on success or a negative error code.
    /// </returns>
    public int SetTheme(int id)
    {
      CheckPointer();
      return Native.reactional_set_theme(_cpointer, id);
    }

    /// <summary>
    ///   Unset the track currently processed by the Engine.
    /// </summary>
    /// <returns>
    ///   0 on success or a negative error code.
    /// </returns>
    public int UnsetTrack()
    {
      CheckPointer();
      return Native.reactional_unset_track(_cpointer);
    }

    /// <summary>
    ///   Unset the theme currently processed by the Engine.
    /// </summary>
    /// <returns>
    ///   0 on success or a negative error code.
    /// </returns>
    public int UnsetTheme()
    {
      CheckPointer();
      return Native.reactional_unset_theme(_cpointer);
    }

    /// <summary>
    ///   Get the track that is currently processed by the Engine.
    /// </summary>
    /// <returns>
    ///   The track ID on success or a negative error code if no track is set.
    /// </returns>
    public int GetTrack()
    {
      CheckPointer();
      return Native.reactional_get_track(_cpointer);
    }

    /// <summary>
    ///   Get the theme that is currently processed by the Engine.
    /// </summary>
    /// <returns>
    ///   The track ID on success or a negative error code if no theme is set.
    /// </returns>
    public int GetTheme()
    {
      CheckPointer();
      return Native.reactional_get_theme(_cpointer);
    }

    /// <summary>
    ///   Remove a track from the engine, freeing its resources.
    /// </summary>
    /// <returns>
    ///   0 on success or a negative error code.
    /// </returns>
    public int RemoveTrack(int id)
    {
      CheckPointer();
      return Native.reactional_remove_track(_cpointer, id);
    }

    /// <summary>
    ///   Get the number of tracks added to the engine.
    /// </summary>
    /// <returns>
    ///   The number of tracks added to the engine.
    /// </returns>
    public int GetNumTracks()
    {
      CheckPointer();
      return Native.reactional_get_num_tracks(_cpointer);
    }

    //
    // *** Utils ***
    //

    /// <summary>
    ///   Sets the log level.
    /// </summary>
    /// <param name="level">
    ///   The log level.
    /// </param>
    public static void SetLogLevel(LogLevel level)
    {
      Native.reactional_set_log_level((int)level);
    }

    /// <summary>
    ///   Get the version of the unmanaged code.
    /// </summary>
    /// <returns>
    ///   The version string, formatted as "major.minor.patch".
    /// </returns>
    public static string GetVersion()
    {
      return Native.StringifyPointer(Native.reactional_get_version());
    }

    /// <summary>
    ///   Get the Git reversion of the unmanaged code.
    /// </summary>
    /// <returns>
    ///   The Git reversion string.
    /// </returns>
    public static string GetGitRevision()
    {
      return Native.StringifyPointer(Native.reactional_get_git_revision());
    }

    /// <summary>
    ///   Get the build type of the unmanaged code.
    /// </summary>
    /// <returns>
    ///   The build type string, such as "Debug" or "Release".
    /// </returns>
    public static string GetBuildType()
    {
      return Native.StringifyPointer(Native.reactional_get_build_type());
    }

    //
    // *** Parameters ***
    //

    /// <summary>
    ///   Get the number of parameters the track has.
    /// </summary>
    /// <param name="id">
    ///   The track ID.
    /// </param>
    /// <returns>
    ///   The number of parameters the track has.
    /// </returns>
    /// <exception cref="EngineNullException">Thrown if the C pointer for the track is NULL.</exception>
    public int GetNumParameters(int id)
    {
      CheckPointer();
      return Native.reactional_get_num_params(_cpointer, id);
    }

    /// <summary>
    ///   Find a parameter by name.
    /// </summary>
    /// <param name="id">The track ID.</param>
    /// <param name="name">The name of the parameter.</param>
    /// <returns>
    ///   An index to the parameter on success or a negative error code on failure.
    /// </returns>
    /// <exception cref="EngineNullException">Thrown if the C pointer for the track is NULL.</exception>
    public int FindParameter(int id, string name)
    {
      CheckPointer();
      return Native.reactional_find_param(_cpointer, id, name);
    }

    /// <summary>
    ///   Get the type of a parameter.
    /// </summary>
    /// <param name="id">The track ID.</param>
    /// <param name="index">The index of the parameter.</param>
    /// <returns>The parameter type.</returns>
    /// <exception cref="EngineNullException">Thrown if the C pointer for the track is NULL.</exception>
    /// <exception cref="EngineErrorException">Thrown if the unmanaged code returns an error.</exception>
    public ParamType GetParameterType(int id, int index)
    {
      CheckPointer();
      int t = Native.reactional_get_param_type(_cpointer, id, index);
      if (t < 0)
        throw new EngineErrorException(t);
      if (t > (int)ParamType.Trigger)
        throw new EngineErrorException(Native.ErrorUnsupported);
      return (ParamType)t;
    }

    /// <summary>
    ///   Get the name of a parameter.
    /// </summary>
    /// <param name="id">The track ID.</param>
    /// <param name="index">The index of the parameter.</param>
    /// <returns>The parameter name.</returns>
    /// <exception cref="EngineNullException">Thrown if the C pointer for the track is NULL.</exception>
    /// <exception cref="EngineErrorException">Thrown if the unmanaged code returns an error.</exception>
    public string GetParameterName(int id, int index)
    {
      CheckPointer();
      IntPtr s = Native.reactional_get_param_name(_cpointer, id, index);
      if (s == IntPtr.Zero)
        throw new EngineErrorException(Native.ErrorNoEnt);
      return Native.StringifyPointer(s);
    }

    /// <summary>
    ///   Get the value of a parameter.
    /// </summary>
    /// <param name="id">The track ID.</param>
    /// <param name="index">The index of the parameter.</param>
    /// <returns>The parameter value.</returns>
    /// <exception cref="EngineNullException">Thrown if the C pointer for the track is NULL.</exception>
    /// <exception cref="EngineErrorException">Thrown if the unmanaged code returns an error.</exception>
    public object GetParameterValue(int id, int index)
    {
      CheckPointer();
      int t = Native.reactional_get_param_type(_cpointer, id, index);
      if (t < 0)
        throw new EngineErrorException(t);
      int result = Native.ErrorUnsupported;
      switch ((ParamType)t)
      {
        case ParamType.Bool:
          bool tmpbool = false;
          result = Native.reactional_get_param_bool(_cpointer, id, index, ref tmpbool);
          if (result >= 0)
            return tmpbool;
          break;
        case ParamType.Int:
          Int64 tmpint = 0;
          result = Native.reactional_get_param_int(_cpointer, id, index, ref tmpint);
          if (result >= 0)
            return tmpint;
          break;
        case ParamType.Float:
          double tmpfloat = 0.0;
          result = Native.reactional_get_param_float(_cpointer, id, index, ref tmpfloat);
          if (result >= 0)
            return tmpfloat;
          break;
        case ParamType.String:
          result = Native.reactional_get_param_string(_cpointer, id, index, IntPtr.Zero, 0);
          if (result == 0)
            return "";
          if (result > 0)
          {
            IntPtr mem = Marshal.AllocHGlobal(result + 1);
            Native.reactional_get_param_string(_cpointer, id, index, mem, result + 1);
            String s = Native.StringifyPointer(mem);
            Marshal.FreeHGlobal(mem);
            return s;
          }
          break;
      }
      throw new EngineErrorException(result);
    }

    /// <summary>
    ///   Set the value of a parameter.
    /// </summary>
    /// <param name="id">The track ID.</param>
    /// <param name="index">The index of the parameter.</param>
    /// <param name="value">The value.</param>
    /// <exception cref="EngineNullException">Thrown if the C pointer for the track is NULL.</exception>
    /// <exception cref="EngineErrorException">Thrown if the unmanaged code returns an error.</exception>
    /// <exception cref="System.ArgumentException">Thrown if the value has an invalid type.</exception>
    public void SetParameterValue(int id, int index, bool value)
    {
      CheckPointer();
      int ret = Native.reactional_set_param_bool(_cpointer, id, index, value);
      if (ret < 0)
        throw new EngineErrorException(ret);
    }

    /// <summary>
    ///   Set the value of a parameter.
    /// </summary>
    /// <param name="id">The track ID.</param>
    /// <param name="index">The index of the parameter.</param>
    /// <param name="value">The value.</param>
    /// <exception cref="EngineNullException">Thrown if the C pointer for the track is NULL.</exception>
    /// <exception cref="EngineErrorException">Thrown if the unmanaged code returns an error.</exception>
    /// <exception cref="System.ArgumentException">Thrown if the value has an invalid type.</exception>
    public void SetParameterValue(int id, int index, float value)
    {
      CheckPointer();
      int ret = Native.reactional_set_param_float(_cpointer, id, index, value);
      if (ret < 0)
        throw new EngineErrorException(ret);
    }

    /// <summary>
    ///   Set the value of a parameter.
    /// </summary>
    /// <param name="id">The track ID.</param>
    /// <param name="index">The index of the parameter.</param>
    /// <param name="value">The value.</param>
    /// <exception cref="EngineNullException">Thrown if the C pointer for the track is NULL.</exception>
    /// <exception cref="EngineErrorException">Thrown if the unmanaged code returns an error.</exception>
    /// <exception cref="System.ArgumentException">Thrown if the value has an invalid type.</exception>
    public void SetParameterValue(int id, int index, double value)
    {
      CheckPointer();
      int ret = Native.reactional_set_param_float(_cpointer, id, index, value);
      if (ret < 0)
        throw new EngineErrorException(ret);
    }

    /// <summary>
    ///   Set the value of a parameter.
    /// </summary>
    /// <param name="id">The track ID.</param>
    /// <param name="index">The index of the parameter.</param>
    /// <param name="value">The value.</param>
    /// <exception cref="EngineNullException">Thrown if the C pointer for the track is NULL.</exception>
    /// <exception cref="EngineErrorException">Thrown if the unmanaged code returns an error.</exception>
    /// <exception cref="System.ArgumentException">Thrown if the value has an invalid type.</exception>
    public void SetParameterValue(int id, int index, int value)
    {
      CheckPointer();
      int ret = Native.reactional_set_param_int(_cpointer, id, index, value);
      if (ret < 0)
        throw new EngineErrorException(ret);
    }

    /// <summary>
    ///   Set the value of a parameter.
    /// </summary>
    /// <param name="id">The track ID.</param>
    /// <param name="index">The index of the parameter.</param>
    /// <param name="value">The value.</param>
    /// <exception cref="EngineNullException">Thrown if the C pointer for the track is NULL.</exception>
    /// <exception cref="EngineErrorException">Thrown if the unmanaged code returns an error.</exception>
    /// <exception cref="System.ArgumentException">Thrown if the value has an invalid type.</exception>
    public void SetParameterValue(int id, int index, long value)
    {
      CheckPointer();
      int ret = Native.reactional_set_param_int(_cpointer, id, index, value);
      if (ret < 0)
        throw new EngineErrorException(ret);
    }

    /// <summary>
    ///   Set the value of a parameter.
    /// </summary>
    /// <param name="id">The track ID.</param>
    /// <param name="index">The index of the parameter.</param>
    /// <param name="value">The value.</param>
    /// <exception cref="EngineNullException">Thrown if the C pointer for the track is NULL.</exception>
    /// <exception cref="EngineErrorException">Thrown if the unmanaged code returns an error.</exception>
    /// <exception cref="System.ArgumentException">Thrown if the value has an invalid type.</exception>
    public void SetParameterValue(int id, int index, string value)
    {
      CheckPointer();
      int ret = Native.reactional_set_param_string(_cpointer, id, index, value);
      if (ret < 0)
        throw new EngineErrorException(ret);
    }

    /// <summary>
    ///   Trigger a parameter of trigger type.
    /// </summary>
    /// <param name="id">The track ID.</param>
    /// <param name="index">The index of the parameter.</param>
    /// <returns>The parameter value.</returns>
    /// <exception cref="EngineNullException">Thrown if the C pointer for the track is NULL.</exception>
    /// <exception cref="EngineErrorException">Thrown if the unmanaged code returns an error.</exception>
    public void TriggerParameter(int id, int index)
    {
      CheckPointer();
      int result = Native.reactional_param_trig(_cpointer, id, index);
      if (result < 0)
        throw new EngineErrorException(result);
    }

    /// <summary>
    ///   Get the value of a parameter.
    /// </summary>
    /// <param name="id">The track ID.</param>
    /// <param name="index">The index of the parameter.</param>
    /// <returns>
    ///   The parameter value. If index is out of bounds the function returns 0.
    /// </returns>
    /// <exception cref="EngineNullException">Thrown if the C pointer for the track is NULL.</exception>
    /// <exception cref="EngineErrorException">Thrown if the unmanaged code returns an error.</exception>
    public bool GetParameterBool(int id, int index)
    {
      CheckPointer();
      bool ret = false;
      int result = Native.reactional_get_param_bool(_cpointer, id, index, ref ret);
      if (result < 0)
        throw new EngineErrorException(result);
      return ret;
    }

    /// <summary>
    ///   Set the value of a parameter.
    /// </summary>
    /// <param name="id">The track ID.</param>
    /// <param name="index">The index of the parameter.</param>
    /// <param name="value">The new value.</param>
    /// <exception cref="EngineNullException">Thrown if the C pointer for the track is NULL.</exception>
    /// <exception cref="EngineErrorException">Thrown if the unmanaged code returns an error.</exception>
    public void SetParameterBool(int id, int index, bool value)
    {
      CheckPointer();
      int result = Native.reactional_set_param_bool(_cpointer, id, index, value);
      if (result < 0)
        throw new EngineErrorException(result);
    }

    /// <summary>
    ///   Get the value of a parameter.
    /// </summary>
    /// <param name="id">The track ID.</param>
    /// <param name="index">The index of the parameter.</param>
    /// <returns>
    ///   The parameter value. If index is out of bounds the function returns 0.
    /// </returns>
    /// <exception cref="EngineNullException">Thrown if the C pointer for the track is NULL.</exception>
    /// <exception cref="EngineErrorException">Thrown if the unmanaged code returns an error.</exception>
    public long GetParameterInt(int id, int index)
    {
      CheckPointer();
      Int64 ret = 0;
      int result = Native.reactional_get_param_int(_cpointer, id, index, ref ret);
      if (result < 0)
        throw new EngineErrorException(result);
      return ret;
    }

    /// <summary>
    ///   Set the value of a parameter.
    /// </summary>
    /// <param name="id">The track ID.</param>
    /// <param name="index">The index of the parameter.</param>
    /// <param name="value">The new value.</param>
    /// <exception cref="EngineNullException">Thrown if the C pointer for the track is NULL.</exception>
    /// <exception cref="EngineErrorException">Thrown if the unmanaged code returns an error.</exception>
    public void SetParameterInt(int id, int index, long value)
    {
      CheckPointer();
      int result = Native.reactional_set_param_int(_cpointer, id, index, value);
      if (result < 0)
        throw new EngineErrorException(result);
    }

    /// <summary>
    ///   Get the value of a parameter.
    /// </summary>
    /// <param name="id">The track ID.</param>
    /// <param name="index">The index of the parameter.</param>
    /// <returns>
    ///   The parameter value. If index is out of bounds the function returns 0.
    /// </returns>
    /// <exception cref="EngineNullException">Thrown if the C pointer for the track is NULL.</exception>
    /// <exception cref="EngineErrorException">Thrown if the unmanaged code returns an error.</exception>
    public double GetParameterFloat(int id, int index)
    {
      CheckPointer();
      double ret = 0.0;
      int result = Native.reactional_get_param_float(_cpointer, id, index, ref ret);
      if (result < 0)
        throw new EngineErrorException(result);
      return ret;
    }

    /// <summary>
    ///   Set the value of a parameter.
    /// </summary>
    /// <param name="id">The track ID.</param>
    /// <param name="index">The index of the parameter.</param>
    /// <param name="value">The new value.</param>
    /// <exception cref="EngineNullException">Thrown if the C pointer for the track is NULL.</exception>
    /// <exception cref="EngineErrorException">Thrown if the unmanaged code returns an error.</exception>
    public void SetParameterFloat(int id, int index, double value)
    {
      CheckPointer();
      int result = Native.reactional_set_param_float(_cpointer, id, index, value);
      if (result < 0)
        throw new EngineErrorException(result);
    }

    /// <summary>
    ///   Trigger a parameter.
    /// </summary>
    /// <param name="id">The track ID.</param>
    /// <exception cref="EngineNullException">Thrown if the C pointer for the track is NULL.</exception>
    /// <exception cref="EngineErrorException">Thrown if the unmanaged code returns an error.</exception>
    public void SetParameterTrig(int id, int index)
    {
      CheckPointer();
      int result = Native.reactional_param_trig(_cpointer, id, index);
      if (result < 0)
        throw new EngineErrorException(result);
    }

    //
    // *** Controls ***
    //

    /// <summary>
    ///   Get the number of controls the track has.
    /// </summary>
    /// <param name="id">The track ID.</param>
    /// <returns>
    ///   The number of controls the track has.
    /// </returns>
    /// <exception cref="EngineNullException">Thrown if the C pointer for the track is NULL.</exception>
    public int GetNumControls(int id)
    {
      CheckPointer();
      return Native.reactional_get_num_controls(_cpointer, id);
    }

    /// <summary>
    ///   Find a control by name.
    /// </summary>
    /// <param name="id">The track ID.</param>
    /// <param name="name">The name of the control.</param>
    /// <returns>
    ///   An index to the control on success or a negative error code on failure.
    /// </returns>
    /// <exception cref="EngineNullException">Thrown if the C pointer for the track is NULL.</exception>
    public int FindControl(int id, string name)
    {
      CheckPointer();
      return Native.reactional_find_control(_cpointer, id, name);
    }

    /// <summary>
    ///   Get the name of a control.
    /// </summary>
    /// <param name="id">The track ID.</param>
    /// <param name="index">The index of the control.</param>
    /// <returns>
    ///   The control name.
    /// </returns>
    /// <exception cref="EngineNullException">Thrown if the C pointer for the track is NULL.</exception>
    /// <exception cref="EngineErrorException">Thrown if the unmanaged code returns an error.</exception>
    public string GetControlName(int id, int index)
    {
      CheckPointer();
      IntPtr s = Native.reactional_get_control_name(_cpointer, id, index);
      if (s == IntPtr.Zero)
        throw new EngineErrorException(Native.ErrorNoEnt);
      return Native.StringifyPointer(s);
    }

    /// <summary>
    ///   Get the description of a control.
    /// </summary>
    /// <param name="id">The track ID.</param>
    /// <param name="index">The index of the control.</param>
    /// <returns>
    ///   The control name.
    /// </returns>
    /// <exception cref="EngineNullException">Thrown if the C pointer for the track is NULL.</exception>
    /// <exception cref="EngineErrorException">Thrown if the unmanaged code returns an error.</exception>
    public string GetControlDescription(int id, int index)
    {
      CheckPointer();
      IntPtr s = Native.reactional_get_control_description(_cpointer, id, index);
      if (s == IntPtr.Zero)
        throw new EngineErrorException(Native.ErrorNoEnt);
      return Native.StringifyPointer(s);
    }

    /// <summary>
    ///   Get the value of a control.
    /// </summary>
    /// <param name="id">The track ID.</param>
    /// <param name="index">The index of the control.</param>
    /// <returns>
    ///   The control value. If index is out of bounds the function returns 0.
    /// </returns>
    /// <exception cref="EngineNullException">Thrown if the C pointer for the track is NULL.</exception>
    /// <exception cref="EngineErrorException">Thrown if the unmanaged code returns an error.</exception>
    public double GetControlValue(int id, int index)
    {
      CheckPointer();
      //double ret = 0.0;
      double result = Native.reactional_get_control_value(_cpointer, id, index);
      if (result < 0)
        throw new EngineErrorException(-7);
      return result;
    }

    /// <summary>
    ///   Set the value of a control.
    /// </summary>
    /// <param name="id">The track ID.</param>
    /// <param name="index">The index of the control.</param>
    /// <param name="value">The new value.</param>
    /// <exception cref="EngineNullException">Thrown if the C pointer for the track is NULL.</exception>
    /// <exception cref="EngineErrorException">Thrown if the unmanaged code returns an error.</exception>
    public void SetControlValue(int id, int index, double value)
    {
      CheckPointer();
      int result = Native.reactional_set_control_value(_cpointer, id, index, value);
      if (result < 0)
        throw new EngineErrorException(result);
    }

    /// <summary>
    ///   Set the value array of a control.
    /// </summary>
    /// <param name="id">The track ID.</param>
    /// <param name="index">The index of the control.</param>
    /// <param name="values">The values to set.</param>
    /// <exception cref="EngineNullException">Thrown if the C pointer for the track is NULL.</exception>
    /// <exception cref="EngineErrorException">Thrown if the unmanaged code returns an error.</exception>
    public void SetControlValue(int id, int index, double[] value)
    {
      CheckPointer();
      int result = Native.reactional_set_control_value_array(_cpointer, id, index, value, value.Length);
      if (result < 0)
        throw new EngineErrorException(result);
    }

    /// <summary>
    ///   Get the value of a control.
    /// </summary>
    /// <param name="id">The track ID.</param>
    /// <param name="index">The index of the control.</param>
    /// <returns>
    ///   0 on success or a negative error code.
    /// </returns>
    /// <exception cref="EngineNullException">Thrown if the C pointer for the track is NULL.</exception>
    /// <exception cref="EngineErrorException">Thrown if the unmanaged code returns an error.</exception>
    public double GetControlValueArray(int id, int index, ref double[] values)
    {
      CheckPointer();
      int result = Native.reactional_get_control_value_array(_cpointer, id, index, ref values, values.Length);
      if (result < 0)
        throw new EngineErrorException(-7);
      return result;
    }

    /// <summary>
    ///   Get the size of a control value array.
    /// </summary>
    /// <param name="id">The track ID.</param>
    /// <param name="index">The index of the control.</param>
    /// <returns>
    ///   The value array size on success or a negative error code.
    /// </returns>
    /// <exception cref="EngineNullException">Thrown if the C pointer for the track is NULL.</exception>
    /// <exception cref="EngineErrorException">Thrown if the unmanaged code returns an error.</exception>
    public int GetControlValueArraySize(int id, int index)
    {
      CheckPointer();
      int result = Native.reactional_get_control_value_array_size(_cpointer, id, index);
      if (result < 0)
        throw new EngineErrorException(-7);
      return result;
    }

    /// <summary>
    ///   Get the type of a control.
    /// </summary>
    /// <param name="id">The track ID.</param>
    /// <param name="index">The index of the control.</param>
    /// <returns>
    ///   The control type.
    /// </returns>
    /// <exception cref="EngineNullException">Thrown if the C pointer for the track is NULL.</exception>
    /// <exception cref="EngineErrorException">Thrown if the unmanaged code returns an error.</exception>
    public string GetControlType(int id, int index)
    {
      CheckPointer();
      IntPtr s = Native.reactional_get_control_type(_cpointer, id, index);
      if (s == IntPtr.Zero)
        throw new EngineErrorException(Native.ErrorNoEnt);
      return Native.StringifyPointer(s);
    }

    /// <summary>
    ///   Get the level of a control.
    /// </summary>
    /// <param name="id">The track ID.</param>
    /// <param name="index">The index of the control.</param>
    /// <returns>
    ///   The control level.
    /// </returns>
    /// <exception cref="EngineNullException">Thrown if the C pointer for the track is NULL.</exception>
    /// <exception cref="EngineErrorException">Thrown if the unmanaged code returns an error.</exception>
    public string GetControlLevel(int id, int index)
    {
      CheckPointer();
      IntPtr s = Native.reactional_get_control_level(_cpointer, id, index);
      if (s == IntPtr.Zero)
        throw new EngineErrorException(Native.ErrorNoEnt);
      return Native.StringifyPointer(s);
    }

    /// <summary>
    ///   Render audio into planar buffers.
    /// </summary>
    /// <param name="sampleRate">The sample rate.</param>
    /// <param name="numFrames">The number of frames to render.</param>
    /// <param name="numChannels">The number of channels to render.</param>
    /// <param name="buffers">Store audio frames in these buffers.</param>
    /// <exception cref="EngineNullException">Thrown if the C pointer for the track is NULL.</exception>
    /// <exception cref="EngineErrorException">Thrown if the unmanaged code returns an error.</exception>
    public void RenderPlanar(double sampleRate, int numFrames, int numChannels, float[][] buffers)
    {
      CheckPointer();
      int result = Native.reactional_render_planar(_cpointer, sampleRate, numFrames, numChannels, buffers);
      if (result < 0)
        throw new EngineErrorException(result);
    }

    /// <summary>
    ///   Render audio into planar buffers.
    /// </summary>
    /// <param name="sampleRate">The sample rate.</param>
    /// <param name="numFrames">The number of frames to render.</param>
    /// <param name="numChannels">The number of channels to render.</param>
    /// <param name="buffer">Store audio frames in this buffer.</param>
    /// <exception cref="EngineNullException">Thrown if the C pointer for the track is NULL.</exception>
    /// <exception cref="EngineErrorException">Thrown if the unmanaged code returns an error.</exception>
    public void RenderInterleaved(double sampleRate, int numFrames, int numChannels, float[] buffer)
    {
      CheckPointer();
      int result = Native.reactional_render_interleaved(_cpointer, sampleRate, numFrames, numChannels, buffer);
      if (result == -6)
        return;
      if (result < 0)
        throw new EngineErrorException(result);
    }

    /// <summary>
    ///   Process the track internals.
    /// </summary>
    /// <param name="systemTime">The current system time or -1 to use the system clock.</param>
    /// <exception cref="EngineNullException">Thrown if the C pointer for the track is NULL.</exception>
    /// <exception cref="EngineErrorException">Thrown if the unmanaged code returns an error.</exception>
    public void Process(long systemTime)
    {
      CheckPointer();
      int result = Native.reactional_process(_cpointer, systemTime);
      if (result < 0)
        throw new EngineErrorException(result);
    }

    /// <summary>
    ///   Push an event to the track input.
    /// </summary>
    /// <param name="id">The track ID.</param>
    /// <param name="message">The message to push.</param>
    /// <param name="microbeats">Schedule the message at this absolute microbeats offset.</param>
    /// <exception cref="EngineNullException">Thrown if the C pointer for the track is NULL.</exception>
    /// <exception cref="EngineErrorException">Thrown if the unmanaged code returns an error.</exception>
    public void Push(int id, OSCMessage message, long microbeats = 0)
    {
      CheckPointer();
      byte[] oscData = new byte[128];
      int oscSize = message.Encode(oscData);
      int result = Native.reactional_event_push(_cpointer, id, microbeats, oscData, oscSize);
      if (result < 0)
        throw new EngineErrorException(result);
    }

    /// <summary>
    ///   Poll OSC events from the track.
    /// </summary>
    /// <param name="id">The track ID.</param>
    /// <param name="startBeat">Store the start beat here.</param>
    /// <param name="maxMessages">The maximum number of messages to get or <= 0 for all.</param>
    /// <returns>An array of message or null if no messages were available.</returns>
    /// <exception cref="EngineNullException">Thrown if the C pointer for the track is NULL.</exception>
    /// <exception cref="EngineErrorException">Thrown if the unmanaged code returns an error.</exception>
    public OSCMessage[] Poll(int id, ref long startBeat, int maxMessages = 0)
    {
      CheckPointer();
      int num = Native.reactional_event_poll_begin(_cpointer, id, ref startBeat);
      if (maxMessages > 0 && num > maxMessages)
        num = maxMessages;
      if (num > 0)
      {
        OSCMessage[] messages = new OSCMessage[num];
        int messageCount = 0;
        for (int i = 0; i < num; i++)
        {
          int oscSize = 0;
          IntPtr oscData = Native.reactional_event_poll(_cpointer, id, i, ref oscSize);
          if (oscSize > 0 && oscData != IntPtr.Zero)
          {
            OSCMessage msg = new OSCMessage();
            int result = msg.Decode(oscData, oscSize);
            if (result < 0)
              throw new EngineErrorException(result);
            messages[messageCount++] = msg;
          }
        }
        Native.reactional_event_poll_end(_cpointer, id, num);
        Array.Resize(ref messages, messageCount);
        return messages;
      }
      return null;
    }

    /// <summary>
    ///   Poll event struct pointers from the track.
    /// </summary>
    /// <param name="id">The track ID.</param>
    /// <param name="startBeat">Store the start beat here.</param>
    /// <param name="maxEvents">The maximum number of events to get or <= 0 for all.</param>
    /// <returns>An array of event pointers or null if no messages were available.</returns>
    /// <exception cref="EngineNullException">Thrown if the C pointer for the track is NULL.</exception>
    public IntPtr[] PollRaw(int id, ref long startBeat, int maxEvents = 0)
    {
      CheckPointer();
      int num = Native.reactional_event_poll_begin(_cpointer, id, ref startBeat);
      if (maxEvents > 0 && num > maxEvents)
        num = maxEvents;
      if (num > 0)
      {
        int eventCount = 0;
        IntPtr[] events = new IntPtr[num];
        for (int i = 0; i < num; i++)
        {
          IntPtr ev = Native.reactional_event_poll_struct(_cpointer, i);
          if (ev != IntPtr.Zero)
          {
            events[eventCount] = ev;
            eventCount++;
          }
        }
        Native.reactional_event_poll_end(_cpointer, id, num);
        Array.Resize(ref events, eventCount);
        return events;
      }
      return null;
    }

    /// <summary>
    ///   Get the type member value from an event struct pointer.
    /// </summary>
    /// <param name="ev">The event struct pointer.</param>
    /// <returns>The member value.</returns>
    static public int EventStructGetType(IntPtr ev)
    {
      return ev != IntPtr.Zero ? Native.reactional_evstruct_get_type(ev) : -1;
    }

    /// <summary>
    ///   Get the offset member value from an event struct pointer.
    /// </summary>
    /// <param name="ev">The event struct pointer.</param>
    /// <returns>The member value.</returns>
    static public Int64 EventStructGetOffset(IntPtr ev)
    {
      return ev != IntPtr.Zero ? Native.reactional_evstruct_get_offset(ev) : -1;
    }

    /// <summary>
    ///   Get the duration member value from an event struct pointer.
    /// </summary>
    /// <param name="ev">The event struct pointer.</param>
    /// <returns>The member value.</returns>
    static public Int64 EventStructGetDuration(IntPtr ev)
    {
      return ev != IntPtr.Zero ? Native.reactional_evstruct_get_duration(ev) : -1;
    }

    /// <summary>
    ///   Get the lane index member value from an event struct pointer.
    /// </summary>
    /// <param name="ev">The event struct pointer.</param>
    /// <returns>The member value.</returns>
    static public int EventStructGetLaneIndex(IntPtr ev)
    {
      return ev != IntPtr.Zero ? Native.reactional_evstruct_get_lane_index(ev) : -1;
    }

    /// <summary>
    ///   Get the sink index member value from an event struct pointer.
    /// </summary>
    /// <param name="ev">The event struct pointer.</param>
    // <returns>The member value.</returns>
    static public int EventStructGetSinkIndex(IntPtr ev)
    {
      return ev != IntPtr.Zero ? Native.reactional_evstruct_get_sink_index(ev) : -1;
    }

    /// <summary>
    ///   Get the output index member value from an event struct pointer.
    /// </summary>
    /// <param name="ev">The event struct pointer.</param>
    /// <returns>The member value.</returns>
    static public int EventStructGetOutputIndex(IntPtr ev)
    {
      return ev != IntPtr.Zero ? Native.reactional_evstruct_get_output_index(ev) : -1;
    }

    /// <summary>
    ///   Get the priority member value from an event struct pointer, note that
    ///   lower value means higher priority (ascending sort).
    /// </summary>
    /// <param name="ev">The event struct pointer.</param>
    /// <returns>The member value.</returns>
    static public int EventStructGetPriority(IntPtr ev)
    {
      return ev != IntPtr.Zero ? Native.reactional_evstruct_get_priority(ev) : -1;
    }

    /// <summary>
    ///   Check if a theme was created as part of a theme.
    /// </summary>
    /// <param name="ev">The event struct pointer.</param>
    /// <returns>The member value.</returns>
    static public bool EventStructGetIsTheme(IntPtr ev)
    {
      return ev != IntPtr.Zero ? Native.reactional_evstruct_get_is_theme(ev) : false;
    }

    /// <summary>
    ///   Get the next quantized beat from the track.
    /// </summary>
    /// <param name="id">The track ID.</param>
    /// <param name="track">The track.</param>
    /// <param name="quant">The quantization.</param>
    /// <param name="phase">The phase.</param>
    /// <returns>The quantized beat.
    /// <exception cref="EngineNullException">Thrown if the C pointer for the track is NULL.</exception>
    public Int64 GetNextQuantBeat(int id, Int64 quant, Int64 phase)
    {
      CheckPointer();
      return Native.reactional_get_next_quant_beat(_cpointer, id, quant, phase);
    }

    /// <summary>Get the number of assets the track requires.</summary>
    /// <param name="id">The track ID.</param>
    /// <returns>The number of assets the track requires.</returns>
    /// <exception cref="EngineNullException">Thrown if the C pointer for the track is NULL.</exception>
    public int GetNumAssets(int id)
    {
      CheckPointer();
      return Native.reactional_get_num_assets(_cpointer, id);
    }

    /// <summary>Get the ID of an asset.</summary>
    /// <param name="id">The track ID.</param>
    /// <param name="index">The index of the asset.</param>
    /// <returns>The ID or an empty string if index is out of bounds.</returns>
    /// <exception cref="EngineNullException">Thrown if the C pointer for the track is NULL.</exception>
    public string GetAssetId(int id, int index)
    {
      CheckPointer();
      IntPtr s = Native.reactional_get_asset_id(_cpointer, id, index);
      if (s != IntPtr.Zero)
        return Native.StringifyPointer(s);
      return "";
    }

    /// <summary>Get the type of an asset.</summary>
    /// <param name="id">The track ID.</param>
    /// <param name="index">The index of the asset.</param>
    /// <returns>The type or an empty string if index is out of bounds.</returns>
    /// <exception cref="EngineNullException">Thrown if the C pointer for the track is NULL.</exception>
    public string GetAssetType(int id, int index)
    {
      CheckPointer();
      IntPtr s = Native.reactional_get_asset_type(_cpointer, id, index);
      if (s != IntPtr.Zero)
        return Native.StringifyPointer(s);
      return "";
    }

    /// <summary>Get the URI of an asset.</summary>
    /// <param name="id">The track URI.</param>
    /// <param name="index">The index of the asset.</param>
    /// <returns>The type or an empty string if index is out of bounds.</returns>
    /// <exception cref="EngineNullException">Thrown if the C pointer for the track is NULL.</exception>
    public string GetAssetUri(int id, int index)
    {
      CheckPointer();
      IntPtr s = Native.reactional_get_asset_uri(_cpointer, id, index);
      if (s != IntPtr.Zero)
        return Native.StringifyPointer(s);
      return "";
    }

    /// <summary>Set the data for an asset.</summary>
    /// <param name="id">The track ID.</param>
    /// <param name="asset_id">The ID of the asset.</param>
    /// <param name="asset_type">The type of data to set.</param>
    /// <param name="asset_data">The data.</param>
    /// <param name="key">The key.</param>
    /// <returns>The type or NULL if index is out of bounds.</returns>
    /// <exception cref="EngineNullException">Thrown if the C pointer for the track is NULL.</exception>
    /// <exception cref="EngineErrorException">Thrown if the unmanaged code returns an error.</exception>
    public void SetAssetData(int id, string asset_id, string asset_type, byte[] asset_data, byte[] key)
    {
      CheckPointer();

      GCHandle? keyHandle = null;
      GCHandle dataHandle = GCHandle.Alloc(asset_data, GCHandleType.Pinned);

      if (key != null)
      {
        keyHandle = GCHandle.Alloc(key, GCHandleType.Pinned);
      }

      IntPtr keyPtr = keyHandle?.AddrOfPinnedObject() ?? IntPtr.Zero;
      IntPtr dataPtr = dataHandle.AddrOfPinnedObject();

      int ret = -666;

      try
      {
        ret = Native.reactional_set_asset_data(_cpointer, id, asset_id, asset_type, dataPtr, asset_data.Length, keyPtr, key != null ? key.Length : -1);
      }
      finally
      {
        dataHandle.Free();
        keyHandle?.Free();
      }

      if (ret < 0)
      {
        throw new EngineErrorException(ret);
      }
    }
    public void SetAssetPath(int id, string asset_id, string asset_type, string path = "")
    {
      CheckPointer();
      int ret = Native.reactional_set_asset_data(_cpointer, id, asset_id, asset_type, IntPtr.Zero, 0, IntPtr.Zero, 0);
      if (ret < 0)
        throw new EngineErrorException(ret);
    }

    /// <summary>Get the number of stingers the track has.</summary>
    /// <param name="id">The track ID.</param>
    /// <returns>The number of tracks the stinger has.</returns>
    /// <exception cref="EngineNullException">Thrown if the C pointer for the track is NULL.</exception>
    public int GetNumStingers(int id)
    {
      CheckPointer();
      return Native.reactional_get_num_stingers(_cpointer, id);
    }

    /// <summary>Start a stinger.</summary>
    /// <param name="id">The track ID.</param>
    /// <param name="index">The index of the stinger to start.</returns>
    /// <param name="startOffset">Schedule at this microbeat starting offset.</returns>
    /// <param name="behaviour">How to act when a stinger is re-triggered.</returns>
    /// <returns>True if the stinger was scheduled or false the index is out of bounds.</returns>
    /// <exception cref="EngineNullException">Thrown if the C pointer for the track is NULL.</exception>
    /// <exception cref="EngineErrorException">Thrown if the unmanaged code returns an error.</exception>
    public bool StartStinger(int id, int index, long startOffset, int behaviour)
    {
      CheckPointer();
      int ret = Native.reactional_stinger_start(_cpointer, id, index, startOffset, behaviour);
      if (ret == Native.ErrorNoEnt)
        return false;
      else if (ret < 0)
        throw new EngineErrorException(ret);
      return true;
    }

    /// <summary>Get the number of states.</summary>
    /// <param name="id"> The track ID.</param>
    /// <returns>The number of states.</returns>
    /// <exception cref="EngineNullException">Thrown if the C pointer for the track is NULL.</exception>
    public int GetNumStates(int id)
    {
      CheckPointer();
      return Native.reactional_get_num_states(_cpointer, id);
    }

    /// <summary>Find a state from a name.</summary>
    /// <param name="id"> The track ID.</param>
    /// <param name="name"> The name to search for.</param>
    /// <returns>The state index or a negative error code on failure.</returns>
    /// <exception cref="EngineNullException">Thrown if the C pointer for the track is NULL.</exception>
    public int FindState(int id, string name)
    {
      CheckPointer();
      return Native.reactional_find_state(_cpointer, id, name);
    }

    /// <summary>Set a state.</summary>
    /// <param name="id"> The track ID.</param>
    /// <param name="index"> The state index to set.</param>
    /// <param name="lagMultiplier"> A lag multiplier for the control values. If < 0 the default will be used.</param>
    /// <returns>The state index or a negative error code on failure.</returns>
    /// <exception cref="EngineNullException">Thrown if the C pointer for the track is NULL.</exception>
    /// <exception cref="EngineErrorException">Thrown if the unmanaged code returns an error.</exception>
    public void SetState(int id, int index, double lagMultiplier = -1)
    {
      CheckPointer();
      int ret = Native.reactional_set_state(_cpointer, id, index, lagMultiplier);
      if (ret < 0)
        throw new EngineErrorException(ret);
    }

    /// <summary>Get the number of snapshots.</summary>
    /// <param name="id"> The track ID.</param>
    /// <returns>The number of snapshots.</returns>
    /// <exception cref="EngineNullException">Thrown if the C pointer for the track is NULL.</exception>
    public int GetNumSnapshots(int id)
    {
      CheckPointer();
      return Native.reactional_get_num_snapshots(_cpointer, id);
    }

    /// <summary>Find a snapshot from a name.</summary>
    /// <param name="id"> The track ID.</param>
    /// <param name="name"> The name to search for.</param>
    /// <returns>The snapshot index or a negative error code on failure.</returns>
    /// <exception cref="EngineNullException">Thrown if the C pointer for the track is NULL.</exception>
    public int FindSnapshot(int id, string name)
    {
      CheckPointer();
      return Native.reactional_find_snapshot(_cpointer, id, name);
    }

    /// <summary>Set a snapshot.</summary>
    /// <param name="id"> The track ID.</param>
    /// <param name="index"> The snapshot index to set.</param>
    /// <param name="lagMultiplier"> A lag multiplier for the control values. If < 0 the default will be used.</param>
    /// <exception cref="EngineNullException">Thrown if the C pointer for the track is NULL.</exception>
    /// <exception cref="EngineErrorException">Thrown if the unmanaged code returns an error.</exception>
    public void SetSnapshot(int id, int index, double lagMultiplier = -1)
    {
      CheckPointer();
      int ret = Native.reactional_set_snapshot(_cpointer, id, index, lagMultiplier);
      if (ret < 0)
        throw new EngineErrorException(ret);
    }

    public void TrackFade(float target, Int64 beat_offset, Int64 time_duration, bool stop_finish = false)
    {
      CheckPointer();
      int ret = Native.reactional_track_fade(_cpointer, target, beat_offset, time_duration, stop_finish);
      if (ret < 0)
        throw new EngineErrorException(ret);
    }

    public void ThemeFade(float target, Int64 beat_offset, Int64 time_duration, bool stop_finish = false)
    {
      CheckPointer();
      int ret = Native.reactional_theme_fade(_cpointer, target, beat_offset, time_duration, stop_finish);
      if (ret < 0)
        throw new EngineErrorException(ret);
    }

    static public String ValidateTrack(byte[] data, byte[] key = null)
    {
      StringBuilder sb = new StringBuilder(data.Length); // Allocates length + 1 bytes
      int ret = -1;
      if (key != null)
        ret = Native.reactional_validate_track(data, data.Length, key, key.Length, sb, data.Length + 1);
      else
        ret = Native.reactional_validate_track(data, data.Length, null, -1, sb, data.Length + 1);              
      if (ret < 0)
        throw new EngineErrorException(ret);
      return sb.ToString();
    }

    protected double GetChannelAmp(int channel, bool is_track)
    {
      CheckPointer();
      double amp = 0.0;
      int track_id = is_track ? Native.reactional_get_track(_cpointer) : Native.reactional_get_theme(_cpointer);
      if (track_id < 0)
        return 0.0;
      int ret = Native.reactional_get_channel_amp(_cpointer, track_id, channel, ref amp);
      if (ret < 0)
        return 0.0;
      return amp;
    }

    public double GetTrackChannelAmp(int channel)
    {
      return GetChannelAmp(channel, true);
    }

    public double GetThemeChannelAmp(int channel)
    {
      return GetChannelAmp(channel, false);
    }

    protected void SetChannelAmp(int channel, double amp, bool is_track)
    {
      CheckPointer();
      int track_id = is_track ? Native.reactional_get_track(_cpointer) : Native.reactional_get_theme(_cpointer);
      if (track_id < 0)
        return;
      int ret = Native.reactional_set_channel_amp(_cpointer, track_id, channel, amp);
      if (ret < 0)
        throw new EngineErrorException(ret);
    }

    public void SetTrackChannelAmp(int channel, double amp)
    {
      SetChannelAmp(channel, amp, true);
    }

    public void SetThemeChannelAmp(int channel, double amp)
    {
      SetChannelAmp(channel, amp, false);
    }

    /// <summary>Get the number of parts for the track.</summary>
    /// <param name="id">The track ID.</param>
    /// <returns>The number of parts for the track.</returns>
    /// <exception cref="EngineNullException">Thrown if the C pointer for the track is NULL.</exception>
    /// <exception cref="EngineErrorException">Thrown if the unmanaged code returns an error.</exception>
    public int GetNumParts(int id)
    {
      CheckPointer();
      int ret = Native.reactional_get_num_parts(_cpointer, id);
      if (ret < 0)
        throw new EngineErrorException(ret);
      return ret;
    }

    /// <summary>Get the current part index at runtime.</summary>
    /// <param name="id">The track ID.</param>
    /// <returns>The current part in the track.</returns>
    /// <exception cref="EngineNullException">Thrown if the C pointer for the track is NULL.</exception>
    /// <exception cref="EngineErrorException">Thrown if the unmanaged code returns an error.</exception>
    public int GetCurrentPart(int id)
    {
      CheckPointer();
      int ret = Native.reactional_get_current_part(_cpointer, id);
      if (ret < 0)
        ret = -1;
        //throw new EngineErrorException(ret);
      return ret;
    }

    /// <summary>Get the name of a part in a track.</summary>
    /// <param name="id">The track ID.</param>
    /// <param name="partIndex">The part index.</param>
    /// <returns>The part name.</returns>
    /// <exception cref="EngineNullException">Thrown if the C pointer for the track is NULL.</exception>
    public string GetPartName(int id, int partIndex)
    {
      CheckPointer();
      IntPtr s = Native.reactional_get_part_name(_cpointer, id, partIndex);
      if (s != IntPtr.Zero)
        return Native.StringifyPointer(s);
      return "";
    }

    /// <summary>Get the offset of a part in a track.</summary>
    /// <param name="id">The track ID.</param>
    /// <param name="partIndex">The part index.</param>
    /// <returns>The part offset.</returns>
    /// <exception cref="EngineNullException">Thrown if the C pointer for the track is NULL.</exception>
    /// <exception cref="EngineErrorException">Thrown if the unmanaged code returns an error.</exception>
    public Int64 GetPartOffset(int id, int partIndex)
    {
      CheckPointer();
      Int64 ret = Native.reactional_get_part_offset(_cpointer, id, partIndex);
      if (ret < 0)
        throw new EngineErrorException(ret);
      return ret;
    }

    /// <summary>Get the duration of a part in a track.</summary>
    /// <param name="id">The track ID.</param>
    /// <param name="partIndex">The part index.</param>
    /// <returns>The part duration.</returns>
    /// <exception cref="EngineNullException">Thrown if the C pointer for the track is NULL.</exception>
    /// <exception cref="EngineErrorException">Thrown if the unmanaged code returns an error.</exception>
    public Int64 GetPartDuration(int id, int partIndex)
    {
      CheckPointer();
      Int64 ret = Native.reactional_get_part_duration(_cpointer, id, partIndex);
      if (ret < 0)
        throw new EngineErrorException(ret);
      return ret;
    }

    /// <summary>Get the number of bars in a track.</summary>
    /// <param name="id">The track ID.</param>
    /// <returns>The number of bars for the track.</returns>
    /// <exception cref="EngineNullException">Thrown if the C pointer for the track is NULL.</exception>
    /// <exception cref="EngineErrorException">Thrown if the unmanaged code returns an error.</exception>
    public int GetNumBars(int id)
    {
      CheckPointer();
      int ret = Native.reactional_get_num_bars(_cpointer, id);
      if (ret < 0)
        throw new EngineErrorException(ret);
      return ret;
    }

    /// <summary>Get the current bar index at runtime.</summary>
    /// <param name="id">The track ID.</param>
    /// <returns>The current bar in the track.</returns>
    /// <exception cref="EngineNullException">Thrown if the C pointer for the track is NULL.</exception>
    /// <exception cref="EngineErrorException">Thrown if the unmanaged code returns an error.</exception>
    public int GetCurrentBar(int id)
    {
      CheckPointer();
      int ret = Native.reactional_get_current_bar(_cpointer, id);
      if (ret < 0)
        throw new EngineErrorException(ret);
      return ret;
    }

    /// <summary>Get the offset of a bar in a track.</summary>
    /// <param name="id">The track ID.</param>
    /// <param name="barIndex">The bar index.</param>
    /// <returns>The bar offset.</returns>
    /// <exception cref="EngineNullException">Thrown if the C pointer for the track is NULL.</exception>
    /// <exception cref="EngineErrorException">Thrown if the unmanaged code returns an error.</exception>
    public Int64 GetBarOffset(int id, int barIndex)
    {
      CheckPointer();
      Int64 ret = Native.reactional_get_bar_offset(_cpointer, id, barIndex);
      if (ret < 0)
        throw new EngineErrorException(ret);
      return ret;
    }

    /// <summary>Get the duration of a bar in a track.</summary>
    /// <param name="id">The track ID.</param>
    /// <param name="barIndex">The bar index.</param>
    /// <returns>The bar duration.</returns>
    /// <exception cref="EngineNullException">Thrown if the C pointer for the track is NULL.</exception>
    /// <exception cref="EngineErrorException">Thrown if the unmanaged code returns an error.</exception>
    public Int64 GetBarDuration(int id, int barIndex)
    {
      CheckPointer();
      Int64 ret = Native.reactional_get_bar_duration(_cpointer, id, barIndex);
      if (ret < 0)
        throw new EngineErrorException(ret);
      return ret;
    }

    /// <summary>
    /// Get the unmanaged c pointer.
    /// </summary>
    public IntPtr GetPointer()
    {
      CheckPointer();
      return _cpointer;
    }

  };
  // END OF ENGINE

  /// <summary>
  ///   Thrown if an OSC operation fails.
  /// </summary>
  class OSCException : Exception
  {

    /// <summary>
    ///   Create an OSC exception.
    /// </summary>
    /// <param name="message">The error message.</param>
    public OSCException(string message)
    : base(message)
    {
    }

    /// <summary>
    ///   Basic constructor.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <param name="inner">The inner exception.</param>
    public OSCException(string message, Exception inner)
        : base(message, inner)
    {
    }

  };

  /// <summary>
  ///   Used to encode/decode OSC data.
  /// </summary>
  public class OSCMessage
  {

    /// <summary>
    ///   Used to force type 'S' (capital S).
    /// </summary>
    public class Symbol
    {

      /// <summary>
      ///   Internal value.
      /// </summary>
      protected string _value;

      /// <summary>
      ///   The string value.
      /// </summary>
      public string Value
      {
        get { return _value != null ? _value : ""; }
        set { _value = value; }
      }

      /// <summary>
      ///   Creates a new symbol.
      /// </summary>
      public Symbol(string value = "")
      {
        Value = value;
      }

      /// <summary>
      ///   Check for equality between two objects.
      /// </summary>
      /// <param name="other">The other object to compare with.</param>
      /// <returns>True if the two objects are considered equal.</returns>
      public override bool Equals(object other)
      {
        if (other != null && other.GetType() == typeof(Symbol))
          return Value == ((Symbol)other).Value;
        return false;
      }

      /// <summary>
      ///   Get the hash code of the object.
      /// </summary>
      /// <returns>The hash code.</returns>
      public override int GetHashCode()
      {
        return Value.GetHashCode();
      }

      /// <summary>
      ///   Convert the MIDI object to a human readable format.
      /// </summary>
      /// <returns>A human readable string</returns>
      public override string ToString()
      {
        return Value != null ? Value : "";
      }

    };

    /// <summary>
    ///   Used to force type 'm'.
    /// </summary>
    public class MIDI
    {

      /// <summary>
      ///   The status byte.
      /// </summary>
      public byte Status;

      /// <summary>
      ///   The first data byte.
      /// </summary>
      public byte Data1;

      /// <summary>
      ///   The second data byte.
      /// </summary>
      public byte Data2;

      /// <summary>
      ///   Create a MIDI object.
      /// </summary>
      public MIDI(byte status, byte data1, byte data2)
      {
        Status = status;
        Data1 = data1;
        Data2 = data2;
      }

      /// <summary>
      ///   Check for equality between two objects.
      /// </summary>
      /// <param name="other">The other object to compare with.</param>
      /// <returns>True if the two objects are considered equal.</returns>
      public override bool Equals(object other)
      {
        if (other != null && other.GetType() == typeof(MIDI))
        {
          MIDI m = (MIDI)other;
          if (Status == m.Status && Data1 == m.Data1 && Data2 == m.Data2)
            return true;
        }
        return false;
      }

      /// <summary>
      ///   Get the hash code of the object.
      /// </summary>
      /// <returns>The hash code.</returns>
      public override int GetHashCode()
      {
        return (Status << 16) | (Data1 << 8) | (Data2);
      }

      /// <summary>
      ///   Convert the MIDI object to a human readable format.
      /// </summary>
      /// <returns>A human readable string</returns>
      public override string ToString()
      {
        return String.Format("0x{0:X2}, 0x{1:X2}, 0x{2:X2}", Status, Data1, Data2);
      }


    };

    /// <summary>
    ///   Keep track of the values.
    /// </summary>
    protected List<object> _values = new List<object>();

    /// <summary>
    ///   The number of values the message has.
    /// </summary>
    public int Count
    {
      get
      {
        return _values.Count;
      }
    }

    /// <summary>
    ///   Keep track of the address.
    /// </summary>
    protected string _address = "";

    /// <summary>
    ///   The address of the message.
    /// </summary>
    public string Address
    {
      get
      {
        return _address != null ? _address : "";
      }
      set
      {
        _address = value;
      }
    }

    /// <summary>
    ///   The typetag of the message.
    /// </summary>
    public string Typetag
    {
      get
      {
        char[] types = new char[_values.Count];
        for (int i = 0; i < _values.Count; i++)
        {
          types[i] = GetTypetagCode(_values[i]);
        }
        return new string(types);
      }
    }

    /// <summary>
    ///   Get the type tag code for an object.
    /// </summary>
    /// <param name="obj">The object.</param>
    /// <returns>The typetag code on success or '?' if the object type is invalid.</returns>
    public static char GetTypetagCode(object obj)
    {
      if (obj == null)
        return 'N';
      Type t = obj.GetType();
      if (t == typeof(Int32))
        return 'i';
      else if (t == typeof(Single))
        return 'f';
      else if (t == typeof(string))
        return 's';
      else if (t == typeof(byte[]))
        return 'b';
      else if (t == typeof(Int64))
        return 'h';
      else if (t == typeof(UInt64))
        return 't';
      else if (t == typeof(Double))
        return 'd';
      else if (t == typeof(Symbol))
        return 'S';
      else if (t == typeof(byte))
        return 'c';
      else if (t == typeof(UInt32))
        return 'r';
      else if (t == typeof(MIDI))
        return 'm';
      else if (t == typeof(bool))
        return (bool)obj ? 'T' : 'F';
      return '?';
    }

    /// <summary>
    ///   Check if the value has a valid type.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>True if the value has a valid type, false if not.</returns>
    public static bool IsValueValid(object value)
    {
      return GetTypetagCode(value) != '?';
    }

    /// <summary>
    ///   Check if all of the values has a valid type.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The index of the first invalid type or -1 if all types are valid.</returns>
    public static int FindInvalidValue(List<object> values)
    {
      for (int i = 0; i < values.Count; i++)
      {
        if (GetTypetagCode(values[i]) == '?')
          return i;
      }
      return -1;
    }

    /// <summary>
    ///   Check if all of the values has a valid type.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The index of the first invalid type or -1 if all types are valid.</returns>
    public static int FindInvalidValue(object[] values)
    {
      for (int i = 0; i < values.Length; i++)
      {
        if (GetTypetagCode(values[i]) == '?')
          return i;
      }
      return -1;
    }

    /// <summary>
    ///   Operator overload.
    /// </summary>
    public object this[int i] => _values[i];

    /// <summary>
    ///   Creates a new message.
    /// </summary>
    public OSCMessage()
    {

    }

    /// <summary>
    ///   Creates a new message.
    /// </summary>
    /// <param name="address">The address.</param>
    public OSCMessage(string address) : this()
    {
      Address = address;
    }

    /// <summary>
    ///   Creates a new message.
    /// </summary>
    /// <param name="address">The address.</param>
    /// <param name="value">A value.</param>
    public OSCMessage(string address, object value) : this(address)
    {
      Add(value);
    }

    /// <summary>
    ///   Creates a new message.
    /// </summary>
    /// <param name="address">The address.</param>
    /// <param name="values">The values.</param>
    public OSCMessage(string address, List<object> values) : this(address)
    {
      AddRange(values);
    }

    /// <summary>
    ///   Creates a new message.
    /// </summary>
    /// <param name="address">The address.</param>
    /// <param name="values">The values.</param>
    public OSCMessage(string address, object[] values) : this(address)
    {
      AddRange(values);
    }

    /// <summary>
    ///   Creates a new message.
    /// </summary>
    /// <param name="oscData">Create a new message from this OSC data.</param>
    public OSCMessage(byte[] oscData) : this()
    {
      Decode(oscData);
    }

    /// <summary>
    ///   Append a value to the OSC message.
    /// </summary>
    /// <param name="value">A single value to add.</param>
    /// <exception cref="OSCException">Thrown if the value type is invalid.</exception>
    public void Add(object value)
    {
      if (!IsValueValid(value))
        throw new OSCException("Invalid value type.");
      _values.Add(value);
    }

    /// <summary>
    ///   Append multiple values to the OSC message.
    /// </summary>
    /// <param name="values">A list of values.</param>
    /// <exception cref="OSCException">Thrown if any of the value types is invalid.</exception>
    public void AddRange(List<object> values)
    {
      int errorValue = FindInvalidValue(values);
      if (errorValue >= 0)
        throw new OSCException("Invalid value type at index " + errorValue + ".");
      _values.AddRange(values);
    }

    /// <summary>
    ///   Append multiple values to the OSC message.
    /// </summary>
    /// <param name="values">An array of values.</param>
    /// <exception cref="OSCException">Thrown if any of the value types is invalid.</exception>
    public void AddRange(object[] values)
    {
      int errorValue = FindInvalidValue(values);
      if (errorValue >= 0)
        throw new OSCException("Invalid value type at index " + errorValue + ".");
      _values.AddRange(values);
    }

    /// <summary>
    ///   Insert a value into the OSC message.
    /// </summary>
    /// <param name="index">Insert at this index.</param>
    /// <param name="value">A single value to insert.</param>
    /// <exception cref="OSCException">Thrown if the value type is invalid.</exception>
    public void Insert(int index, object value)
    {
      if (!IsValueValid(value))
        throw new OSCException("Invalid value type.");
      _values.Insert(index, value);
    }

    /// <summary>
    ///   Insert multiple values into the OSC message.
    /// </summary>
    /// <param name="index">Insert at this index.</param>
    /// <param name="values">A list of values.</param>
    /// <exception cref="OSCException">Thrown if the value type is invalid.</exception>
    public void InsertRange(int index, List<object> values)
    {
      int errorValue = FindInvalidValue(values);
      if (errorValue >= 0)
        throw new OSCException("Invalid value type at index " + errorValue + ".");
      _values.InsertRange(index, values);
    }

    /// <summary>
    ///   Insert multiple values into the OSC message.
    /// </summary>
    /// <param name="index">Insert at this index.</param>
    /// <param name="values">An array of values.</param>
    /// <exception cref="OSCException">Thrown if the value type is invalid.</exception>
    public void InsertRange(int index, object[] values)
    {
      int errorValue = FindInvalidValue(values);
      if (errorValue >= 0)
        throw new OSCException("Invalid value type at index " + errorValue + ".");
      _values.InsertRange(index, values);
    }

    /// <summary>
    ///   Set a value for the OSC message.
    /// </summary>
    /// <param name="index">The index of the value to set.</param>
    /// <param name="value">The value to set.</param>
    /// <exception cref="OSCException">Thrown if the value type is invalid.</exception>
    public void Set(int index, object value)
    {
      if (!IsValueValid(value))
        throw new OSCException("Invalid value type.");
      _values[index] = value;
    }

    /// <summary>
    ///   Set multiple values for the OSC message.
    /// </summary>
    /// <param name="index">The index of the first value to set.</param>
    /// <param name="values">The values to set.</param>
    /// <exception cref="OSCException">Thrown if any of the value types is invalid.</exception>
    public void SetRange(int index, List<object> values)
    {
      int errorValue = FindInvalidValue(values);
      if (errorValue >= 0)
        throw new OSCException("Invalid value type at index " + errorValue + ".");
      for (int i = 0; i < values.Count; i++)
        _values[i + index] = values[i];
    }

    /// <summary>
    ///   Set multiple values for the OSC message.
    /// </summary>
    /// <param name="index">The index of the first value to set.</param>
    /// <param name="values">The values to set.</param>
    /// <exception cref="OSCException">Thrown if any of the value types is invalid.</exception>
    public void SetRange(int index, object[] values)
    {
      int errorValue = FindInvalidValue(values);
      if (errorValue >= 0)
        throw new OSCException("Invalid value type at index " + errorValue + ".");
      for (int i = 0; i < values.Length; i++)
        _values[i + index] = values[i];
    }

    /// <summary>
    ///   Remove a value from the OSC message.
    /// </summary>
    /// <param name="index">The index of the value to remove.</param>
    public void RemoveAt(int index)
    {
      _values.RemoveAt(index);
    }

    /// <summary>
    ///   Remove a range of values from the OSC message.
    /// </summary>
    /// <param name="index">The index of the first value to remove.</param>
    /// <param name="count">The number of values to remove.</param>
    public void RemoveAt(int index, int count)
    {
      _values.RemoveRange(index, count);
    }

    /// <summary>
    ///   Remove all values from the OSC message.
    /// </summary>
    public void Clear()
    {
      _values.Clear();
    }

    /// <summary>
    ///   Get a value from the OSC message.
    /// </summary>
    /// <param name="index">The index of the value to get.</param>
    public object Get(int index)
    {
      return _values[index];
    }

    /// <summary>
    ///   Get multiple values from the OSC message.
    /// </summary>
    /// <param name="index">The index of the first value to get.</param>
    /// <param name="count">The number of values to get.</param>
    public List<object> GetRange(int index, int count)
    {
      return _values.GetRange(index, count);
    }

    /// <summary>
    ///   Converts the OSC message into a human readable string.
    /// </summary>
    /// <returns>A human readable string representation of the OSC message.</returns>
    public override string ToString()
    {
      string tt = Typetag;
      string ret = "<address=\"" + Address + "\" typetag=\"" + tt + "\"";
      for (int i = 0; i < _values.Count; i++)
      {
        if (_values[i] == null)
          continue;
        Type t = _values[i].GetType();
        if (t == typeof(bool))
          continue;
        if (t == typeof(byte[]))
        {
          ret += " [";
          for (int j = 0; j < ((byte[])_values[i]).Length - 1; j++)
            ret += String.Format("0x{0:X2}, ", ((byte[])_values[i])[j]);
          ret += String.Format("0x{0:X2}]", ((byte[])_values[i])[((byte[])_values[i]).Length - 1]);
        }
        else
          ret += " [" + _values[i] + "]";
      }
      return ret + ">";
    }

    /// <summary>
    ///   Encodes OSC data and from the message values.
    /// </summary>
    /// <param name="oscData">Encode the OSC data to these bytes.</param>
    /// <param name="oscSize">Store at most this many bytes from oscData.</param>
    /// <returns>The number of encoded bytes on success.</returns>
    /// <exception cref="EngineErrorException">Thrown if the encoding failed.</exception>
    /// <remarks>It's not thread safe to encode and decode a message at the same time.</remarks>
    public int Encode(IntPtr oscData, int oscSize)
    {
      Int32 numValues = 0;
      string tt = Typetag;
      int result = 0;
      Native.OSCValue[] values = new Native.OSCValue[_values.Count];
      List<Native.PointerValue> unmanaged = new List<Native.PointerValue>();
      GCHandle valuesHandle = GCHandle.Alloc(values, GCHandleType.Pinned);
      try
      {
        for (int i = 0; i < _values.Count; i++)
        {
          if (_values[i] == null)
            continue;
          Type t = _values[i].GetType();
          if (t == typeof(bool))
            continue;
          if (t == typeof(Int32))
            values[i].i = (Int32)_values[i];
          else if (t == typeof(Single))
            values[i].f = (Single)_values[i];
          else if (t == typeof(string))
          {
            values[i].s.size = ((string)_values[i]).Length;
            values[i].s.ptr = Marshal.StringToHGlobalAnsi((string)_values[i]);
            unmanaged.Add(values[i].s);
          }
          else if (t == typeof(byte[]))
          {
            values[i].b.size = ((byte[])_values[i]).Length;
            values[i].b.ptr = Marshal.AllocHGlobal(values[i].s.size);
            Marshal.Copy((byte[])_values[i], 0, values[i].b.ptr, values[i].s.size);
            unmanaged.Add(values[i].b);
          }
          else if (t == typeof(Int64))
            values[i].h = (Int64)_values[i];
          else if (t == typeof(UInt64))
            values[i].t = (UInt64)_values[i];
          else if (t == typeof(Double))
            values[i].d = (Double)_values[i];
          else if (t == typeof(Symbol))
          {
            string s = ((Symbol)_values[i]).Value;
            values[i].S.size = s.Length;
            values[i].S.ptr = Marshal.StringToHGlobalAnsi(s);
            unmanaged.Add(values[i].S);
          }
          else if (t == typeof(byte))
            values[i].c = (byte)_values[i];
          else if (t == typeof(UInt32))
            values[i].r = (UInt32)_values[i];
          else if (t == typeof(MIDI))
          {
            values[i].m.status = ((MIDI)_values[i]).Status;
            values[i].m.data1 = ((MIDI)_values[i]).Data1;
            values[i].m.data2 = ((MIDI)_values[i]).Data2;
            values[i].m.pad = 0x00;
          }
          else
            throw new OSCException("You have found a bug!");
        }
        IntPtr valuesPtr = valuesHandle.AddrOfPinnedObject();
        result = Native.reactional_osc_message_encode(oscData, oscSize, Address, Address.Length, tt, tt.Length, valuesPtr, ref numValues);
      }
      finally
      {
        valuesHandle.Free();
        for (int i = 0; i < unmanaged.Count; i++)
          Marshal.FreeHGlobal(unmanaged[i].ptr);
      }
      return result;
    }

    /// <summary>
    ///   Encodes OSC data and from the message values.
    /// </summary>
    /// <param name="oscData">Encode the OSC data to these bytes.</param>
    /// <returns>The number of encoded bytes on success.</returns>
    /// <exception cref="EngineErrorException">Thrown if the encoding failed.</exception>
    /// <remarks>It's not thread safe to encode and decode a message at the same time.</remarks>
    public int Encode(byte[] oscData)
    {
      GCHandle dataHandle = GCHandle.Alloc(oscData, GCHandleType.Pinned);
      int result;
      try
      {
        IntPtr ptr = dataHandle.AddrOfPinnedObject();
        result = Encode(ptr, oscData.Length);
      }
      finally
      {
        dataHandle.Free();
      }
      return result;
    }

    /// <summary>
    ///   Decodes OSC data and updates the message values.
    /// </summary>
    /// <param name="oscData">The OSC data to decode.</param>
    /// <param name="oscSize">Read at most this many bytes from oscData.</param>
    /// <param name="maxValues">The maximum number of values to store.</param>
    /// <returns>The number of decoded bytes on success.</returns>
    /// <exception cref="EngineErrorException">Thrown if the decoding failed.</exception>
    /// <remarks>It's not thread safe to encode and decode at the same time.</remarks>
    public int Decode(IntPtr oscData, int oscSize, int maxValues = 16)
    {
      int result;
      Int32 numValues = 0;
      Native.OSCValue[] values = new Native.OSCValue[maxValues];
      IntPtr address = IntPtr.Zero;
      Int32 addressLength = 0;
      IntPtr typetag = IntPtr.Zero;
      Int32 typetagLength = 0;
      GCHandle valuesHandle = GCHandle.Alloc(values, GCHandleType.Pinned);
      try
      {
        IntPtr valuesPtr = valuesHandle.AddrOfPinnedObject();
        result = Native.reactional_osc_message_decode(oscData, oscSize, ref address, ref addressLength, ref typetag, ref typetagLength, maxValues, valuesPtr, ref numValues);
      }
      finally
      {
        valuesHandle.Free();
      }
      Address = Native.StringifyPointer(address);
      string t = Native.StringifyPointer(typetag);
      _values.Clear();
      for (int i = 0; i < numValues; i++)
      {
        switch (t[i])
        {
          case 'i':
            _values.Add(values[i].i);
            break;
          case 'f':
            _values.Add(values[i].f);
            break;
          case 's':
            _values.Add(Native.StringifyPointer(values[i].s.ptr));
            break;
          case 'b':
            byte[] bytes = new byte[values[i].b.size];
            Marshal.Copy(values[i].b.ptr, bytes, 0, values[i].b.size);
            _values.Add(bytes);
            break;
          case 'h':
            _values.Add(values[i].h);
            break;
          case 't':
            _values.Add(values[i].t);
            break;
          case 'd':
            _values.Add(values[i].d);
            break;
          case 'S':
            _values.Add(new Symbol(Native.StringifyPointer(values[i].s.ptr)));
            break;
          case 'c':
            _values.Add(values[i].c);
            break;
          case 'r':
            _values.Add(values[i].r);
            break;
          case 'm':
            _values.Add(new MIDI(values[i].m.status, values[i].m.data1, values[i].m.data2));
            break;
          case 'T':
            _values.Add(true);
            break;
          case 'F':
            _values.Add(false);
            break;
          case 'N':
            _values.Add(null);
            break;
          case 'I':
            break;
          default:
            throw new OSCException("You have found a bug!");
        }
      }
      return result;
    }

    /// <summary>
    ///   Decodes OSC data and updates the message values.
    /// </summary>
    /// <param name="oscData">The OSC data to decode.</param>
    /// <param name="maxValues">The maximum number of values to store.</param>
    /// <returns>The number of decoded bytes on success.</returns>
    /// <exception cref="EngineErrorException">Thrown if the decoding failed.</exception>
    /// <remarks>It's not thread safe to encode and decode at the same time.</remarks>
    public int Decode(byte[] oscData, int maxValues = 16)
    {
      GCHandle dataHandle = GCHandle.Alloc(oscData, GCHandleType.Pinned);
      int result;
      try
      {
        IntPtr ptr = dataHandle.AddrOfPinnedObject();
        result = Decode(ptr, oscData.Length, maxValues);
      }
      finally
      {
        dataHandle.Free();
      }
      return result;
    }

    /// <summary>
    ///   Check for equality between two objects.
    /// </summary>
    /// <param name="other">The other object to compare with.</param>
    /// <returns>True if the two objects are considered equal.</returns>
    public override bool Equals(object other)
    {
      if (other != null && other.GetType() == typeof(OSCMessage))
      {
        OSCMessage m = (OSCMessage)other;
        if (Address != m.Address)
          return false;
        if (Count != m.Count)
          return false;
        for (int i = 0; i < Count; i++)
        {
          object v = m.Get(i);
          if (_values[i] != null && v != null)
          {
            if (_values[i].GetType() != v.GetType())
              return false;
            if (_values[i].GetType() == typeof(byte[]))
            {
              if (((byte[])_values[i]).Length != ((byte[])v).Length)
                return false;
              for (int j = 0; j < ((byte[])_values[i]).Length; j++)
                if (((byte[])_values[i])[j] != ((byte[])v)[j])
                  return false;
            }
          }
          else if (_values[i] == null)
          {
            if (v != null)
              return false;
          }
          else if (!_values[i].Equals(v))
            return false;
        }
        return true;
      }
      return false;
    }

    /// <summary>
    ///   Get the hash code of the object.
    /// </summary>
    /// <returns>The hash code.</returns>
    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

  };

};
