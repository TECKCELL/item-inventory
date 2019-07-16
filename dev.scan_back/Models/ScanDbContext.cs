using System.Data.Entity;

namespace dev.scan_back.Models
{
    public class ScanDbContext : DbContext
    {
        private static bool _created = false;

        public ScanDbContext() : base("DefaultConnection")
        {
            if (!_created)
            {
                _created = true;
                this.Database.CreateIfNotExists();   
                

            }
        }
        protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.Entity<Import>()
                .HasMany<SCANS>(q => q.Scans)
                .WithRequired(o => o.Import)
                .HasForeignKey<int>(k => k.ImportId);

            builder.Entity<Import>()
               .HasMany<assort>(q => q.Assort)
               .WithRequired(o => o.Import)
               .HasForeignKey<int>(k => k.ImportId);

            builder.Entity<Client>()
                .HasMany(q => q.Imports)
                .WithRequired(o => o.Client);

            builder.Entity<SCANS>()
                .HasRequired<Import>(i => i.Import)
                .WithMany(s => s.Scans)
                .HasForeignKey<int>(k => k.ImportId);


            builder.Entity<assort>()
                .HasRequired<Import>(i => i.Import)
                .WithMany(s => s.Assort)
                .HasForeignKey<int>(k => k.ImportId);

            builder.Entity<ProductFile>()
                .HasRequired<Operation>(o => o.Operation);
               
            builder.Entity<Operation>()
                .HasMany<ProductFile>(p => p.productFiles)
                .WithRequired(o => o.Operation);
            
        }

        public DbSet<assort> assort { get; set; }

        public DbSet<SCANS> Scans { get; set; }

        public DbSet<Import> Import { get; set; }

        public DbSet<Client> Client { get; set; }

        public DbSet<Procurement> Procurement { get; set; }

        public DbSet<ProductFile> ProductFile { get; set; }

        public DbSet<Operation> Operation { get; set; }

        public System.Data.Entity.DbSet<dev.scan_back.Models.OperationViewModel> OperationViewModels { get; set; }
    }
}