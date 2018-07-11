using System;


namespace _URLTester.Test
{
    public class RedirectTestManager<T>
    {
        private readonly IURLTest<T> redirectTest;

        public RedirectTestManager(IURLTest<T> redirectTest)
        {
            this.redirectTest = redirectTest ?? throw new ArgumentNullException("parser");
        }

        public bool LoadCSV()
        {
            return redirectTest.LoadCSV();
        }

        public bool TestLinks()
        {
            return redirectTest.TestLinks();
        }

        public bool OutPutResults()
        {
            return redirectTest.OutPutResults();
        }

        public void OutPutErrorMessages()
        {
           redirectTest.OutPutErrorMessages();
        }
    }

}
