using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Runtime.InteropServices;
using System.IO;
using OnRadio;
using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;
using Tirage.OnRadio;

namespace RunOnlyOne
{


    public static class RunOnlyOneClass
    {
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern int ShowWindow(int hwnd, int nCmdShow);

        static Mutex _syncObject;
        static readonly string AppPath = Path.GetFileNameWithoutExtension(Application.ExecutablePath);

       


        // Находит запущенную копию приложения и разворачивает окно
        // <param name="UniqueValue">уникальное значение для каждой программы (можно имя)</param>
        // <returns>true - если приложение было запущено</returns>
        public static bool ChekRunProgramm(string UniqueValue)
        {
     
            bool applicationRun = false;
            _syncObject = new Mutex(true, UniqueValue, out applicationRun);
            if (!applicationRun)
           
            {//восстановить/развернуть окно                              
                try
                {
                    Process[] procs = Process.GetProcessesByName(AppPath);
                    
                    foreach (Process proc in procs)
                        if (proc.Id != Process.GetCurrentProcess().Id)
                        {
                            
                          ShowWindow((int)proc.MainWindowHandle, 1);//нормально развернутое
                          // ShowWindow((int)proc.MainWindowHandle, 3);//максимально развернутое
                            SetForegroundWindow(proc.MainWindowHandle);
                            // Program.f1.add_c();
                            //if (OnRadio.Form1.ActiveForm.WindowState == FormWindowState.Minimized)
                            //{
                            //    OnRadio.Form1.ActiveForm.WindowState = FormWindowState.Normal;

                            //}
                          
                                // MessageBox.Show(MyStrings.RunOnly, MyStrings.button8Click2, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);

                                break;
                        }
                }
                catch { return false; }
            }
            return !applicationRun;
        }
    }
}
