using LesWebService.App_Class;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using Web_LED.App_Class;

namespace WEB_LES_SERVICE.Models.DataManage {
    public class M_DataFunctionTest {



        //private ClassDataBase classDataBase = new ClassDataBase();
        private string MAIN_TABLE_NAME = "FT8_main_data";


        //private ClassDataBase classDataBase = new ClassDataBase();

        public Object doInsertDataFunctionTest_T8(F_DataFunctionTest_T8 f_DataFunctionTest_T8) {
            

            ClassDataBase classDataBase = new ClassDataBase();
            string jsonStr = JsonConvert.SerializeObject(f_DataFunctionTest_T8); ;
            //Product deserializedProduct = JsonConvert.DeserializeObject<Product>(json);
            f_DataFunctionTest_T8.work_station_no = "WO"+f_DataFunctionTest_T8.work_station_no;

            Dictionary<string, string> returnObject = new Dictionary<string, string>();
            Dictionary<string, string> dataMainFields = new Dictionary<string, string>();
            string mainDataId = "";
            string whereMainData = "  code_no='" + f_DataFunctionTest_T8.code_no + "' AND work_station_no='" + f_DataFunctionTest_T8.work_station_no + "' AND date_create = '" + SystemClass.getDateNowForDB() + "'";
            mainDataId = classDataBase.selectOnceData(MAIN_TABLE_NAME, "id", whereMainData + " ORDER BY id DESC ");

            string sqlLastWo = @"SELECT TOP 1
                                        work_station_no
                                     FROM FT8_main_data
                                     ORDER BY id DESC";
            DataRow dataRowLastWo = classDataBase.getDataRow(sqlLastWo);


            string code_no = f_DataFunctionTest_T8.code_no;
            string work_station_no = f_DataFunctionTest_T8.work_station_no;
            string led_count = "1";
            string led_count_real = "1";




            string led_no = f_DataFunctionTest_T8.led_no;

            if (mainDataId.Equals("") || !dataRowLastWo["work_station_no"].ToString().Equals(f_DataFunctionTest_T8.work_station_no) ) {
                mainDataId = classDataBase.nextNumber("id", "FT8_main_data", "");
                dataMainFields.Add("id", mainDataId);
                dataMainFields.Add("work_station_no", f_DataFunctionTest_T8.work_station_no);
                dataMainFields.Add("code_no", f_DataFunctionTest_T8.code_no);
                dataMainFields.Add("date_create", SystemClass.getDateNowForDB());
                dataMainFields.Add("work_station_count", "1");
                dataMainFields.Add("work_station_count_real", "1");

                classDataBase.insertData(dataMainFields, MAIN_TABLE_NAME);

            }
            else {
                
                classDataBase.updateCommand("UPDATE " + MAIN_TABLE_NAME + " SET last_data_string='" + jsonStr + "' Where id=" + mainDataId);

                string sqlMaxWorkStation = @"   SELECT 
                                                        MAX(work_station_count)+1 AS work_station_count
                                                        ,  MAX(work_station_count_real)+1 AS  work_station_count_real
                                                    FROM " + MAIN_TABLE_NAME + @"
                                                    WHERE " + whereMainData;
                DataRow dataRow = classDataBase.getDataRow(sqlMaxWorkStation);


                string sqlCountLedNo = @"SELECT COUNT(id)  AS COUNT_LED_NO
                                            FROM FT8_main_data_detail
                                            WHERE led_no = '" + led_no + @"' 
                                            AND FT8_main_data_id = " + mainDataId;
                DataRow dataRowCountLedNo = classDataBase.getDataRow(sqlCountLedNo);
                string sqlUpdateLedNo = "";
                if (int.Parse(dataRowCountLedNo["COUNT_LED_NO"].ToString()) == 0) {

                    sqlUpdateLedNo = " , work_station_count_real=" + dataRow["work_station_count_real"].ToString();
                }
                string sqlCountAllWorkStation = @"SELECT 
                                                    SUM(work_station_count_real) AS COUNT_ALL_WORKSTATION
                                                    FROM[dbo].[FT8_main_data]
                                                    WHERE work_station_no = '" + work_station_no + "'";
                DataRow dataRowCountWorkStaion = classDataBase.getDataRow(sqlCountAllWorkStation);
                led_count_real = dataRowCountWorkStaion["COUNT_ALL_WORKSTATION"].ToString();


                led_count = dataRow["work_station_count"].ToString();
                classDataBase.updateCommand("UPDATE " + MAIN_TABLE_NAME + " SET work_station_count='" + led_count + "' " + sqlUpdateLedNo + " Where id=" + mainDataId);
            }

            Dictionary<string, string> dataDetailFields = new Dictionary<string, string>();
            DateTime dateTimeCreate = DateTime.ParseExact(f_DataFunctionTest_T8.data_datetime, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            //string actionResult = f_DataFunctionTest_T8.ActionResult;
            string actionResult = (f_DataFunctionTest_T8.ActionResult.TrimStart().TrimEnd().ToString().Equals("PASS") ? "1" : "0");

            dataDetailFields.Add("FT8_main_data_id", mainDataId);
            dataDetailFields.Add("string_test", jsonStr);
            dataDetailFields.Add("led_no", led_no);
            dataDetailFields.Add("data_watt", f_DataFunctionTest_T8.data_watt);
            dataDetailFields.Add("data_PF", f_DataFunctionTest_T8.data_PF);
            dataDetailFields.Add("data_THDi", f_DataFunctionTest_T8.data_THDi);
            dataDetailFields.Add("data_volt", f_DataFunctionTest_T8.data_volt);
            //dataDetailFields.Add("data_mA", f_DataFunctionTest_T8.);
            dataDetailFields.Add("data_THDv", f_DataFunctionTest_T8.data_THDv);
            dataDetailFields.Add("power_out_watt", f_DataFunctionTest_T8.power_out_watt);
            dataDetailFields.Add("VLED", f_DataFunctionTest_T8.VLED);
            dataDetailFields.Add("ILED", f_DataFunctionTest_T8.ILED);
            dataDetailFields.Add("Efficiency", f_DataFunctionTest_T8.Efficiency);
            dataDetailFields.Add("ActionResult", (actionResult));
            dataDetailFields.Add("LowWatt", f_DataFunctionTest_T8.LowWatt);
            dataDetailFields.Add("HighWatt", f_DataFunctionTest_T8.HighWatt);
            dataDetailFields.Add("MaxTHDi", f_DataFunctionTest_T8.MaxTHDi);


            dataDetailFields.Add("data_datetime", dateTimeCreate.ToString("yyyy-MM-dd HH:mm:ss"));
            dataDetailFields.Add("date_create", SystemClass.getCurrentDateTimeInsert());
            dataDetailFields.Add("led_count", led_count);


            if (f_DataFunctionTest_T8.led_no.Length >= 20) {
                string sqlCountLedNo = "SELECT    COUNT(led_no) AS led_count_no FROM  FT8_main_data_detail WHERE    led_no = '" + f_DataFunctionTest_T8.led_no + "'";
                DataRow rowCountLed = classDataBase.getDataRow(sqlCountLedNo);
                int led_count_no = int.Parse(rowCountLed["led_count_no"].ToString()) + 1 ;
                if (led_count_no >= 250) {led_count_no = 250;}

                dataDetailFields.Add("led_count_no", led_count_no.ToString()  )  ;
            }
            classDataBase.insertData(dataDetailFields, "FT8_main_data_detail");

            Dictionary<string, string> dataParams = new Dictionary<string, string>();
            dataParams.Add("@workstation_no", work_station_no);
            //DataTable dataTableProcedure = classDataBase.getDataTableProcedure("sp_getTimeDiffFT8", dataParams);
            DataRow dataRowProcedure = classDataBase.getDataRowProcedure("sp_getTimeDiffFT8", dataParams);
            string ledSec = ((int)dataRowProcedure["DATEDIFF_HOUR"]).ToString("00.##") + ":" + ((int)dataRowProcedure["DATEDIFF_MINUTE"]).ToString("00.##") + ":" + ((int)dataRowProcedure["DATEDIFF_SECOND"]).ToString("00.##");

            //##########################################################################     RETURN DATA.
            returnObject.Add("result", "SUCCESS");
            returnObject.Add("code", f_DataFunctionTest_T8.code_no);
            returnObject.Add("workOrder", f_DataFunctionTest_T8.work_station_no);
            returnObject.Add("dateTime", f_DataFunctionTest_T8.data_datetime);
            returnObject.Add("ledNumber", f_DataFunctionTest_T8.led_no);

                       
            string sURL = ConfigClass.URL_WEB_SEND_DATA_REALTIME_FT8 ;
            sURL += "?dataWatt=" + f_DataFunctionTest_T8.data_watt.Trim() + "&dataResult="+ actionResult;
            WebRequest wrGETURL;
            wrGETURL = WebRequest.Create(sURL);

            Stream objStream;
            objStream = wrGETURL.GetResponse().GetResponseStream();


            string sURL2 = ConfigClass.URL_WEB_GET_DATA_REALTIME_FT8; 
            //(ledCount, result , watt , dataPF, dataTHDi ) 
            sURL2 += "?ledCount="+ led_count_real + "&dataWatt=" + f_DataFunctionTest_T8.data_watt+ "&dataPF="+ f_DataFunctionTest_T8.data_PF + "&dataTHDi="+ f_DataFunctionTest_T8.data_THDi + "&dataResult=" + actionResult+ "&led_no="+ f_DataFunctionTest_T8.led_no.Trim() + "&lastSec=" + ledSec;
            WebRequest wrGETURL2;
            wrGETURL2 = WebRequest.Create(sURL2);

            Stream objStream2;
            objStream2 = wrGETURL2.GetResponse().GetResponseStream();
             
            classDataBase.closeConnection();

            return JsonConvert.DeserializeObject<F_DataFunctionTest_T8>(jsonStr);
        }

        public Object doInsertDataFunctionTest(F_DataFunctionTest fDataFunctionTest) {

            ClassDataBase classDataBase = new ClassDataBase();

            string mainPd3Id = "";
            string whereMainPd3Id = "  code_no='" + fDataFunctionTest.code_no + "' AND work_station_no ='" + fDataFunctionTest.wo_no + "' AND date_create = '" + SystemClass.getDateNowForDB() + "'";
            mainPd3Id = classDataBase.selectOnceData("FT_PD3_main", "id", whereMainPd3Id + " ORDER BY id DESC ");

            string pd3ConfigTypeId = classDataBase.selectOnceData("pd3_config_type", "id", "codex='" + fDataFunctionTest.codex+"'");

            Dictionary<string, string> dataMainFields = new Dictionary<string, string>();
            string workStationCount = "1"; 
            //dataMainFields.Add("id", "" );
            if (mainPd3Id.Equals("")) {

                mainPd3Id = classDataBase.nextNumber("id", "FT_PD3_main", "");

                dataMainFields.Add("id", mainPd3Id);
                dataMainFields.Add("pd3_config_type_id", pd3ConfigTypeId);
                dataMainFields.Add("date_create", SystemClass.getCurrentDateTimeInsert());
                dataMainFields.Add("work_station_no", fDataFunctionTest.wo_no);
                dataMainFields.Add("code_no", fDataFunctionTest.code_no);      
                dataMainFields.Add("work_station_count", workStationCount );
                

                classDataBase.insertData(dataMainFields, "FT_PD3_main");

            }
            else {

                string sqlMaxCode = @"  SELECT work_station_count+1 AS work_station_count
                                            FROM  FT_PD3_main
                                            WHERE " + whereMainPd3Id;
                DataRow dataRowCode = classDataBase.getDataRow(sqlMaxCode);
                workStationCount = dataRowCode["work_station_count"].ToString();
                classDataBase.updateCommand("UPDATE FT_PD3_main  SET work_station_count='" + workStationCount + "'   Where id=" + mainPd3Id);
            }

            //Debug.WriteLine(fDataFunctionTest);
            //##########################################################################     DATA DETAILS.
            Dictionary<string, string> dataDetailFields = new Dictionary<string, string>();            

            dataDetailFields.Add("FT_PD3_datas_id", mainPd3Id);
            dataDetailFields.Add("lot_no", fDataFunctionTest.lot_no);
            dataDetailFields.Add("led_no", fDataFunctionTest.led_no);
            dataDetailFields.Add("dataVolt", fDataFunctionTest.dataVolt);
            dataDetailFields.Add("dataCurrent", fDataFunctionTest.dataCurrent);
            dataDetailFields.Add("dataPower", fDataFunctionTest.dataPower);
            dataDetailFields.Add("dataResult", fDataFunctionTest.dataResult);
            dataDetailFields.Add("pairResult", fDataFunctionTest.pairResult);
            dataDetailFields.Add("create_datetime", SystemClass.getDateNowForDB());            
            dataDetailFields.Add("data_datetime", fDataFunctionTest.dateTime );
            dataDetailFields.Add("work_station_count" , workStationCount); 

            classDataBase.insertData(dataDetailFields, "FT_PD3_main_detail");

            string jsonStr = JsonConvert.SerializeObject(fDataFunctionTest); ;
            //Product deserializedProduct = JsonConvert.DeserializeObject<Product>(json);
            return JsonConvert.DeserializeObject<F_DataFunctionTest>(jsonStr); 
        }     
        

        public Object getDataPd3LedType(string ledSlotTypeCode) {

            ClassDataBase classDataBase = new ClassDataBase();

            string ledTypeSlotId = classDataBase.selectOnceData("led_type_slot", "led_type_slot_id", " codex= '"+ledSlotTypeCode+"' ");

            if (!ledTypeSlotId.Equals("") ) {
                string sql = @"SELECT * FROM  pd3_config_type TB1 Where  TB1.led_type_slot_id = " + ledTypeSlotId + " AND led_type_usable  = 1 ;";
                DataTable dataTable = classDataBase.getDataTable(sql);

                List<F_DataPd3LedType> lists = new List<F_DataPd3LedType>(); 

                foreach (DataRow dataRow in dataTable.Rows) {
                    F_DataPd3LedType f_DataPd3LedType = new F_DataPd3LedType();
                    f_DataPd3LedType.codex = dataRow["codex"].ToString();
                    f_DataPd3LedType.led_type_name = dataRow["led_type_name"].ToString();
                    f_DataPd3LedType.code_no = dataRow["code_no"].ToString();
                    lists.Add(f_DataPd3LedType);
                }

                return lists; 


            }
            else {
                return new { result = " ledSlotTypeCode : NOT FOUND !!!." };
            }
            

        }









    }

