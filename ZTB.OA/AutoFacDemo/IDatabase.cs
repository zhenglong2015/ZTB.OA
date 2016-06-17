// ==========================================
// Author                  :    WIN-JH13BJM99UM 
// Create Time           :    2016/6/17 10:43:05
// Update Time          :    2016/6/17 10:43:05
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
    interface IDatabase
    {
        string Name { get; }

        void Select(string commandText);

        void Insert(string commandText);

        void Update(string commandText);

        void Delete(string commandText);
    }
}
