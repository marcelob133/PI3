using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppEC.Models
{
    [NotMapped]
    public class EsqueciSenhaViewModel
    {
        [Required(ErrorMessage = "E-mail é obrigatório")]
        [StringLength(100, MinimumLength = 5)]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        [DisplayName("E-mail")]
        public virtual string emailCliente { get; set; }
    }
}