using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Agencia.Models
{
    public partial class ModelNew
    {        
        public int id { get; set; }
        public DateTime? fechaEntrada { get; set; }
        public DateTime? fechaSalida { get; set; }
        public int? cantidadPersonas { get; set; }
        public string ciudadDestino { get; set; }
        public string nombreEmergencia { get; set; }
        public string apellidoEmergencia { get; set; }
        public string telefomoEmergencia { get; set; }
        public string nombre_usuario { get; set; }
        public string nombre_hotel { get; set; }
        public string nombre_habitacion { get; set; }
    }
}