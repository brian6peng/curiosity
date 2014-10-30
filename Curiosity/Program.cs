using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit;
using MailKit.Net.Pop3;
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

            string contents = Console.ReadLine();
            contents = "https://www.be4u.cn/Mission";
            var barcodeWriter = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new EncodingOptions
                {
                    Height = 300,
                    Width = 300
                }
            };
            Bitmap bitmap = barcodeWriter.Write(contents);
            bitmap.Save("d:\\code\\qr.png", ImageFormat.Png);
        }

        private static void ReciveEmail()
        {
            LoggingExtensions.Logging.Log.InitializeWith<LoggingExtensions.log4net.Log4NetLog>();

            using (var client = new Pop3Client())
            {
                client.Connect("pop.126.com", 110, false);

                // Note: since we don't have an OAuth2 token, disable
                // the XOAUTH2 authentication mechanism.
                client.AuthenticationMechanisms.Remove("XOAUTH2");

                client.Authenticate("dfdfd", "fdfdf");

                int count = client.GetMessageCount();
                "Main".Log().Info("fdfdfd");
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
    }
}
