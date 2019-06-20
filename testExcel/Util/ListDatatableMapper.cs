using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Qx.Util.Office
{
    public class ListDatatableMapper<T> where T : new()
    {
        public static DataTable ListToDataTable(List<T> entitys)
        {

            //检查实体集合不能为空
            if (entitys == null || entitys.Count < 1)
            {
                return new DataTable();
            }

            //取出第一个实体的所有Propertie
            Type entityType = entitys[0].GetType();
            PropertyInfo[] entityProperties = entityType.GetProperties();

            //生成DataTable的structure
            //生产代码中，应将生成的DataTable结构Cache起来，此处略
            DataTable dt = new DataTable("dt");
            for (int i = 0; i < entityProperties.Length; i++)
            {
                dt.Columns.Add(entityProperties[i].Name, entityProperties[i].PropertyType);
                //dt.Columns.Add(entityProperties[i].Name);
            }

            //将所有entity添加到DataTable中
            foreach (object entity in entitys)
            {
                //检查所有的的实体都为同一类型
                if (entity.GetType() != entityType)
                {
                    throw new Exception("要转换的集合元素类型不一致");
                }
                object[] entityValues = new object[entityProperties.Length];
                for (int i = 0; i < entityProperties.Length; i++)
                {
                    entityValues[i] = entityProperties[i].GetValue(entity, null);

                }
                dt.Rows.Add(entityValues);
            }
            return dt;
        }
        public static IList<T> DataTableToList(DataTable dt)
        {
            // 定义集合   
            IList<T> ts = new List<T>();
            // 获得此模型的类型  
            Type type = typeof(T);
            string tempName = "";
            foreach (DataRow dr in dt.Rows)
            {
                T t = new T();
                // 获得此模型的公共属性   
                PropertyInfo[] propertys = t.GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    tempName = pi.Name;  // 检查DataTable是否包含此列   
                    if (dt.Columns.Contains(tempName))
                    {
                        // 判断此属性是否有Setter     
                        if (!pi.CanWrite) continue;
                        string value = dr[tempName].ToString();

                        pi.SetValue(t, ToPriorityType(pi, value));
                        // Type generactTypeDefinition = pi.PropertyType.GetGenericTypeDefinition();
                        // if (generactTypeDefinition == typeof(Nullable<>))
                        // {
                        //     object obj = Convert.ChangeType(value, Nullable.GetUnderlyingType(pi.PropertyType));
                        //     pi.SetValue(t, obj, null);
                        // }

                    }
                }
                ts.Add(t);
            }
            return ts;
        }
        //  将val 转换为 类的属性的 类型
        private static object ToPriorityType(PropertyInfo field, string val)
        {
            object obj = null;
            //if (!dic.Keys.Contains(field.Name))
            //    continue;
            //val = dic[field.Name];
            object defaultVal;
            if (field.PropertyType.Name.Equals("String"))
            {
                defaultVal = "";
            }
            else if (field.PropertyType.Name.Equals("Boolean"))
            {
                defaultVal = false;
                val = (val.Equals("1") || val.Equals("on")).ToString();
            }
            else if (field.PropertyType.Name.Equals("Decimal"))
            {
                defaultVal = 0M;
            }
            else
            {
                defaultVal = 0;
            }
            if (!field.PropertyType.IsGenericType)
            {
                // 如果不是泛型类型, 直接转换
                obj = string.IsNullOrEmpty(val) ? defaultVal : Convert.ChangeType(val, field.PropertyType);
            }
            else
            {
                Type genericTypeDefinition = field.PropertyType.GetGenericTypeDefinition();
                if (genericTypeDefinition == typeof(Nullable<>))
                    obj = string.IsNullOrEmpty(val) ? defaultVal : Convert.ChangeType(val, Nullable.GetUnderlyingType(field.PropertyType));
            }
            return obj;

        }

    }
}
