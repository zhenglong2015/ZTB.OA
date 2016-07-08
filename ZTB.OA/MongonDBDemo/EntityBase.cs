// ==========================================
// Author                  :    WIN-JH13BJM99UM 
// Create Time           :    2016/7/8 9:53:39
// Update Time          :    2016/7/8 9:53:39
// ==========================================
// Class Description     :    
// ==========================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongonDBDemo
{
    /// <summary>
    /// 实体基类
    /// </summary>
    public class EntityBase
    {
        /// <summary>
        /// 主键
        /// </summary>
        [BsonId]
        public ObjectId Id { get; set; }
    }
}
