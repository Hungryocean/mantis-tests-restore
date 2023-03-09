using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mantis_tests.Mantis;
using NUnit.Framework;

namespace mantis_tests
{
    [TestFixture]
    public class AddNewIssues : TestBase
    {
        [Test]
        public void AddNewIssue()
        {
            AccountData account = new AccountData()
            {
                Name = "administrator",
                Password = "root"
            };
            ProjectData project = new ProjectData()
            {
                Id = "1"
            };

            IssueData issue = new IssueData()
            {
                Summary = "some short test",
                Description = "some long test",
                Category = "General"
            };

            app.Api.CreateNewIssue(account, project, issue);

        }
    }
}
