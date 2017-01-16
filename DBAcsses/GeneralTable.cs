using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAcsses
{
    public abstract class GeneralTable
    {
        public int LengthOfTable;
        private string TableName;
        protected DataTable Table;
        protected int CurrentRow;
        protected string PrimaryKey;

        public GeneralTable(string tableName, string primaryKey)
        {

            this.PrimaryKey = primaryKey;
            this.TableName = tableName;
            DBConnector.GetInstance().AddTable(tableName);
            this.Table = DBConnector.GetInstance().GetTable(tableName);

            if (IsEmpty())
            {
                this.CurrentRow = -1;
            }
            else
            {
                this.CurrentRow = 0;
            }

            //לתקן אחר כך
            DataColumn[] arr = new DataColumn[1];
            arr[0] = DBConnector.GetInstance().Dataset.Tables[tableName].Columns[primaryKey];
            this.Table.PrimaryKey = arr;

            LengthOfTable = Table.Rows.Count;
        }

        public void GoToFirst()
        {
            if (IsEmpty())
                throw new Exception("ניווט על טבלה ריקה");
            CurrentRow = 0;
        }

        public void GoToLast()
        {
            if (IsEmpty())
                throw new Exception("ניווט על טבלה ריקה");
            CurrentRow = Table.Rows.Count - 1;
        }

        public void MoveNext()
        {
            if (IsEmpty())
                throw new Exception("ניווט על טבלה ריקה");
            CurrentRow = (CurrentRow + 1) % Table.Rows.Count;
        }

        public void MovePrev()
        {
            if (IsEmpty())
                throw new Exception("ניווט על טבלה ריקה");

            if (CurrentRow == 0)
                CurrentRow = Table.Rows.Count - 1;
            else
                CurrentRow = CurrentRow - 1;
        }

        public bool Find(object Key)
        {
            int index = 0;
            foreach (DataRow dataRow in Table.Rows)
            {
                if (dataRow[PrimaryKey].Equals(Key))
                {
                    CurrentRow = index;
                    return true;
                }
                else
                    index++;
            }
            return false;
        }


        public DataRow FindRow(object Key)
        {

            int index = 0;
            foreach (DataRow dataRow in Table.Rows)
            {
                if (dataRow[PrimaryKey].Equals(Key))
                {
                    CurrentRow = index;
                    return Table.Rows[CurrentRow];
                }
                else
                    index++;
            }
            return null;

        }



        /// <summary>
        /// Change the currentRow value
        /// defult : 0
        /// </summary>
        /// <param name="key"></param>
        public void GoTo(object key)
        {
            int index = 0;
            bool check = false;
            foreach (DataRow dataRow in Table.Rows)
            {
                if (dataRow[PrimaryKey].Equals(key))
                {
                    CurrentRow = index;
                    check = true;
                    break;

                }
                else
                    index++;
            }
            if (!check)
                CurrentRow = 0;
        }

        public int Size()
        {
            return Table.Rows.Count;
        }

        public void Save()
        {
            DBConnector.GetInstance().Update(Table.TableName);
        }


        public DataRow GetCurrentRowData()
        {
            return Table.Rows[CurrentRow];
        }


        protected void AddRow(IEntity obj)
        {
            DataRow dr = Table.NewRow();
            obj.populate(dr);
            Table.Rows.Add(dr);
            this.Save();
        }
        public void UpdateRow(IEntity obj)
        {
            obj.populate(Table.Rows[CurrentRow]);
            this.Save();
        }

        protected void DeleteRow(DataRow dr)
        {
            Table.Rows.Remove(dr);
            this.Save();
        }

        public DataColumn GetPrimaryKeyColumn()
        {
            return Table.Columns[PrimaryKey];
        }

        public DataColumn GetColumn(string s)
        {
            return Table.Columns[s];
        }

        public DataRow[] Filter(string filterString)
        {
            if (filterString.Trim().Length == 0)
                return Table.Select();
            return Table.Select(filterString);
        }

        public bool IsEmpty()
        {
            return Table.Rows.Count == 0;
        }
    }
}

