using _URLTester.Objects;
using Core.Objects;
using Parsers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace _URLTester.Test
{
    public class RedirectTest<T> : IURLTest<T>
    {
        protected List<UrlData> urlList;
        protected List<ErrorMessage> errorMessages;
        protected string baseURL;
        protected string csvString;
        protected string outputText;

        public RedirectTest(string baseURL, string csvString, string outputText)
        {
            this.baseURL = baseURL;
            this.csvString = csvString;
            this.outputText = outputText;
        }

        public bool LoadCSV()
        {
            errorMessages = new List<ErrorMessage>();
            IParser<T> csvParser = new CSVParser<T>();
            var fileParser = new FileParser<T>(csvParser);
            urlList = fileParser.ParseFile<UrlData>(csvString, ref errorMessages);


            if (errorMessages.Count >= 1)
            {
                return false;
            }

            return true;
        }

        public virtual bool TestLinks()
        {
            errorMessages = new List<ErrorMessage>();
            var returnValue = true;
            foreach(var item in urlList)
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
                }
                catch (WebException webEx)
                {
                    errorMessages.Add(new ErrorMessage(String.Format("An error occurred with this url - {0} | {1}", item.url, webEx.Message)));
                    item.errorMessage = string.Format("{0} -- {1}", webEx.Message, webEx.InnerException);
                    returnValue = false;              
                }
            }
     
            return returnValue;
        }

        public bool OutPutResults()
        {

            if (!string.IsNullOrEmpty(outputText))
            {
                WriteOutputFile();
            }

            foreach (var item in urlList)
            {
                var output = string.Format("Path - {0} | Results -  {1} {2} Actual Path - {3}", item.url, ((int)item.headerResponseCode).ToString(), item.headerResponseCode.ToString(), item.actualRedirect);
                Console.WriteLine(output);
            }

            return true;
        }

        private void WriteOutputFile()
        {
            try
            {
                var newPath = MakeUnique(outputText);

                using (StreamWriter sw = File.CreateText(newPath.FullName))
                {
                    foreach (var item in urlList)
                    {

                        if (string.IsNullOrEmpty(item.errorMessage))
                        {
                            var output = string.Format("Path - {0} | Results -  {1} {2} Actual Path - {3}", item.url, ((int)item.headerResponseCode).ToString(), item.headerResponseCode.ToString(), item.actualRedirect);
                            sw.WriteLine(output);
                        } else {
                            var output = string.Format("Path - {0} | Results - {1}", item.url, item.errorMessage);
                            sw.WriteLine(output);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessages.Add(new ErrorMessage(ex.Message, true));
            }
        }
        
        private FileInfo MakeUnique(string path)
        {
            string dir = Path.GetDirectoryName(path);
            string fileName = Path.GetFileNameWithoutExtension(path);
            string fileExt = Path.GetExtension(path);

            for (int i = 1; ; ++i)
            {
                if (!File.Exists(path))
                    return new FileInfo(path);

                path = Path.Combine(dir, fileName + " " + i + fileExt);
            }
        }
        
        public void OutPutErrorMessages()
        {
            foreach(var error in errorMessages)
            {
                Console.WriteLine(error.message);
            }
        }
    }

}
