using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LesWebService.App_Class {
    public class ConfigClass {

        public static String NEXT_NUMBER_START = "1";
        public static String NEXT_NUMBER_START_ZERO = "0";

        public static readonly string CONNECT_STRING_IWEB = "Server=192.168.9.6;Uid=Engineer2;PASSWORD=Engineer2;database=Engineer;Max Pool Size=400;Connect Timeout=600;";


        //public static readonly String URL_WEB_SEND_DATA_REALTIME_FT8 = "http://192.168.18.100:8088/Home/SendDataRealTime";  // FOR SERVER.
        //public static readonly String URL_WEB_SEND_DATA_REALTIME_FT8 = "http://localhost:64662/Home/SendDataRealTime";  // FOR SERVER.
        
        public static readonly String URL_WEB_SEND_DATA_REALTIME_FT8 = "http://192.168.18.100:8088/Home/SendDataRealTime";  // FOR SERVER.
        public static readonly String URL_WEB_GET_DATA_REALTIME_FT8 = "http://192.168.18.100:8080/DivisionPD2/getDataRealTimeFT8";  // FOR SERVER GET DATA.



    }
}