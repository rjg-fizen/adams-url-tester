using _URLTester.Objects;
using Core.Objects;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace _URLTester.Test
{
    public class ParallelRedirectTest<T> : RedirectTest<T>
    {
        public ParallelRedirectTest(string baseURL, string csvString, string outputText) : base(baseURL, csvString, outputText)
        {
        }

        public override bool TestLinks()
        {
            errorMessages = new List<ErrorMessage>();
            var returnValue = true;
            
            Parallel.ForEach(urlList, (item) =>
            {
                var retval = TestLink(ref item);
                if(returnValue == true && retval == false)
                {
                    returnValue = retval;
                }
            });

            return returnValue;
        }

        private bool TestLink(ref UrlData item)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseURL + item.url);
            string responseBody = String.Empty;
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    item.headerResponseCode = response.StatusCode;
                    item.actualRedirect = response.ResponseUri;
                }
                return true;
            }
            catch (WebException webEx)
            {
                errorMessages.Add(new ErrorMessage(String.Format("An error occurred with this url - {0} | {1}", item.url, webEx.Message)));
                item.errorMessage = string.Format("{0} -- {1}", webEx.Message, webEx.InnerException);
                return false;
            }
        }
    }
}
