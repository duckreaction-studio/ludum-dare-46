﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Helper
{
    public class Utils
    {
        public static void ClearLogs()
        {
#if UNITY_EDITOR
            var logEntries = System.Type.GetType("UnityEditor.LogEntries, UnityEditor.dll");
            var clearMethod = logEntries.GetMethod("Clear", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
            clearMethod.Invoke(null, null);
#endif
        }
    }

}