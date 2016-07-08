using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using MongonDBDemo;

namespace MongoDbTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Mongo DB Test";
            //InsertTest();
            //QueryTest();
            //UpdateTest();
            DeleteTest();

            Console.WriteLine("Finish!");

            Console.ReadLine();
        }

        /// <summary>
        /// 插入数据测试
        /// </summary>
        static void InsertTest()
        {
            var random = new Random();
            for (var i = 1; i <= 10; i++)
            {
                var item = new Student()
                {
                    Name = "我的名字" + i,
                    Age = random.Next(25, 30),
                    State = i % 2 == 0 ? State.Normal : State.Unused
                };
                MongoDbHepler.Insert(DbConfigParams.ConntionString, DbConfigParams.DbName, "student", item);
            }
        }

        /// <summary>
        /// 查询测试
        /// </summary>
        static void QueryTest()
        {
            var queryBuilder = new QueryBuilder<Student>();
            var query = queryBuilder.GTE(x => x.Age, 27);
            var ltModel = MongoDbHepler.GetManyByCondition<Student>(DbConfigParams.ConntionString, DbConfigParams.DbName,
               "student", query);
            if (ltModel != null && ltModel.Count > 0)
            {
                foreach (var item in ltModel)
                {
                    Console.WriteLine("姓名：{0}，年龄：{1}，状态：{2}",
                        item.Name, item.Age, GetStateDesc(item.State));
                }
            }
        }

        /// <summary>
        /// 更新测试
        /// </summary>
        static void UpdateTest()
        {
            var queryBuilder = new QueryBuilder<Student>();
            var query = queryBuilder.GTE(x => x.Age, 27);
            var dictUpdate = new Dictionary<string, BsonValue>();
            dictUpdate["State"] = State.Unused;
            MongoDbHepler.Update(DbConfigParams.ConntionString, DbConfigParams.DbName, "student", query,
                dictUpdate);
        }

        /// <summary>
        /// 删除测试
        /// </summary>
        static void DeleteTest()
        {
            var queryBuilder = new QueryBuilder<Student>();
            var query = queryBuilder.GTE(x => x.Age, 28);
            MongoDbHepler.DeleteByCondition(DbConfigParams.ConntionString, DbConfigParams.DbName, "student", query);
        }

        /// <summary>
        /// 获取状态描述
        /// </summary>
        /// <param name="state">状态</param>
        /// <returns>状态描述</returns>
        static string GetStateDesc(State state)
        {
            string result = string.Empty;
            switch (state)
            {
                case State.All:
                    result = "全部";
                    break;
                case State.Normal:
                    result = "正常";
                    break;
                case State.Unused:
                    result = "未使用";
                    break;
                default:
                    throw new ArgumentOutOfRangeException("state");
            }
            return result;
        }
    }
}