using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Caching.Logging
{//log a ait detay bilgileri tutulacak

    //Framework logfornet ürünü üzerinden gerçekleştireceğiz 
    //yoğun olarak hatta en çok kullanılan olandır. ve iyi bir programdır.
    //logglama backend de çalışmalı.
    public class LogDetail
    {
        public string MethodName { get; set; }

        public List<LogParameter> LogParameters { get; set; }



    }
}
