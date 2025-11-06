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
    /// TeleporterDefine_Elements
    /// </summary>
    public class TeleporterDefine_Elements : DataRowBase
    {
        private int m_Id = 0;

        /// <summary>
        /// 获取TeleporterDefine_Elements表主键id
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
        /// 获取LinkTo。
        /// </summary>
        public int LinkTo
        {
            get;
            private set;
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
        /// 获取Description。
        /// </summary>
        public string Description
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
            LinkTo = int.Parse(columnStrings[index++]);
            MapID = int.Parse(columnStrings[index++]);
            Description = columnStrings[index++];
            index++;

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
                    LinkTo = binaryReader.Read7BitEncodedInt32();
                    MapID = binaryReader.Read7BitEncodedInt32();
                    Description = binaryReader.ReadString();
                }
            }

            return true;
        }

    }
}
