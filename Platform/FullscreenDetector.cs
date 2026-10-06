using System.Runtime.InteropServices;

namespace CodeQuest.Platform;

public static class FullscreenDetector
{
    [DllImport("kernel32.dll")]
    private static extern IntPtr GetConsoleWindow();

    [DllImport("user32.dll")]
    private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

    [DllImport("user32.dll")]
    private static extern bool IsZoomed(IntPtr hWnd);

    [DllImport("user32.dll")]
    private static extern IntPtr MonitorFromWindow(IntPtr hwnd, uint dwFlags);

    [DllImport("user32.dll")]
    private static extern bool GetMonitorInfo(IntPtr hMonitor, ref MONITORINFO lpmi);

    [StructLayout(LayoutKind.Sequential)]
    private struct RECT { public int Left, Top, Right, Bottom; }

    [StructLayout(LayoutKind.Sequential)]
    private struct MONITORINFO
    {
        public int cbSize;
        public RECT rcMonitor;
        public RECT rcWork;
        public uint dwFlags;
    }

    private const uint MONITOR_DEFAULTTONEAREST = 2;

    private static bool IsFullscreenViaPInvoke()
    {
        try
        {
            IntPtr hWnd = GetConsoleWindow();
            if (hWnd == IntPtr.Zero) return false;
            if (IsZoomed(hWnd)) return true;
            if (!GetWindowRect(hWnd, out RECT wr)) return false;

            IntPtr hMon = MonitorFromWindow(hWnd, MONITOR_DEFAULTTONEAREST);
            var mi = new MONITORINFO { cbSize = Marshal.SizeOf<MONITORINFO>() };
            if (!GetMonitorInfo(hMon, ref mi)) return false;

            return wr.Left <= mi.rcMonitor.Left
                && wr.Top <= mi.rcMonitor.Top
                && wr.Right >= mi.rcMonitor.Right
                && wr.Bottom >= mi.rcMonitor.Bottom;
        }
        catch { return false; }
    }

    /// <summary>
    /// Renvoie true si on doit afficher le message "Appuyez sur F11".
    /// Combine P/Invoke (Windows conhost classique) et une heuristique
    /// par taille (Windows Terminal, où P/Invoke échoue).
    /// </summary>
    public static bool ShouldShowFullscreenHint()
    {
        try
        {
            if (Console.WindowWidth >= 120 && Console.WindowHeight >= 34)
                return false; // déjà assez grand
        }
        catch { return false; }

        if (IsFullscreenViaPInvoke()) return false;

        return true;
    }
}