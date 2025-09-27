using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using SkillBridge.Message;

namespace Common.Data
{
    public enum QuestType
    {
        [Description("主线")]
        Main,
        [Description("支线")]
        Branch,
        [Description("副本")]
        Copy
    }

    public enum QuestTargetType
    {
        None = 0,
        Kill = 1,
        Item = 2
    }

    public class QuestDefine
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public int LimitLevel { get; set; }
        public CHARACTER_CLASS LimitClass { get; set; }

        public int PreQuest { get; set; }

        public QuestType Type { get; set; }

        public int AcceptNPC { get; set;}
        public int SubmitNPC { get; set; }

        public string Overview { get; set; }
        public string Dialog { get; set; }
        public string DialogAccept { get; set; }
        public string DialogDeny { get; set; }
        public string DialogIncomplete { get; set; }
        public string DialogFinish { get; set; }
        public List<QuestTargetType> TargetTypes { get; set; }
        public List<int> Targets { get; set; }
        public List<int> TargetNums { get; set; }

        public int RewardGold { get; set; }
        public int RewardExp { get; set; }
        public List<int> RewardItems { get; set; }
        public List<int> RewardItemCounts { get; set; }
    }
}
