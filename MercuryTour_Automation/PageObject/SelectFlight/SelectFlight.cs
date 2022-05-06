using System;
using OpenQA.Selenium;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;

namespace MercuryTour_Automation.PageObject.SelectFlight
{
    public class SelectFlight
    {
        public IWebDriver driver;

        By btnSelectContinue = By.XPath("//input[@name='reserveFlights']");


        public SelectFlight(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void ProceedWithFlight()
        {
            driver.FindElement(btnSelectContinue).Click();
        }
    }
}
