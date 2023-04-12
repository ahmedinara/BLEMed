using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSMediator.Core.Models
{
    public class EncriptedModel
    {
        public EncriptedModel(string CardNo)
        {
            cardNo = CardNo;
            encriptionKey = "1111111111111111";
        }
        public string cardNo { get; set; }
        public string encriptionKey { get; set; }

    }
}
