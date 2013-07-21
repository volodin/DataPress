using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataPress.Cms.Storage
{
    using System.Data.Entity;

    public class DpCmsContext : DbContext
    {
        public DpCmsContext() : base("DataPress.Cms.Storage") { }
        public DpCmsContext(string nameOrConnectionStr) : base(nameOrConnectionStr) { }

        static DpCmsContext()
        {
            //Database.SetInitializer<DpCmsContext>(null);
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<DpCmsContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
        }
    }
}
