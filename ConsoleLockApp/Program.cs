using System;
using System.IO;
using System.Net.Mail;

namespace ConsoleLockApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string pass = "passssssswooord";
            string strCmdText;
            string otp = new Random().Next(100000).ToString();
            string root = @"c:\Control Panel.{21EC2020-3AEA-1069-A2DD-08002B30309D}";
            int o = 0;
            // If directory , goto unlock
            if (Directory.Exists(root))
            {
                goto unlock;
            } // If directory , goto lock folder
            else if (Directory.Exists(@"E:\Folder"))
            {
            confirm:
                Console.WriteLine("Are you sure to lock this folder? (Y/N)");
                var opt = (char)Console.Read();
                if (opt.ToString().ToUpper() == "Y")
                {
                    goto locks;
                }
                else if (opt.ToString().ToUpper() == "N") { goto end; }
                else { Console.WriteLine("Invalid Choice"); goto confirm; }
            }
            else { goto end; }
        locks:
            Directory.Move(@"E:/Folder", root);
            strCmdText = "/C attrib +h +s \"c:/Control Panel.{21EC2020-3AEA-1069-A2DD-08002B30309D}\"";
            System.Diagnostics.Process.Start("CMD.exe", strCmdText);
            Console.WriteLine("Folder Locked");
            goto end;
        unlock:
            Console.WriteLine("Enter Password To Unlock");
            var p = Console.ReadLine();
            if (p == pass || (p == otp && o == 1))
            {
                strCmdText = "/C attrib -h -s \"c:/Control Panel.{21EC2020-3AEA-1069-A2DD-08002B30309D}\"";
                System.Diagnostics.Process.Start("CMD.exe", strCmdText);
                Directory.Move(root, @"C:/Folder");
                Console.WriteLine("Folder Unlocked");
            }
            else
            {
                Console.WriteLine("Invalid Password");
                Console.WriteLine("Send OTP ? (y/n)");
                var opt = (char)Console.Read();
                if (opt.ToString().ToUpper() == "Y")
                {
                    goto mail;
                }
                else if (opt.ToString().ToUpper() == "N")
                {
                    goto end;
                }
                else
                {
                    Console.WriteLine("Invalid Choice"); goto end;
                }
            mail:
                o = 1;
                try
                {
                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                    mail.From = new MailAddress("username@gmail.com");
                    mail.To.Add("security_mail_address_for_OTP@gmail.com");
                    mail.Subject = "OTP";
                    mail.Body = "OTP:" + otp;
                    SmtpServer.Port = 587;
                    SmtpServer.Credentials = new System.Net.NetworkCredential("username@gmail.com", "passwor");
                    SmtpServer.EnableSsl = true;
                    Console.WriteLine("Sending mail . . . . . ");
                    SmtpServer.Send(mail);
                    Console.WriteLine("mail Send");
                    goto unlockmail;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString()); goto end;
                }
            }
            goto end;
        unlockmail:
            Console.WriteLine("Enter Password To Unlock");
            Console.ReadLine();
            var p1 = Console.ReadLine();
            if (p1 == otp && o == 1)
            {
                strCmdText = "/C attrib -h -s \"e:/Control Panel.{21EC2020-3AEA-1069-A2DD-08002B30309D}\"";
                System.Diagnostics.Process.Start("CMD.exe", strCmdText);
                Directory.Move(root, @"E:/Folder");
                Console.WriteLine("Folder Unlocked");
            }
            else
            {
                Console.WriteLine("Invalid Password");
            }
            goto end;
        end:
            Console.WriteLine("exit");
            Console.ReadKey();
        }

    }
}
