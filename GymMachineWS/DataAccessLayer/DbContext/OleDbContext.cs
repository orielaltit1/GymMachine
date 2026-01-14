using System.Data;
using System.Data.OleDb;

namespace GymMachineWS
{
    public class OleDbContext : IDbContext
    {
        //אובייקט האחראי על הקשר עם מסד נתונים
        //יוצר קשר עם מסד נתונים
        //סוגר קשר עם מסד נתונים
        OleDbConnection connection;
        //אחראי להעביר את הפקודות למסד נתונים
        //מקבל חזרה את הפקודות
        OleDbCommand command;
        OleDbTransaction transaction;

        public OleDbContext()
        {
            this.connection = new OleDbConnection();
            // מורכב מ2 חלקים
            //מהו סוג של מסד נתונים
            //מיקום הקובץ במחשב
            string path = @"C:\GymMachineProject\GymMachine\GymMachineWS";
            this.connection.ConnectionString = $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={path}\App_Data\GymMachineStore.accdb";
            //this.connection.ConnectionString = $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={Directory.GetCurrentDirectory()}\App_Data\GymMachineStore.accdb";
            this.command = new OleDbCommand();
            this.command.Connection = this.connection;
        }
        public void BeginTransaction()
        {
            this.transaction = this.connection.BeginTransaction();
        }

        public void CloseConnection()
        {
            this.connection.Close();
        }

        public void Commit()
        {
            this.transaction.Commit();
        }

        public int Delete(string sql)
        {
            return ChangeDb(sql);
        }

        public int Insert(string sql)
        {
            return ChangeDb(sql);
        }

        public void OpenConnection()
        {
            this.connection.Open();
        }

        public void RollBack()
        {
            this.transaction.Rollback();
        }

        public IDataReader Select(string sql)
        {
            this.command.CommandText = sql;
            IDataReader dataReader = this.command.ExecuteReader();
            this.command.Parameters.Clear();
            return dataReader;
        }

        public int Update(string sql)
        {
            return ChangeDb(sql);
        }
        private int ChangeDb(string sql) 
        {
            this.command.CommandText = sql;
            int records = this.command.ExecuteNonQuery();
            this.command.Parameters.Clear();
            return records;
        }
     
     

       
        public Object GetValue(String sql)
        {
            this.command.CommandText = sql;
            Object value = this.command.ExecuteScalar();
            this.command.Parameters.Clear();
            return value;
        }

        public void AddParamter(string v, object value)
        {
            this.command.Parameters.Add(new OleDbParameter(v, value));
        }


        //כאשר רוצים להוציא ערך - ExecuteScalar
    }
}
