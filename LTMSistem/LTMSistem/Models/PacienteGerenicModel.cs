using Microsoft.AspNetCore.Mvc;

namespace LTMSistem.Models
{
    public class PacienteGerenicModel
    {
        public List<PacienteModel> Pacientes { get; set; }
        public PacienteModel Paciente { get; set; }
        [FromQuery]
        public string Nome { get; set; }
        [FromQuery]
        public string Email { get; set; }
    }
}
