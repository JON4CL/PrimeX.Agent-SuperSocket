using PrimeX.Base.ConfigurationContainer;
using PrimeX.Base.Log;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace PrimeX.Agent.Networking
{
    class MessageSender
    {
        private ConfigurationModule ConfigModule;
        private String ServerEndPoint;
        //private static HttpWebRequest Request;
        //private static HttpClient Client = new HttpClient();

        //var client = new ClientWebSocket();

        public MessageSender()
        {
            ConfigModule = ConfigurationContainer.Instance.GetConfigurationByModule("MessageSender");
            ServerEndPoint = ConfigModule.GetValue("ServerEndPoint");
            //await client.ConnectAsync(new Uri("ws://localhost:5000/ws"), CancellationToken.None);
        }

        public bool SendMessage(byte[] data)
        {
            Logger.LogInfo(this, "SendMessage");
            //try {
            //    Request = (HttpWebRequest)WebRequest.Create(ServerEndPoint);
            //    Request.Credentials = CredentialCache.DefaultCredentials;
            //    ((HttpWebRequest)Request).UserAgent = "PRIME AGENT";
            //    Request.Method = "POST";
            //    Request.ContentLength = data.Length;
            //    Request.ContentType = "application/x-prime-message";

            //    using (var stream = Request.GetRequestStream())
            //    {
            //        stream.Write(data, 0, data.Length);
            //    }

            //    using (var response = (HttpWebResponse)Request.GetResponse())
            //    {
            //        var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            //        Console.WriteLine("<- SERVER Response StatusCode:'{0}', responseString:'{0}'", response.StatusCode, responseString);
            //    }


            //    // PARA RETORNO EN BYTE ARRAY SE DEBE HACER LO SIGUIENTE
            //    //result = response.Content.ReadAsStringAsync().Result.Replace("\"", string.Empty);
            //    //mybytearray = Convert.FromBase64String(result);

            //} catch (Exception e)
            //{
            //    Logger.LogError(this, e.StackTrace);
            //    return false;
            //}
            ByteArrayContent byteArrayContent = new ByteArrayContent(data);
            byteArrayContent.Headers.ContentType = new MediaTypeHeaderValue("application/x-prime-message");
            //var response = Client.PostAsync(ServerEndPoint, byteArrayContent).Result;
            //Console.WriteLine("<- SERVER Response StatusCode:'{0}', responseString:'{1}'", response.StatusCode, response.Content.ReadAsStringAsync().Result);

            return true;
        }
    }
}
