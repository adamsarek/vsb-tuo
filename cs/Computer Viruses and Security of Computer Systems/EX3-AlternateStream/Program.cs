using Microsoft.Win32.SafeHandles;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
//#pragma comment(linker, "/SUBSYSTEM:windows /ENTRY:mainCRTStartup")

namespace AlternateStream
{
    static class Program
    {
        private static DateTime date = DateTime.UtcNow;
        private static string student = "SAR0083";
        private static string fileName = Application.StartupPath + @"\file.txt";
        private static string fileContent = "This file is useless, unless you find out the truth hidden in its alternate data stream!";
        private static string fileAltStreamName = "secret";
        private static string fileAltStreamContent = string.Join("; ", new string[] { date.ToString(), student });
        private static string fileFullName = string.Join(":", new string[] { fileName, fileAltStreamName });

        /// <summary>
        /// Hlavní vstupní bod aplikace.
        /// </summary>
        [STAThread]
        static void Main()
        {
            WriteFileStream(fileName, fileContent);
            WriteFileStream(fileFullName, fileAltStreamContent);

            DebugFileStream(fileName, ReadFileStream(fileName));
            DebugFileStream(fileFullName, ReadFileStream(fileFullName));

            Application.Run();
        }

        public static void DebugFileStream(string path, string content)
        {
            Debug.WriteLine("Stream: {0}; Content: {1}", path, content);
        }

        public static string ReadFileStream(string path)
        {
            using (StreamReader sr = new StreamReader(CreateFileStream(path, FileAccess.Read, FileMode.Open, FileShare.Read)))
            {
                return sr.ReadToEnd();
            }
        }

        public static void WriteFileStream(string path, string content)
        {
            using (StreamWriter sw = new StreamWriter(CreateFileStream(path, FileAccess.Write, FileMode.OpenOrCreate, FileShare.Delete)))
            {
                sw.Write(content);
            }
        }

        public static FileStream CreateFileStream(string path, FileAccess access, FileMode mode, FileShare share)
        {
            SafeFileHandle handle;
            try
            {
                handle = CreateFile(path, access, share, IntPtr.Zero, mode, 0, IntPtr.Zero);
            } catch(IOException e)
            {
                throw e;
            }

            return new FileStream(handle, access);
        }

        [DllImport("kernel32.dll")]
        public static extern SafeFileHandle CreateFile(
            string lpFileName,
            FileAccess dwDesiredAccess,
            FileShare dwShareMode,
            IntPtr lpSecurityAttributes,
            FileMode dwCreationDisposition,
            uint dwFlagsAndAttributes,
            IntPtr hTemplateFile
        );
    }
}
