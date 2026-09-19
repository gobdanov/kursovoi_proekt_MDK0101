using KAMA_PRO_CRUD_APP.classes.models;
using KAMA_PRO_CRUD_APP2.classes.models;
using Microsoft.EntityFrameworkCore;

namespace API_KAMA_PRO_CRUD_APP
{
    public class DBContext : DbContext
    {

        private string ConnectionString = "Server=127.0.1.16;Port=3306;Database=kama_pro_db;Uid=root;Pwd=;";


        public DbSet<Assemblages> Assemblages { get; set; }
        public DbSet<Assemblers> Assemblers { get; set; }
        public DbSet<Component_linkto_Trailer> Component_linkto_Trailer { get; set; }
        public DbSet<Components> Components { get; set; }
        public DbSet<Packs> Packs { get; set; }
        public DbSet<Plan_linkto_Trailer> Plan_linkto_Trailer { get; set; }
        public DbSet<Plans> Plans { get; set; }
        public DbSet<Trailers> Trailers { get; set; }
        public DbSet<Payments> Payments { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                ConnectionString,
                ServerVersion.AutoDetect(ConnectionString));
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


            modelBuilder.Entity<Component_linkto_Trailer>()
                .HasNoKey();

            modelBuilder.Entity<Payments>().HasKey(p => p.Id);

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

            modelBuilder.Entity<Payments>().
                HasOne<Assemblers>()
                .WithMany()
                .HasForeignKey(a => a.Assembler);
        }
    }
}
