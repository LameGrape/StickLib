using BepInEx;
using UnityEngine;
using System.Collections.Generic;

namespace StickLib
{
    internal class DefaultCommands
    {

        internal static void Hello(List<string> args)
        {
            Log.Message("Hello world!");
        }
        internal static void Help(List<string> args)
        {
            Log.Message(string.Join(", ", Log.commands.ToArray()));
        }
    }
}