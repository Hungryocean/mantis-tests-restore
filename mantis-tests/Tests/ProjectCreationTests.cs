using System;
using System.Collections.Generic;
using System.Security.Principal;
using mantis_tests.Mantis;
using NUnit.Framework;

namespace mantis_tests
{
    [TestFixture]
    public class ProjectCreationTests : AuthTestBase
    {
        [SetUp]
        public void Init()
        {
            app.Api.DeleteExistingProject(account, new ProjectData("New project"));
        }

        [Test]
        public void ProjectCreationTest()
        {
            List<ProjectData> oldProjectsList = app.Api.GetProjectsList(account);

            ProjectData project = new ProjectData("New project")
            {
                Status = "development",
                Visibility = "public",
                Enabled = "True",
                Description = GenerateRandomString(100),
            };

            app.Api.CreateNewProject(account, project);

            Assert.AreEqual(oldProjectsList.Count + 1, app.Api.GetCountProjects(account));

            List<ProjectData> newProjectsList = app.Api.GetProjectsList(account);

            project.Visibility = "public";
            oldProjectsList.Add(project);
            oldProjectsList.Sort();
            newProjectsList.Sort();

            Assert.AreEqual(oldProjectsList, newProjectsList);
        }

    }
}
