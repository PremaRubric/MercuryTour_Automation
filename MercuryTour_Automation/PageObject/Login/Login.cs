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
using OfficeOpenXml;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MercuryTour_Automation.PageObject.Login
{
    public class Login
    {
        public IWebDriver driver;
        public Login(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);


        }


        [FindsBy(How = How.Name, Using = "userName")]
        public IWebElement txtUserName { get; set; }

        [FindsBy(How = How.Name, Using = "password")]
        public IWebElement txtPassword { get; set; }

        [FindsBy(How = How.Name, Using = "login")]
        public IWebElement btnLogin { get; set; }


        public static IEnumerable<string[]> ReadExcel()
        {   // open excel file
            using (ExcelPackage package = new ExcelPackage(new FileInfo("DataTable.xlsx")))
            {
                //Read sheet "Register" 
                ExcelWorksheet worksheet = package.Workbook.Worksheets["Login"];
                int rowCount = worksheet.Dimension.End.Row;
                for (int i = 2; i <= rowCount; i++)
                {
                    //get data from each row
                    yield return new string[] {
                        worksheet.Cells[i, 1].Value?.ToString().Trim(),
                        worksheet.Cells[i, 2].Value?.ToString().Trim(),

                    };
                }
            }

        }

        [DynamicData(nameof(ReadExcel), DynamicDataSourceType.Method)]
        public void enterCredentials(string[] Login)
        {
           
            txtUserName.SendKeys(Login[0]);
            txtPassword.SendKeys(Login[1]);            
            btnLogin.Click();

           
        }

    }
}
