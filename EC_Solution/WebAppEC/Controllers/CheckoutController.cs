using System;
using System.Collections.Generic;
using System.Web.Mvc;
using WebAppEC.Models;

namespace WebAppEC.Controllers
{
    public class CheckoutController : Controller
    {
        [Authorize]
        public ActionResult EfetuarPagamento()
        {
            //Modificação Cookie
            if (HttpContext.Request.Cookies["cliente"] == null)
            {
                return RedirectToAction("Login", "Cliente");
            }                
            else
            {
                if (Session["Carrinho"] != null)
                {
                    List<ItemCarrinho> listaCarrinho = new List<ItemCarrinho>();
                    listaCarrinho = (List<ItemCarrinho>)Session["Carrinho"];
                    if (listaCarrinho.Count == 0)
                    {                        
                        return RedirectToAction("Carrinho", "Produto"); 
                    }
                    return View();
                }
                return RedirectToAction("Carrinho", "Produto");                
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult EfetuarPagamento(Checkout checkout)
        {
            if(ModelState.IsValid)
            {
                bool blnRet = false;
                Checkout objModel = new Checkout();

                int intIdCliente = 0;

                if (HttpContext.Request.Cookies["cliente"] == null)
                    return RedirectToAction("Login", "Cliente");
                else
                    intIdCliente = Convert.ToInt32(HttpContext.Request.Cookies.Get("Cliente").Value);

                List<ItemCarrinho> listaCarrinho = new List<ItemCarrinho>();

                if (Session["Carrinho"] != null)
                {
                    listaCarrinho = (List<ItemCarrinho>)Session["Carrinho"];

                }

                try
                {
                    blnRet = objModel.EfetuarPagamento(checkout, listaCarrinho, intIdCliente);
                }
                catch (Exception)
                {

                    blnRet = false;
                }

                

                if (blnRet)
                {
                    return View("Confirmado");
                }
                else
                {
                    return View("Falha");
                }            
            }           
             return View(checkout); 
        }

        public ActionResult Confirmado()
        {
            return View();
        }

        public ActionResult Falha()
        {
            return View();
        }
    }
}