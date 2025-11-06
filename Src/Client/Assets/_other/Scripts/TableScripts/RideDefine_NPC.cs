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
    /// RideDefine_NPC
    /// </summary>
    public class RideDefine_NPC : DataRowBase
    {
        private int m_Id = 0;

        /// <summary>
        /// 获取RideDefine_NPC表主键id
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
        /// 获取Resource。
        /// </summary>
        public string Resource
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
            Level = int.Parse(columnStrings[index++]);
            LimitClass = columnStrings[index++];
            Resource = columnStrings[index++];
            Icon = columnStrings[index++];

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
                    Level = binaryReader.Read7BitEncodedInt32();
                    LimitClass = binaryReader.ReadString();
                    Resource = binaryReader.ReadString();
                    Icon = binaryReader.ReadString();
                }
            }

            return true;
        }

    }
}
