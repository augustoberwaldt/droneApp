using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DroneApp;
using System.Linq;
namespace DroneAppTest
{
    [TestClass]
    public class DroneUnitTest
    {
        [TestMethod]
        public void Input_NNNNNLLLLL()
        {
            Tuple <int,int> point = Program.Evaluate("NNNNNLLLLL");
            Assert.AreEqual("(5,5)", string.Format("({0},{1})", point.Item1, point.Item2));
        }

        [TestMethod]
        public void Input_NLNLNLNLNL()
        {
            Tuple<int, int> point = Program.Evaluate("NLNLNLNLNL");
            Assert.AreEqual("(5,5)", string.Format("({0},{1})", point.Item1, point.Item2));
        }

        [TestMethod]
        public void Input_NNNNNXLLLLLX()
        {
            Tuple<int, int> point = Program.Evaluate("NNNNNXLLLLLX");
            Assert.AreEqual("(4,4)", string.Format("({0},{1})", point.Item1, point.Item2));
        }

        [TestMethod]
        public void Input_SSSSSOOOOO()
        {
            Tuple<int, int> point = Program.Evaluate("SSSSSOOOOO");
            Assert.AreEqual("(-5,-5)", string.Format("({0},{1})", point.Item1, point.Item2));
        }

        [TestMethod]
        public void Input_S5O5()
        {
            Tuple<int, int> point = Program.Evaluate("S5O5");
            Assert.AreEqual("(-5,-5)", string.Format("({0},{1})", point.Item1, point.Item2));
        }

        [TestMethod]
        public void Input_NNX2()
        {
            Tuple<int, int> point = Program.Evaluate("NNX2");
            Assert.AreEqual("(999,999)", string.Format("({0},{1})", point.Item1, point.Item2));
        }

        [TestMethod]
        public void Input_N123LSX()
        {
            Tuple<int, int> point = Program.Evaluate("N123LSX");
            Assert.AreEqual("(1,123)", string.Format("({0},{1})", point.Item1, point.Item2));
        }

        [TestMethod]
        public void Input_NLS3X()
        {
            Tuple<int, int> point = Program.Evaluate("NLS3X");
            Assert.AreEqual("(1,1)", string.Format("({0},{1})", point.Item1, point.Item2));
        }

        [TestMethod]
        public void Input_NNNXLLLXX()
        {
            Tuple<int, int> point = Program.Evaluate("NNNXLLLXX");
            Assert.AreEqual("(1,2)", string.Format("({0},{1})", point.Item1, point.Item2));
        }

        [TestMethod]
        public void Input_N40L30S20O10NLSOXX()
        {
            Tuple<int, int> point = Program.Evaluate("N40L30S20O10NLSOXX");
            Assert.AreEqual("(21,21)", string.Format("({0},{1})", point.Item1, point.Item2));
        }

        [TestMethod]
        public void Input_NLSOXXN40L30S20O10()
        {
            Tuple<int, int> point = Program.Evaluate("NLSOXXN40L30S20O10");
            Assert.AreEqual("(21,21)", string.Format("({0},{1})", point.Item1, point.Item2));
        }

        [TestMethod]
        public void Input_NULL()
        {
            Tuple<int, int> point = Program.Evaluate(null);
            Assert.AreEqual("(999,999)", string.Format("({0},{1})", point.Item1, point.Item2)); // Entrada nula
        }

        [TestMethod]
        public void Input_EMPTY()
        {
            Tuple<int, int> point = Program.Evaluate("");
            Assert.AreEqual("(999,999)", string.Format("({0},{1})", point.Item1, point.Item2)); // Entrada vazia
        }

        [TestMethod]
        public void Input_WHITESPACE()
        {
            Tuple<int, int> point = Program.Evaluate("   ");
            Assert.AreEqual("(999,999)", string.Format("({0},{1})", point.Item1, point.Item2)); // Entrada espaço vazio
        }

        [TestMethod]
        public void Input_123()
        {
            Tuple<int, int> point = Program.Evaluate("123");
            Assert.AreEqual("(999,999)", string.Format("({0},{1})", point.Item1, point.Item2)); // Entrada inválida
        }

        [TestMethod]
        public void Input_123N()
        {
            Tuple<int, int> point = Program.Evaluate("123N");
            Assert.AreEqual("(999,999)", string.Format("({0},{1})", point.Item1, point.Item2)); // passos antes da direçao
        }

        [TestMethod]
        public void Input_N2147483647N()
        {
            Tuple<int, int> point = Program.Evaluate("N2147483647N");
            Assert.AreEqual("(999,999)", string.Format("({0},{1})", point.Item1, point.Item2)); // Overflow
        }

        [TestMethod]
        public void Input_NNI()
        {
            Tuple<int, int> point = Program.Evaluate("NNI");
            Assert.AreEqual("(999,999)", string.Format("({0},{1})", point.Item1, point.Item2)); // Commando inválido
        }

        [TestMethod]
        public void Input_N2147483647XN()
        {
            Tuple<int, int> point = Program.Evaluate("N2147483647XN");
            Assert.AreEqual("(0,1)", string.Format("({0},{1})", point.Item1, point.Item2)); // Overflow cancelado
        }

        [TestMethod]
        public void Input_BIGSTRING()
        {
            string input = new string(
               Enumerable.Repeat('N', 1000).Concat(
               Enumerable.Repeat('S', 500)).Concat(
               Enumerable.Repeat('L', 1000)).Concat(
               Enumerable.Repeat('O', 500)).ToArray());

            Tuple<int, int> point = Program.Evaluate(input);

            Assert.AreEqual("(500,500)", string.Format("({0},{1})", point.Item1, point.Item2)) ;
        }
    }
}
