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
namespace MercuryTour_Automation.TestScript
{
    [TestFixture]
    public class TestRunner : Browser
    {

        //call the class and function to get the browername
        [Test]
        public void TestRegistration([ValueSource(typeof(Browser), "BrowserToRunWith")] string BrowserName,
        [ValueSource(typeof(Registeration), "ReadExcel")] string[] register)       
        {
           
            //call function open browsers that need to run
            Open(BrowserName);


            //call function register to input data from excel
            var Register = new Registeration(driver);
            Register.RegisterPage(register);

        }


        [Test, Order(1)]
        //Scenario 1 test with flight preference
        public void TestBookAFlight(
         [ValueSource(typeof(Browser), "BrowserToRunWith")] string BrowserName,
         [ValueSource(typeof(Login), "ReadExcel")] string[] Login,
         [ValueSource(typeof(FindAFlight), "ReadExcel")] string[] FindAFlight,
         [ValueSource(typeof(BookFlight), "ReadExcel")] string[] BookFlight
         
         )
        {
            //call function open browsers that need to run

            Open(BrowserName);

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
            try
            {
                IWebElement confirmationImage = driver.FindElement(By.XPath("/html/body/div/table/tbody/tr/td[2]/table/tbody/tr[4]/td/table/tbody/tr[1]/td[2]/table/tbody/tr[1]/td/img"));
                NUnit.Framework.Assert.IsNotNull(confirmationImage);
                Console.WriteLine("The image is " + confirmationImage.GetAttribute("src"));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }

            Thread.Sleep(8000);

        }

        [Test, Order(2)]
        public void TestLogin([ValueSource(typeof(Browser), "BrowserToRunWith")] string BrowserName,
         [ValueSource(typeof(Login), "ReadExcel")] string[] Login)
        {
            //call function open browsers that need to run

            Open(BrowserName);

            var loginPage = new Login(driver);
            loginPage.EnterCredentials(Login);
            try
            {
                IWebElement flightImage = driver.FindElement(By.XPath("/html/body/div/table/tbody/tr/td[2]/table/tbody/tr[4]/td/table/tbody/tr/td[2]/table/tbody/tr[1]/td/img"));
                NUnit.Framework.Assert.IsNotNull(flightImage);
                Console.WriteLine("The image is " + flightImage.GetAttribute("src"));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            //Thread.Sleep(8000);
        }


    }
}
