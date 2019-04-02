using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using Web_LED.App_Class;

namespace WEB_LED.Models.DataManage {
    public class M_ActionDataFT_Driver {

        private ClassDataBase classDataBase = new ClassDataBase();


      


        public Object doInsertDataFT_Driver(string str) {

            String[] strings = str.Split('|');


            Dictionary<string, string> returnObject = new Dictionary<string, string>();


            if (strings.Length != 17) {
                returnObject.Add("result", "FAILURE |(pipe) NOT MATCH.");
                returnObject.Add("code", "ERROR");
                returnObject.Add("workOrder", "-");
                returnObject.Add("dateTime", DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"));
                returnObject.Add("ledNumber", "-");

            }
            else {
                Dictionary<string, string> dataMainFields = new Dictionary<string, string>();


                string code_no = strings[16].Trim();
                string po_no = strings[14].Trim(); 

                string dataId = classDataBase.nextNumber("id", "FT_Driver", "");
                dataMainFields.Add("id", dataId);
                dataMainFields.Add("po_no", po_no);
                dataMainFields.Add("code_no", code_no);
                dataMainFields.Add("string_data", str.Trim());
                dataMainFields.Add("date_create", SystemClass.getCurrentDateTimeInsert());
                classDataBase.insertData(dataMainFields, "FT_Driver");
          
                string mainDriverId = "";
                string whereMainDriver = "  code_no='" + code_no + "' AND po_no='" + po_no + "' AND date_create = '" + SystemClass.getDateNowForDB() + "'";
                mainDriverId = classDataBase.selectOnceData("FT_driver_main", "id", whereMainDriver + " ORDER BY id DESC ");


                Dictionary<string, string> dataMainDriverFields = new Dictionary<string, string>();
                if (mainDriverId.Equals("")) {

                    mainDriverId = classDataBase.nextNumber("id", "FT_driver_main", "");
                    dataMainDriverFields.Add("id" , mainDriverId);
                    dataMainDriverFields.Add("po_no", po_no);
                    dataMainDriverFields.Add("code_no", code_no);
                    dataMainDriverFields.Add("date_create", SystemClass.getDateNowForDB());
                    dataMainDriverFields.Add("driver_count", "1");

                    classDataBase.insertData(dataMainDriverFields, "FT_driver_main");

                }
                else {

                    string sqlMaxCode = @"  SELECT driver_count+1 AS driver_count
                                            FROM  FT_driver_main
                                            WHERE " + whereMainDriver;
                    DataRow dataRowCode = classDataBase.getDataRow(sqlMaxCode);
                    string driverCount = dataRowCode["driver_count"].ToString();
                    classDataBase.updateCommand("UPDATE FT_driver_main  SET driver_count='" + driverCount + "'   Where id=" + mainDriverId);
                }

                //##########################################################################     DATA DETAILS.
                Dictionary<string, string> dataDetailFields = new Dictionary<string, string>();
                DateTime dateTimeCreate = DateTime.ParseExact(strings[0], "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                string actionResult = (strings[12].TrimStart().TrimEnd().ToString().Equals("PASS") ? "1" : "0");

                dataDetailFields.Add("FT_driver_main_id", mainDriverId);
                //dataDetailFields.Add("create_datetime", SystemClass.getDateNowForDB());
                dataDetailFields.Add("create_datetime", SystemClass.getCurrentDateTimeInsert());
                dataDetailFields.Add("data_datetime", dateTimeCreate.ToString("yyyy-MM-dd HH:mm:ss"));
                dataDetailFields.Add("test_id", strings[1].Trim());
                dataDetailFields.Add("data_watt", strings[2].Trim());
                dataDetailFields.Add("data_pf", strings[3].Trim());
                dataDetailFields.Add("data_THDi", strings[4].Trim());
                dataDetailFields.Add("data_volt", strings[5].Trim());
                dataDetailFields.Add("data_Amp", strings[6].Trim());
                dataDetailFields.Add("data_THDv", strings[7].Trim());
                dataDetailFields.Add("data_PowerOutLed", strings[8].Trim());                
                dataDetailFields.Add("data_VLED", strings[9].Trim());
                dataDetailFields.Add("data_ILED", strings[10].Trim());
                dataDetailFields.Add("data_Efficiency", strings[11].Trim());
                dataDetailFields.Add("action_result", actionResult );
                dataDetailFields.Add("string_data", str);

                classDataBase.insertData(dataDetailFields, "FT_driver_main_detail");

                classDataBase.closeConnection(); 
                //##########################################################################     RETURN DATA.
                returnObject.Add("result", "SUCCESS");                
                returnObject.Add("po", strings[14]);
                returnObject.Add("code", strings[16]);
                returnObject.Add("dateTime", strings[0]);
                returnObject.Add("ledNumber", strings[1]);

            }

            return returnObject;

        }


        public Object P_KO_TestData() {

            string sql = "SELECT TOP 100 * FROM P_KO_TestData ORDER BY id DESC ;";
            DataTable dateTable = classDataBase.getDataTable(sql);
            List<Dictionary<string, string>> lists = new List<Dictionary<string, string>>();
            foreach (DataRow dataRow in dateTable.Rows) {
                Dictionary<string, string> data = new Dictionary<string, string>();
                data.Add("id", dataRow["id"].ToString());
                data.Add("date_time", dataRow["date_time"].ToString());
                data.Add("data_dim", dataRow["data_dim"].ToString());
                lists.Add(data);


            }

            classDataBase.closeConnection();

            return lists;

        }

        public Object loadDataInsertTrack() {
            string sql = "SELECT TOP 100 * FROM FT_Driver ORDER BY id DESC ;";
            DataTable dateTable = classDataBase.getDataTable(sql);
            List<Dictionary<string, string>> lists = new List<Dictionary<string, string>>();

            foreach (DataRow dataRow in dateTable.Rows) {
                Dictionary<string, string> data = new Dictionary<string, string>();
                data.Add("id", dataRow["id"].ToString());
                data.Add("po_no", dataRow["po_no"].ToString());
                data.Add("code_no", dataRow["code_no"].ToString());
                data.Add("date_create", dataRow["date_create"].ToString());
                data.Add("string_data", dataRow["string_data"].ToString());

                lists.Add(data);


            }

            classDataBase.closeConnection();

            return lists;
        }






    }
}