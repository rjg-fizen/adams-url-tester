# URLTester
A simple 301 redirects tester for a list of urls using an input (csv or json) file.  

### Sample Test File (csv)  
URL,expectedRedirect  
/blog,/new  
/contact-us,/contact  

### Sample Test File (Json) 
[
	{
      "URL": "/about",
      "expectedRedirect": "https://example.com/about-us"
    },
	{
      "URL": "/links",
      "expectedRedirect": "https://example.com/sitemap.xml"
	}
]


* * *

### Options:  
*         -f              CSV or Json File Path that contains the url list to be tested.  
*         -d              Hostname Domain eg. https://www.example.com  
*         -o              Optional output text file eg. c:/output.csv  
*         -t              Runs test as a mutlithread operation.
*         -h Help         Help Manual
  
Sample Arguements  
*         -d https://www.example.com -f C:\301test.csv -o c:\output.csv  


