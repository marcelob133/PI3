using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebAppEC.Models;

namespace WebAppEC.Controllers
{
    public class ProdutoController : Controller
    {
        private Entidades db = new Entidades();
       
        public ActionResult Detalhe(int id = 0)
        {
            if (db.Produtos.Where(p => p.idProduto == id).Count() > 0)
            {
                var resultado = db.Produtos.Where(p => p.idProduto == id).FirstOrDefault();
                return View(resultado);
            }
            else
            {
                return View("produtoNaoEncontrado");
            }
        }

        [HttpPost]
        public JsonResult Carrinho(string cart)
        {
            List<ItemCarrinho> cartList = new List<ItemCarrinho>();

            if (cart != null)
            {
                var produtos = JsonConvert.DeserializeObject<List<ItemCarrinho>>(cart);
               
                foreach (var produto in produtos)
                {
                    var qtd = int.Parse(produto.productQtd.ToString());
                    if (qtd > 0)
                    {
                        var idProduto = int.Parse(produto.productId.ToString());
                        var resultado = db.Produtos.Where(m => m.idProduto == idProduto).FirstOrDefault();
                        ItemCarrinho prod = new ItemCarrinho();
                        prod.productId = idProduto;
                        prod.productQtd = qtd;
                        prod.productName = resultado.nomeProduto;
                        resultado.descontoPromocao = resultado.descontoPromocao == null ? 0 : resultado.descontoPromocao;
                        prod.productPrice = resultado.precProduto - (decimal)resultado.descontoPromocao;
                        prod.productTotal = qtd * prod.productPrice;
                        string imgProduct = string.Empty;
                        if (resultado.imagem != null)
                        {
                            string base64String = Convert.ToBase64String(resultado.imagem);
                            imgProduct = "data:image/png;base64," + base64String;
                        }
                        else
                        {
                            imgProduct = "/img/default_product.png";
                        }
                        prod.productImage = imgProduct;
                        cartList.Add(prod);
                    }
                }
                Session["Carrinho"] = cartList;
            }
            return Json(JsonConvert.SerializeObject(cartList));
        }

        public ActionResult Categoria(int id = 0)
        {    
            if (id == -1)
            {
                ViewBag.Mensagem = "Todas os produtos das demais categorias...";
                return View(db.Produtos.Where(p => p.idCategoria > 5).
                    OrderBy(o => o.idCategoria).ToList());
            }        
            if(db.Categorias.Where(p => p.idCategoria == id).Count() > 0)
            {
                ViewBag.Mensagem = db.Categorias.Find(id).nomeCategoria.ToString();
                return View(db.Produtos.Where(p => p.idCategoria == id).ToList());
            }
            else
            {
                return View(db.Produtos.ToList());
            }            
        }

        public ActionResult Consultar(string pesquisar)
        {
            if (!string.IsNullOrEmpty(pesquisar))
                ViewBag.Mensagem = "Resultado da Busca por: '" + pesquisar + "'";
            var resultado = db.Produtos.Where(s => s.nomeProduto.Contains(pesquisar) || s.descProduto.Contains(pesquisar));
            return View("Categoria", resultado);
        }

        public ActionResult Sortear()
        {
            var resultado = db.Produtos.Where(p => p.ativoProduto.Equals("1")).Distinct().OrderBy(s => Guid.NewGuid()).Take(5);
            return PartialView("_Destaques", resultado);
        }

        public ActionResult SortearUm(int id)
        {
            var resultado = db.Produtos.Where(s => s.ativoProduto.Equals("1")).OrderBy(s => Guid.NewGuid()).Where(s => s.idCategoria == id).FirstOrDefault();
            return PartialView("_Destaque", resultado);
        }

        public ActionResult Carrinho()
        {
            ViewBag.IsCartPage = true;
            return View();
        }
	}
}