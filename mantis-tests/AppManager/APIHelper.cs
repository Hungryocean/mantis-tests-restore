namespace mantis_tests
{
    public class APIHelper : HelperBase
    {
        public APIHelper(ApplicationManager manager) : base(manager) { }
        Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();

        public void CreateNewIssue(AccountData account, ProjectData project, IssueData issueData)
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            Mantis.IssueData issue = new Mantis.IssueData();
            issue.summary = issueData.Summary;
            issue.description = issueData.Description;
            issue.category = issueData.Category;
            issue.project = new Mantis.ObjectRef();
            issue.project.id = project.Id;
            client.mc_issue_add(account.Name, account.Password, issue);

        }

        public void CreateNewProject(AccountData account, ProjectData project)
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            Mantis.ProjectData projects = new Mantis.ProjectData();
            projects.name = project.ProjectName;
            projects.description = project.Description;
            projects.status = new Mantis.ObjectRef();
            projects.status.name = project.Status;
            projects.enabled = project.Enabled == "True" ? true : false;
            projects.view_state = new Mantis.ObjectRef();
            projects.view_state.name = project.Visibility;

            client.mc_project_add(account.Name, account.Password, projects);
        }
        public void RemoveProject(AccountData account, int index)
        {
            List<ProjectData> projectsList = GetProjectsList(account);

            var id = projectsList[0].Id;
            client.mc_project_delete(account.Name, account.Password, id);
        }

        public void CreateIfNotExist(AccountData account)
        {
            if (GetProjectsList(account).Count == 0)
            {
                Mantis.ProjectData project = new Mantis.ProjectData();
                project.name = new ProjectData("test").ProjectName;

                client.mc_project_add(account.Name, account.Password, project);
            }
        }

        public void DeleteExistingProject(AccountData account, ProjectData project)
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            List<ProjectData> projectsList = GetProjectsList(account);

            if (projectsList.Count == 0)
                return;

            var indexOfExistProject = projectsList.FindIndex(x => x.ProjectName == project.ProjectName);

            if (indexOfExistProject != -1)
            {
                var id = projectsList.Find(x => x.ProjectName == project.ProjectName).Id;
                client.mc_project_delete(account.Name, account.Password, id);
            }
        }

        public List<ProjectData> GetProjectsList(AccountData account)
        {
            List<ProjectData> projectsList = new List<ProjectData>();

            var projects = client.mc_projects_get_user_accessible(account.Name, account.Password);

            foreach (var project in projects)
            {
                projectsList.Add(new ProjectData()
                {
                    Id = project.id,
                    ProjectName = project.name,
                    Description = project.description,
                    Status = project.status.name,
                    Enabled = project.enabled.ToString(),
                    Visibility = project.view_state.name
                });
            }

            return projectsList;
        }
        public int GetCountProjects(AccountData account)
        {
            return GetProjectsList(account).Count;
        }

    }
}
