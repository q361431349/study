using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleApp3
{

    public class Student
    {
        public string LastName { get; set; }
        public List<int> Scores { get; set; }
    }

    public class Student1
    {
        public string First { get; set; }
        public string Last { get; set; }
        public int ID { get; set; }

        public List<int> Scores;
    }

    class Program
    {
        static void Main(string[] args)
        {
            #region demo1
            int[] scores = new int[] { 97, 92, 81, 60 };

            IEnumerable<int> scoreQuery =
                from score in scores
                where score > 80
                select score;
            var result = scores.Where(s => s > 80);
            foreach (int i in result)
            {
                Console.Write(i + "----");
            }

            Console.WriteLine();
            #endregion

            #region demo2
            IEnumerable<int> scoreQuery1 =
                from score in scores
                where score > 80
                orderby score descending
                select score;
            var result1 = scores.Where(s => s > 80).OrderByDescending(s => s);
            foreach (int i in scoreQuery1)
            {
                Console.Write(i + "----");
            }
            Console.WriteLine();
            #endregion

            #region demo3
            int scoreCount = scoreQuery1.Count();
            Console.WriteLine(scoreCount);
            #endregion


            #region demo4
            List<Student> students = new List<Student>
            {
               new Student {LastName="大娃", Scores= new List<int> {97, 72, 81, 60}},
               new Student {LastName="二娃", Scores= new List<int> {75, 84, 91, 39}},
               new Student {LastName="三娃", Scores= new List<int> {88, 94, 65, 85}},
               new Student {LastName="四娃", Scores= new List<int> {97, 89, 85, 82}},
               new Student {LastName="幺娃", Scores= new List<int> {35, 72, 91, 70}}
            };

            var scoreQuery2 = from student in students
                              from score in student.Scores
                              where score > 90
                              select new { Last = student.LastName, score };

            var scoreQuery3 = from student in students
                              where student.Scores.Where(score => score > 90).Count() > 0
                              select new { Last = student.LastName, student.Scores };

            //var result= students.SelectMany(s => s.Scores.Where(a=>a>90).Select(score=>
            //new {Last=s.LastName,score }));

            var scoreQuery4 = students.Where(s => s.Scores.Where(i => i > 90).Count() > 0).Select(std => new
            {
                Last = std.LastName,
                std.Scores
            });
            Console.WriteLine("scoreQuery2:");

            foreach (var student in scoreQuery2)
            {
                Console.WriteLine("{0} 分数: {1}", student.Last, student.score);
            }

            Console.WriteLine();
            #endregion


            #region demo5
            char[] upperCase = { 'A', 'B', 'C' };
            char[] lowerCase = { 'x', 'y', 'z' };


            var joinQuery1 =
                from upper in upperCase
                from lower in lowerCase
                select new { upper, lower };

            var resultJoinQuery1 = upperCase.Select(a => lowerCase.Select(b => new { upper = a, lower = b }));


            var joinQuery2 =
                from lower in lowerCase
                where lower != 'x'
                from upper in upperCase
                select new { lower, upper };

            var resultJoinQuery2 = upperCase.Select(a => lowerCase.Where(b => b != 'x').Select(b => new { upper = a, lower = b }));

            Console.WriteLine("Cross join:");

            foreach (var pair in resultJoinQuery1)
            {
                foreach (var item in pair)
                {
                    Console.WriteLine("{0} is matched to {1}", item.upper, item.lower);
                }

            }

            Console.WriteLine("Filtered non-equijoin:");

            foreach (var pair in resultJoinQuery2)
            {
                foreach (var item in pair)
                {
                    Console.WriteLine("{0} is matched to {1}", item.lower, item.upper);
                }
                
            }

            Console.WriteLine();
            #endregion

            #region demo6
            List<Student1> students1 = GetStudents();


            var booleanGroupQuery =
                from student in students1
                group student by student.Scores.Average() >= 80;

            var result6 = students1.GroupBy(s => s.Scores.Average() >= 80);

            //
            foreach (var studentGroup in result6)
            {
                Console.WriteLine(studentGroup.Key == true ? "High averages" : "Low averages");
                foreach (var student in studentGroup)
                {
                    Console.WriteLine("   {0}, {1}:{2}", student.Last, student.First, student.Scores.Average());
                }
            }
            Console.WriteLine();
            #endregion


            #region demo7
            var studentQuery =
                from student in students1
                let avg = (int)student.Scores.Average()
                group student by (avg / 10) into g
                orderby g.Key
                select g;

            var result7 = students1.Select(stu => new { stu, avg = (int)stu.Scores.Average() }).GroupBy(stu => stu.avg / 10).OrderBy(stu => stu.Key).Select(g => g);


            // Execute the query.
            foreach (var studentGroup in result7)
            {
                int temp = studentGroup.Key * 10;
                Console.WriteLine("Students with an average between {0} and {1}", temp, temp + 10);
                foreach (var student in studentGroup)
                {

                    Console.WriteLine("   {0}, {1}:{2}", student.stu.Last, student.stu.First, student.stu.Scores.Average());
                }
            }

            Console.WriteLine();
            #endregion


            #region demo8
            JoinDemonstration app = new JoinDemonstration();

            app.InnerJoin();
            app.GroupJoin();
            app.GroupInnerJoin();
            app.GroupJoin3();
            app.LeftOuterJoin();
            app.LeftOuterJoin2();



            #endregion

            #region demo9
            var studentsToXML = new XElement("Root",
             from student in students1
             let scores1 = string.Join(",", student.Scores)
             select new XElement("student",
                        new XElement("First", student.First),
                        new XElement("Last", student.Last),
                        new XElement("Scores", scores1)
                     )
                 );
            var result9 = students1.Select(stu => new { stu, scores1 = string.Join(",", stu.Scores) }).Select(stu =>
             new XElement("student",
                          new XElement("First", stu.stu.First),
                          new XElement("Last", stu.stu.Last),
                          new XElement("Scores", stu.scores1)
                       ));
            Console.WriteLine(studentsToXML);
            #endregion


            #region demo10

            MQ mq = new MQ();

            int[] nums = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };


            var myQuery1 = mq.QueryMethod1(ref nums);


            Console.WriteLine("Results of executing myQuery1:");

            foreach (string s in myQuery1)
            {
                Console.WriteLine(s);
            }


            Console.WriteLine("\nResults of executing myQuery1 directly:");

            foreach (string s in mq.QueryMethod1(ref nums))
            {
                Console.WriteLine(s);
            }

            IEnumerable<string> myQuery2;

            mq.QueryMethod2(ref nums, out myQuery2);


            Console.WriteLine("\nResults of executing myQuery2:");
            foreach (string s in myQuery2)
            {
                Console.WriteLine(s);
            }


            myQuery1 = from item in myQuery1
                       orderby item descending
                       select item;


            Console.WriteLine("\nResults of executing modified myQuery1:");
            foreach (string s in myQuery1)
            {
                Console.WriteLine(s);
            }
            #endregion

            Console.Read();
        }

        public static List<Student1> GetStudents()
        {

            List<Student1> students = new List<Student1>
            {
               new Student1 {First="Svetlana", Last="Omelchenko", ID=111, Scores= new List<int> {97, 72, 81, 60}},
               new Student1 {First="Claire", Last="O'Donnell", ID=112, Scores= new List<int> {75, 84, 91, 39}},
               new Student1 {First="Sven", Last="Mortensen", ID=113, Scores= new List<int> {99, 89, 91, 95}},
               new Student1 {First="Cesar", Last="Garcia", ID=114, Scores= new List<int> {72, 81, 65, 84}},
               new Student1 {First="Debra", Last="Garcia", ID=115, Scores= new List<int> {97, 89, 85, 82}}
            };
            return students;
        }
    }

    class MQ
    {
        public IEnumerable<string> QueryMethod1(ref int[] ints)
        {
            var intsToStrings = from i in ints
                                where i > 4
                                select i.ToString();
            return intsToStrings;
        }


        public void QueryMethod2(ref int[] ints, out IEnumerable<string> returnQ)
        {
            var intsToStrings = from i in ints
                                where i < 4
                                select i.ToString();
            returnQ = intsToStrings;
        }

    }
}
