using _URLTester.Objects;
using _URLTester.Test;
using System;

namespace _URLTester
{
    class Program
    {


        static void Main(string[] args)
        {
            Console.WriteLine("URLTester");

            if (args.Length == 0)
            {
                PrintHelp();
                return;
            }
            
            var appArgs = ParseArguments(args);

            if (appArgs.help)
            {
                PrintHelp();
                return;
            }
            
            if (string.IsNullOrEmpty(appArgs.domain) || string.IsNullOrEmpty(appArgs.csvFilePath))
            {
                PrintMissingArguments();
                PrintHelp();
                return;
            }

            IURLTest<UrlData> test = null;
            if (appArgs.mutlithreaded)
            {
                test = new ParallelRedirectTest<UrlData>(appArgs.domain, appArgs.csvFilePath, appArgs.outputText);
            }
            else
            {
                test = new RedirectTest<UrlData>(appArgs.domain, appArgs.csvFilePath, appArgs.outputText);
            }


            var testManager = new RedirectTestManager<UrlData>(test);

            Console.WriteLine("Loading File.....");

            if (!testManager.LoadCSV())
            {
                //if errors then display them
                testManager.OutPutErrorMessages();
                Console.ReadLine();
                return;
            }

            if (!testManager.TestLinks())
            {
                //if errors then display them
                testManager.OutPutErrorMessages();
                Console.ReadLine();
                return;
            }
            
            Console.WriteLine("Running.....");
            testManager.OutPutResults();
            Console.ReadLine();
        }

        private static Arguments ParseArguments(string[] args)
        {
            try
            {
                var appArgs = new Arguments();

                for (int i = 0; i < args.Length; i++)
                {
                    switch (args[i])
                    {
                        case "-f":
                            i++;
                            appArgs.csvFilePath = args[i];
                            break;
                        case "-d":
                            i++;
                            appArgs.domain = args[i];
                            break;
                        case "-o":
                            i++;
                            appArgs.outputText = args[i];
                            break;
                        case "-t":
                            i++;
                            appArgs.mutlithreaded = true;
                            break;
                        case "-h":
                            appArgs.help = true;
                            break;
                    }
                }
                
                return appArgs;
            }
            catch
            {
                return null;
            }
        }

        private static void PrintHelp()
        {
            //-d http://www.example.com -f C:\Users\adamw\source\repos\301Tester\301Tester\301test.csv
            Console.WriteLine("Usage: URLTester [-f] [-d] [-o] [-h]");
            Console.WriteLine("");
            Console.WriteLine("Options:");
            Console.WriteLine("\t -f \t \t CSV File Path that contains the url list to be tested.");
            Console.WriteLine("\t -d \t \t Hostname Domain eg. https://www.example.com");
            Console.WriteLine("\t -o \t \t Optional output text file eg. https://www.example.com");
            Console.WriteLine("\t -t \t \t Runs test as a mutlithread operation. https://www.example.com");
            Console.WriteLine("\t -h Help \t Help Manual");
            Console.WriteLine("");
            Console.WriteLine("Sample Arguements");
            Console.WriteLine("\t" + @" -d https://www.example.com -f C:\301test.csv -o C:\output.txt");
            Console.ReadLine();
        }

        private static void PrintMissingArguments()
        {
            Console.WriteLine("Missing Arguments -- Please try again.");
            Console.WriteLine("");
        }
    }

}
