using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppEC.Models
{
    [NotMapped]
    public class PedidoViewModel
    {
        [DisplayName("Número do Pedido")]
        public virtual int idPedido { get; set; }
        public virtual int idCliente { get; set; }
        [DisplayName("Cliente")]
        public virtual string nomeCompletoCliente { get; set; }
        public virtual byte idStatus { get; set; }
        [DisplayName("Status")]
        public virtual string descStatus { get; set; }
        [DisplayName("Data do Pedido")]
        public virtual DateTime dataPedido { get; set; }
        public virtual byte idTipoPagto { get; set; }
        [DisplayName("Tipo de Pagamento")]
        public virtual string descTipoPagto { get; set; }
        [DisplayName("Total (R$)")]
        public virtual decimal totalPagto { get; set; }
    }
}