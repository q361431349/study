using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp8
{
    class Program
    {
        static void Main(string[] args)
        {
            Expression<Func<int, int>> addFive = (num) => num + 5;

            if (addFive.NodeType == ExpressionType.Lambda)
            {
                var lambdaExp = (LambdaExpression)addFive;

                var parameter = lambdaExp.Parameters.First();

                Console.WriteLine(parameter.Name);
                Console.WriteLine(parameter.Type);
            }

            //1+2
            var one = Expression.Constant(1, typeof(int));
            var two = Expression.Constant(2, typeof(int));
            var addition = Expression.Add(one, two);

            Console.WriteLine(addition);


            Expression<Func<int>> add = () => 1 + 2;
            var func = add.Compile();

            var answer = func();
            Console.WriteLine(answer);


            var func1 = CreateBoundFunc();

            var answer1 = func1(new Random().Next(1, 99));
            Console.WriteLine(answer1);


            var constant = Expression.Constant(24, typeof(int));

            Console.WriteLine($"This is a/an {constant.NodeType} expression type");
            Console.WriteLine($"The type of the constant value is {constant.Type}");
            Console.WriteLine($"The value of the constant value is {constant.Value}");


            Expression<Func<int, int, int>> addition1 = (a, b) => a + b;

            Console.WriteLine($"This expression is a {addition1.NodeType} expression type");
            Console.WriteLine($"The name of the lambda is {((addition1.Name == null) ? "<null>" : addition1.Name)}");
            Console.WriteLine($"The return type is {addition1.ReturnType}");
            Console.WriteLine($"The expression has {addition1.Parameters.Count} arguments. They are:");
            foreach (var argumentExpression in addition1.Parameters)
            {
                Console.WriteLine($"\tParameter Type: {argumentExpression.Type}, Name: {argumentExpression.Name}");
            }

            var additionBody = (BinaryExpression)addition1.Body;
            Console.WriteLine($"The body is a {additionBody.NodeType} expression");
            Console.WriteLine($"The left side is a {additionBody.Left.NodeType} expression");
            var left = (ParameterExpression)additionBody.Left;
            Console.WriteLine($"\tParameter Type: {left.Type}, Name: {left.Name}");
            Console.WriteLine($"The right side is a {additionBody.Right.NodeType} expression");
            var right = (ParameterExpression)additionBody.Right;
            Console.WriteLine($"\tParameter Type: {right.Type}, Name: {right.Name}");


            Visitor visitor = new LambdaVisitor(addition1);
            visitor.Visit("");
            Visitor visitor1 = new BinaryVisitor((BinaryExpression)addition1.Body);
            visitor1.Visit("");


            var lambda = Expression.Lambda(
                Expression.Add(
                    Expression.Constant(1, typeof(int)),
                    Expression.Constant(2, typeof(int))
                )
            );


            Console.ReadKey();



        }

        private static Func<int, int> CreateBoundFunc()
        {
            var constant = new Resource();
            Expression<Func<int, int>> expression = (b) => constant.Argument + b;
            var rVal = expression.Compile();
            return rVal;

        }


    }

    public class Resource : IDisposable
    {
        private bool isDisposed = false;
        public int Argument
        {
            get
            {
                if (!isDisposed)
                    return 5;
                else throw new ObjectDisposedException("Resource");
            }
        }

        public void Dispose()
        {
            isDisposed = true;
        }
    }
}
