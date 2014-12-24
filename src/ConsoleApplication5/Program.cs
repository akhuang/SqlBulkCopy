using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Newtonsoft.Json;
using System.Reflection;
using System.ComponentModel;
using System.Data.SqlClient;

namespace ConsoleApplication5
{
    class Program
    {
        static void Main(string[] args)
        {
            string msg = "{\"result\":\"success\",\"msg\":{\"data\":[{\"id\":562,\"unique_id\":\"ccic_dev_11-1346132060.0\",\"number_trunk\":\"6001\",\"customer_number\":\"01041005986\",\"customer_number_type\":1,\"customer_area_code\":\"010\",\"customer_province\":\"北京\",\"customer_city\":\"北京\",\"customer_crm_id\":\"\",\"customer_vip\":0,\"client_number\":\"01041005915\",\"client_area_code\":\"010\",\"cno\":\"2001\",\"exten\":\"\",\"client_name\":\"周忠海固话\",\"client_crm_id\":\"\",\"start_time\":1346132060,\"answer_time\":1346132062,\"bridge_time\":1346132066,\"end_time\":1346132094,\"bill_duration\":32,\"bridge_duration\":28,\"total_duration\":34,\"cost\":0,\"ivr_id\":0,\"ivr_name\":\"\",\"queue_name\":\"\",\"record_file\":\"3000001-20120828133420-01041005986-01041005915-record-ccic_dev_11-1346132060.0.mp3\",\"score\":0,\"in_case_lib\":0,\"task_id\":17,\"task_name\":\"测试数据\",\"call_type\":4,\"status\":28,\"mark\":0,\"mark_data\":\"\",\"end_reason\":1001,\"gw_ip\":\"172.16.15.245\",\"create_time\":\"2012-08-28 13:34:54\",\"user_field\":\"\"}],\"totalCount\":1}}";

            dynamic ob = JsonConvert.DeserializeObject<dynamic>(msg);

            List<OutBoundCall> list = ob.msg.data.ToObject<List<OutBoundCall>>();
            DataTable dt = list.ConvertToDatatable11();

            WriteToDatabase(dt);

            Console.ReadKey();
        }

        static void WriteToDatabase(DataTable dt)
        {
            // get your connection string
            string connString = "Data Source=10.33.11.52;Initial Catalog=crm_8.0.1;Persist Security Info=True;User ID=sa;Password=sa";
            // connect to SQL
            using (SqlConnection connection =
                    new SqlConnection(connString))
            {
                // make sure to enable triggers
                // more on triggers in next post
                SqlBulkCopy bulkCopy =
                    new SqlBulkCopy
                    (
                    connection,
                    SqlBulkCopyOptions.TableLock |
                    SqlBulkCopyOptions.FireTriggers |
                    SqlBulkCopyOptions.UseInternalTransaction | SqlBulkCopyOptions.KeepIdentity,
                    null
                    );

                // set the destination table name
                bulkCopy.DestinationTableName = "OutBoundCall";
                connection.Open();

                // write the data in the "dataTable"
                bulkCopy.WriteToServer(dt);
                connection.Close();
            }
        }


    }

    public class OutBoundCall
    {
        public OutBoundCall()
        {
        }


        /// <summary>
        /// 
        /// </summary>
        public long? TinetId
        {
            get;
            set;
        }


        /// <summary>
        /// 
        /// </summary>
        public string UniquieId
        {
            get;
            set;
        }


        /// <summary>
        /// 
        /// </summary>
        public string Numer_Thunk
        {
            get;
            set;
        }


        /// <summary>
        /// 
        /// </summary>
        public string Customer_Number
        {
            get;
            set;
        }


        /// <summary>
        /// 
        /// </summary>
        public string Client_Number
        {
            get;
            set;
        }


        /// <summary>
        /// 
        /// </summary>
        public string Client_Area_Code
        {
            get;
            set;
        }


        /// <summary>
        /// 
        /// </summary>
        public string Cno
        {
            get;
            set;
        }


        /// <summary>
        /// 
        /// </summary>
        public string Exten
        {
            get;
            set;
        }


        /// <summary>
        /// 
        /// </summary>
        public string Client_Name
        {
            get;
            set;
        }


        /// <summary>
        /// 
        /// </summary>
        public long? Start_Time
        {
            get;
            set;
        }


        /// <summary>
        /// 
        /// </summary>
        public long? Answer_Time
        {
            get;
            set;
        }


        /// <summary>
        /// 
        /// </summary>
        public long? Bridge_Time
        {
            get;
            set;
        }


        /// <summary>
        /// 
        /// </summary>
        public long? End_Time
        {
            get;
            set;
        }


        /// <summary>
        /// 
        /// </summary>
        public long? Bill_Duration
        {
            get;
            set;
        }


        /// <summary>
        /// 
        /// </summary>
        public long? Bridge_Duration
        {
            get;
            set;
        }


        /// <summary>
        /// 
        /// </summary>
        public long? Total_Duration
        {
            get;
            set;
        }


        /// <summary>
        /// 
        /// </summary>
        public decimal? Cost
        {
            get;
            set;
        }


        /// <summary>
        /// 
        /// </summary>
        public string Record_File
        {
            get;
            set;
        }


        /// <summary>
        /// 
        /// </summary>
        public int? Score
        {
            get;
            set;
        }


        /// <summary>
        /// 
        /// </summary>
        public int? Status
        {
            get;
            set;
        }


        /// <summary>
        /// 
        /// </summary>
        public int? Mark
        {
            get;
            set;
        }


        /// <summary>
        /// 
        /// </summary>
        public string Mark_Data
        {
            get;
            set;
        }


        /// <summary>
        /// 
        /// </summary>
        public string End_Reason
        {
            get;
            set;
        }


