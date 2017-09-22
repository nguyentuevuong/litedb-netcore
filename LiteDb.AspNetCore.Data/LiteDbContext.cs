using LiteDB;
using Microsoft.AspNetCore.Hosting;
using static System.IO.Path;
using static System.IO.Directory;

namespace LiteDb.AspNetCore.Data
{
    public class LiteDbContext
    {
        public LiteRepository LiteRepository { get; private set; }

        public LiteDatabase LiteDatabase => LiteRepository?.Database;

        public LiteDbContext(IHostingEnvironment env)
        {
            string databasePath = Combine(env.ContentRootPath, "App_Data");

            if (!Exists(databasePath))
                CreateDirectory(databasePath);

            LiteRepository = new LiteRepository(Combine(databasePath, "Database.db"));
        }
    }
}
