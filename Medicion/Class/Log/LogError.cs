using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Reflection;

namespace Medicion.Class.LogError
{
   
    public class LogErrorMedicion :PropertiesLogError
    {
        private static string m_exePath = string.Empty;
        

        public  void LogWrite()
        {
            String LogError = "LogError.Log";
            m_exePath = HttpRuntime.AppDomainAppPath + "LOG";
            if (!File.Exists(m_exePath + "\\" + LogError))
                File.Create(m_exePath + "\\" + LogError);

            try
            {
                using (StreamWriter w = File.AppendText(m_exePath + "\\" + LogError))
                    AppendLog(logMessage, w, logModule);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private static void AppendLog(string logMessage, TextWriter txtWriter, string logModule)
        {
            try
            {
                txtWriter.Write("\r\nLog Entry : ");
                txtWriter.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
                txtWriter.WriteLine("  : Module: ", logModule);
                txtWriter.WriteLine("  :{0}", logMessage);
                txtWriter.WriteLine("-------------------------------");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

}