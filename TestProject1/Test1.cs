namespace TestProject1
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void TestMethod1()
        {
            // Arrange
            int expected = 1;
            int actual = 1;

            // Act & Assert
            Assert.AreEqual(expected, actual);

        }
    }
}
