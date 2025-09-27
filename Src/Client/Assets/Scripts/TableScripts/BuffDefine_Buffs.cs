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
    /// BuffDefine_Buffs
    /// </summary>
    public class BuffDefine_Buffs : DataRowBase
    {
        private int m_Id = 0;

        /// <summary>
        /// 获取BuffDefine_Buffs表主键id
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
        /// 获取Icon。
        /// </summary>
        public string Icon
        {
            get;
            private set;
        }
        /// <summary>
        /// 获取Target。
        /// </summary>
        public string Target
        {
            get;
            private set;
        }
        /// <summary>
        /// 获取Trigger。
        /// </summary>
        public string Trigger
        {
            get;
            private set;
        }
        /// <summary>
        /// 获取Effect。
        /// </summary>
        public string Effect
        {
            get;
            private set;
        }
        /// <summary>
        /// 获取CD。
        /// </summary>
        public int CD
        {
            get;
            private set;
        }
        /// <summary>
        /// 获取Duration。
        /// </summary>
        public float Duration
        {
            get;
            private set;
        }
        /// <summary>
        /// 获取Interval。
        /// </summary>
        public float Interval
        {
            get;
            private set;
        }
        /// <summary>
        /// 获取AD。
        /// </summary>
        public float AD
        {
            get;
            private set;
        }
        /// <summary>
        /// 获取AP。
        /// </summary>
        public float AP
        {
            get;
            private set;
        }
        /// <summary>
        /// 获取ADFator。
        /// </summary>
        public float ADFator
        {
            get;
            private set;
        }
        /// <summary>
        /// 获取APFator。
        /// </summary>
        public float APFator
        {
            get;
            private set;
        }
        /// <summary>
        /// 获取DEFRatio。
        /// </summary>
        public float DEFRatio
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
            Icon = columnStrings[index++];
            Target = columnStrings[index++];
            Trigger = columnStrings[index++];
            Effect = columnStrings[index++];
            CD = int.Parse(columnStrings[index++]);
            Duration = float.Parse(columnStrings[index++]);
            Interval = float.Parse(columnStrings[index++]);
            AD = float.Parse(columnStrings[index++]);
            AP = float.Parse(columnStrings[index++]);
            ADFator = float.Parse(columnStrings[index++]);
            APFator = float.Parse(columnStrings[index++]);
            DEFRatio = float.Parse(columnStrings[index++]);

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
                    Icon = binaryReader.ReadString();
                    Target = binaryReader.ReadString();
                    Trigger = binaryReader.ReadString();
                    Effect = binaryReader.ReadString();
                    CD = binaryReader.Read7BitEncodedInt32();
                    Duration = binaryReader.ReadSingle();
                    Interval = binaryReader.ReadSingle();
                    AD = binaryReader.ReadSingle();
                    AP = binaryReader.ReadSingle();
                    ADFator = binaryReader.ReadSingle();
                    APFator = binaryReader.ReadSingle();
                    DEFRatio = binaryReader.ReadSingle();
                }
            }

            return true;
        }

    }
}
