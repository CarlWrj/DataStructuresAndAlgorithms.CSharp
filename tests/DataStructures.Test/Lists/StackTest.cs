using DataStructures.Lists.Stacks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Test.Lists
{
    public class StackTest
    {
        [Fact]
        public static void SequenceStackTest()
        {
            var stack = new SequenceStack<int>(6);

            //入栈
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            stack.Push(4);
            stack.Push(5);
            stack.Push(6);
            Assert.Equal(6, stack.Top);

            //出栈
            stack.Pop();
            Assert.Equal(5, stack.Top);
            stack.Pop();
            stack.Pop();
            Assert.Equal(3, stack.Top);
        }

        [Fact]
        public static void LinkStackTest()
        {
            var stack = new LinkStack<int>();

            //入栈
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            stack.Push(4);
            stack.Push(5);
            stack.Push(6);
            Assert.Equal(6, stack.Top);

            //出栈
            stack.Pop();
            Assert.Equal(5, stack.Top);
            stack.Pop();
            stack.Pop();
            Assert.Equal(3, stack.Top);
        }
    }
}
