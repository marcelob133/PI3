using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace WebAppEC.Models
{
    [Table("Cliente", Schema = "dbo")]
    public class Cliente
    {
        [Key]
        public int idCliente { get; set; }
        [Required(ErrorMessage = "Preencha o campo Nome")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "O campo Nome deve conter entre 5 e 100 caracteres")]
        [MaxLength(100)]
        [DisplayName("Nome")]
        public string nomeCompletoCliente { get; set; }
        [Required(ErrorMessage = "Preencha o campo E-mail")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "O campo E-mail no máximo 100 caracteres")]
        [DisplayName("E-mail")]
        [Remote("VerificarEmail", "Cliente")]
        [EmailAddress(ErrorMessage = "Digite um e-mail válido")]
        public string emailCliente { get; set; }
        [Required(ErrorMessage = "Preencha o campo Senha")]
        [StringLength(64, MinimumLength = 7, ErrorMessage = "O campo Senha deve conter entre 7 e 64 caracteres")]
        [DataType(DataType.Password)]
        [DisplayName("Senha")]
        public string senhaCliente { get; set; }
        [NotMapped]
        [DataType(DataType.Password)]
        [DisplayName("Confirmar Senha")]
        [System.Web.Mvc.Compare("senhaCliente", ErrorMessage = "As senhas não conferem")]
        public virtual string confirmarSenha { get; set; }
        [Required(ErrorMessage = "Preencha o campo CPF")]
        [StringLength(14, MinimumLength = 11, ErrorMessage = "O campo CPF deve conter no mínimo 11 caracteres")]
        [DisplayName("CPF")]
        [Remote("VerificarCPF", "Cliente")]
        public string CPFCliente { get; set; }
        [StringLength(20, ErrorMessage = "O campo Celular deve conter no máximo 20 caracteres")]
        [DisplayName("Celular")]
        public string celularCliente { get; set; }
        [StringLength(20, ErrorMessage = "O campo Tel. Comercial deve conter no máximo 20 caracteres")]
        [DisplayName("Tel. Comercial")]
        public string telComercialCliente { get; set; }
        [StringLength(20, ErrorMessage = "O campo Tel. Residencial deve conter no máximo 20 caracteres")]
        [DisplayName("Tel. Residencial")]
        public string telResidencialCliente { get; set; }
        [Required(ErrorMessage = "Preencha o campo Data de Nascimento")]
        [DataType(DataType.Date)]
        [DisplayName("Data de Nascimento")]
        public DateTime? dtNascCliente { get; set; }
        [DisplayName("Deseja receber newsletter?")]
        public bool recebeNewsLetter { get; set; }
    }
}