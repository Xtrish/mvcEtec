using Microsoft.AspNetCore.Mvc;

namespace LTMSistem.Models
{
    public class ConsultaGerenicModel
    {
        public List<ConsultaModel> Consultas { get; set; }
        public ConsultaModel Consulta { get; set; }
        [FromQuery]
        public string NomeDentista { get; set; }
        [FromQuery]
        public string NomePaciente { get; set; }
    }
}
