using System;
using System.IO;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace ProductionPlanning
{
    internal class Logger
    {
        public static void SaveError(string RaisedBy, string ErrorMessage)
        {
            var LogTime = DateTime.Now;
            string filename = "Log-" + LogTime.ToString("yyyy-MM-dd") + ".txt";
            string ErrorLogPath = Application.StartupPath + "/ErrorLog/";
            string filepath = ErrorLogPath + filename;
            if (!Directory.Exists(ErrorLogPath))
            {
                Directory.CreateDirectory(ErrorLogPath);
            }

            if (!File.Exists(filepath))
            {
                var stwriter = File.CreateText(filepath);
                stwriter.WriteLine("EPPS Errors at " + LogTime.ToString("yyyy-MM-dd"));
                stwriter.WriteLine("-----------------------------------------------");
                stwriter.Close();
            }

            if (File.Exists(filepath))
            {
                using (var stwriter = new StreamWriter(filepath, true))
                {
                    stwriter.WriteLine("-----------------------------------------------");
                    stwriter.WriteLine("---- Error At: " + Conversions.ToString(LogTime));
                    stwriter.WriteLine("---- Raised By: " + RaisedBy);
                    stwriter.WriteLine(ErrorMessage);
                    stwriter.WriteLine("---- END --------------------------------------");
                }
            }
        }

        public static void LogException(string RaisedBy, Exception Ex)
        {
            SaveError(RaisedBy, Ex.Message + Constants.vbCrLf + Ex.Source + Constants.vbCrLf + Ex.StackTrace);
        }

        public static void LogInfo(string Message)
        {
            var LogTime = DateTime.Now;
            string filename = "InfoLog-" + LogTime.ToString("yyyy-MM-dd") + ".txt";
            string infoLogPath = Application.StartupPath + "/InfoLog/";
            string filepath = infoLogPath + filename;
            if (!Directory.Exists(infoLogPath))
            {
                Directory.CreateDirectory(infoLogPath);
            }

            if (!File.Exists(filepath))
            {
                var stwriter = File.CreateText(filepath);
                stwriter.WriteLine("EPPS Info Logs at " + LogTime.ToString("yyyy-MM-dd"));
                stwriter.WriteLine("-----------------------------------------------");
                stwriter.Close();
            }

            if (File.Exists(filepath))
            {
                using (var stwriter = new StreamWriter(filepath, true))
                {
                    stwriter.WriteLine($"-- {LogTime.ToString()}: {Message}");
                }
            }
        }
    }
}