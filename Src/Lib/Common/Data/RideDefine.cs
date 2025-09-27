using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillBridge.Message;

namespace Common.Data
{
    public class RideDefine
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Level { get; set; }
        public CHARACTER_CLASS LimitClass { get; set; }
        public string Icon { get; set; }
        public string Resource { get; set; }
    }
}
