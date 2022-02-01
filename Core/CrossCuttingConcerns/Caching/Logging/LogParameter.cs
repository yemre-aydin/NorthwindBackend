using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Caching.Logging
{ //logglanacak operasyondaki alanın parametreleri olacak
    public class LogParameter
    {
        public string Name { get; set; }
        public object Value { get; set; }

        public string Type { get; set; }

    }
}
