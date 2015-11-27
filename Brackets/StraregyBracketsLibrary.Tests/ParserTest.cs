using NUnit.Framework;

namespace StraregyBracketsLibrary.Tests
{
    public class ParserTest
    {
        [Test]
        public void TryParse_OnlyFioInput_Success()
        {
            var parser = new Parser();
            string fio;
            var result = parser.TryParse("Иванов Ваня Ванович", out fio);

            Assert.That(result, Is.True);
            Assert.That(fio, Is.EqualTo("Иванов Ваня Ванович"));
        }

        [Test]
        public void TryParse_FioWithIP_Success()
        {
            var parser = new Parser();
            string fio;
            var result = parser.TryParse("ИП Иванов Ваня Ванович", out fio);

            Assert.That(result, Is.True);
            Assert.That(fio, Is.EqualTo("Иванов Ваня Ванович"));
        }
        [Test]
        public void TryParse_FioWithChP_Success()
        {
            var parser = new Parser();
            string fio;
            var result = parser.TryParse("Иванов Ваня Ванович ЧП", out fio);

            Assert.That(result, Is.True);
            Assert.That(fio, Is.EqualTo("Иванов Ваня Ванович"));
        }
    }
}