using Amazon;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZenithApp1.Controllers
{
    public class SESController
    {
        static readonly string senderAddress = "";
        static readonly string receiverAddress = "";

        // The subject line for the email.
        static readonly string subject = "Zenith Test Email";

        // The email body for recipients with non-HTML email clients.
        static readonly string textBody = "Zenith test email\r\n"
                                        + "This email was sent programmatically "
                                        + "from the Zenith application.";

        // The HTML body of the email.
        static readonly string htmlBody = @"<html>
        <head></head>
        <body>
          <h1>Zenith Test Email</h1>
          <p>This test email was sent programmatically.</p>
        </body>
        </html>";

        public void sendEmail()
        {
            using (var client = new AmazonSimpleEmailServiceClient(RegionEndpoint.USEast1))
            {
                var sendRequest = new SendEmailRequest
                {
                    Source = senderAddress,
                    Destination = new Destination
                    {
                        ToAddresses =
                        new List<string> { receiverAddress }
                    },
                    Message = new Message
                    {
                        Subject = new Content(subject),
                        Body = new Body
                        {
                            Html = new Content
                            {
                                Charset = "UTF-8",
                                Data = htmlBody
                            },
                            Text = new Content
                            {
                                Charset = "UTF-8",
                                Data = textBody
                            }
                        }
                    },
                };
                try
                {
                    var response = client.SendEmail(sendRequest);
                }
                catch (Exception)
                {
                    //Not sent
                }
            }
        }
    }
}