        /// <summary>
        /// 
        /// </summary>
        public DateTime? Create_Time
        {
            get;
            set;
        }


    }


    public static class CustomLINQtoDataSetMethods
    {
        public static DataTable ConvertToDatatable11<T>(this IList<T> data)
        {
            PropertyDescriptorCollection props =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }
        public static DataTable CopyToDataTable<T>(this IEnumerable<T> source)
        {
            return new ObjectShredder<T>().Shred(source, null, null);
        }

        public static DataTable CopyToDataTable<T>(this IEnumerable<T> source,
                                                    DataTable table, LoadOption? options)
        {
            return new ObjectShredder<T>().Shred(source, table, options);
        }

    }

    public class ObjectShredder<T>
    {
        private System.Reflection.FieldInfo[] _fi;
        private System.Reflection.PropertyInfo[] _pi;
        private System.Collections.Generic.Dictionary<string, int> _ordinalMap;
        private System.Type _type;

        // ObjectShredder constructor. 
        public ObjectShredder()
        {
            _type = typeof(T);
            _fi = _type.GetFields();
            _pi = _type.GetProperties();
            _ordinalMap = new Dictionary<string, int>();
        }

        /// <summary> 
        /// Loads a DataTable from a sequence of objects. 
        /// </summary> 
        /// <param name="source">The sequence of objects to load into the DataTable.</param>
        /// <param name="table">The input table. The schema of the table must match that 
        /// the type T.  If the table is null, a new table is created with a schema  
        /// created from the public properties and fields of the type T.</param> 
        /// <param name="options">Specifies how values from the source sequence will be applied to 
        /// existing rows in the table.</param> 
        /// <returns>A DataTable created from the source sequence.</returns> 
        public DataTable Shred(IEnumerable<T> source, DataTable table, LoadOption? options)
        {
            // Load the table from the scalar sequence if T is a primitive type. 
            if (typeof(T).IsPrimitive)
            {
                return ShredPrimitive(source, table, options);
            }

            // Create a new table if the input table is null. 
            if (table == null)
            {
                table = new DataTable(typeof(T).Name);
            }

            // Initialize the ordinal map and extend the table schema based on type T.
            table = ExtendTable(table, typeof(T));

            // Enumerate the source sequence and load the object values into rows.
            table.BeginLoadData();
            using (IEnumerator<T> e = source.GetEnumerator())
            {
                while (e.MoveNext())
                {
                    if (options != null)
                    {
                        table.LoadDataRow(ShredObject(table, e.Current), (LoadOption)options);
                    }
                    else
                    {
                        table.LoadDataRow(ShredObject(table, e.Current), true);
                    }
                }
            }
            table.EndLoadData();

            // Return the table. 
            return table;
        }

        public DataTable ShredPrimitive(IEnumerable<T> source, DataTable table, LoadOption? options)
        {
            // Create a new table if the input table is null. 
            if (table == null)
            {
                table = new DataTable(typeof(T).Name);
            }

            if (!table.Columns.Contains("Value"))
            {
                table.Columns.Add("Value", typeof(T));
            }

            // Enumerate the source sequence and load the scalar values into rows.
            table.BeginLoadData();
            using (IEnumerator<T> e = source.GetEnumerator())
            {
                Object[] values = new object[table.Columns.Count];
                while (e.MoveNext())
                {
                    values[table.Columns["Value"].Ordinal] = e.Current;

                    if (options != null)
                    {
                        table.LoadDataRow(values, (LoadOption)options);
                    }
                    else
                    {
                        table.LoadDataRow(values, true);
                    }
                }
            }
            table.EndLoadData();

            // Return the table. 
            return table;
        }

        public object[] ShredObject(DataTable table, T instance)
        {

            FieldInfo[] fi = _fi;
            PropertyInfo[] pi = _pi;

            if (instance.GetType() != typeof(T))
            {
                // If the instance is derived from T, extend the table schema 
                // and get the properties and fields.
                ExtendTable(table, instance.GetType());
                fi = instance.GetType().GetFields();
                pi = instance.GetType().GetProperties();
            }

            // Add the property and field values of the instance to an array.
            Object[] values = new object[table.Columns.Count];
            foreach (FieldInfo f in fi)
            {
                values[_ordinalMap[f.Name]] = f.GetValue(instance);
            }

            foreach (PropertyInfo p in pi)
            {
                values[_ordinalMap[p.Name]] = p.GetValue(instance, null);
            }

            // Return the property and field values of the instance. 
            return values;
        }

        public DataTable ExtendTable(DataTable table, Type type)
        {
            // Extend the table schema if the input table was null or if the value  
            // in the sequence is derived from type T.             
            foreach (FieldInfo f in type.GetFields())
            {
                if (!_ordinalMap.ContainsKey(f.Name))
                {
                    // Add the field as a column in the table if it doesn't exist 
                    // already.
                    DataColumn dc = table.Columns.Contains(f.Name) ? table.Columns[f.Name]
                        : table.Columns.Add(f.Name, f.FieldType);

                    // Add the field to the ordinal map.
                    _ordinalMap.Add(f.Name, dc.Ordinal);
                }
            }
            foreach (PropertyInfo p in type.GetProperties())
            {
                if (!_ordinalMap.ContainsKey(p.Name))
                {
                    // Add the property as a column in the table if it doesn't exist 
                    // already.
                    DataColumn dc = table.Columns.Contains(p.Name) ? table.Columns[p.Name]
                        : table.Columns.Add(p.Name, p.PropertyType);

                    // Add the property to the ordinal map.
                    _ordinalMap.Add(p.Name, dc.Ordinal);
                }
            }

            // Return the table. 
            return table;
        }
    }
}
