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

namespace MercuryTour_Automation.TestScript
{
    [TestFixture]
    public class TestRunner : Browser
    {

        //call the class and function to get the browername
        [Test]
       // [TestCaseSource(typeof(Browser), "BrowserToRunWith")]   
        public void TestRun([ValueSource(typeof(Browser), "BrowserToRunWith")] String BrowserName,
        [ValueSource(typeof(Registeration), "ReadExcel")] string[] register)       
        {
           
            //call function open browsers that need to run
            Open(BrowserName);


            //call function register to input data from excel
            var Register = new Registeration(driver);
            Register.RegisterPage(register);

        }

        [Test]
        public void TestFindFlight([ValueSource(typeof(Browser), "BrowserToRunWith")] String BrowserName,
        [ValueSource(typeof(Login), "ReadExcel")] string[] Login)
        {
            //call function open browsers that need to run
            Open(BrowserName);

            var loginPage = new Login(driver);            
            loginPage.enterCredentials(Login);
            
            

            
        }




    }
}
