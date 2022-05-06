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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OfficeOpenXml;
using System.IO;

namespace MercuryTour_Automation.PageObject.FindFlight
{
    public class FindAFlight
    {
        public  IWebDriver driver;

        By rbnOnewayTrip = By.XPath("//input[@value='oneway']");
        By drpPassengers = By.Name("passCount");
        By drpDepartingFrom = By.Name("fromPort");
        By drpFromMonth = By.Name("fromMonth");
        By drpFromDay = By.Name("fromDay");
        By drpArriving = By.Name("toPort");
        By drpToMonth = By.Name("toMonth");
        By drpToDay = By.Name("toDay");
        By rbnEconomy = By.XPath("//input[@value='Coach']");
        By rbnBusiness = By.XPath("//input[@value='Business']");
        By rbnFirst = By.XPath("//input[@value='First']");
        By drpAirline = By.XPath("//select[@name='airline']");
        By btnContinue = By.XPath("//input[@name='findFlights']");

        public FindAFlight(IWebDriver driver)
        {
            this.driver = driver;

        }




        public static IEnumerable<string[]> ReadExcel()
        {   // open excel file
            using (ExcelPackage package = new ExcelPackage(new FileInfo("DataTable.xlsx")))
            {
                //Read sheet "FindAFlight" 
                ExcelWorksheet worksheet = package.Workbook.Worksheets["FindAFlight"];
                int rowCount = worksheet.Dimension.End.Row;
                for (int i = 2; i <= rowCount; i++)
                {
                    //get data from each row
                    yield return new string[] {
                        worksheet.Cells[i, 1].Value?.ToString().Trim(),
                        worksheet.Cells[i, 2].Value?.ToString().Trim(),
                        worksheet.Cells[i, 3].Value?.ToString().Trim(),
                        worksheet.Cells[i, 4].Value?.ToString().Trim(),
                        worksheet.Cells[i, 5].Value?.ToString().Trim(),
                        worksheet.Cells[i, 6].Value?.ToString().Trim(),
                        worksheet.Cells[i, 7].Value?.ToString().Trim(),
                        worksheet.Cells[i, 8].Value?.ToString().Trim(),
                        worksheet.Cells[i, 9].Value?.ToString().Trim(),
                        worksheet.Cells[i, 10].Value?.ToString().Trim(),




                    };

                }

            }

        }


        public void selectFlightDetails(string [] FindAFlight)
        {
            if (FindAFlight[0].ToString() == "oneway")
            {
                driver.FindElement(rbnOnewayTrip).Click();
            }

            SelectElement passengersdrp = new SelectElement(driver.FindElement(drpPassengers));
            passengersdrp.SelectByValue(FindAFlight[1].ToString());

            SelectElement drpDeparting = new SelectElement(driver.FindElement(drpDepartingFrom));
            drpDeparting.SelectByValue(FindAFlight[2].ToString());

            SelectElement month = new SelectElement(driver.FindElement(drpFromMonth));
            month.SelectByValue(FindAFlight[3].ToString());

            SelectElement day = new SelectElement(driver.FindElement(drpFromDay));
            day.SelectByValue(FindAFlight[4].ToString());

            SelectElement arriving = new SelectElement(driver.FindElement(drpArriving));
            arriving.SelectByValue(FindAFlight[5].ToString());

            SelectElement returning = new SelectElement(driver.FindElement(drpToMonth));
            returning.SelectByValue(FindAFlight[6].ToString());

            SelectElement toDay = new SelectElement(driver.FindElement(drpToDay));
            toDay.SelectByValue(FindAFlight[7].ToString());

        }


       
            public void flightPreferences(string [] FindAFlight)
        {
            if (FindAFlight[8].ToString() == "Business")
            {
                driver.FindElement(rbnBusiness).Click();
            } 
            if (FindAFlight[8].ToString() == "First") 
            { 
                    driver.FindElement(rbnFirst).Click(); 
            }
            SelectElement airline = new SelectElement(driver.FindElement(drpAirline));
            airline.SelectByText(FindAFlight[9].ToString());
        
            driver.FindElement(btnContinue).Click();
           

        }


    }
}
