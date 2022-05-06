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
using MercuryTour_Automation.PageObject.FindFlight;
using OfficeOpenXml;
using System.IO;

namespace MercuryTour_Automation.PageObject.BookAFlight
{
    public class BookFlight
    {
        public IWebDriver driver;

        By txtFirstName = By.Name("passFirst0");
        By txtLastName = By.Name("passLast0");
        By txtFirstName1 = By.Name("passFirst1");
        By txtLastName1 = By.Name("passLast1");
        By txtCreditNumber = By.Name("creditnumber");
        By btnSecurePurchase = By.XPath("//input[@name='buyFlights']");

        public BookFlight(IWebDriver driver)
        {
            this.driver = driver;
        }

        public static IEnumerable<string[]> ReadExcel()
        {   // open excel file
            using (ExcelPackage package = new ExcelPackage(new FileInfo("DataTable.xlsx")))
            {
                //Read sheet "FindAFlight" 
                ExcelWorksheet worksheet = package.Workbook.Worksheets["BookFlight"];
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
                        

                    };

                }

            }

        }



        public void EnterDetails(string [] BookFlight)
        {

            driver.FindElement(txtFirstName).SendKeys(BookFlight[0].ToString());
            driver.FindElement(txtLastName).SendKeys(BookFlight[2].ToString());
            driver.FindElement(txtCreditNumber).SendKeys(BookFlight[4].ToString());
          

        }
        public void EnterPassenger2Details(string [] BookFlight)
        {
            driver.FindElement(txtFirstName1).SendKeys(BookFlight[1].ToString());
            driver.FindElement(txtLastName1).SendKeys(BookFlight[3].ToString());
            
        }

        public void SecurePayment()
        {
            driver.FindElement(btnSecurePurchase).Click();
        }
    }
}
