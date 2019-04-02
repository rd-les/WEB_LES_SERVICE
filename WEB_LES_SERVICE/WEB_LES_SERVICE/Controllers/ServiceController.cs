using LesWebService.Services.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace LesWebService.Controllers
{
    public class ServiceController : Controller
    {
        // GET: Service
        /*
        public ActionResult Index()
        {
            return View();
        }
        */
        

        public JsonResult GetDataWorkStaion() {
            WorkStationIweb workStationIweb = new WorkStationIweb(); 

            return Json(workStationIweb.getDataWorkStationIweb() , JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetDataWorkStaionReleased() {
            WorkStationIweb workStationIweb = new WorkStationIweb();
            
            return Json(workStationIweb.getDataWorkStationIwebReleased(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAutoComplateWorkStationIwebReleased() {
            WorkStationIweb workStationIweb = new WorkStationIweb();

            return Json(workStationIweb.getAutoComplateWorkStationIwebReleased(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetWorkStationDetail() {
            string workStationId = Request["workStationId"];
            if (!workStationId.Equals("")) {
                WorkStationIweb workStationIweb = new WorkStationIweb();
                return Json(workStationIweb.getWorkStationDetail(workStationId), JsonRequestBehavior.AllowGet);
            }
            else {
                return null;
            }

        }




    }
}