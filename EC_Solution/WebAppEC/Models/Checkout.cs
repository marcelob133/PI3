using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Linq;

namespace WebAppEC.Models
{
    [NotMapped]
    public class Checkout
    {
        public virtual int ID { get; set; }
        [Required(ErrorMessage = "Preencha o campo Nome do Titular")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "O campo Nome do Titular do Cartão deve conter entre 5 e 100 caracteres")]
        [DisplayName("Nome do Titular do Cartão:")]
        public virtual string NomeImpresso { get; set; }
        [Required(ErrorMessage = "Preencha o campo Número do Cartão")]
        [DisplayName("Número do Cartão")]
        public virtual string NumeroCartao { get; set; }
        [Required(ErrorMessage = "Preencha o campo Data de Vencimento")]
        [DataType(DataType.Date)]
        [DisplayName("Vencimento (mm/aa)")]
        public virtual string Vencimento { get; set; }
        [Required(ErrorMessage = "Preencha o campo Código de Segurança")]
        [DisplayName("Código de Segurança")]
        public string CodSeguranca { get; set; }
        [Required(ErrorMessage = "Preencha o campo Valor")]
        [DisplayName("Valor")]
        [DataType(DataType.Currency)]
        public virtual decimal Valor { get; set; }
        [Required(ErrorMessage = "Preencha o campo Parcelas")]
        [DisplayName("Parcelas")]
        public virtual int Parcelas { get; set; }

        public bool EfetuarPagamento(Checkout checkout, List<ItemCarrinho> listaCarrinho, int idCliente, Endereco endereco = null)
        {
            try
            {
                //br.com.webint.www.CGatewayPagamento objWS = new
                //    br.com.webint.www.CGatewayPagamento();
                //objWS.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["Timeout"]);
                bool blnRet = true;
                //blnRet = objWS.validarCartao(checkout.NomeImpresso, checkout.NumeroCartao, checkout.Vencimento, checkout.CodSeguranca, (double)checkout.Valor, checkout.Parcelas);
                if (blnRet)
                {
                    return SalvarPedido(listaCarrinho, idCliente, endereco, 1);
                }
                else
                {
                    return SalvarPedido(listaCarrinho, idCliente, endereco, 0);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool SalvarPedido(List<ItemCarrinho> listaCarrinho, int idCliente, Endereco endereco, int intStatus = 0)
        {
            using (Entidades db = new Entidades())
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        Pedido p = new Pedido();
                        p.dataPedido = DateTime.Now;
                        p.idAplicacao = 3; //Online - site
                        if (intStatus == 1)
                            p.idStatus = 3; //Enviado para a Transportadora
                        else
                            p.idStatus = 2; //Aguardando Pagamento
                        p.idTipoPagto = 1; //Cartão de Crédito
                        p.idCliente = idCliente;
                        if (endereco != null)
                        {
                            p.idEndereco = endereco.idEndereco;
                        }
                        else
                        {
                            try
                            {
                                Endereco end = (from x in db.Enderecos
                                                where x.idCliente == idCliente
                                                select x).First();
                                p.idEndereco = end.idEndereco;
                            }
                            catch (Exception)
                            {
                                //Isto é um recurso apenas porque a base de dados, infelizmente, 
                                //tem clientess cadastrados sem endereço...
                                p.idEndereco = 1;
                            }
                        }
                        db.Pedidos.Add(p);
                        db.SaveChanges();
                        int intPedido = p.idPedido;
                        for (int i = 0; i < listaCarrinho.Count; i++)
                        {
                            ItemPedido itemPedido = new ItemPedido();
                            itemPedido.idPedido = intPedido;
                            itemPedido.idProduto = listaCarrinho[i].productId;
                            itemPedido.qtdProduto = (short)listaCarrinho[i].productQtd;
                            itemPedido.precoVendaItem = listaCarrinho[i].productPrice;
                            db.ItensPedido.Add(itemPedido);
                            db.SaveChanges();
                            try
                            {
                                Estoque est = (from x in db.Estoques
                                               where x.idProduto == itemPedido.idProduto
                                               select x).First();
                                est.qtdProdutoDisponivel -= itemPedido.qtdProduto;
                                db.SaveChanges();
                            }
                            catch
                            {
                                //Não fazendo nada, pois há produtos sem estoque... :(
                            }
                        }
                        dbContextTransaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                        throw ex;
                    }
                }
            }
        }
    }
}