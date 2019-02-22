using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using mlShared.Models;


namespace mlShared.Data
{
    public class ApplicationDbContext : IdentityDbContext<MlUser>
    {
        public DbSet<World> Worlds { get; set; }
        public DbSet<Permission> Permissions { get; set; }

        public static string DefaultConnectionString = null;

        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }

        private static DbContextOptions getAddMigrationOptions() {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseMySql(Util.GetSetting("dev-db-connection"));
            return builder.Options;
        }

        /// <summary>
        /// Only for EF add-migration command.
        /// </summary>
        public ApplicationDbContext() : base(getAddMigrationOptions()) {}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public static void WithDb(Action<ApplicationDbContext> doThis)
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseMySql(DefaultConnectionString);
            using (var db = new ApplicationDbContext(builder.Options))
            {
                doThis(db);
            }
        }

        public static T WithDb<T>(Func<ApplicationDbContext, T> doThis)
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseMySql(DefaultConnectionString);
            using (var db = new ApplicationDbContext(builder.Options))
            {
                return doThis(db);
            }
        }

        public static async Task WithDbAsync(Func<ApplicationDbContext, Task> doThis)
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseMySql(DefaultConnectionString);
            using (var db = new ApplicationDbContext(builder.Options))
            {
                await doThis(db);
            }
        }

        public static async Task<T> WithDbAsync<T>(Func<ApplicationDbContext, Task<T>> doThis)
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseMySql(DefaultConnectionString);
            using (var db = new ApplicationDbContext(builder.Options))
            {
                return await doThis(db);
            }
        }

        public static void TryToMigrate(string connectionString = null)
        {
            try
            {
                Migrate(connectionString);
            }
            catch (Exception ex)
            {
                Util.LogException("DB Migration Failed", ex);
            }
        }

        public static void Migrate(string connectionString = null) {
            if (connectionString == null) {
                connectionString = DefaultConnectionString;
            }

            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseMySql(connectionString);
            using (var db = new ApplicationDbContext(builder.Options))
            {
                db.Database.Migrate();
            }
        }
    }
}



