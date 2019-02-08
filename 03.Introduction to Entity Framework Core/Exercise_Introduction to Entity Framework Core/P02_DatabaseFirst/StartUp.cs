using System;
using System.Globalization;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using P02_DatabaseFirst.Data;
using P02_DatabaseFirst.Data.Models;


namespace P02_DatabaseFirst
{
    public class StartUp
    {
        public static void Main()
        {
            using (SoftUniContext context = new SoftUniContext())
            {

                //var projects = context.EmployeesProjects.Where(x => x.ProjectId == 2);
                //context.EmployeesProjects.RemoveRange(projects);

                //var project = context.Projects.Find(2);
                //context.Projects.Remove(project);


                //context.SaveChanges();

                var projects = context.Projects.Take(10).ToArray();

                foreach (var p in projects)
                {
                    Console.WriteLine(p.Name);
                }



            }

        }
    }
}
