using System;
using P01_StudentSystem.Data;

namespace P01_StudentSystem
{
    public class StartUp
    {
        public static void Main()
        {
            using (StudentSystemContext context = new StudentSystemContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }  
        }
    }
}
