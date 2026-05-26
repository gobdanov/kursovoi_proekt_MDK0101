using KAMA_PRO_CRUD_APP.classes.models;
using Microsoft.EntityFrameworkCore;

namespace KAMA_PRO_CRUD_APP2.classes.contexts
{
    public class DBContext : DbContext
    {
        public DbSet<Assemblages> Assemblages { get; set; }
        public DbSet<Assemblers> Assemblers { get; set; }
        public DbSet<Component_linkto_Trailer> Component_linkto_Trailer { get; set; }
        public DbSet<Components> Components { get; set; }
        public DbSet<Packs> Packs { get; set; }
        public DbSet<Plan_linkto_Trailer> Plan_linkto_Trailer { get; set; }
        public DbSet<Plans> Plans { get; set; }
        public DbSet<Trailers> Trailers { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                "server=localhost;database=kama_pro_db;user=root;password=;",
                new MySqlServerVersion(new Version(8,0,34)));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //ПЕРВИЧНЫЕ КЛЮЧИ
            //настройка первичного ключа для Traiers
            modelBuilder.Entity<Trailers>()
                .HasKey(o => o.Name);

            //настройка первичного ключа для Plans
            modelBuilder.Entity<Plans>()
                .HasKey(o => o.Name);

            //настройка первичного ключа для Components
            modelBuilder.Entity<Components>()
                .HasKey(o => o.Name);

            //настройка первичного ключа для Components
            modelBuilder.Entity<Components>()
                .HasKey(o => o.Name);

            //настройка первичного ключа для Assemblages
            modelBuilder.Entity<Assemblages>()
                .HasKey(AV => new { AV.Assembler, AV.VIN });


            //ВНЕШНИЕ КЛЮЧИ
            //настройка внешнего ключа для Assemblages
            modelBuilder.Entity<Assemblages>()
                .HasOne<Assemblers>()
                .WithMany()
                .HasForeignKey(a => a.Assembler);

            //настройка внешнего ключа для Component_linkto_Trailer
            modelBuilder.Entity<Component_linkto_Trailer>()
                .HasOne<Components>()
                .WithMany()
                .HasForeignKey(CT => CT.Component);

            modelBuilder.Entity<Component_linkto_Trailer>()
                .HasOne<Trailers>()
                .WithMany()
                .HasForeignKey(CT => CT.Trailer);

            //настройка внешнего ключа для Plan_linkto_Trailer
            modelBuilder.Entity<Plan_linkto_Trailer>()
                .HasOne<Plans>()
                .WithMany()
                .HasForeignKey(PT => PT.Plan);

            modelBuilder.Entity<Plan_linkto_Trailer>()
                .HasOne<Trailers>()
                .WithMany()
                .HasForeignKey(PT => PT.Trailer);

            modelBuilder.Entity<Plan_linkto_Trailer>()
                .HasOne<Packs>()
                .WithMany()
                .HasForeignKey(PT => PT.Pack);


        }


    }
}
