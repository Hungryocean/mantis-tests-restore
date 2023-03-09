using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace mantis_tests
{
    public class LoginHelper : HelperBase
    {
        private string baseURL;
        public LoginHelper(ApplicationManager manager, string baseURL) : base(manager)
        {
            this.baseURL = baseURL;
        }

        public void OpenLoginPage()
        {
            if (driver.Url == baseURL + "mantisbt-2.25.4/login_page.php")
            {
                return;
            }

            driver.Navigate().GoToUrl(baseURL + "mantisbt-2.25.4/login_page.php");
        }

        public void Login(AccountData account)
        {
            if (IsLoggedIn())
            {
                if (IsLoggedIn(account))
                {
                    return;
                }

                Logout();
            }
            Type(By.Name("username"), account.Name);
            driver.FindElement(By.XPath("//input[@value='Login']")).Click();
            Type(By.Name("password"), account.Password);
            driver.FindElement(By.XPath("//input[@value='Login']")).Click();

        }

        public bool IsLoggedIn()
        {
            return IsElementPresent(By.CssSelector("span.user-info"));
        }

        public bool IsLoggedIn(AccountData account)
        {
            return IsLoggedIn()
                && GetLoggedUserName() == account.Name;
        }

        public string GetLoggedUserName()
        {
            return driver.FindElement(By.CssSelector("span.user-info")).Text;
        }

        public void Logout()
        {
            if (IsLoggedIn())
            {
                driver.FindElement(By.LinkText("Выход")).Click();
            }
        }


    }
}
