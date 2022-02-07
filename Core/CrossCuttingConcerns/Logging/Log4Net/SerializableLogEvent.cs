
using log4net.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Caching.Logging.Log4Net
{
    [Serializable]//bu sınıfın serializable olması gerekiyor

    public class SerializableLogEvent
    {
            private LoggingEvent _loggingEvent;
        public SerializableLogEvent(LoggingEvent loggingEvent)
        {
            _loggingEvent = loggingEvent;
        }

        public object Message => _loggingEvent.MessageObject;


}
}
