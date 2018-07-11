using System;
using System.Net;

namespace _URLTester.Objects
{
    public interface IUrlData
    {

    }

    public abstract class BaseUrlData
    {
        public string url { get; set; }
        public string expectedRedirect { get; set; }

        public Uri actualRedirect { get; set; }
        public HttpStatusCode headerResponseCode { get; set; }

        public string errorMessage { get; set; }

    }

    public class UrlData : BaseUrlData
    {

    }
}
