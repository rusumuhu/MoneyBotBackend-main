using System;

namespace MoneyBotBackend.Models.Dto
{
    public class MoneyOperation
    {
        public double Sum { get; set; }
        public string Operation { get; set; }
        public DateTime DateTime { get; set; }
    }
}
