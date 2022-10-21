using BepInEx;
using BepInEx.Configuration;
using UnityEngine;
using System.Collections.Generic;
using System;
using HarmonyLib;

namespace StickLib
{
    public class Log : MonoBehaviour
    {
        static List<string> messages = new List<string>();
        Rect console = new Rect(20, 20, 524, 600);
        static Vector2 scrollPosition;
        static string command = "";
        public static List<string> commands = new List<string>();
        static List<Action<List<string>>> actions = new List<Action<List<string>>>();
        public static bool consoleOpen = false;

        public static void Message(string message, bool showTime = true)
        {
            string time = DateTime.Now.ToLongTimeString();
            if (!showTime) time = "";
            message = $"[{time}] {message}";
            messages.Add(message);
            scrollPosition = new Vector2(0, 100f * messages.Count);
            if (messages.Count > 256) //Config.ConsoleLimit.Value
            {
                messages.RemoveAt(0);
            }
        }
        public static void Error(string message, bool showTime = true)
        {
            string time = DateTime.Now.ToLongTimeString();
            if (!showTime) time = "";
            message = $"<color=red>[{time}] {message}</color>";
            messages.Add(message);
            scrollPosition = new Vector2(0, 100f * messages.Count);
            if (messages.Count > 256) //Config.ConsoleLimit.Value
            {
                messages.RemoveAt(0);
            }
        }

        public static bool RegisterCommand(string name, Action<List<string>> action)
        {
            if (commands.IndexOf(name) >= 0)
            {
                return false;
            }
            commands.Add(name.ToLower());
            actions.Add(action);
            return true;
        }

        void Start()
        {
            RegisterCommand("hello", DefaultCommands.Hello);
            RegisterCommand("help", DefaultCommands.Help);
        }

        void Update()
        {
            if (Input.GetKeyDown("`"))
            {
                consoleOpen = !consoleOpen;
            }
        }

        void OnGUI()
        {
            if (consoleOpen) console = GUI.Window(1001, console, ConsoleWindow, "Console");
        }
        void ConsoleWindow(int index)
        {
            GUI.DragWindow(new Rect(0, 0, 10000, 20));

            scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(512), GUILayout.Height(512));
            foreach (string str in messages)
            {
                GUILayout.Label(str);
            }
            GUILayout.EndScrollView();

            command = GUILayout.TextField(command);
            if (GUILayout.Button("Run"))
            {
                ParseCommand(command);
                command = "";
            }
        }

        public void ParseCommand(string message)
        {
            string cmd = message.Split()[0];
            List<string> args = new List<string>(message.Split());
            args.RemoveAt(0);

            string argsJoined = string.Join(" ", args.ToArray());
            Message($"<color=grey>> {cmd} {argsJoined}</color>");

            int index = commands.IndexOf(cmd);
            if (index >= 0)
            {
                actions[index](args);
            }
            else
            {
                Error($"Command does not exist.");
            }
        }
    }
}