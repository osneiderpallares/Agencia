//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Agencia.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Usuarios
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Usuarios()
        {
            this.Alojamiento = new HashSet<Alojamiento>();
        }
    
        public int id { get; set; }
        public Nullable<int> tipoDocumento { get; set; }
        [Required(ErrorMessage = "El campo es obligatorio")]
        [MinLength(10, ErrorMessage = "El campo debe tener menos de 10 caracteres")]
        public string numeroDocumento { get; set; }
        [Required(ErrorMessage = "El campo es obligatorio")]
        [MinLength(200, ErrorMessage = "El campo debe tener menos de 200 caracteres")]
        public string nombres { get; set; }
        [Required(ErrorMessage = "El campo es obligatorio")]
        [MinLength(200, ErrorMessage = "El campo debe tener menos de 200 caracteres")]
        public string apellidos { get; set; }
        public string email { get; set; }
        public string genero { get; set; }
        [Required(ErrorMessage = "El campo es obligatorio")]
        [MinLength(10, ErrorMessage = "El campo debe tener menos de 10 caracteres")]
        public string telefono { get; set; }
        public Nullable<System.DateTime> fechaNacimiento { get; set; }
        [Required(ErrorMessage = "El campo es obligatorio")]
        [MinLength(100, ErrorMessage = "El campo debe tener menos de 100 caracteres")]
        public string nombreUsuario { get; set; }
        public string clave { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Alojamiento> Alojamiento { get; set; }
        public virtual tiposDocumentos tiposDocumentos { get; set; }
    }
}