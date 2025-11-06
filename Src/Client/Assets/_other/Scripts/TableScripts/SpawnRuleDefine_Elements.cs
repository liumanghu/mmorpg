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
    /// SpawnRuleDefine_Elements
    /// </summary>
    public class SpawnRuleDefine_Elements : DataRowBase
    {
        private int m_Id = 0;

        /// <summary>
        /// 获取SpawnRuleDefine_Elements表主键id
        /// </summary>
        public override int Id
        {
            get
            {
                return m_Id;
            }
        }

        /// <summary>
        /// 获取MapID。
        /// </summary>
        public int MapID
        {
            get;
            private set;
        }
        /// <summary>
        /// 获取SpawnMonID。
        /// </summary>
        public int SpawnMonID
        {
            get;
            private set;
        }
        /// <summary>
        /// 获取SpawnLevel。
        /// </summary>
        public int SpawnLevel
        {
            get;
            private set;
        }
        /// <summary>
        /// 获取SpawnPeriod。
        /// </summary>
        public int SpawnPeriod
        {
            get;
            private set;
        }
        /// <summary>
        /// 获取SpawnPoint。
        /// </summary>
        public int SpawnPoint
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
            index++;
            m_Id = int.Parse(columnStrings[index++]);
            MapID = int.Parse(columnStrings[index++]);
            SpawnMonID = int.Parse(columnStrings[index++]);
            SpawnLevel = int.Parse(columnStrings[index++]);
            SpawnPeriod = int.Parse(columnStrings[index++]);
            SpawnPoint = int.Parse(columnStrings[index++]);

            return true;
        }

        public override bool ParseDataRow(byte[] dataRowBytes, int startIndex, int length, object userData)
        {
            using (MemoryStream memoryStream = new MemoryStream(dataRowBytes, startIndex, length, false))
            {
                using (BinaryReader binaryReader = new BinaryReader(memoryStream, Encoding.UTF8))
                {
                    m_Id = binaryReader.Read7BitEncodedInt32();
                    MapID = binaryReader.Read7BitEncodedInt32();
                    SpawnMonID = binaryReader.Read7BitEncodedInt32();
                    SpawnLevel = binaryReader.Read7BitEncodedInt32();
                    SpawnPeriod = binaryReader.Read7BitEncodedInt32();
                    SpawnPoint = binaryReader.Read7BitEncodedInt32();
                }
            }

            return true;
        }

    }
}
