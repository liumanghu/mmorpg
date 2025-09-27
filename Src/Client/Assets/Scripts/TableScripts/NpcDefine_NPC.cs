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
    /// NpcDefine_NPC
    /// </summary>
    public class NpcDefine_NPC : DataRowBase
    {
        private int m_Id = 0;

        /// <summary>
        /// 获取NpcDefine_NPC表主键id
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
        /// 获取Function。
        /// </summary>
        public string Function
        {
            get;
            private set;
        }
        /// <summary>
        /// 获取MapId。
        /// </summary>
        public int MapId
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
            Function = columnStrings[index++];
            MapId = int.Parse(columnStrings[index++]);
            Param = int.Parse(columnStrings[index++]);

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
                    Function = binaryReader.ReadString();
                    MapId = binaryReader.Read7BitEncodedInt32();
                    Param = binaryReader.Read7BitEncodedInt32();
                }
            }

            return true;
        }

    }
}
