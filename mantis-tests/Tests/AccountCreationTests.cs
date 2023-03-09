using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using System.Text;


namespace mantis_tests
{
    [TestFixture]
    public class AccountCreationTests : TestBase
    {
        [TestFixtureSetUp]
        public void setUpConfig()
        {
            app.Ftp.BackupFile("/config_inc.php");
            using (Stream localFile = File.Open("config_inc.php", FileMode.Open))
            {
                app.Ftp.Upload("/config_inc.php", localFile);
            }
        }

        [Test]
        public void TestAccountRegistration()
        {
            AccountData account = new AccountData()
            {
                Name = ("User")+(GenerateRandomString(100)),
                Password = ("User") + (GenerateRandomString(100)),
                Email = ("User") + (GenerateRandomString(150))+("@localhost.localdomain")


            };


            List<AccountData> accounts = app.Admin.GetAllAccounts();
            AccountData existingAccount = accounts.Find(x => x.Name == account.Name);

            if (existingAccount != null)
            {
                app.Admin.DeleteAccount(existingAccount);
            }



            app.James.Delete(account);
            app.James.Add(account);

            app.Registration.Register(account);
             
        }

        [TestFixtureTearDown]
        public void restoreConfig()
        {
            app.Ftp.RestoreBackupFile("/config_inc.php");
        }

    }
}
