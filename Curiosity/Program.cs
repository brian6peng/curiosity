using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit;
using MailKit.Net.Pop3;
using MimeKit;

namespace Curiosity
{
    class Program
    {
        static void Main(string[] args)
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
