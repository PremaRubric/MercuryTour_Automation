using MercuryTour_Automation.PageObject.Browser;
using MercuryTour_Automation.PageObject.Registration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MercuryTour_Automation.PageObject.Login;
using MercuryTour_Automation.PageObject.FindFlight;
using MercuryTour_Automation.PageObject.BookAFlight;
using MercuryTour_Automation.PageObject.SelectFlight;
using OpenQA.Selenium;
using System.Threading;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;


namespace MercuryTour_Automation.TestScript
{

    [TestFixture]
    public class TestRunner : Browser
    {

        ExtentReports Extent = null;
        ExtentTest Test = null;

        [OneTimeSetUp]
        public void ExtentSetStart() {

            Extent = new ExtentReports();
            var htmlReport = new ExtentHtmlReporter(@"C:\Users\sd420410\source\repos\test2\MercuryTour_Automation\Report\Index.html");
            Extent.AttachReporter(htmlReport);
        }

        [OneTimeTearDown]
        public void ExtentClose() {

            Extent.Flush();
        
        }

        //call the class and function to get the browername
        [Test, Order(1)]
        public void TestRegistration([ValueSource(typeof(Browser), "BrowserToRunWith")] string BrowserName,
        [ValueSource(typeof(Registeration), "ReadExcel")] string[] register)       
        {
            Test = Extent.CreateTest("Test for Registration").Info("Test Started");

            //call function open browsers that need to run
            Open(BrowserName);
            Test.Log(Status.Info, "Brower " + BrowserName + " lauched");

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(8); //Implicit Wait


            //call function register to input data from excel
            var Register = new Registeration(driver);
            Register.RegisterPage(register);

            Test.Log(Status.Pass, "Registration for successfully completed");

        }


        [Test, Order(3)]
        //Scenario 1 test with flight preference
        public void TestBookAFlight(
         [ValueSource(typeof(Browser), "BrowserToRunWith")] string BrowserName,
         [ValueSource(typeof(Login), "ReadExcel")] string[] Login,
         [ValueSource(typeof(FindAFlight), "ReadExcel")] string[] FindAFlight,
         [ValueSource(typeof(BookFlight), "ReadExcel")] string[] BookFlight
         
         )
        {
            Test = Extent.CreateTest("Test for Booking a Flight").Info("Test Started");
            //call function open browsers that need to run

            Open(BrowserName);
            Test.Log(Status.Info, "Brower " + BrowserName + " lauched");

            var loginPage = new Login(driver);
            loginPage.EnterCredentials(Login);

            FindAFlight findFlight = new FindAFlight(driver);
            SelectFlight selectflight = new SelectFlight(driver);
            BookFlight bookFlight = new BookFlight(driver);

            findFlight.selectFlightDetails(FindAFlight);
            findFlight.flightPreferences(FindAFlight);

            selectflight.ProceedWithFlight();
            bookFlight.EnterDetails(BookFlight);
            bookFlight.EnterPassenger2Details(BookFlight);
            bookFlight.SecurePayment();
            Test.Log(Status.Pass, "Test for booking a Flight successfully completed");
            try
            {
                IWebElement confirmationImage = driver.FindElement(By.XPath("/html/body/div/table/tbody/tr/td[2]/table/tbody/tr[4]/td/table/tbody/tr[1]/td[2]/table/tbody/tr[1]/td/img"));
                NUnit.Framework.Assert.IsNotNull(confirmationImage);
                Console.WriteLine("The image is " + confirmationImage.GetAttribute("src"));

                Test.Log(Status.Pass, "Confirmation image is available");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Test.Log(Status.Fail, ex.ToString());
            }

            Thread.Sleep(8000);//Explicit Wait

        }

        [Test, Order(2)]
        public void TestLogin([ValueSource(typeof(Browser), "BrowserToRunWith")] string BrowserName,
         [ValueSource(typeof(Login), "ReadExcel")] string[] Login)
        {
            Test = Extent.CreateTest("Test for Login").Info("Test Started");
            //call function open browsers that need to run

            Open(BrowserName);

            Test.Log(Status.Info, "Brower " + BrowserName + " lauched");

            var loginPage = new Login(driver);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(6); //implicit Wait


            loginPage.EnterCredentials(Login);

            Test.Log(Status.Pass, "Test on Login succesfully completed");
            try
            {
                IWebElement flightImage = driver.FindElement(By.XPath("/html/body/div/table/tbody/tr/td[2]/table/tbody/tr[4]/td/table/tbody/tr/td[2]/table/tbody/tr[1]/td/img"));
                NUnit.Framework.Assert.IsNotNull(flightImage);
                Console.WriteLine("The image is " + flightImage.GetAttribute("src"));

                Test.Log(Status.Pass, "Title FindFlight Exists");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                Test.Log(Status.Fail, ex.ToString());
            }
            //Thread.Sleep(8000);
        }


    }
}
