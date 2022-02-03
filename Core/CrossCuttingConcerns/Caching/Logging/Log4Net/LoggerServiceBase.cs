﻿using log4net;
using log4net.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;

namespace Core.CrossCuttingConcerns.Caching.Logging.Log4Net
{//logger ı devreye sokucaz
    //logger service log işlemini yapacağımız servis olacak
    public class LoggerServiceBase
    {
        private ILog _log;
        public LoggerServiceBase(string name)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(File.OpenRead("log4net.config"));

            ILoggerRepository loggerRepository = LogManager.CreateRepository(Assembly.GetEntryAssembly(),
                typeof(log4net.Repository.Hierarchy.Hierarchy));
            log4net.Config.XmlConfigurator.Configure(loggerRepository, xmlDocument["log4net"]);

            _log = LogManager.GetLogger(loggerRepository.Name, name);
            


        }
        public bool IsInfoEnable => _log.IsInfoEnabled;//info açık mı ona bakıyor
        public bool IsDebugEnable => _log.IsDebugEnabled;//info açık mı ona bakıyor
        public bool IsWarnEnable => _log.IsWarnEnabled;//info açık mı ona bakıyor
        public bool IsFatalEnable => _log.IsFatalEnabled;//info açık mı ona bakıyor
        public bool IsErrorEnable => _log.IsErrorEnabled;//info açık mı ona bakıyor



        public void Info(object logMessage)
        {
            if (IsInfoEnable)
            {
                _log.Info(logMessage);
            }
            if (IsDebugEnable)
            {
                _log.Debug(logMessage);
            }
            if (IsWarnEnable)
            {
                _log.Warn(logMessage);
            }
            if (IsFatalEnable)
            {
                _log.Fatal(logMessage);
            }
            if (IsErrorEnable)
            {
                _log.Error(logMessage);
            }

        }

    }
}