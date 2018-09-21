using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace WindowsTimeService
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            this.timer.Enabled = true;
            try
            {
                var configinterval = ConfigurationManager.AppSettings["interval"];
                if (configinterval != null)
                {
                    //定时器执行间隔
                    this.timer.Interval = Convert.ToDouble(configinterval.ToString());
                }
            }
            catch(Exception ex)
            {
                WriteErrorLog(ex);
            }
        }

        protected override void OnStop()
        {
            this.timer.Enabled = false;
        }

        protected void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            
            CheckFilepathIsExist();
            //记录服务调用时间
            WriteStartLog();
            try
            {
                //服务执行代码
                
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex);
            }
        }

        #region 公用属性
        /// <summary>
        /// 日志文件路径
        /// </summary>
        public string LogFilePath
        {
            get
            {
                var configvalue = ConfigurationManager.AppSettings["filepath"];
                string filepath = configvalue==null?"C:\\TimeServerLog/": configvalue.ToString();
                return filepath + DateTime.Today.ToString("yyyyMMdd") + ".txt";
            }
        }
        /// <summary>
        /// 日志记录
        /// </summary>
        public class LogRecord
        {
            public string Name { get; set; }
            public string Content { get; set; }
        }
        #endregion

        #region 服务基本函数
        /// <summary>
        /// 日志文件路径检查
        /// </summary>
        private void CheckFilepathIsExist()
        {
            if (!System.IO.File.Exists(LogFilePath))
            {
                System.IO.FileStream fsnew = System.IO.File.Create(LogFilePath);
                fsnew.Close();
            }
        }
        /// <summary>
        /// 记录服务调用时间
        /// </summary>
        private void WriteStartLog()
        {
            using (System.IO.FileStream fs = System.IO.File.Open(LogFilePath, System.IO.FileMode.Append, System.IO.FileAccess.Write))
            {
                using (System.IO.StreamWriter w = new System.IO.StreamWriter(fs))
                {
                    w.WriteLine("-------------------------------------------------------------");
                    w.WriteLine("调用服务时间：{0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                    w.WriteLine("-------------------------------------------------------------");
                    w.Flush();
                    w.Close();
                }
            }
        }
        /// <summary>
        /// 记录异常日志
        /// </summary>
        /// <param name="ex"></param>
        private void WriteErrorLog(Exception ex)
        {
            using (System.IO.FileStream fs = System.IO.File.Open(LogFilePath, System.IO.FileMode.Append, System.IO.FileAccess.Write))
            {
                using (System.IO.StreamWriter w = new System.IO.StreamWriter(fs))
                {
                    w.WriteLine("*************************************************************");
                    w.WriteLine("出错时间：{0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                    w.WriteLine("出错原因：{0}", ex.Message);
                    w.WriteLine("*************************************************************");
                    w.Flush();
                    w.Close();
                }
            }
        }
        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="logrecord"></param>
        private void WriteLog(LogRecord logrecord)
        {
            using (System.IO.FileStream fs = System.IO.File.Open(LogFilePath, System.IO.FileMode.Append, System.IO.FileAccess.Write))
            {
                using (System.IO.StreamWriter w = new System.IO.StreamWriter(fs))
                {
                    w.WriteLine("-------------------------------------------------------------");
                    w.WriteLine("{0}：{1}", logrecord.Name, logrecord.Content);
                    w.WriteLine("-------------------------------------------------------------");
                    w.Flush();
                    w.Close();
                }
            }
        }
        /// <summary>
        /// 记录纯文本
        /// </summary>
        /// <param name="text"></param>
        private void WriteLog(string text)
        {
            using (System.IO.FileStream fs = System.IO.File.Open(LogFilePath, System.IO.FileMode.Append, System.IO.FileAccess.Write))
            {
                using (System.IO.StreamWriter w = new System.IO.StreamWriter(fs))
                {
                    w.WriteLine("-------------------------------------------------------------");
                    w.WriteLine("{0}：{1}", DateTime.Now, text);
                    w.WriteLine("-------------------------------------------------------------");
                    w.Flush();
                    w.Close();
                }
            }
        }
        #endregion
    }
}
