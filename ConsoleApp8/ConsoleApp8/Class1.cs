using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp8
{
    public abstract class Visitor
    {
        private readonly Expression node;

        protected Visitor(Expression node)
        {
            this.node = node;
        }

        public abstract void Visit(string prefix);

        public ExpressionType NodeType => node.NodeType;
        public static Visitor CreateFromExpression(Expression node)
        {
            switch (node.NodeType)
            {
                case ExpressionType.Constant:
                    return new ConstantVisitor((ConstantExpression)node);
                case ExpressionType.Lambda:
                    return new LambdaVisitor((LambdaExpression)node);
                case ExpressionType.Parameter:
                    return new ParameterVisitor((ParameterExpression)node);
                case ExpressionType.Add:
                case ExpressionType.Equal:
                case ExpressionType.Multiply:
                    return new BinaryVisitor((BinaryExpression)node);
                case ExpressionType.Conditional:
                    return new ConditionalVisitor((ConditionalExpression)node);
                case ExpressionType.Call:
                    return new MethodCallVisitor((MethodCallExpression)node);
                default:
                    Console.Error.WriteLine($"Node not processed yet: {node.NodeType}");
                    return default;
            }
        }
    }

   
    public class LambdaVisitor : Visitor
    {
        private readonly LambdaExpression node;
        public LambdaVisitor(LambdaExpression node) : base(node)
        {
            this.node = node;
        }

        public override void Visit(string prefix)
        {
            Console.WriteLine($"{prefix}This expression is a {NodeType} expression type");
            Console.WriteLine($"{prefix}The name of the lambda is {node.Name ?? "<null>"}");
            Console.WriteLine($"{prefix}The return type is {node.ReturnType}");
            Console.WriteLine($"{prefix}The expression has {node.Parameters.Count} argument(s). They are:");
          
            foreach (var argumentExpression in node.Parameters)
            {
                var argumentVisitor = CreateFromExpression(argumentExpression);
                argumentVisitor.Visit(prefix + "\t");
            }
            Console.WriteLine($"{prefix}The expression body is:");
           
            var bodyVisitor = CreateFromExpression(node.Body);
            bodyVisitor.Visit(prefix + "\t");
        }
    }

    
    public class BinaryVisitor : Visitor
    {
        private readonly BinaryExpression node;
        public BinaryVisitor(BinaryExpression node) : base(node)
        {
            this.node = node;
        }

        public override void Visit(string prefix)
        {
            Console.WriteLine($"{prefix}This binary expression is a {NodeType} expression");
            var left = CreateFromExpression(node.Left);
            Console.WriteLine($"{prefix}The Left argument is:");
            left.Visit(prefix + "\t");
            var right = CreateFromExpression(node.Right);
            Console.WriteLine($"{prefix}The Right argument is:");
            right.Visit(prefix + "\t");
        }
    }


    public class ParameterVisitor : Visitor
    {
        private readonly ParameterExpression node;
        public ParameterVisitor(ParameterExpression node) : base(node)
        {
            this.node = node;
        }

        public override void Visit(string prefix)
        {
            Console.WriteLine($"{prefix}This is an {NodeType} expression type");
            Console.WriteLine($"{prefix}Type: {node.Type}, Name: {node.Name}, ByRef: {node.IsByRef}");
        }
    }

  
    public class ConstantVisitor : Visitor
    {
        private readonly ConstantExpression node;
        public ConstantVisitor(ConstantExpression node) : base(node)
        {
            this.node = node;
        }

        public override void Visit(string prefix)
        {
            Console.WriteLine($"{prefix}This is an {NodeType} expression type");
            Console.WriteLine($"{prefix}The type of the constant value is {node.Type}");
            Console.WriteLine($"{prefix}The value of the constant value is {node.Value}");
        }
    }

    public class ConditionalVisitor : Visitor
    {
        private readonly ConditionalExpression node;
        public ConditionalVisitor(ConditionalExpression node) : base(node)
        {
            this.node = node;
        }

        public override void Visit(string prefix)
        {
            Console.WriteLine($"{prefix}This expression is a {NodeType} expression");
            var testVisitor = CreateFromExpression(node.Test);
            Console.WriteLine($"{prefix}The Test for this expression is:");
            testVisitor.Visit(prefix + "\t");
            var trueVisitor = CreateFromExpression(node.IfTrue);
            Console.WriteLine($"{prefix}The True clause for this expression is:");
            trueVisitor.Visit(prefix + "\t");
            var falseVisitor = CreateFromExpression(node.IfFalse);
            Console.WriteLine($"{prefix}The False clause for this expression is:");
            falseVisitor.Visit(prefix + "\t");
        }
    }

    public class MethodCallVisitor : Visitor
    {
        private readonly MethodCallExpression node;
        public MethodCallVisitor(MethodCallExpression node) : base(node)
        {
            this.node = node;
        }

        public override void Visit(string prefix)
        {
            Console.WriteLine($"{prefix}This expression is a {NodeType} expression");
            if (node.Object == null)
                Console.WriteLine($"{prefix}This is a static method call");
            else
            {
                Console.WriteLine($"{prefix}The receiver (this) is:");
                var receiverVisitor = CreateFromExpression(node.Object);
                receiverVisitor.Visit(prefix + "\t");
            }

            var methodInfo = node.Method;
            Console.WriteLine($"{prefix}The method name is {methodInfo.DeclaringType}.{methodInfo.Name}");
            
            Console.WriteLine($"{prefix}The Arguments are:");
            foreach (var arg in node.Arguments)
            {
                var argVisitor = CreateFromExpression(arg);
                argVisitor.Visit(prefix + "\t");
            }
        }
    }
}
