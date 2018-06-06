using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppEC.Models
{
    [Table("StatusPedido", Schema = "dbo")]
    public class StatusPedido
    {
        [Key]
        public int idStatus { get; set; }
        public string descStatus { get; set; }
    }
}