using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using NUnit.Framework;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using OfficeOpenXml;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MercuryTour_Automation.PageObject.Registration
{
    class Registeration
    {

        public IWebDriver driver;
        public Registeration(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver,this);


        }

      //Find element by Xpath
       [FindsBy(How = How.XPath, Using = "//a[@href='com.mercurytours.servlet.RegisterServlet']")]
        public IWebElement btnRegister { get; set; }
        
        //find element for username by name tag
        [FindsBy(How = How.Name, Using = "userName")]
        public IWebElement txtUserName { get; set; }

        //find element for Password by name tag
        [FindsBy(How = How.Name, Using = "password")]
        public IWebElement txtPassword { get; set; }

        //find element for confirm password
       [FindsBy(How = How.Name, Using = "confirmPassword")]
        public IWebElement txtConfirmPassword { get; set; }

        //find element for button register
        [FindsBy(How = How.Name, Using = "register")]
        public IWebElement btnsubmit { get; set; }

        
        public static IEnumerable<string[]> ReadExcel()
        {   // open excel file
            using (ExcelPackage package = new ExcelPackage(new FileInfo("DataTable.xlsx")))
            {
                //Read sheet "Register" 
                ExcelWorksheet worksheet = package.Workbook.Worksheets["Registration"];
                int rowCount = worksheet.Dimension.End.Row;     
                for (int i=2;i<=rowCount;i++)
                {
                    //get data from each row
                    yield return new string[] {
                        worksheet.Cells[i, 1].Value?.ToString().Trim(),
                        worksheet.Cells[i, 2].Value?.ToString().Trim(),
                        worksheet.Cells[i, 3].Value?.ToString().Trim()

                       
                        
                    };
                }
            }



        }


       [DynamicData(nameof(ReadExcel), DynamicDataSourceType.Method)]
        public void RegisterPage(string [] register){

            btnRegister.Click();
            txtUserName.SendKeys(register[0]);
            txtPassword.SendKeys(register[1]);
            txtConfirmPassword.SendKeys(register[2]);
            btnsubmit.Click();
           
        }
        
        

    }
}
