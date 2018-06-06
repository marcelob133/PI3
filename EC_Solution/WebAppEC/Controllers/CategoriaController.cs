using System.Linq;
using System.Web.Mvc;
using WebAppEC.Models;
namespace WebAppEC.Controllers
{
    public class CategoriaController : Controller
    {
        private Entidades db = new Entidades();

        public ActionResult Index()
        {
            return View(db.Categorias.ToList());
        }

        public ActionResult _MenuCategorias()
        {
            return PartialView(db.Categorias.ToList());
        }
	}
}