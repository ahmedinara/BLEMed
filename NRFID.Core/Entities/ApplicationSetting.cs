﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSMediator.Core.Entities
{
   public class ApplicationSetting
    {
        
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public int GroupId { get; set; }
        public ApplicationGroupSetting applicationGroupSetting{ get; set; }
    }
}
