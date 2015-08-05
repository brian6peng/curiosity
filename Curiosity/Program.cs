using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit;
using MailKit.Net.Pop3;
using MailKit.Net.Smtp;
using MimeKit;
using ZXing;
using ZXing.Common;

namespace Curiosity
{
    class Program
    {
        static void Main(string[] args)
        {
            //ReciveEmail();
            SendEmail();

            //string contents = Console.ReadLine();
            //contents = "https://www.be4u.cn/Mission";
            //var barcodeWriter = new BarcodeWriter
            //{
            //    Format = BarcodeFormat.QR_CODE,
            //    Options = new EncodingOptions
            //    {
            //        Height = 300,
            //        Width = 300
            //    }
            //};
            //Bitmap bitmap = barcodeWriter.Write(contents);
            //bitmap.Save("d:\\code\\qr.png", ImageFormat.Png);
        }

        private static void ReciveEmail()
        {
            using (var client = new Pop3Client())
            {
                client.Connect("pop.126.com", 110, false);

                // Note: since we don't have an OAuth2 token, disable
                // the XOAUTH2 authentication mechanism.
                client.AuthenticationMechanisms.Remove("XOAUTH2");

                client.Authenticate("dfdfd", "fdfdf");

                int count = client.GetMessageCount();
                for (int i = 0; i < count; i++)
                {
                    var message = client.GetMessage(i);
                    if (message.Subject == "Web of Science Alert -")
                    {
                        var textBody = message.Body as TextPart;
                        Console.WriteLine("Subject: {0}", textBody.Text);
                    }
                }

                client.Disconnect(true);
            }
        }

        private static void SendEmail()
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("lib", "lib1@nwpu.edu.cn"));
            message.To.Add(new MailboxAddress("brian", "18092036839@126.com"));
            message.Subject = "How you doin'?";

            message.Body = new TextPart("html")
            {
                Text = @"<br>Hey Cha<br>ndler,

I just wanted to let you know that Monica and I were going to go play some paintball, you in?

-- Joey"
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.nwpu.edu.cn");

                // Note: since we don't have an OAuth2 token, disable
                // the XOAUTH2 authentication mechanism.
                client.AuthenticationMechanisms.Remove("XOAUTH2");

                // Note: only needed if the SMTP server requires authentication
                client.Authenticate("lib1@nwpu.edu.cn", "lib3632");

                client.Send(message);
                client.Disconnect(true);
            }

        }

    }
}
