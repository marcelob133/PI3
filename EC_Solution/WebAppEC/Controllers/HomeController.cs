using System;
using System.Linq;
using System.Web.Mvc;
using WebAppEC.Models;

namespace WebAppEC.Controllers
{
    public class HomeController : Controller
    {
        private Entidades db = new Entidades();

        public ActionResult Index()
        {
            var resultado = db.Produtos.
                Where(p => p.ativoProduto.Equals("1")).Distinct().
                OrderBy(s => Guid.NewGuid()).
                Take(8);
            return View(resultado);
        }
    }
}