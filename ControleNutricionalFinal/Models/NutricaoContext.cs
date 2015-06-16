using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace ControleNutricionalFinal.Models
{
    public class NutricaoContext : DbContext
    {
        public NutricaoContext()
            : base()
        {
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Alimento> Alimentos { get; set; }
        public DbSet<Grupo> Grupos { get; set; }
        public DbSet<Refeicao> Refeicao { get; set; }
        public DbSet<AlimentoRefeicao> AlimentoRefeicao { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var mapAlimento = modelBuilder.Entity<Alimento>();
            mapAlimento.Property(a => a.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            mapAlimento.HasKey(a => a.Id);

            var mapGrupos = modelBuilder.Entity<Grupo>();
            mapGrupos.Property(g => g.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            mapGrupos.HasKey(g => g.Id);

            var mapRefeicao = modelBuilder.Entity<Refeicao>();
            mapRefeicao.Property(rf => rf.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            mapRefeicao.HasKey(rf => rf.Id);

            var mapAlimentoRefeicao = modelBuilder.Entity<AlimentoRefeicao>();
            mapAlimentoRefeicao.Property(arf => arf.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            mapAlimentoRefeicao.HasKey(arf => arf.Id);


            modelBuilder.Entity<Alimento>()
                        .HasRequired<Grupo>(a => a.Grupo1)
                        .WithMany(g => g.Alimentos)
                        .HasForeignKey(a => a.Grupo);
        }
    }
}