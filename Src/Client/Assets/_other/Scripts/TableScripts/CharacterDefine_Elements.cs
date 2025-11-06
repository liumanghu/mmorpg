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
    /// CharacterDefine_Elements
    /// </summary>
    public class CharacterDefine_Elements : DataRowBase
    {
        private int m_Id = 0;

        /// <summary>
        /// 获取CharacterDefine_Elements表主键id
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
        /// 获取Class。
        /// </summary>
        public string Class
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
        /// 获取Description。
        /// </summary>
        public string Description
        {
            get;
            private set;
        }
        /// <summary>
        /// 获取AI。
        /// </summary>
        public string AI
        {
            get;
            private set;
        }
        /// <summary>
        /// 获取Height。
        /// </summary>
        public float Height
        {
            get;
            private set;
        }
        /// <summary>
        /// 获取InitLevel。
        /// </summary>
        public int InitLevel
        {
            get;
            private set;
        }
        /// <summary>
        /// 获取Speed。
        /// </summary>
        public int Speed
        {
            get;
            private set;
        }
        /// <summary>
        /// 获取MaxHP。
        /// </summary>
        public float MaxHP
        {
            get;
            private set;
        }
        /// <summary>
        /// 获取MaxMP。
        /// </summary>
        public float MaxMP
        {
            get;
            private set;
        }
        /// <summary>
        /// 获取GrowthSTR。
        /// </summary>
        public float GrowthSTR
        {
            get;
            private set;
        }
        /// <summary>
        /// 获取GrowthINT。
        /// </summary>
        public float GrowthINT
        {
            get;
            private set;
        }
        /// <summary>
        /// 获取GrowthDEX。
        /// </summary>
        public float GrowthDEX
        {
            get;
            private set;
        }
        /// <summary>
        /// 获取STR。
        /// </summary>
        public float STR
        {
            get;
            private set;
        }
        /// <summary>
        /// 获取INT。
        /// </summary>
        public float INT
        {
            get;
            private set;
        }
        /// <summary>
        /// 获取DEX。
        /// </summary>
        public float DEX
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
        /// 获取DEF。
        /// </summary>
        public float DEF
        {
            get;
            private set;
        }
        /// <summary>
        /// 获取MDEF。
        /// </summary>
        public float MDEF
        {
            get;
            private set;
        }
        /// <summary>
        /// 获取SPD。
        /// </summary>
        public float SPD
        {
            get;
            private set;
        }
        /// <summary>
        /// 获取CRI。
        /// </summary>
        public float CRI
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
            Class = columnStrings[index++];
            Resource = columnStrings[index++];
            Description = columnStrings[index++];
            AI = columnStrings[index++];
            Height = float.Parse(columnStrings[index++]);
            InitLevel = int.Parse(columnStrings[index++]);
            Speed = int.Parse(columnStrings[index++]);
            MaxHP = float.Parse(columnStrings[index++]);
            MaxMP = float.Parse(columnStrings[index++]);
            GrowthSTR = float.Parse(columnStrings[index++]);
            GrowthINT = float.Parse(columnStrings[index++]);
            GrowthDEX = float.Parse(columnStrings[index++]);
            STR = float.Parse(columnStrings[index++]);
            INT = float.Parse(columnStrings[index++]);
            DEX = float.Parse(columnStrings[index++]);
            AD = float.Parse(columnStrings[index++]);
            AP = float.Parse(columnStrings[index++]);
            DEF = float.Parse(columnStrings[index++]);
            MDEF = float.Parse(columnStrings[index++]);
            SPD = float.Parse(columnStrings[index++]);
            CRI = float.Parse(columnStrings[index++]);

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
                    Class = binaryReader.ReadString();
                    Resource = binaryReader.ReadString();
                    Description = binaryReader.ReadString();
                    AI = binaryReader.ReadString();
                    Height = binaryReader.ReadSingle();
                    InitLevel = binaryReader.Read7BitEncodedInt32();
                    Speed = binaryReader.Read7BitEncodedInt32();
                    MaxHP = binaryReader.ReadSingle();
                    MaxMP = binaryReader.ReadSingle();
                    GrowthSTR = binaryReader.ReadSingle();
                    GrowthINT = binaryReader.ReadSingle();
                    GrowthDEX = binaryReader.ReadSingle();
                    STR = binaryReader.ReadSingle();
                    INT = binaryReader.ReadSingle();
                    DEX = binaryReader.ReadSingle();
                    AD = binaryReader.ReadSingle();
                    AP = binaryReader.ReadSingle();
                    DEF = binaryReader.ReadSingle();
                    MDEF = binaryReader.ReadSingle();
                    SPD = binaryReader.ReadSingle();
                    CRI = binaryReader.ReadSingle();
                }
            }

            return true;
        }

    }
}
