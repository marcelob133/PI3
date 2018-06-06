using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppEC.Models
{
    [Table("Pedido", Schema = "dbo")]
    public class Pedido
    {
        [Key]
        public int idPedido { get; set; }
        public int idCliente { get; set; }
        public int idStatus { get; set; }
        public DateTime dataPedido { get; set; }
        public int idTipoPagto { get; set; }
        public int idEndereco { get; set; }
        public int idAplicacao { get; set; }
    }
}