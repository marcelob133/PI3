using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppEC.Models
{
    [Table("TipoPagamento", Schema = "dbo")]
    public class TipoPagamento
    {
        [Key]
        public int idTipoPagto { get; set; }
        public string descTipoPagto { get; set; }
    }
}
