using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppEC.Models
{
    [Table("ItemPedido", Schema = "dbo")]
    public class ItemPedido
    {
        [Column(Order = 1), Key]
        public int idProduto { get; set; }
        [Column(Order = 2), Key]
        public int idPedido { get; set; }
        public short qtdProduto { get; set; }
        public decimal precoVendaItem { get; set; }
    }
}