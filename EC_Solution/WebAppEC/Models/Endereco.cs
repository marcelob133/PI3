using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppEC.Models
{
    [Table("Endereco", Schema = "dbo")]
    public class Endereco
    {
        [Key]
        public int idEndereco { get; set; }
        public int idCliente { get; set; }
        [Required(ErrorMessage = "Preencha o campo Endereço")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "O campo Endereço deve conter no mínimo 3 e no máximo 50 caracteres")]
        [DisplayName("Nome do Endereço")]
        public string nomeEndereco { get; set; }
        [Required(ErrorMessage = "Preencha o campo Logradouro")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O campo Logradouro deve conter no mínimo 3 e no máximo 100 caracteres")]
        [DisplayName("Logradouro")]
        public string logradouroEndereco { get; set; }
        [Required(ErrorMessage = "O campo Número é obrigatório")]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "O campo Número deve conter no mínimo 1 e no máximo 10 caracteres")]
        [DisplayName("Número")]
        public string numeroEndereco { get; set; }
        [Required(ErrorMessage = "Preencha o campo CEP")]
        [StringLength(9, MinimumLength = 8, ErrorMessage = "O campo CEP deve conter no mínimo 8 e no máximo 9 caracteres")]
        [DisplayName("CEP")]
        public string CEPEndereco { get; set; }
        [StringLength(10, ErrorMessage = "O campo Complemento deve conter no máximo 10 caracteres")]
        [DisplayName("Complemento")]
        public string complementoEndereco { get; set; }
        [Required(ErrorMessage = "Preencha o campo Cidade")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "O campo cidade deve conter no mínimo 3 e no máximo 50 caracteres")]
        [DisplayName("Cidade")]
        public string cidadeEndereco { get; set; }
        [Required(ErrorMessage = "Preencha o campo País")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "O campo País deve conter no mínimo 3 e no máximo 50 caracteres")]
        [DisplayName("País")]
        public string paisEndereco { get; set; }
        [Required(ErrorMessage = "Preencha o campo Unidade Federativa (UF)")]
        [StringLength(2, MinimumLength = 2)]
        [DisplayName("UF")]
        public string UFEndereco { get; set; }
    }
}