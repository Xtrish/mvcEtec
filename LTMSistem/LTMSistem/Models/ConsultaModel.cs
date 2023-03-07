using LTMSistem.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace LTMSistem.Models
{
    public class ConsultaModel
    {
        [Key]
        public long Id { get; set; }
        public long? UsuarioAlteracao { get; set; }
        [Required(ErrorMessage = "Paciente Obrigatório")]
        public long PacienteId { get; set; }
        public virtual PacienteModel? Paciente { get; set; }
        public virtual List<PacienteModel>? Pacientes { get; set; }
        [Required(ErrorMessage = "Dentista Obrigatório")]
        public long DentistaId { get; set; }
        public virtual UsuarioModel? Dentista { get; set; }
        public virtual List<UsuarioModel>? Dentistas { get; set; }
        [Required(ErrorMessage = "Data da Consulta Obrigatória")]
        public DateTime DataConsulta { get; set; }
        [Required(ErrorMessage = "Procedimento Obrigatório")]
        public ProcedimentoEnum Procedimento { get; set; }
        public string? Observacao { get; set; }
        public bool Status { get; set; }
        [Required(ErrorMessage = "Duração Obrigatória")]
        public virtual DateTime DuracaoConsulta { get; set; }
        public DateTime DataFinal { get; set; }

    }
}
