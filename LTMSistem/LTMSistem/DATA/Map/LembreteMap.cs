using LTMSistem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LTMSistem.DATA.Map
{
    public class LembreteMap : IEntityTypeConfiguration<LembreteModel>
    {
        public void Configure(EntityTypeBuilder<LembreteModel> builder)
        {
            builder.HasKey(x=> x.Id);
            builder.HasOne(x => x.Usuario);
        }
    }
}
