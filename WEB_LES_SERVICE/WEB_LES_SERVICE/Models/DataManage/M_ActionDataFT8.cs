using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Web_LED.App_Class;

namespace WEB_LED.Models.DataManage {
    public class M_ActionDataFT8 {


        private ClassDataBase classDataBase = new ClassDataBase();

        public Object doInsertDataFT8(string str) {


            //str = "7600462|WO1800363|15/06/2018 14:57:40|7600462WO1800363180307001191|0|0|0|0|0|0|0|0|0|NaN|FAIL|16.5|22.5|15";
            String[] strings = str.Split('|');

            Dictionary<string, string> returnObject = new Dictionary<string, string>();


            if (strings.Length!= 18) {
                returnObject.Add("result", "FAILURE");
                returnObject.Add("code", "ERROR");
                returnObject.Add("workOrder", "-");
                returnObject.Add("dateTime", DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"));
                returnObject.Add("ledNumber", "-");

            }
            else {


                Dictionary<string, string> dataMainFields = new Dictionary<string, string>();

                string mainDataId = classDataBase.nextNumber("id", "FT8_main_data", "");
                dataMainFields.Add("id", mainDataId);
                dataMainFields.Add("work_station_no", strings[1]);
                dataMainFields.Add("code_no", strings[0]);
                dataMainFields.Add("string_data", str);
                dataMainFields.Add("date_create", SystemClass.getCurrentDateTimeInsert());

                classDataBase.insertData(dataMainFields, "FT8_main_data");
                

                Dictionary<string, string> dataDetailFields = new Dictionary<string, string>();
         
                dataDetailFields.Add("FT8_main_data_id", "" );
                dataDetailFields.Add("string_test", str );
                dataDetailFields.Add("test_id", strings[0]);
                dataDetailFields.Add("data_watt", strings[0]);
                dataDetailFields.Add("data_PF", strings[0]);
                dataDetailFields.Add("data_THDi", strings[0]);
                dataDetailFields.Add("data_volt", strings[0]);
                dataDetailFields.Add("data_mA", strings[0]);
                dataDetailFields.Add("data_THDv", strings[0]);
                dataDetailFields.Add("power_out_watt", strings[0]);
                dataDetailFields.Add("VLED", strings[0]);
                dataDetailFields.Add("ILED", strings[0]);
                dataDetailFields.Add("Efficiency", strings[0]);
                dataDetailFields.Add("ActionResult", strings[0]);
                dataDetailFields.Add("LowWatt", strings[0]);
                dataDetailFields.Add("HighWatt", strings[0]);
                dataDetailFields.Add("MaxTHDi", strings[0]);

                //##########################################################################     RETURN DATA.
                returnObject.Add("result", "SUCCESS");
                returnObject.Add("code", strings[0]);
                returnObject.Add("workOrder", strings[1]);
                returnObject.Add("dateTime", strings[2]);
                returnObject.Add("ledNumber", strings[3]);

            }


            

            return returnObject;
        }




        public Object loadDataInsertTrack() {
            string sql = "SELECT TOP 100 * FROM FT8_main_data ORDER BY id DESC ;";
            DataTable dateTable = classDataBase.getDataTable(sql);
            List<Dictionary<string, string>> lists = new List<Dictionary<string, string>>();  

            foreach (DataRow dataRow in dateTable.Rows) {
                Dictionary<string, string> data = new Dictionary<string, string>();
                data.Add("id", dataRow["id"].ToString());
                data.Add("work_station_no", dataRow["work_station_no"].ToString() );
                data.Add("code_no", dataRow["code_no"].ToString());
                data.Add("date_create", dataRow["date_create"].ToString());
                data.Add("string_data", dataRow["string_data"].ToString());

                lists.Add(data); 


            }


            return lists; 
        }




    }
}