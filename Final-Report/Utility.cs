using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Text;
using System.Dynamic;
using System.Web.Mvc;
using System.Web.UI;


namespace Final_Report
{
    public static class Utility
    {
        public static void MsgBox(String ex, Page pg, Object obj)
        {
            string s = "<SCRIPT language='javascript'>alert('" + ex.Replace("\r\n", "\\n").Replace("'", "") + "'); </SCRIPT>";
            Type cstype = obj.GetType();
            ClientScriptManager cs = pg.ClientScript;
            cs.RegisterClientScriptBlock(cstype, s, s.ToString());
        }
        public static ExpandoObject ToExpando(this object anonymousObject)
        {
            IDictionary<string, object> anonymousDictionary = HtmlHelper.AnonymousObjectToHtmlAttributes(anonymousObject);
            IDictionary<string, object> expando = new ExpandoObject();
            foreach (var item in anonymousDictionary)
                expando.Add(item);
            return (ExpandoObject)expando;
        }

        public static Image ByteArrayToImage(byte[] fileBytes)
        {
            using (var stream = new MemoryStream(fileBytes))
            {
                return Image.FromStream(stream);
            }
        }
        public static Byte[] ImageToByteArray(Image img)
        {
            using (var stream = new MemoryStream())
            {
                img.Save(stream, img.RawFormat);
                return stream.ToArray();
            }
        }

        public static string ConvertImageToBase64(string path)
        {
            using (Image image = Image.FromFile(path))
            {
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();

                    // Convert byte[] to Base64 String
                    string base64String = "data:image/jpeg;base64," + Convert.ToBase64String(imageBytes);
                    return base64String;
                }
            }
        }

        public static string ConvertImageToBase64(Image image)
        {

            using (MemoryStream m = new MemoryStream())
            {
                image.Save(m, image.RawFormat);
                byte[] imageBytes = m.ToArray();

                // Convert byte[] to Base64 String
                string base64String = "data:image/jpeg;base64,"+ Convert.ToBase64String(imageBytes);
                return base64String;
            }

        }

        public static String ConvertImageURLToBase64(String url)
        {
            WebClient client = new WebClient();
            // Convert byte[] to Base64 String
            string base64String = ConvertImageToBase64(GetImageURL(url));
            return base64String;

        }
        public static Image GetImageURL(string url)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            WebClient client = new WebClient();
            byte[] imageData = client.DownloadData(url);
            using (MemoryStream ms = new MemoryStream(imageData))
            {
                return Image.FromStream(ms);
            }
        }
    }
}