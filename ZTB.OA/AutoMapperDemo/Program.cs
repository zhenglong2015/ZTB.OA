using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace AutoMapperDemo
{
    class Program
    {
        static void Main(string[] args)
        {

            Mapper.Initialize(x =>
            {
                x.ReplaceMemberName("Ä", "A");//替换字符要放到CreateMap之前
                x.ReplaceMemberName("í", "i");
                x.ReplaceMemberName("Tool", "Car");

                //识别前缀 / 后缀
                x.RecognizePrefixes("P");


                //不映射任何字段
                x.ShouldMapField = fi => false;
                //只映射getter是private的属性
                x.ShouldMapProperty = pi => pi.GetMethod != null && pi.GetMethod.IsPrivate;
             
                x.AddProfile<AliensPersonProfile>();
                x.CreateMap<Aliens, Person>();
                x.AddProfile<AliensPersonProfile>();
            });




            Mapper.CreateMap<Aliens, Person>().ForMember(dest => dest.Age, opt => opt.Condition(src => src.Age > 0 && src.Age < 149));
            var p1 = Mapper.Map<Person>(new Aliens() { Age = -1 });//不符合映射条件
            var p2 = Mapper.Map<Person>(new Aliens() { Age = 0 });//不符合映射条件
            var p3 = Mapper.Map<Person>(new Aliens() { Age = 1 });//符合映射条件
            var p4 = Mapper.Map<Person>(new Aliens() { Age = 148 });//符合映射条件
            var p5 = Mapper.Map<Person>(new Aliens() { Age = 149 });//不符合映射条件

            var p6 = Mapper.Map<Person>(new Aliens() { MyName="ZTB"});

            var p7= Mapper.Map<Person>(new Aliens() {  Ävíator=3 });
            var p8 = Mapper.Map<Person>(new Aliens() { ToolName = "UFO" });



            Console.WriteLine(p1.Age);//映射不成功，返回Person.Age默认值22
            Console.WriteLine(p2.Age);//映射不成功，返回Person.Age默认值22
            Console.WriteLine(p3.Age);//映射成功，返回新值1
            Console.WriteLine(p4.Age);//映射成功，返回新值148
            Console.WriteLine(p5.Age);//映射不成功，返回新值22


            Console.WriteLine(p6.my_name);

            Console.WriteLine(p7.Aviator);
            Console.WriteLine(p8.CarName);
            Console.Read();
        }
    }
    public class AliensPersonProfile : Profile
    {
        
        protected override void Configure()
        {
            SourceMemberNamingConvention = new LowerUnderscoreNamingConvention();
            DestinationMemberNamingConvention = new PascalCaseNamingConvention();//命名惯例放在CreateMap之后
        }
    }

    public class Person
    {
        public string Gender { get; set; }
        public int Aviator { get; set; }
        public string CarName { get; set; }
        public uint Age { get; set; }
        public string my_name { get; set; }
        public Person()
        {
            Age = 22;
        }
    }

    public class Aliens
    {
        public string PGender { get; set; }
        public int Ävíator { get; set; }
        public string ToolName { get; set; }
        public int Age { get; set; }
        public string MyName { set; get; }

        public Aliens()
        {
            Age = -23;
        }
    }
}
