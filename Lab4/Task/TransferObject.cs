using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab4
{
    public class TransferObject
    {
        string formString;

        int firstSelectValue;

        int secondSelectValue;

        public string FormData { get => formString; set => formString = value; }
        public int FirstSelectValue { get => firstSelectValue; set => firstSelectValue = value; }
        public int SecondSelectValue { get => secondSelectValue; set => secondSelectValue = value; }
    }
}
