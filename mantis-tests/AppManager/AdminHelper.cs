using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleBrowser.WebDriver;
using System.Security.Principal;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using OpenQA.Selenium;


namespace mantis_tests
{
    public class AdminHelper : HelperBase
    {
        private string baseUrl;
        public AdminHelper(ApplicationManager manager, String baseUrl) : base(manager)
        {
            this.baseUrl = baseUrl;
        }

        public List<AccountData> GetAllAccounts()
        {
            List<AccountData> accounts = new List<AccountData>();
            IWebDriver driver = OpenAppAndLogin();
            driver.Url = baseUrl + "/manage_user_page.php";
            IList<IWebElement> rows = driver.FindElements(By.XPath("//div[@id='main-container']/div[2]/div[2]/div/div/div[4]/div[2]/div[2]/div/table/tbody/tr"));
            foreach (IWebElement row in rows)
            {
                IWebElement link = row.FindElement(By.TagName("a"));
                string name = link.Text;
                string href = link.GetAttribute("href");
                Match m = Regex.Match(href, @"\d+$");
                string id = m.Value;
                accounts.Add(new AccountData()
                {
                    Id = id,
                    Name = name
                });

            }
            return accounts;
        }

        public void DeleteAccount(AccountData account)
        {
            IWebDriver driver = OpenAppAndLogin();
            driver.Url = baseUrl + "/manage_user_edit_page.php?user_id=" + account.Id;

            driver.FindElement(By.XPath("//input[@value='Delete User']")).Click();
            driver.FindElement(By.XPath("//input[@value='Delete Account']")).Click();
        }

        private IWebDriver OpenAppAndLogin()
        {
            //IWebDriver driver = new SimpleBrowserDriver();
            driver.Url = baseUrl + "/login_page.php";
            driver.FindElement(By.Id("username")).SendKeys("administrator");
            driver.FindElement(By.XPath("//input[@value='Login']")).Click();
            driver.FindElement(By.Id("password")).SendKeys("root");
            driver.FindElement(By.XPath("//input[@value='Login']")).Click();
            return driver;
        }
    }
}
