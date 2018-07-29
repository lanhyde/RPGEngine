using System.Runtime.InteropServices;
using System;
using System.Management;
using System.Threading;
using System.Diagnostics;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
internal struct TokPriv1Luid
{
    public int Count;
    public long Luid;
    public int Attr;
}
public abstract class WindowsCore {
    internal const int SE_PRIVILEGE_ENABLED         = 0x00000002;
    internal const int TOKEN_QUERY                  = 0x00000008;
    internal const int TOKEN_ADJUST_PRIVILEGES      = 0x00000020;
    internal const string SE_SHUTDOWN_NAME          = "SeShutdownPrivilege";
    internal const int EWX_LOGOFF                   = 0x00000000;
    internal const int EWX_SHUTDOWN                 = 0x00000001;
    internal const int EWX_REBOOT                   = 0x00000002;
    internal const int EWX_FORCE                    = 0x00000004;
    internal const int EWX_POWEROFF                 = 0x00000008;
    internal const int EWX_FORCEIFHUNG              = 0x00000010;
    [DllImport("user32.dll", EntryPoint = "SetWindowLongA")]
    internal static extern int SetWindowLong(int hwnd, int nIndex, long dwNewLong);
    [DllImport("user32.dll", EntryPoint = "SetLayedWindowAttributes")]
    internal static extern int SetLayedWindowAttributes(int hwnd, int crKey, byte bAlpha, int dwFlags);
    [DllImport("user32.dll")]
    internal static extern bool ShowWindowAsync(int hWnd, int nCmdShow);
    [DllImport("user32.dll", EntryPoint = "GetActiveWindow")]
    internal static extern int GetActiveWindow();
    [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
    internal static extern int GetWindowLong(int hWnd, int nIndex);
    [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
    internal static extern int SetWindowPos(int hWnd, int hwndInsertAfter, int x, int y, int xc, int cy, int uFlags);
    [DllImport("kernel32.dll", ExactSpelling = true)]
    internal static extern IntPtr GetCurrentProcess();
    [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
    internal static extern bool OpenProcessToken(IntPtr h, int acc, ref IntPtr phtok);
    [DllImport("advapi32.dll", SetLastError = true)]
    internal static extern bool LookupPrivilegeValue(string host, string name, ref long pluid);
    [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
    internal static extern bool AdjustTokenPrivileges(IntPtr htok, bool disall, ref TokPriv1Luid newst,
                                                        int len, IntPtr prev, IntPtr relen);
    [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
    internal static extern bool ExitWindowsEx(int flag, int rea);
}
