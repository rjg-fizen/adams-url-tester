namespace _URLTester.Objects
{
    public class Arguments
    {
        public string csvFilePath { get; set; }
        public string domain { get; set; }
        public string outputText { get; set; }
        public bool mutlithreaded { get; set; }
        public bool help { get; set; }

        public Arguments()
        {
            help = false;
        }
    }
}
