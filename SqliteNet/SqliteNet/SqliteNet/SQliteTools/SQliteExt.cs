using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using NLog;
using System.Configuration;
using System.IO;

namespace SqliteNet.SQliteTools
{
    /// <summary>
    /// Class for admin SQLite.
    /// </summary>
    public class SQliteExt
    {
        public static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private static string DataBaseName = AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings["databasename"].ToString();
        private static SQLiteConnection dbConnection;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="recreate">Indicate if recreate database.</param>
        public SQliteExt(bool recreate)
        {
            InitializeDataBase(recreate);
        }

        private void InitializeDataBase(bool recreate)
        {
            if (!File.Exists(DataBaseName) || recreate)
            {
                CreateDataBase();
                OpenConnection();
                CreateTables();
            }
            else
                OpenConnection();
        }

        /// <summary>
        /// Create database.
        /// </summary>
        private void CreateDataBase()
        {
            try
            {
                SQLiteConnection.CreateFile(DataBaseName);
            }
            catch (Exception ex)
            {
                Logger.Error("Error creating database " + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Open Connection
        /// </summary>
        private void OpenConnection()
        {
            try
            {
                dbConnection = new SQLiteConnection("Data Source=" + DataBaseName + ";Version=3;");
                dbConnection.Open();
            }
            catch (Exception ex)
            {
                Logger.Error("Error creating database " + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Create tables.
        /// </summary>
        private void CreateTables()
        {
            try
            {
                foreach (var item in Querys.GetAllTables())
                {
                    SQLiteCommand command = new SQLiteCommand(item, dbConnection);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error creating tables " + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Execute query and return values.
        /// </summary>
        /// <param name="queryValue">query</param>
        /// <returns>SQLiteDataReader value</returns>
        public static SQLiteDataReader ExecuteReader(string queryValue)
        {
            try
            {
                SQLiteCommand command = new SQLiteCommand(queryValue, dbConnection);
                var result = command.ExecuteReader();

                if (result.Read())
                    return result;

                return null;
            }
            catch (Exception ex)
            {
                Logger.Error("Error executing reader " + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Execute non reader.
        /// </summary>
        /// <param name="queryValue">query value</param>
        public static void ExecuteNonReader(string queryValue)
        {
            try
            {
                SQLiteCommand command = new SQLiteCommand(queryValue, dbConnection);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Logger.Error("Error executing non reader " + ex.Message);
                throw;
            }
        }
    }
}
