using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataPress.Storage
{
    using System.Data.Entity;

    public class DpDataContext : DbContext
    {
        public DpDataContext() : base("DataPress.Cms.Storage") { }
        public DpDataContext(string nameOrConnectionStr) : base(nameOrConnectionStr) { }

        static DpDataContext()
        {
            //Database.SetInitializer<DpCmsContext>(null);
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<DpDataContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
        }
    }
}
