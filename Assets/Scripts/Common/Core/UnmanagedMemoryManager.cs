using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public static class UnmanagedMemoryManager {
    public static int SystemDefaultCharSize
    {
        get
        {
            return Marshal.SystemDefaultCharSize;
        }
    }
    public static int SystemMaxDBCSCharSize
    {
        get
        {
            return Marshal.SystemMaxDBCSCharSize;
        }
    }

    public static int SizeOf(Type targetType)
    {
        return Marshal.SizeOf(targetType);
    }

    public static IntPtr AllocateHGlobal(int size)
    {
        return Marshal.AllocHGlobal(size);
    }

    public static IntPtr AllocateHGlobal(IntPtr hGlobal)
    {
        return Marshal.AllocHGlobal(hGlobal);
    }

    public static void FreeHGlobal(IntPtr pointer)
    {
        Marshal.FreeHGlobal(pointer);
    }

    public static void Copy(byte[] source, int startIndex, IntPtr destination, int length)
    {
        Marshal.Copy(source, startIndex, destination, length);
    }

    public static void Copy(char[] source, int startIndex, IntPtr destination, int length)
    {
        Marshal.Copy(source, startIndex, destination, length);
    }

    public static void Copy(float[] source, int startIndex, IntPtr destination, int length)
    {
        Marshal.Copy(source, startIndex, destination, length);
    }

    public static void Copy(Int16[] source, int startIndex, IntPtr destination, int length)
    {
        Marshal.Copy(source, startIndex, destination, length);
    }

    public static void Copy(Int32[] source, int startIndex, IntPtr destination, int length)
    {
        Marshal.Copy(source, startIndex, destination, length);
    }

    public static void Copy(Int64[] source, int startIndex, IntPtr destination, int length)
    {
        Marshal.Copy(source, startIndex, destination, length);
    }

    public static void Copy(IntPtr source, byte[] destination, int startIndex, int length)
    {
        Marshal.Copy(source, destination, 0, destination.Length);
    }

    public static void Copy(IntPtr source, char[] destination, int startIndex, int length)
    {
        Marshal.Copy(source, destination, 0, destination.Length);
    }

    public static void Copy(IntPtr source, float[] destination, int startIndex, int length)
    {
        Marshal.Copy(source, destination, 0, destination.Length);
    }

    public static void Copy(IntPtr source, Int16[] destination, int startIndex, int length)
    {
        Marshal.Copy(source, destination, 0, destination.Length);
    }

    public static void Copy(IntPtr source, Int32[] destination, int startIndex, int length)
    {
        Marshal.Copy(source, destination, 0, destination.Length);
    }

    public static void Copy(IntPtr source, Int64[] destination, int startIndex, int length)
    {
        Marshal.Copy(source, destination, 0, destination.Length);
    }

    public static void StructureToPtr(object obj, IntPtr intPtr, bool deleteOld = true)
    {
        Marshal.StructureToPtr(obj, intPtr, deleteOld);
    }

    public static object PtrToStructure(IntPtr intPtr, Type type)
    {
        return Marshal.PtrToStructure(intPtr, type);
    }

    public static IntPtr StringToHGlobalAnsi(string str)
    {
        return Marshal.StringToHGlobalAnsi(str);
    }

    public static IntPtr SecureStringToGlobalAllocAnsi(System.Security.SecureString str)
    {
        return Marshal.SecureStringToGlobalAllocAnsi(str);
    }

    public static IntPtr StringToHGlobalUni(string str)
    {
        return Marshal.StringToHGlobalUni(str);
    }

    public static IntPtr SecureStringToGlobalAllocUnicode(System.Security.SecureString str)
    {
        return Marshal.SecureStringToGlobalAllocUnicode(str);
    }

    public static string PtrToStringAnsi(IntPtr intPtr)
    {
        return Marshal.PtrToStringAnsi(intPtr);
    }

    public static string PtrToStringUnicode(IntPtr intPtr)
    {
        return Marshal.PtrToStringUni(intPtr);
    }

#if UNITY_STANDALONE
    public static int GetWin32LastError()
    {
        return Marshal.GetLastWin32Error();
    }
#endif


}
