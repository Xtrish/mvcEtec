using LTMSistem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LTMSistem.DATA.Map
{
    public class ConsultaMap : IEntityTypeConfiguration<ConsultaModel>
    {
        public void Configure(EntityTypeBuilder<ConsultaModel> builder)
        {
            builder.HasKey(x=> x.PacienteId);
            builder.HasOne(x => x.Paciente);

            //builder.HasKey(x=> x.DentistaId);
            //builder.HasOne(x => x.Dentista);
        }
    }
}
