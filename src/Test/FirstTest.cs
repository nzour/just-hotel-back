using NUnit.Framework;

namespace app.Test
{
    [TestFixture]
    public class FirstTest
    {
        [Test]
        public void Index()
        {
            const bool condition = true;
            Assert.IsTrue(condition);
        }
    }
}