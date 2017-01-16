using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data.Sql;
using System.Data;


namespace DBAcsses
{
 public class DBConnector
    {
        private static DBConnector DataBase;
        private OleDbConnection connction = new OleDbConnection();
        public DataSet Dataset;
        private Dictionary<string, OleDbDataAdapter> Adapters;

        public static DBConnector GetInstance()
        {
            return GetInstance(@"TearonatDBV2");
        }
        public DBConnector(string Constring)
        {
            this.connction = new OleDbConnection(Constring);
            this.Dataset = new DataSet();
            this.Adapters = new Dictionary<string, OleDbDataAdapter>();
        }
        public static DBConnector GetInstance(string DataBaseName)
        {
            if (DataBase == null)
            {
                string path = System.IO.Directory.GetCurrentDirectory();
                int i = path.IndexOf(@"\bin");
                path = path.Substring(0, i) + @"\bin\Debug\Data\" + DataBaseName + @".mdb";
                DataBase = new DBConnector(@"Provider=Microsoft.JET.OLEDB.4.0;Data Source=" + path + ";Persist Security Info=True");
            }
            return DataBase;
        }
        public static void Delete(string DeleteCommand)
        {
           
        }  

        private void AddTable(string TableName, string SQLCommand)
        {
            if (!Adapters.ContainsKey(TableName))
            {
                OleDbDataAdapter Adp = new OleDbDataAdapter(SQLCommand, this.connction);
                OleDbCommandBuilder Builder = new OleDbCommandBuilder(Adp);
               
                    Adp.InsertCommand = Builder.GetInsertCommand();
                    Adp.DeleteCommand = Builder.GetDeleteCommand();
                    Adp.UpdateCommand = Builder.GetUpdateCommand();
               
                Adp.Fill(Dataset, TableName);
                Adapters.Add(TableName, Adp);
            }
        }

        public void AddTable(string TableName)
        {
            AddTable(TableName, "Select * From " + TableName);
        }

        public DataTable GetTable(string TableName)
        {
            return Dataset.Tables[TableName];
        }

        public void RemoveTable(string TableName)
        {
            try
            {
                Dataset.Tables.Remove(TableName);
                Adapters.Remove(TableName);
            }
            catch (Exception ex)
            {
           //     MessageBox.Show("Message : " + ex.Message, "An Excption had ouccerd ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Update(string TableName)
        {
            OleDbDataAdapter adp = (OleDbDataAdapter)Adapters[TableName];
            try
            {
                adp.Update(Dataset, TableName);
            }
            catch (OleDbException ex)
            {
              //  MessageBox.Show(ex.Message);
            }
            }

        public void UpdateAllTables()
        {
            foreach (DataTable dt in Dataset.Tables)
            {
                Update(dt.TableName);
            }
        }

        public void AddRelation(string RelationName, DataColumn PrimaryKey, DataColumn ForeignKey)
        {
            try
            {
                if (!Dataset.Relations.Contains(RelationName))
                    Dataset.Relations.Add(RelationName, PrimaryKey, ForeignKey);
            }
            catch (Exception ex)
            {
             //   MessageBox.Show("Message : " + ex.Message, "An Excption had ouccerd ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        /*////////////////////////////////////////////////////////////////////////////////////////*/


        public int ExecuteNonQuery(string Query) // מחזיר מספר שורות שהושפעו
        {

            connction.Open();
            OleDbCommand command = connction.CreateCommand();
            command.CommandText = Query;
            int a = command.ExecuteNonQuery();
            connction.Close();
            return a;
        }

        public DataRow[] ExecuteQuery(string Query, string TableName)
        {
            connction.Open();
            DataRow[] Rows;
            DataTable table = Dataset.Tables[TableName];
            OleDbCommand Command = connction.CreateCommand();
            Command.CommandText = Query;


            using (OleDbDataReader Reader = Command.ExecuteReader())
            {
                List<string> List = new List<string>();
                while (Reader.Read())
                {
                    List.Add(Reader[0].ToString());
                }
                connction.Close();
                Rows = new DataRow[List.Count];

                int index = 0;
                foreach (string obj in List)
                {

                    Rows[index] = table.NewRow();
                    Rows[index] = table.Rows.Find(obj);
                    index++;
                }
            }
            return Rows;
        }


        public DataRow[] ExecuteQuery(string Query, string[] Col)
        {
            OleDbCommand command = connction.CreateCommand();
            command.CommandText = Query;
            List<string> List = new List<string>();

            connction.Open();
            using (OleDbDataReader Reader = command.ExecuteReader())
            {
                while (Reader.Read())
                {
                    for (int i = 0; i < Col.Length; i++)
                    {
                        List.Add(Reader[i].ToString());
                    }
                }
                connction.Close();
            }
            DataTable table = new DataTable();
            table.Columns.Clear();

            foreach (string item in Col)
            {
                table.Columns.Add(item);
            }

            DataRow[] dataRow = new DataRow[List.Count / Col.Length];
            int Count = 0, DataRowCount = 0;
            dataRow[0] = table.NewRow();
            foreach (object o in List)
            {
                if (Count < Col.Length)
                {
                    dataRow[DataRowCount][Count] = o;
                    Count++;
                }
                else
                {
                    DataRowCount++;
                    Count = 0;
                    dataRow[DataRowCount] = table.NewRow();
                    dataRow[DataRowCount][Count] = o;
                    Count++;

                }


            }
            return dataRow;

        }


        /*///////////////////////////////////////////////////////////////////////////////*/





        public OleDbConnection ReciveConnction()
        {
            return connction;
        }

        /*////////////////////////////////////////////////////////////////////////*/


    }
    
}

  
    

