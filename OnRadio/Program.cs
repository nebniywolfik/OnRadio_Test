﻿using OnRadio;
using RunOnlyOne;
using System;
using System.Windows.Forms;

namespace Tirage.OnRadio
{
    static class Program
    {
        public static Form1 f1; // переменная, которая будет содержать ссылку на форму Form1
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            if (RunOnlyOneClass.ChekRunProgramm("OnRadio")) 
           return;

            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
                
            }
            
        }
    }
}