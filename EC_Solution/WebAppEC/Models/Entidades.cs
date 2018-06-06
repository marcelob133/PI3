using System.Data.Entity;

namespace WebAppEC.Models
{
    public class Entidades : DbContext
    {
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<ItemPedido> ItensPedido { get; set; }
        public DbSet<Estoque> Estoques { get; set; }
        public DbSet<StatusPedido> StatusPedidos { get; set; }
        public DbSet<TipoPagamento> TiposPagamento { get; set; }

        public Entidades() : base("name=Entidades")
        {
        }
    }
}