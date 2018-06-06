using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppEC.Models
{
    [Table("Produto", Schema = "dbo")]
    public class Produto
    {
        [Key]
        public int idProduto { get; set; }
        [Required]
        [DisplayName("Nome do Produto")]
        public string nomeProduto { get; set; }
        [DisplayName("Descrição do Produto")]
        public string descProduto { get; set; }
        [Required]
        [DisplayName("Preço do Produto")]
        public decimal precProduto { get; set; }
        [Required]         
        [DisplayName("Desconto do Produto")]
        public decimal? descontoPromocao { get; set; }
        [Required]
        [DisplayName("Id da Categoria")]
        public int idCategoria { get; set; }
        [Required]
        [DisplayName("Produto Ativo")]
        public string ativoProduto { get; set; }
        [DisplayName("id do Usuário")]
        public int? idUsuario { get; set; }
        [DisplayName("Quantidade Minima do Produto")]
        public int qtdMinEstoque { get; set; }
        public byte[] imagem { get; set; }
    }
}