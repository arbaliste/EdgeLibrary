﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.IO;

namespace EdgeLibrary
{
    public static class DebugLogger
    {
        private static StreamWriter streamWriter;
        public static List<string> Logs;

        public static void Init(string writePath)
        {
            string path = writePath + "/" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + "_" + DateTime.Now.Hour + "." + DateTime.Now.Minute + "." + DateTime.Now.Second + ".txt";
            streamWriter = new StreamWriter(path);
            streamWriter.WriteLine("Logs for:" + DateTime.Now.ToString());
            streamWriter.WriteLine();

            Logs = new List<string>();

            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnApplicationQuit);
        }

        private static void OnApplicationQuit(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.Assert(false, "Test");
        }

        public static void Log(string text, params string[] properties)
        {
            string log = text + " { ";
            foreach (string property in properties)
            {
                log += property + ", ";
            }
            log.Remove(log.Length - 1);
            log += " }";
            Logs.Add(log);

            if (streamWriter != null)
            {
                streamWriter.WriteLine(text);
                if (properties.Length > 0)
                {
                    streamWriter.WriteLine("     {");
                }
                foreach(string property in properties)
                {
                    streamWriter.WriteLine("        " + property);
                }
                if (properties.Length > 0)
                {
                    streamWriter.WriteLine("     }");
                }
                streamWriter.WriteLine();
            }
        }

        public static void LogEvent(string text, params string[] properties)
        {
            string log = text + " { ";
            foreach (string property in properties)
            {
                log += property + ", ";
            }
            log.Remove(log.Length - 1);
            log += " }";
            Logs.Add(log);

            if (streamWriter != null)
            {
                for (int i = 0; i < text.Length/2 + 4; i++)
                {
                    streamWriter.Write("<>");
                }
                streamWriter.WriteLine();
                streamWriter.WriteLine("> " + text);
                if (properties.Length > 0)
                {
                    streamWriter.WriteLine(">    {");
                }
                foreach (string property in properties)
                {
                    streamWriter.WriteLine(">        " + property);
                }
                if (properties.Length > 0)
                {
                    streamWriter.WriteLine(">    }");
                }
                for (int i = 0; i < text.Length/2 + 4; i++)
                {
                    streamWriter.Write("<>");
                }
                streamWriter.WriteLine();
                streamWriter.WriteLine();
            }
        }

        public static void LogAdd(string text, params string[] properties)
        {
            string log = text + " { ";
            foreach (string property in properties)
            {
                log += property + ", ";
            }
            log.Remove(log.Length - 1);
            log += " }";
            Logs.Add(log);

            if (streamWriter != null)
            {
                streamWriter.WriteLine("+ " + text);
                if (properties.Length > 0)
                {
                    streamWriter.WriteLine("+    {");
                }
                foreach (string property in properties)
                {
                    streamWriter.WriteLine("+        " + property);
                }
                if (properties.Length > 0)
                {
                    streamWriter.WriteLine("+    }");
                }
                streamWriter.WriteLine();
            }
        }

        public static void LogRemove(string text, params string[] properties)
        {
            string log = text + " { ";
            foreach (string property in properties)
            {
                log += property + ", ";
            }
            log.Remove(log.Length - 1);
            log += " }";
            Logs.Add(log);

            if (streamWriter != null)
            {
                streamWriter.WriteLine("- " + text);
                if (properties.Length > 0)
                {
                    streamWriter.WriteLine("-    {");
                }
                foreach (string property in properties)
                {
                    streamWriter.WriteLine("-        " + property);
                }
                if (properties.Length > 0)
                {
                    streamWriter.WriteLine("-    }");
                }
                streamWriter.WriteLine();
            }
        }

        public static void LogWarning(string text, params string[] properties)
        {
            string log = text + " { ";
            foreach (string property in properties)
            {
                log += property + ", ";
            }
            log.Remove(log.Length - 1);
            log += " }";
            Logs.Add(log);

            if (streamWriter != null)
            {
                streamWriter.WriteLine("! " + text);
                if (properties.Length > 0)
                {
                    streamWriter.WriteLine("!    {");
                }
                foreach (string property in properties)
                {
                    streamWriter.WriteLine("!        " + property);
                }
                if (properties.Length > 0)
                {
                    streamWriter.WriteLine("!    }");
                }
                streamWriter.WriteLine();
            }
        }

        public static void LogError(string text, params string[] properties)
        {
            string log = text + " { ";
            foreach (string property in properties)
            {
                log += property + ", ";
            }
            log.Remove(log.Length - 1);
            log += " }";
            Logs.Add(log);

            if (streamWriter != null)
            {
                for (int i = 0; i < text.Length + 8; i++)
                {
                    streamWriter.Write('#');
                }
                streamWriter.WriteLine();
                streamWriter.WriteLine("!!! " + text + " !!!");
                if (properties.Length > 0)
                {
                    streamWriter.WriteLine("#    {");
                }
                foreach (string property in properties)
                {
                    streamWriter.WriteLine("#        " + property);
                }
                if (properties.Length > 0)
                {
                    streamWriter.WriteLine("#    }");
                }
                for (int i = 0; i < text.Length + 8; i++)
                {
                    streamWriter.Write('#');
                }
                streamWriter.WriteLine();
                streamWriter.WriteLine();
            }
        }
    }
}
