using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Caching.Logging.Log4Net.Loggers
{//veri tabanına yapacağımız log olacak
    public class DatabaseLogger:LoggerServiceBase
    {

        public DatabaseLogger():base("DatabaseLogger")
        {
            
        }
    }
}
