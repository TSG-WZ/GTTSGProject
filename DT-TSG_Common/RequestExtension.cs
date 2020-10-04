using System.IO;
using System.Net;
using System.Text;

namespace DTTSG_Common
{
    public class RequestExtension
    {


        public string GetInfo(string url, string contentMethod = "Get", string contentType = "application/json")
        {
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
            myRequest.Method = contentMethod;
            myRequest.ContentType = contentType;
            myRequest.Proxy = null;

            using (HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse())
            {
                using (StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
        }


    }
}
