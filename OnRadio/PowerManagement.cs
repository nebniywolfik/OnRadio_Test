﻿using System;
using System.Runtime.InteropServices;

namespace PowerManagement
{
    static class xbs_system_functions
    {
        private static volatile WindowsNativeMethods.EXECUTION_STATE fPreviousExecutionState;

        public static void PreventSystemFromSleeping()
        {
            try
            {
                fPreviousExecutionState = WindowsNativeMethods.SetThreadExecutionState(WindowsNativeMethods.EXECUTION_STATE.ES_CONTINUOUS | WindowsNativeMethods.EXECUTION_STATE.ES_AWAYMODE_REQUIRED | WindowsNativeMethods.EXECUTION_STATE.ES_SYSTEM_REQUIRED);
            }
            catch (DllNotFoundException)
            {
            }
            catch (EntryPointNotFoundException)
            {
            }
        }
        public static void restoreSystemSleepState()
        {
            try
            {
                WindowsNativeMethods.SetThreadExecutionState(WindowsNativeMethods.EXECUTION_STATE.ES_CONTINUOUS);
            }
            catch (DllNotFoundException)
            {
            }
            catch (EntryPointNotFoundException)
            {
            }
        }
    }
    internal static class WindowsNativeMethods
    {
        [FlagsAttribute]
        public enum EXECUTION_STATE : uint
        {
            ES_AWAYMODE_REQUIRED = 0x00000040,
            ES_CONTINUOUS = 0x80000000,
            ES_DISPLAY_REQUIRED = 0x00000002,
            ES_SYSTEM_REQUIRED = 0x00000001
            // Legacy flag, should not be used.
            // ES_USER_PRESENT = 0x00000004
        }
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags);
    }
}

