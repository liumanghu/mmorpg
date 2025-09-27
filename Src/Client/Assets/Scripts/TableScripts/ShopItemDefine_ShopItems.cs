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
    /// ShopItemDefine_ShopItems
    /// </summary>
    public class ShopItemDefine_ShopItems : DataRowBase
    {
        private int m_Id = 0;

        /// <summary>
        /// 获取ShopItemDefine_ShopItems表主键id
        /// </summary>
        public override int Id
        {
            get
            {
                return m_Id;
            }
        }

        /// <summary>
        /// 获取Count。
        /// </summary>
        public int Count
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
        /// 获取Status。
        /// </summary>
        public int Status
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
            Count = int.Parse(columnStrings[index++]);
            Price = int.Parse(columnStrings[index++]);
            Status = int.Parse(columnStrings[index++]);

            return true;
        }

        public override bool ParseDataRow(byte[] dataRowBytes, int startIndex, int length, object userData)
        {
            using (MemoryStream memoryStream = new MemoryStream(dataRowBytes, startIndex, length, false))
            {
                using (BinaryReader binaryReader = new BinaryReader(memoryStream, Encoding.UTF8))
                {
                    m_Id = binaryReader.Read7BitEncodedInt32();
                    Count = binaryReader.Read7BitEncodedInt32();
                    Price = binaryReader.Read7BitEncodedInt32();
                    Status = binaryReader.Read7BitEncodedInt32();
                }
            }

            return true;
        }

    }
}
