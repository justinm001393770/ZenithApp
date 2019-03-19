using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZenithApp1.Controllers
{
    public class SNSController
    {
        public void sendText(string phone, string message, string sentFrom)
        {
            using (AmazonSimpleNotificationServiceClient client = new AmazonSimpleNotificationServiceClient())
            {
                try
                {
                    PublishRequest req = new PublishRequest();
                    PublishResponse res = new PublishResponse();

                    req.PhoneNumber = "1" + phone;
                    req.Message = sentFrom + ":" + Environment.NewLine + message + Environment.NewLine + Environment.NewLine + "(Message sent via the Zenith Application)";
                    req.MessageAttributes["AWS.SNS.SMS.SenderID"] = new MessageAttributeValue { StringValue = "Zenith", DataType = "String" };
                    req.MessageAttributes["AWS.SNS.SMS.MaxPrice"] = new MessageAttributeValue { StringValue = "0.50", DataType = "Number" };
                    req.MessageAttributes["AWS.SNS.SMS.SMSType"] = new MessageAttributeValue { StringValue = "Transactional", DataType = "String" };
                    res = client.Publish(req);
                }
                catch(Exception)
                {

                }
            }
        }
    }
}