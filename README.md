# URLTester
A simple 301 redirects tester for a list of urls using a csv file.  

### Sample Test File (csv)  
URL,expectedRedirect  
/blog,/new  
/contact-us,/contact  
  
* * *

### Options:  
*         -f              CSV File Path that contains the url list to be tested.  
*         -d              Hostname Domain eg. https://www.example.com  
*         -o              Optional output text file eg. https://www.example.com  
*         -t              Runs test as a mutlithread operation. https://www.example.com  
*         -h Help         Help Manual
  
Sample Arguements  
*         -d https://www.example.com -f C:\301test.csv -o C:\output.txt  


