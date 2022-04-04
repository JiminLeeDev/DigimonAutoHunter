using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace DigimonAutoHunter
{
    static class Winapi
    {
        const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        const int MOUSEEVENTF_LEFTUP = 0x0004;
        const int WM_KEYDOWN = 0x100;
        const int WH_KEYBOARD_LL = 13;

        static int hhook = 0;

        [DllImport("kernel32.dll")]
        static extern IntPtr LoadLibrary(string lpFileName);

        [DllImport("user32.dll")]
        static extern int SetWindowsHookEx(int hhook, LowLevelKeyboardProc lpfn, IntPtr hInstance, int threadId);

        [DllImport("user32.dll")]
        static extern int CallNextHookEx(int hhook, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        static extern bool UnHookWindowsEx(int idHook);

        [DllImport("user32.dll")]
        static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, int dwExtraInfo);

        delegate int LowLevelKeyboardProc(int Code, IntPtr wParam, IntPtr lParam);

        public static event Action<int> KeyDown;

        public static void StartHook()
        {
            if (hhook != 0)
            {
                return;
            }

            hhook = SetWindowsHookEx(WH_KEYBOARD_LL, new LowLevelKeyboardProc((int code, IntPtr wParam, IntPtr lParam) =>
            {
                if (code >= 0 && wParam == (IntPtr)WM_KEYDOWN)
                {
                    KeyDown.Invoke(Marshal.ReadInt32(lParam));
                }

                return CallNextHookEx(WH_KEYBOARD_LL, code, wParam, lParam);
            }), LoadLibrary("User32"), 0);
        }

        public static void StopHook()
        {
            UnHookWindowsEx(hhook);
        }

        public static void Click(Point clickLocation)
        {
            Cursor.Position = clickLocation;

            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }
    }
}