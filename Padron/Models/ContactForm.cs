using Padron.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Padron.Models
{
    public class ContactForm
    {
        public ContactForm()
        {
            this.CascadingModel = new CascadingModel();
        }
        [Key]
        public int Id { get; set; }
        [Display(Name = "Cedula")]
        [Required(ErrorMessage = "Inserte la cédula")]
        [CheckOnlyCedula]
        public string Cedula { get; set; }
        [Display(Name = "Teléfono")]
        [Required(ErrorMessage = "Ingrese un número de teléfono")]
        public string TelefonoMovil { get; set; }
        [Display(Name = "Nombre Completo")]
        [Required(ErrorMessage = "Inserte su nombre completo")]
        public string FullName { get; set; }
        [Display(Name = "Provincia")]
        [Required(ErrorMessage = "Seleccione una provincia")]
        [ForeignKey("Provincia")]
        public int ProvinciaId { get; set; }
        [Display(Name = "Municupio")]
        [Required(ErrorMessage = "Seleccione un municipio")]
        [ForeignKey("Municipio")]
        public int MunicipioId { get; set; }
        [Required(ErrorMessage = "Ingrese un Sector")]
        public string Sector { get; set; }
        [ForeignKey("Coordinador")]
        [Required(ErrorMessage = "Coordinador no especificado")]

        public int CoordinadorId { get; set; }
        [Display(Name = "Correo electrónico")]
        [Required(ErrorMessage = "Ingrese un Correo Electrónico")]
        public string Email { get; set; }
        public string ColaboradorDigitalRedes { get; set; }
        public string Comentario { get; set; }
        public int Deleted { get; set; }
        [NotMapped]
        public bool IsInstagram { get; set; }
        [NotMapped]

        public bool IsFacebook { get; set; }
        [NotMapped]

        public bool IsTwitter { get; set; }
        [NotMapped]

        public bool IsOther { get; set; }
        [NotMapped]
        public Guid? CoordinadorGuid { get; set; }

        [NotMapped]
        public CascadingModel CascadingModel { get; set; }
        public void ConcatRedes()
        {
            this.ColaboradorDigitalRedes =  (string.Concat(
                this.IsInstagram ? "Instagram," : "",
                this.IsFacebook ? "Facebook," : "",
                this.IsTwitter ? "Twitter," : "",
                this.IsOther ? "Other" : ""));
        }  
        public virtual User Coordinador { get; set; }
        public virtual Provincia Provincia { get; set; }
        public virtual Municipio Municipio { get; set; }



    }


}