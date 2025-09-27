using GameFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace MMORPG.DataTable
{
    /// <summary>
    /// QuestDefine_Quest
    /// </summary>
    public class QuestDefine_Quest : DataRowBase
    {
        private int m_Id = 0;

        /// <summary>
        /// 获取QuestDefine_Quest表主键id
        /// </summary>
        public override int Id
        {
            get
            {
                return m_Id;
            }
        }

        /// <summary>
        /// 获取Name。
        /// </summary>
        public string Name
        {
            get;
            private set;
        }
        /// <summary>
        /// 获取LimitLevel。
        /// </summary>
        public int LimitLevel
        {
            get;
            private set;
        }
        /// <summary>
        /// 获取LimitClass。
        /// </summary>
        public int LimitClass
        {
            get;
            private set;
        }
        /// <summary>
        /// 获取PreQuest。
        /// </summary>
        public int PreQuest
        {
            get;
            private set;
        }
        /// <summary>
        /// 获取PostQuest。
        /// </summary>
        public int PostQuest
        {
            get;
            private set;
        }
        /// <summary>
        /// 获取Type。
        /// </summary>
        public string Type
        {
            get;
            private set;
        }
        /// <summary>
        /// 获取AcceptNPC。
        /// </summary>
        public int AcceptNPC
        {
            get;
            private set;
        }
        /// <summary>
        /// 获取SubmitNPC。
        /// </summary>
        public int SubmitNPC
        {
            get;
            private set;
        }
        /// <summary>
        /// 获取Overview。
        /// </summary>
        public string Overview
        {
            get;
            private set;
        }
        /// <summary>
        /// 获取Dialog。
        /// </summary>
        public string Dialog
        {
            get;
            private set;
        }
        /// <summary>
        /// 获取DialogAccept。
        /// </summary>
        public string DialogAccept
        {
            get;
            private set;
        }
        /// <summary>
        /// 获取DialogDeny。
        /// </summary>
        public string DialogDeny
        {
            get;
            private set;
        }
        /// <summary>
        /// 获取DialogIncomplete。
        /// </summary>
        public string DialogIncomplete
        {
            get;
            private set;
        }
        /// <summary>
        /// 获取DialogFinish。
        /// </summary>
        public string DialogFinish
        {
            get;
            private set;
        }
        /// <summary>
        /// 获取TargetTypes。
        /// </summary>
        public float[] TargetTypes
        {
            get;
            private set;
        }
        /// <summary>
        /// 获取Targets。
        /// </summary>
        public float[] Targets
        {
            get;
            private set;
        }
        /// <summary>
        /// 获取TargetNums。
        /// </summary>
        public float[] TargetNums
        {
            get;
            private set;
        }
        /// <summary>
        /// 获取RewardGold。
        /// </summary>
        public int RewardGold
        {
            get;
            private set;
        }
        /// <summary>
        /// 获取RewardExp。
        /// </summary>
        public int RewardExp
        {
            get;
            private set;
        }
        /// <summary>
        /// 获取RewardItems。
        /// </summary>
        public float[] RewardItems
        {
            get;
            private set;
        }
        /// <summary>
        /// 获取RewardItemCount。
        /// </summary>
        public float[] RewardItemCount
        {
            get;
            private set;
        }


        public override bool ParseDataRow(string dataRowString, object userData)
        {
            string[] columnStrings = dataRowString.Split(DataTableExtension.DataSplitSeparators);
            for (int i = 0; i < columnStrings.Length; i++)
            {
                columnStrings[i] = columnStrings[i].Trim(DataTableExtension.DataTrimSeparators);
            }

            int index = 0;
            index++;
            m_Id = int.Parse(columnStrings[index++]);
            Name = columnStrings[index++];
            LimitLevel = int.Parse(columnStrings[index++]);
            LimitClass = int.Parse(columnStrings[index++]);
            PreQuest = int.Parse(columnStrings[index++]);
            PostQuest = int.Parse(columnStrings[index++]);
            Type = columnStrings[index++];
            AcceptNPC = int.Parse(columnStrings[index++]);
            SubmitNPC = int.Parse(columnStrings[index++]);
            Overview = columnStrings[index++];
            Dialog = columnStrings[index++];
            DialogAccept = columnStrings[index++];
            DialogDeny = columnStrings[index++];
            DialogIncomplete = columnStrings[index++];
            DialogFinish = columnStrings[index++];
            TargetTypes = DataTableExtension.ParseArray(columnStrings[index++]);
            Targets = DataTableExtension.ParseArray(columnStrings[index++]);
            TargetNums = DataTableExtension.ParseArray(columnStrings[index++]);
            RewardGold = int.Parse(columnStrings[index++]);
            RewardExp = int.Parse(columnStrings[index++]);
            RewardItems = DataTableExtension.ParseArray(columnStrings[index++]);
            RewardItemCount = DataTableExtension.ParseArray(columnStrings[index++]);

            return true;
        }

        public override bool ParseDataRow(byte[] dataRowBytes, int startIndex, int length, object userData)
        {
            using (MemoryStream memoryStream = new MemoryStream(dataRowBytes, startIndex, length, false))
            {
                using (BinaryReader binaryReader = new BinaryReader(memoryStream, Encoding.UTF8))
                {
                    m_Id = binaryReader.Read7BitEncodedInt32();
                    Name = binaryReader.ReadString();
                    LimitLevel = binaryReader.Read7BitEncodedInt32();
                    LimitClass = binaryReader.Read7BitEncodedInt32();
                    PreQuest = binaryReader.Read7BitEncodedInt32();
                    PostQuest = binaryReader.Read7BitEncodedInt32();
                    Type = binaryReader.ReadString();
                    AcceptNPC = binaryReader.Read7BitEncodedInt32();
                    SubmitNPC = binaryReader.Read7BitEncodedInt32();
                    Overview = binaryReader.ReadString();
                    Dialog = binaryReader.ReadString();
                    DialogAccept = binaryReader.ReadString();
                    DialogDeny = binaryReader.ReadString();
                    DialogIncomplete = binaryReader.ReadString();
                    DialogFinish = binaryReader.ReadString();
                    TargetTypes = binaryReader.ReadArray();
                    Targets = binaryReader.ReadArray();
                    TargetNums = binaryReader.ReadArray();
                    RewardGold = binaryReader.Read7BitEncodedInt32();
                    RewardExp = binaryReader.Read7BitEncodedInt32();
                    RewardItems = binaryReader.ReadArray();
                    RewardItemCount = binaryReader.ReadArray();
                }
            }

            return true;
        }

    }
}
