using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace mantis_tests
{
    public class HelperBase
    {
        protected ApplicationManager manager;
        protected IWebDriver driver;


        public HelperBase (ApplicationManager manager)
        {
            this.manager = manager;
            driver = manager.Driver;
        }

        public void Type(By locatoor, string text)
        {
            if (text != null)
            {
                driver.FindElement(locatoor).Click();
                driver.FindElement(locatoor).Clear();
                driver.FindElement(locatoor).SendKeys(text);
            }
        }

        public void SelectElement(By locator, string text)
        {
            if (text != null)
            {
                new SelectElement(driver.FindElement(locator)).SelectByText(text);
            }
        }

        public bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

    }
}