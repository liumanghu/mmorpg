using GameFramework.DataTable;
using System;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace MMORPG.DataTable
{
    public static class DataTableExtension
    {
        internal static readonly char[] DataSplitSeparators = new char[] { '\t' };
        internal static readonly char[] DataTrimSeparators = new char[] { '\"' };
        
        public static void LoadDataTable(
            this DataTableComponent dataTableComponent, 
            string dataTableName, 
            string dataTableAssetName, 
            object userData,
            int tablePriority = 100)
        {
            if (string.IsNullOrEmpty(dataTableName))
            {
                Log.Warning("Data table name is invalid.");
                return;
            }

            var dataTableNameArray = dataTableName.Split(".");
            if (dataTableNameArray.Length != 2)
            {
                Log.Error($"{dataTableName}命名错误");
                return;
            }

            var dataRowClassName = dataTableNameArray[0];
            var dataRowType = Type.GetType($"MMORPG.DataTable.{dataRowClassName}");
            if (dataRowType == null)
            {
                Log.Error("Can not get data row type with class name '{0}'.", dataRowClassName);
                return;
            }
            
            DataTableBase dataTable = dataTableComponent.CreateDataTable(dataRowType);
            dataTable.ReadData(dataTableAssetName, tablePriority, userData);
        }

        public static Color32 ParseColor32(string value)
        {
            string[] splitedValue = value.Split(',');
            return new Color32(byte.Parse(splitedValue[0]), byte.Parse(splitedValue[1]), byte.Parse(splitedValue[2]), byte.Parse(splitedValue[3]));
        }

        public static Color ParseColor(string value)
        {
            string[] splitedValue = value.Split(',');
            return new Color(float.Parse(splitedValue[0]), float.Parse(splitedValue[1]), float.Parse(splitedValue[2]), float.Parse(splitedValue[3]));
        }

        public static Quaternion ParseQuaternion(string value)
        {
            string[] splitedValue = value.Split(',');
            return new Quaternion(float.Parse(splitedValue[0]), float.Parse(splitedValue[1]), float.Parse(splitedValue[2]), float.Parse(splitedValue[3]));
        }

        public static Rect ParseRect(string value)
        {
            string[] splitedValue = value.Split(',');
            return new Rect(float.Parse(splitedValue[0]), float.Parse(splitedValue[1]), float.Parse(splitedValue[2]), float.Parse(splitedValue[3]));
        }

        public static Vector2 ParseVector2(string value)
        {
            string[] splitedValue = value.Split(',');
            return new Vector2(float.Parse(splitedValue[0]), float.Parse(splitedValue[1]));
        }

        public static Vector3 ParseVector3(string value)
        {
            string[] splitedValue = value.Split(',');
            return new Vector3(float.Parse(splitedValue[0]), float.Parse(splitedValue[1]), float.Parse(splitedValue[2]));
        }

        public static Vector4 ParseVector4(string value)
        {
            string[] splitedValue = value.Split(',');
            return new Vector4(float.Parse(splitedValue[0]), float.Parse(splitedValue[1]), float.Parse(splitedValue[2]), float.Parse(splitedValue[3]));
        }
        
        /// <summary>
        /// 转换Array类型
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static float[] ParseArray(string value)
        {
            if (value.StartsWith("[") && value.EndsWith("]"))
            {
                value = value.Substring(1, value.Length - 2);
            }
                
            var strArray = value.Split(",");
            var length = strArray.Length;
            var result = new float[strArray.Length];
            for (var i = 0; i < length; i++)
            {
                var f = float.Parse(strArray[i]);
                result[i] = f;
            }
    
            return result;
        }
    }
}
