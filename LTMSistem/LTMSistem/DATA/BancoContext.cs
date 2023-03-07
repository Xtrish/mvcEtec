
using LTMSistem.DATA.Map;
using LTMSistem.Models;
using Microsoft.EntityFrameworkCore;

namespace LTMSistem.DATA
{
    public class BancoContext : DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options) : base(options)
        {
        }

        public DbSet<PacienteModel> Pacientes { get; set; }
        public DbSet<UsuarioModel> Usuarios { get; set; }
        public DbSet<LembreteModel> Lembretes { get; set; }
        public DbSet<ConsultaModel> Consultas { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<LembreteModel>()
            //    .HasOne(lembrete => lembrete.Usuario)
            //    .WithMany(usuario => usuario.Lembretes)
            //    .HasForeignKey(lembrete => lembrete.UsuarioId);
            //.OnDelete(DeleteBehavior.Restrict);  Deleção noCascata

            modelBuilder.ApplyConfiguration(new LembreteMap());
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ConsultaModel>()
                .HasOne(consulta => consulta.Paciente)
                .WithMany(paciente => paciente.Consultas)
                .HasForeignKey(consulta => consulta.PacienteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ConsultaModel>()
               .HasOne(consulta => consulta.Dentista)
               .WithMany(dentista => dentista.Consultas)
               .HasForeignKey(consulta => consulta.DentistaId)
               .OnDelete(DeleteBehavior.Restrict);



            //modelBuilder.ApplyConfiguration(new ConsultaMap());
            //   base.OnModelCreating(modelBuilder);


        }



    }
}
