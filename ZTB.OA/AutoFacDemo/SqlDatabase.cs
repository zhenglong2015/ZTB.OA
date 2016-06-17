// ==========================================
// Author                  :    WIN-JH13BJM99UM 
// Create Time           :    2016/6/17 10:43:29
// Update Time          :    2016/6/17 10:43:29
// ==========================================
// Class Description     :    
// ==========================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFacDemo
{
    class SqlDatabase : IDatabase
    {
        public string Name
        {
            get { return "sqlserver"; }
        }

        public void Select(string commandText)
        {
            Console.WriteLine(string.Format("'{0}' is a query sql in {1}!", commandText, Name));
        }

        public void Insert(string commandText)
        {
            Console.WriteLine(string.Format("'{0}' is a insert sql in {1}!", commandText, Name));
        }

        public void Update(string commandText)
        {
            Console.WriteLine(string.Format("'{0}' is a update sql in {1}!", commandText, Name));
        }

        public void Delete(string commandText)
        {
            Console.WriteLine(string.Format("'{0}' is a delete sql in {1}!", commandText, Name));
        }
    }
}
