using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChaseGame;

namespace GameTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestGameConstructorValid()
        {
            // arrange  
            int boardSizeX = 160;
            int boardSizeY = 90;
            ChaseGame chaseGame = new ChaseGame(boardSizeX, boardSizeY);

            // act  
            account.Debit(debitAmount);

            // assert  
            double actual = account.Balance;
            Assert.AreEqual(expected, actual, 0.001, "Account not debited correctly");  
        }
    }
}
