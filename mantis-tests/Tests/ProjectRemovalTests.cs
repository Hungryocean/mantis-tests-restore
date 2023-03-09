using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace mantis_tests
{
    [TestFixture]
    public class ProjectRemovalTests : AuthTestBase
    {
        [SetUp]
        public void Init()
        {
            app.Api.CreateIfNotExist(account);
        }

        [Test]
        public void RemoveProject()
        {
            List<ProjectData> oldProjectsList = app.Api.GetProjectsList(account);

            app.Api.RemoveProject(account, 0);

            List<ProjectData> newProjectsList = app.Api.GetProjectsList(account);

            Assert.AreEqual(oldProjectsList.Count - 1, app.Api.GetCountProjects(account));

            oldProjectsList.RemoveAt(0);
            oldProjectsList.Sort();
            newProjectsList.Sort();
            Assert.AreEqual(oldProjectsList, newProjectsList);
        }
    }
}
