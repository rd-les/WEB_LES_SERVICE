using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Web_LED.App_Class;
using WEB_LED.Models.DataManage;
using WEB_LES_SERVICE.Models.DataManage;

namespace WEB_LES_SERVICE.Controllers
{
    public class DataManageController : Controller
    {
        // GET: DataManage
        public ActionResult Index()
        {
            return View();
        }




        public JsonResult getDataPd3LedType() {


            if (!string.IsNullOrEmpty(Request["led_slot_type"])) {

                string ledSlotTypeCode = Request["led_slot_type"];
                M_DataFunctionTest mDataFunctionTest = new M_DataFunctionTest();
                return Json(new { result = "SERVER STATUS PASS." + DateTime.Now, datas = mDataFunctionTest.getDataPd3LedType(ledSlotTypeCode) }, JsonRequestBehavior.AllowGet);



            }
            else {
                return Json(new { result = "led_slot_type NOT FOUND." + DateTime.Now }, JsonRequestBehavior.AllowGet); ;
            }





            /*
             

            
            if (!string.IsNullOrEmpty(Request["stringJson"])) {

                string stringJson = Request["stringJson"] ;

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                //dynamic jsonObject = serializer.Deserialize<dynamic>(json);
                F_DataFunctionTest jsonObjects = JsonConvert.DeserializeObject<F_DataFunctionTest>(stringJson);
                M_DataFunctionTest mDataFunctionTest = new M_DataFunctionTest();


                //mDataFunctionTest.doInsertDataFunctionTest(jsonObjects); 

                return Json(new { result = "SERVER STATUS PASS." + DateTime.Now , datas = mDataFunctionTest.doInsertDataFunctionTest(jsonObjects) }, JsonRequestBehavior.AllowGet);
            }
            else {
                return Json(new { result = "SERVER STATUS FAIL." + DateTime.Now }, JsonRequestBehavior.AllowGet); ;
            }
             */

            //return Json(workStationIweb.getDataWorkStationIwebReleased(), JsonRequestBehavior.AllowGet);
            return null; 
        }



        public ActionResult P_KO_TestData() {

            M_ActionDataFT_Driver mActionDataFT_Driver = new M_ActionDataFT_Driver();
            ViewBag.jsonData = JsonConvert.SerializeObject(Json(mActionDataFT_Driver.P_KO_TestData()));

            return View(); 
        }


        public JsonResult doInsertDataFunctionTest_T8() {

            F_DataFunctionTest_T8 f_DataFunctionTest_T8 = new F_DataFunctionTest_T8();

            
            if (!string.IsNullOrEmpty(Request["stringJson"])) {

                string stringJson = Request["stringJson"] ;

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                //dynamic jsonObject = serializer.Deserialize<dynamic>(json);
                F_DataFunctionTest_T8 jsonObjects = JsonConvert.DeserializeObject<F_DataFunctionTest_T8>(stringJson);
                M_DataFunctionTest mDataFunctionTest = new M_DataFunctionTest();

                //mDataFunctionTest.doInsertDataFunctionTest(jsonObjects); 
                return Json(new { result = "SERVER STATUS PASS." + SystemClass.getCurrentDateTimeInsert(), datas = mDataFunctionTest.doInsertDataFunctionTest_T8(jsonObjects) }, JsonRequestBehavior.AllowGet);
            }
            else {
                return Json(new { result = "SERVER STATUS FAIL." + SystemClass.getCurrentDateTimeInsert() }, JsonRequestBehavior.AllowGet); ;
            }
            
             

        }

        public JsonResult doInsertDataFunctionTest() {

            //M_DataFunctionTest xxxx = new M_DataFunctionTest();
            //return Json(xxxx.doInsertDataFunctionTest() , JsonRequestBehavior.AllowGet); 

            F_DataFunctionTest fDataFunctionTest = new F_DataFunctionTest(); 

            
            if (!string.IsNullOrEmpty(Request["stringJson"])) {

                string stringJson = Request["stringJson"] ;

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                //dynamic jsonObject = serializer.Deserialize<dynamic>(json);
                F_DataFunctionTest jsonObjects = JsonConvert.DeserializeObject<F_DataFunctionTest>(stringJson);
                M_DataFunctionTest mDataFunctionTest = new M_DataFunctionTest();


                //mDataFunctionTest.doInsertDataFunctionTest(jsonObjects); 

                return Json(new { result = "SERVER STATUS PASS." + DateTime.Now , datas = mDataFunctionTest.doInsertDataFunctionTest(jsonObjects) }, JsonRequestBehavior.AllowGet);
            }
            else {
                return Json(new { result = "SERVER STATUS FAIL." + DateTime.Now }, JsonRequestBehavior.AllowGet); ;
            }
            
            
        }

        public JsonResult doInsertDataFT_Driver() {
            try {
                
                if (!string.IsNullOrEmpty(Request["string"])) {
                    M_ActionDataFT_Driver mActionDataFT_Driver = new M_ActionDataFT_Driver();
                    string str = Request["string"];
                    return Json(mActionDataFT_Driver.doInsertDataFT_Driver(str.Replace("/", "-")), JsonRequestBehavior.AllowGet);
                }
                else {
                    return Json(SystemClass.returnResultJsonFailureReject(), JsonRequestBehavior.AllowGet);
                    //ViewBag.jsonData = ViewBag.jsonData = JsonConvert.SerializeObject(Json(SystemClass.returnResultJsonFailure()));
                }

            }
            catch (Exception ex) {
                Debug.WriteLine(ex);
                //ViewBag.jsonData = "FAILURE ==========>" + ex;
                //ViewBag.jsonData = JsonConvert.SerializeObject(Json(SystemClass.returnResultJsonFailure()));
                return Json(SystemClass.returnResultJsonFailure(), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult V_TrackDataFT_Driver() {
            M_ActionDataFT_Driver mActionDataFT_Driver = new M_ActionDataFT_Driver();
            ViewBag.jsonData = JsonConvert.SerializeObject(Json(mActionDataFT_Driver.loadDataInsertTrack()));
            return View();
        }
    }
}