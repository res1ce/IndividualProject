using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using MySql.Data.EntityFramework;
using IndidProject.models;

namespace IndidProject
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("name=MySqlConnectionString")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Apteka> Aptekas { get; set; }
        public DbSet<DrugGroup> DrugGroups { get; set; }
        public DbSet<Drug> Drugs { get; set; }
        public DbSet<Avaibality> Availabilitys { get; set; }
    }
}
