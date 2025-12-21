using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Hicmah.Misc
{
    //Experiment to get cassini to stop re-sending static files.
    public class StaticFiles : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.PreSendRequestHeaders += new EventHandler(context_EndRequest);

        }

        void context_EndRequest(object sender, EventArgs e)
        {
            HttpApplication context = sender as HttpApplication;
            string filename = context.Server.MapPath(context.Request.Url.AbsolutePath);
            FileInfo fi = new FileInfo(filename);

            string etag1 = GetFileETag(context.Request.Url.PathAndQuery, fi.LastWriteTime);
            string etag2 = GetFileETag(fi.Name, fi.LastWriteTime);
            string clientEtag = context.Request.Headers["If-None-Match"];
            if (etag1 == context.Request.Headers["If-None-Match"] || fi.Extension == ".css")
            {
                context.Response.Clear();
                context.Response.StatusCode = 304;
            }
        }

        private string GetFileETag(string fileName, DateTime modifyDate)
        {

            string FileString;

            System.Text.Encoder StringEncoder;

            byte[] StringBytes;

            MD5CryptoServiceProvider MD5Enc;

            //use file name and modify date as the unique identifier
            FileString = fileName + modifyDate.ToString("d", CultureInfo.InvariantCulture);
            //get string bytes
            StringEncoder = Encoding.UTF8.GetEncoder();
            StringBytes = new byte[StringEncoder.GetByteCount(FileString.ToCharArray(), 0, FileString.Length, true)];

            StringEncoder.GetBytes(FileString.ToCharArray(), 0, FileString.Length, StringBytes, 0, true);

            //hash string using MD5 and return the hex-encoded hash

            MD5Enc = new MD5CryptoServiceProvider();

            return "\"" + BitConverter.ToString(MD5Enc.ComputeHash(StringBytes)).Replace("-", string.Empty) + "\"";

        }


        public void Dispose()
        {

        }
    }
}
