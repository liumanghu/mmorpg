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
    /// StoryDefine_Elements
    /// </summary>
    public class StoryDefine_Elements : DataRowBase
    {
        private int m_Id = 0;

        /// <summary>
        /// 获取StoryDefine_Elements表主键id
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
        /// 获取SubType。
        /// </summary>
        public string SubType
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
        /// 获取LimitTime。
        /// </summary>
        public int LimitTime
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
        /// 获取Quest。
        /// </summary>
        public float[] Quest
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
            SubType = columnStrings[index++];
            MapId = int.Parse(columnStrings[index++]);
            LimitTime = int.Parse(columnStrings[index++]);
            PreQuest = int.Parse(columnStrings[index++]);
            Quest = DataTableExtension.ParseArray(columnStrings[index++]);

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
                    SubType = binaryReader.ReadString();
                    MapId = binaryReader.Read7BitEncodedInt32();
                    LimitTime = binaryReader.Read7BitEncodedInt32();
                    PreQuest = binaryReader.Read7BitEncodedInt32();
                    Quest = binaryReader.ReadArray();
                }
            }

            return true;
        }

    }
}
