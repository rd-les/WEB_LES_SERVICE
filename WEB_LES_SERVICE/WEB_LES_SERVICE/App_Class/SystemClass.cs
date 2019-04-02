using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;

namespace Web_LED.App_Class {
    public class SystemClass {

        public static readonly String MS_SUCCESS = "SUCCESS";
        public static readonly String MS_FAILURE = "FAILURE";

        


        //public static readonly String PATH_UPLOAD = "~/Uploads";
        //public static readonly String PATH_UPLOAD_REGISTER = "RegisterDocs";
        //public static readonly String PATH_UPLOAD_TEMP = "Temp";

        //public static readonly String PATH_REPORT = "~/Report";
        //public static readonly String PATH_REPORT_TEMP = "~/Report_TEMP";




        public static Object returnResultJsonSuccess() {
            return new { result = MS_SUCCESS };
        }

        public static Object returnResultJsonSuccess(Object obj) {
            return new { result = MS_SUCCESS, resultData = obj };
        }

        public static Object returnResultJsonFailure() {
            return new { result = MS_FAILURE };
        }


        public static Object returnResultJsonFailureReject() {
            return new { result = "SERVER REJECT."+DateTime.Now };
        }


        public static Object returnResultJsonFailureTestDim() {
            Random random = new Random();
            int  dimInt = random.Next(20, 100); 
            ClassDataBase classDB = new ClassDataBase();


            string sql = "INSERT INTO P_KO_TestData  (date_time , data_dim )  VALUES  ('" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' , "+ dimInt + ")  "; 
            classDB.insertCommand(sql);

            classDB.closeConnection(); 

            return new { result = MS_FAILURE , dim = dimInt };
        }

        public static Object returnResultJsonFailure(string messageText) {
            return new { result = MS_FAILURE, message = messageText };
        }

        public static String getDateNow(string dateFormat) {
            string dateStr = SystemClass.getDateNow();
            DateTime dt = DateTime.ParseExact(dateStr, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            return dt.ToString(dateFormat);
        }
        public static String getDateNow() {
            //Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-GB");
            DateTime dateNow = DateTime.Now;
            return dateNow.ToString();
        }

        public static String getDateNowForDB() {
            DateTime dateNow = DateTime.Now;
            return dateNow.ToString("yyyy-MM-dd");
        }


        public static String convertDateFormat(string dateStr, string dateFormat, string dateFormatReturn) {

            /*
             * Example Url -> http://msdn.microsoft.com/en-us/library/8kb3ddd4%28v=vs.110%29.aspx
             * d -> 1 , 2
             * dd -> 01 , 02
             * ddd -> Mon , Fri
             * dddd -> Monday , Friday
             * MM -> 01 , 11    // MONTH
             * MMM -> Jan , Feb // MONTH
             * MMMM -> June     // MONTH
             * yy -> 12 , 13
             * yyyy -> 2014
             * hh -> AM , PM
             * HH -> 01 , 23
             * mm -> 01 , 02   // MINUTE
             * ss -> 09
             * 
             */
            if (!string.IsNullOrEmpty(dateStr)) {
                Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-GB");
                DateTime dt = DateTime.ParseExact(dateStr, dateFormat, CultureInfo.InvariantCulture);
                return dt.ToString(dateFormatReturn);
            }
            else {
                return "";
            }
        }

        public static string returnValueHyphen(object value) {
            if (value == null) {
                return "-";
            }
            else {
                return returnValueHyphen(value.ToString());
            }
        }

        public static string returnValueHyphen(string value) {
            if (isEmpty(value))
                return "-";
            else
                return value;
        }

        public static string returnValue(object value) {
            if (value == null) {
                return "";
            }
            else {
                return returnValue(value.ToString());
            }
        }

        public static string returnValue(string value) {
            if (isEmpty(value))
                return "";
            else
                return value;
        }
        public static string returnValueZero(string value) {
            if (isEmpty(value))
                return "0";
            else
                return value;
        }

        public static Boolean isEmpty(string value) {
            if (string.IsNullOrEmpty(value) || value.Equals("")) {
                return true;
            }
            return false;
        }

        public static string getCurrentDateTimeInsert() {
            string date = "";
            string timeStamp = "";
            try {
                date = getDateNow("dd/MM/yyyy");
                timeStamp = getDateNow("HH:mm:ss");
            }
            catch (Exception ex) {
                date = DateTime.Now.ToString("dd/MM/yyyy");
                timeStamp = DateTime.Now.ToString("HH:mm:ss");
                Console.WriteLine(ex); 
            }

            string result = "";
            if (!string.IsNullOrEmpty(date)) {
                string[] arr = date.Split('/');
                string year = arr[2].Substring(0, 4);
                result = year + "-" + arr[1] + "-" + arr[0] + " " + timeStamp;
                return result;
            }
            return null;
        }

        public static string generateString(int length) {
            Random random = new Random(); 
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }


        //public static bool isInBetween(this int number, int lower, int upper, bool inclusive = false) {
        //    return inclusive ? lower <= number && number <= upper : lower < number && number < upper;
        //}
        public static bool isInBetween(float number, float lower, float upper, bool inclusive = false) {
            return inclusive 
                    ? lower <= number && number <= upper 
                    : lower <= number && number <= upper;
        }

        /*

        public static bool IsBetween<T>(this T item, T start, T end) {
            return Comparer<T>.Default.Compare(item, start) >= 0
                && Comparer<T>.Default.Compare(item, end) <= 0;
        }

    */


    }
}