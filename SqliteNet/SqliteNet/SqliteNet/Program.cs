using SqliteNet.SQliteTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqliteNet
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create database and open connection.
            var sqli = new SQliteExt(false);

            //Execute query non reader.
            SQliteExt.ExecuteNonReader("queryexecute");
            
            //Execute query Reder and return SQLiDataReader.
            var result = SQliteExt.ExecuteReader("queryexecute");
        }
    }
}
