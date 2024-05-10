using DataStructures.Lists.Stacks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Others
{
    /// <summary>
    /// 表达式求值
    /// </summary>
    public class ExpressionEvaluation
    {
        /// <summary>
        /// 左界限符
        /// </summary>
        public const string LeftDelimiter = "(";

        /// <summary>
        /// 右界限符
        /// </summary>
        public const string RightDelimiter = ")";

        /// <summary>
        /// 运算符
        /// </summary>
        public static List<string> Operators = new List<string>
        {
            "+", "-", "*", "/",
        };

        /// <summary>
        /// 运算数
        /// </summary>
        public static List<string> Operands = new List<string>
        {
            "0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
        };

        /// <summary>
        /// 优先级，越大越优先
        /// </summary>
        public static Dictionary<string, int> OperatorPriorities = new Dictionary<string, int>
        {
           {"(",0},
           {"+",1},
           {"-",1},
           {"*",2},
           {"/",2},
        };

        /// <summary>
        /// 根据表达式计算出值
        /// </summary>
        /// <param name="expr"></param>
        /// <returns></returns>
        public static decimal Calculate(string expr)
        {
            //运算数栈
            var operandStack = new LinkStack<decimal>();
            //运算符栈
            var operatorStack = new LinkStack<string>();
            //运算数
            var operand = string.Empty;

            //循环表达式进行计算
            for (int i = 0; i < expr.Length; i++)
            {
                var value = expr[i].ToString();

                if (value == " ")
                {
                    continue;
                }

                //如果是运算数,待组合完毕后再压入运算数栈
                if (Operands.Any(p => p == value))
                {
                    operand += value;
                    continue;
                }

                //压入运算数栈
                if (!string.IsNullOrEmpty(operand))
                {
                    operandStack.Push(decimal.Parse(operand));
                    operand = string.Empty;
                }

                //如果是左界限符或者运算符栈为空，则直接入运算符栈
                if (value == LeftDelimiter || operatorStack.Empty)
                {
                    operatorStack.Push(value);
                    continue;
                }

                //如果是右界限符，那么依次弹出栈内运算数和运算符进行计算，直到弹出左界限符为止
                if (value == RightDelimiter)
                {
                    var oper = operatorStack.Pop();
                    while (oper != LeftDelimiter)
                    {
                        var newValue = Calculate(operandStack, oper);
                        operandStack.Push(newValue);
                        oper = operatorStack.Pop();
                    }
                }
                //如果是运算符
                //依次弹出运算符栈内优先级高于或等于当前运算符的所有运算符，进行计算，直到遇到左界限符或者运算符栈空停止
                //最后再把当前运算符入运算符栈
                else
                {
                    var valuePriority = OperatorPriorities[value];
                    var stackTopPriority = OperatorPriorities[operatorStack.Top];
                    while (stackTopPriority >= valuePriority)
                    {
                        var oper = operatorStack.Pop();
                        var newValue = Calculate(operandStack, oper);
                        operandStack.Push(newValue);

                        if (oper == LeftDelimiter || operatorStack.Empty)
                        {
                            break;
                        }
                        stackTopPriority = OperatorPriorities[operatorStack.Top];
                    }
                    operatorStack.Push(value);
                }
            }

            //如果运算符栈还有值，则继续循环进行运算
            if (!string.IsNullOrEmpty(operand))
            {
                operandStack.Push(decimal.Parse(operand));
            }
            while (!operatorStack.Empty)
            {
                var oper = operatorStack.Pop();
                var newValue = Calculate(operandStack, oper);
                operandStack.Push(newValue);
            }

            return operandStack.Pop();
        }

        /// <summary>
        /// 根据操作符计算值
        /// </summary>
        /// <param name="operandStack"></param>
        /// <param name="oper"></param>
        /// <returns></returns>
        private static decimal Calculate(LinkStack<decimal> operandStack, string oper)
        {
            var right = operandStack.Pop();
            var left = operandStack.Pop();
            var value = default(decimal);
            switch (oper)
            {
                case "+":
                    {
                        value = left + right;
                        break;
                    }
                case "-":
                    {
                        value = left - right;
                        break;
                    }
                case "*":
                    {
                        value = left * right;
                        break;
                    }
                case "/":
                    {
                        value = left / right;
                        break;
                    }
                default:
                    break;
            }
            return value;
        }
    }
}
