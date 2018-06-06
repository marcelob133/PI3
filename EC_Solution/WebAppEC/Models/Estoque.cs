using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppEC.Models
{
    [Table("Estoque", Schema = "dbo")]
    public class Estoque
    {
        [Key]
        public int idProduto { get; set; }
        public int qtdProdutoDisponivel { get; set; }
    }
}