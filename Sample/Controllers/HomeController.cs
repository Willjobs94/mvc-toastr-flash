using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RedWillow.MvcToastrFlash;

namespace Sample.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Show()
        {
            this.Flash(Toastr.SUCCESS, "You have loaded a page that doesn't redirect!");

            return View();
        }
        public ActionResult Redirect()
        {
            this.Flash(Toastr.INFO, "You have redirected back to Index.");

            return RedirectToAction("Index");
        }

        public ActionResult StackedRedirect()
        {
            this.Flash(Toastr.INFO, "First Message", "The first message in the stack.");
            this.Flash(Toastr.WARNING, "Second Message", "A warning as the second message. You need to dismiss this message.", new { timeOut = 0, extendedTimeOut = 0 });
            this.Flash(Toastr.ERROR, "No title on this error.");

            return RedirectToAction("Index");
        }

        public ActionResult RedirectJourney()
        {
            this.Flash(Toastr.INFO, "Start of the Journey");

            return RedirectToAction("RedirectWaypoint");
        }

        public ActionResult RedirectWaypoint()
        {
            this.Flash(Toastr.SUCCESS, "Successful pit stop at the way point.");

            return RedirectToAction("NonFlashingWaypoint");
        }

        public ActionResult NonFlashingWaypoint()
        {
            return RedirectToAction("Index");
        }
    }
}