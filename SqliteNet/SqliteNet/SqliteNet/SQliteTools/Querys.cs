using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqliteNet.SQliteTools
{
    public static class Querys
    {
        public static string CreateTableWallets = "create table user (id int, name varchar(40))";

        public static List<string> GetAllTables()
        {
            //ToDo: The tables query can load from file o write in code.
            var listQuerys = new List<string>()
            {
               CreateTableWallets
            };

            return listQuerys;
        }
    }
}
