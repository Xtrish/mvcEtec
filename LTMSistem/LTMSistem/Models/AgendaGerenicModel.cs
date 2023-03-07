using LTMSistem.Enums;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LTMSistem.Models
{
    public class AgendaGerenicModel
    {
        public List<ConsultaModel> Consultas { get; set; }

        [Key]
        public long Id { get; set; }
        public long? UsuarioAlteracao { get; set; }
        public long PacienteId { get; set; }
        public virtual PacienteModel Paciente { get; set; }
        public virtual List<PacienteModel>? Pacientes { get; set; }
        public long DentistaId { get; set; }
        public virtual UsuarioModel Dentista { get; set; }
        public virtual List<UsuarioModel> Dentistas { get; set; }
        public DateTime DataConsulta { get; set; }
        public ProcedimentoEnum Procedimento { get; set; }
        public string? Observacao { get; set; }
        public bool Status { get; set; }

        public virtual DateTime DuracaoConsulta { get; set; }
        public DateTime DataFinal { get; set; }


       public DateTime DataDia { get; set; }
    }
}
