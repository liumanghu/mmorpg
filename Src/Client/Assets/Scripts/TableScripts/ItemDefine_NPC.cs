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
    /// ItemDefine_NPC
    /// </summary>
    public class ItemDefine_NPC : DataRowBase
    {
        private int m_Id = 0;

        /// <summary>
        /// 获取ItemDefine_NPC表主键id
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
        /// 获取Description。
        /// </summary>
        public string Description
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
        /// 获取Category。
        /// </summary>
        public string Category
        {
            get;
            private set;
        }
        /// <summary>
        /// 获取Level。
        /// </summary>
        public int Level
        {
            get;
            private set;
        }
        /// <summary>
        /// 获取LimitClass。
        /// </summary>
        public string LimitClass
        {
            get;
            private set;
        }
        /// <summary>
        /// 获取CanUse。
        /// </summary>
        public bool CanUse
        {
            get;
            private set;
        }
        /// <summary>
        /// 获取UseCD。
        /// </summary>
        public float UseCD
        {
            get;
            private set;
        }
        /// <summary>
        /// 获取Price。
        /// </summary>
        public int Price
        {
            get;
            private set;
        }
        /// <summary>
        /// 获取SellPrice。
        /// </summary>
        public int SellPrice
        {
            get;
            private set;
        }
        /// <summary>
        /// 获取StackLimit。
        /// </summary>
        public int StackLimit
        {
            get;
            private set;
        }
        /// <summary>
        /// 获取Icon。
        /// </summary>
        public string Icon
        {
            get;
            private set;
        }
        /// <summary>
        /// 获取Function。
        /// </summary>
        public string Function
        {
            get;
            private set;
        }
        /// <summary>
        /// 获取Param。
        /// </summary>
        public int Param
        {
            get;
            private set;
        }
        /// <summary>
        /// 获取Params。
        /// </summary>
        public float[] Params
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
            Description = columnStrings[index++];
            Type = columnStrings[index++];
            Category = columnStrings[index++];
            Level = int.Parse(columnStrings[index++]);
            LimitClass = columnStrings[index++];
            CanUse = bool.Parse(columnStrings[index++]);
            UseCD = float.Parse(columnStrings[index++]);
            Price = int.Parse(columnStrings[index++]);
            SellPrice = int.Parse(columnStrings[index++]);
            StackLimit = int.Parse(columnStrings[index++]);
            Icon = columnStrings[index++];
            Function = columnStrings[index++];
            Param = int.Parse(columnStrings[index++]);
            Params = DataTableExtension.ParseArray(columnStrings[index++]);

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
                    Description = binaryReader.ReadString();
                    Type = binaryReader.ReadString();
                    Category = binaryReader.ReadString();
                    Level = binaryReader.Read7BitEncodedInt32();
                    LimitClass = binaryReader.ReadString();
                    CanUse = binaryReader.ReadBoolean();
                    UseCD = binaryReader.ReadSingle();
                    Price = binaryReader.Read7BitEncodedInt32();
                    SellPrice = binaryReader.Read7BitEncodedInt32();
                    StackLimit = binaryReader.Read7BitEncodedInt32();
                    Icon = binaryReader.ReadString();
                    Function = binaryReader.ReadString();
                    Param = binaryReader.Read7BitEncodedInt32();
                    Params = binaryReader.ReadArray();
                }
            }

            return true;
        }

    }
}
