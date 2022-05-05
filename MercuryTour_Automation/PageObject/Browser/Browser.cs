using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MercuryTour_Automation.PageObject.Browser
{
    public class Browser
    {
        public IWebDriver driver;

       
        public void Open(String BrowserName)
        {
            //open multiple browser 
            if (BrowserName.Equals("Chrome"))
            {
                driver = new ChromeDriver();
            }
            else
            {

                driver = new EdgeDriver();
            }
            //Maximise window
            driver.Manage().Window.Maximize();

            //url that need to run on browser
            driver.Url = "http://localhost:93/servlets/com.mercurytours.servlet.WelcomeServlet";

        }
        


        //close browser
       [TearDown]

        public void Close() {

             driver.Quit();
         }

        //Get the browser name on which the test will run
        public static IEnumerable<String> BrowserToRunWith()
        {
            String[] browsers = {"Chrome","Edge"};

            foreach (String browser in browsers)
            {

                yield return browser;
            }

        }
    }
}