    public class F_DataPd3LedType {

        //public string id { get; set; }
        public string codex { get; set; }
        public string led_type_name { get; set; }
        public string code_no { get; set; }

        /*
        public string led_type_slot_id { get; set; }
        public string w_min { get; set; }
        public string w_max { get; set; }
        public string pf_min { get; set; }
        public string pf_max { get; set; }
        public string led_type_usable { get; set; }
       */

       
    }



    public class F_DataFunctionTest_T8 {

        public string led_no { get; set; }
        public string code_no { get; set; }
        public string work_station_no { get; set; }
        public string data_watt { get; set; }
        public string data_PF { get; set; }
        public string data_THDi { get; set; }
        public string data_volt { get; set; }
        public string data_THDv { get; set; }
        public string power_out_watt { get; set; }
        public string VLED { get; set; }
        public string ILED { get; set; }
        public string Efficiency { get; set; }
        public string ActionResult { get; set; }
        public string LowWatt { get; set; }
        public string HighWatt { get; set; }
        public string MaxTHDi { get; set; }
        public string data_datetime { get; set; }

    }

    public class F_DataFunctionTest {

        public string codex { get; set; }
        public string dateTime { get; set; }
        public string code_no { get; set; }
        public string wo_no { get; set; }
        public string dataWatt { get; set; }
        public string lot_no { get; set; }
        public string led_no { get; set; }
        public string dataVolt { get; set; }
        public string dataCurrent { get; set; }
        public string dataPower { get; set; }
        public string dataResult { get; set; }
        public string pairResult { get; set; }
        



    }
}