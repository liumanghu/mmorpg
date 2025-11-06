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
    /// SkillDefine_Skills
    /// </summary>
    public class SkillDefine_Skills : DataRowBase
    {
        private int m_Id = 0;

        /// <summary>
        /// 获取SkillDefine_Skills表主键id
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
        /// 获取Type。
        /// </summary>
        public string Type
        {
            get;
            private set;
        }
        /// <summary>
        /// 获取UnlockLevel。
        /// </summary>
        public int UnlockLevel
        {
            get;
            private set;
        }
        /// <summary>
        /// 获取CastTarget。
        /// </summary>
        public string CastTarget
        {
            get;
            private set;
        }
        /// <summary>
        /// 获取CastRange。
        /// </summary>
        public int CastRange
        {
            get;
            private set;
        }
        /// <summary>
        /// 获取CastTime。
        /// </summary>
        public float CastTime
        {
            get;
            private set;
        }
        /// <summary>
        /// 获取CD。
        /// </summary>
        public float CD
        {
            get;
            private set;
        }
        /// <summary>
        /// 获取MPCost。
        /// </summary>
        public int MPCost
        {
            get;
            private set;
        }
        /// <summary>
        /// 获取Bullet。
        /// </summary>
        public bool Bullet
        {
            get;
            private set;
        }
        /// <summary>
        /// 获取BulletSpeed。
        /// </summary>
        public float BulletSpeed
        {
            get;
            private set;
        }
        /// <summary>
        /// 获取BulletResource。
        /// </summary>
        public string BulletResource
        {
            get;
            private set;
        }
        /// <summary>
        /// 获取AOERange。
        /// </summary>
        public int AOERange
        {
            get;
            private set;
        }
        /// <summary>
        /// 获取AOEEffect。
        /// </summary>
        public string AOEEffect
        {
            get;
            private set;
        }
        /// <summary>
        /// 获取SkillAnim。
        /// </summary>
        public string SkillAnim
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
        /// 获取HitTimes。
        /// </summary>
        public float[] HitTimes
        {
            get;
            private set;
        }
        /// <summary>
        /// 获取HitEffect。
        /// </summary>
        public string HitEffect
        {
            get;
            private set;
        }
        /// <summary>
        /// 获取Buff。
        /// </summary>
        public float[] Buff
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
            Name = columnStrings[index++];
            Description = columnStrings[index++];
            Icon = columnStrings[index++];
            Type = columnStrings[index++];
            UnlockLevel = int.Parse(columnStrings[index++]);
            CastTarget = columnStrings[index++];
            CastRange = int.Parse(columnStrings[index++]);
            CastTime = float.Parse(columnStrings[index++]);
            CD = float.Parse(columnStrings[index++]);
            MPCost = int.Parse(columnStrings[index++]);
            Bullet = bool.Parse(columnStrings[index++]);
            BulletSpeed = float.Parse(columnStrings[index++]);
            BulletResource = columnStrings[index++];
            AOERange = int.Parse(columnStrings[index++]);
            AOEEffect = columnStrings[index++];
            SkillAnim = columnStrings[index++];
            Duration = float.Parse(columnStrings[index++]);
            Interval = float.Parse(columnStrings[index++]);
            HitTimes = DataTableExtension.ParseArray(columnStrings[index++]);
            HitEffect = columnStrings[index++];
            Buff = DataTableExtension.ParseArray(columnStrings[index++]);
            AD = float.Parse(columnStrings[index++]);
            AP = float.Parse(columnStrings[index++]);
            ADFator = float.Parse(columnStrings[index++]);
            APFator = float.Parse(columnStrings[index++]);

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
                    Type = binaryReader.ReadString();
                    UnlockLevel = binaryReader.Read7BitEncodedInt32();
                    CastTarget = binaryReader.ReadString();
                    CastRange = binaryReader.Read7BitEncodedInt32();
                    CastTime = binaryReader.ReadSingle();
                    CD = binaryReader.ReadSingle();
                    MPCost = binaryReader.Read7BitEncodedInt32();
                    Bullet = binaryReader.ReadBoolean();
                    BulletSpeed = binaryReader.ReadSingle();
                    BulletResource = binaryReader.ReadString();
                    AOERange = binaryReader.Read7BitEncodedInt32();
                    AOEEffect = binaryReader.ReadString();
                    SkillAnim = binaryReader.ReadString();
                    Duration = binaryReader.ReadSingle();
                    Interval = binaryReader.ReadSingle();
                    HitTimes = binaryReader.ReadArray();
                    HitEffect = binaryReader.ReadString();
                    Buff = binaryReader.ReadArray();
                    AD = binaryReader.ReadSingle();
                    AP = binaryReader.ReadSingle();
                    ADFator = binaryReader.ReadSingle();
                    APFator = binaryReader.ReadSingle();
                }
            }

            return true;
        }

    }
}
