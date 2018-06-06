using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppEC.Models
{
    [Table("Categoria", Schema = "dbo")]
    public class Categoria
    {
        [Key]
        public int idCategoria { get; set; }
        [Required]
        [DisplayName("Nome da Categoria")]
        public string nomeCategoria { get; set; }
        [DisplayName("Descrição da Categoria")]
        public string descCategoria { get; set; }
    }
}