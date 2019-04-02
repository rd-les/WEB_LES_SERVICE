using LesWebService.App_Class;
using LesWebService.Models.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Web_LED.App_Class;

namespace LesWebService.Services.Service {
    public class WorkStationIweb {


        ClassDataBase classDataBase = new ClassDataBase(ConfigClass.CONNECT_STRING_IWEB);
        public Object getDataWorkStationIweb() {

            string sql = "SELECT * FROM LES_WorkOrder WHERE WO_NO='WO1802680' ";
            DataRow dataRow = classDataBase.getDataRow(sql);

            I_workStationIweb iWorkStationIweb = new I_workStationIweb();

            iWorkStationIweb.workStationId = dataRow["WO_NO"].ToString();
            iWorkStationIweb.workStationStatus = dataRow["WO_Status"].ToString();
            iWorkStationIweb.itemCodeId = dataRow["Item_ID"].ToString();
            iWorkStationIweb.itemCodeDetail = dataRow["Item_Name"].ToString();

            return iWorkStationIweb;
        }



        public Object getDataWorkStationIwebReleased() {

            string sql = "SELECT * FROM LES_WorkOrder WHERE WO_Status='Released' ";
            DataTable dataTable = classDataBase.getDataTable(sql);

            List<I_workStationIweb> lists = new List<I_workStationIweb>();
            foreach (DataRow dataRow in dataTable.Rows) {
                I_workStationIweb iWorkStationIweb = new I_workStationIweb();
                iWorkStationIweb.workStationId = dataRow["WO_NO"].ToString();
                iWorkStationIweb.workStationStatus = dataRow["WO_Status"].ToString();
                iWorkStationIweb.itemCodeId = dataRow["Item_ID"].ToString();
                iWorkStationIweb.itemCodeDetail = dataRow["Item_Name"].ToString();

                lists.Add(iWorkStationIweb);
            }

            return lists;
        }

        public Object getAutoComplateWorkStationIwebReleased() {
            string sql = "SELECT REPLACE(WO_NO , 'WO' , '') AS WO_NO FROM LES_WorkOrder WHERE WO_Status='Released'  ORDER BY WO_NO DESC ";
            DataTable dataTable = classDataBase.getDataTable(sql);

            List<Object> lists = new List<Object>();
            foreach (DataRow dataRow in dataTable.Rows) {
                //I_workStationIweb iWorkStationIweb = new I_workStationIweb();
                //iWorkStationIweb.workStationId = dataRow["WO_NO"].ToString();               
                lists.Add(dataRow["WO_NO"]);
            }

            return lists;

        }

        public Object getWorkStationDetail(string workStationId) {

            string sql = "SELECT TOP 1 * FROM LES_WorkOrder WHERE WO_NO = '"+ workStationId + "' ORDER BY WO_NO DESC ";
            DataRow dataRow = classDataBase.getDataRow(sql);
            Dictionary<string,string> dataReturn = new Dictionary<string, string>();

            dataReturn.Add("workStationId" , dataRow["WO_NO"].ToString());
            dataReturn.Add("codeNo", dataRow["Item_ID"].ToString());
            dataReturn.Add("workStationItem", dataRow["Req_Qty"].ToString());
            dataReturn.Add("workStationStatus", dataRow["WO_Status"].ToString());

            return dataReturn; 
        }
        






    }
}