using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LongestWordChain;

namespace LongestWordChainCSharpTest
{
    //测试override equals能用
    [TestClass]
    public class CommandTest
    {
        [TestMethod]
        public void Command_Equals()
        {
            Command Test0 = new Command("-t", 'a');
            Command Test1 = new Command("-t", 'a');
            Command Test2 = new Command("-t", 'b');
            Command Test3 = null;
            string Test4 = "hey";

            bool result1 = Test1.Equals(Test2);
            Assert.IsFalse(result1);

            bool result2 = Test1.Equals(Test0);
            Assert.IsTrue(result2);

            bool result3 = Test1.Equals(Test3);
            Assert.IsFalse(result3);

            bool result4 = Test1.Equals(Test4);
            Assert.IsFalse(result4);

        }
    }
}
