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
    /// MapDefine_Elements
    /// </summary>
    public class MapDefine_Elements : DataRowBase
    {
        private int m_Id = 0;

        /// <summary>
        /// 获取MapDefine_Elements表主键id
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
        /// 获取Type。
        /// </summary>
        public string Type
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
        /// 获取PKMode。
        /// </summary>
        public bool PKMode
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
        /// 获取MiniMap。
        /// </summary>
        public string MiniMap
        {
            get;
            private set;
        }
        /// <summary>
        /// 获取Music。
        /// </summary>
        public string Music
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
            Type = columnStrings[index++];
            SubType = columnStrings[index++];
            PKMode = bool.Parse(columnStrings[index++]);
            Resource = columnStrings[index++];
            MiniMap = columnStrings[index++];
            Music = columnStrings[index++];
            Description = columnStrings[index++];

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
                    Type = binaryReader.ReadString();
                    SubType = binaryReader.ReadString();
                    PKMode = binaryReader.ReadBoolean();
                    Resource = binaryReader.ReadString();
                    MiniMap = binaryReader.ReadString();
                    Music = binaryReader.ReadString();
                    Description = binaryReader.ReadString();
                }
            }

            return true;
        }

    }
}
