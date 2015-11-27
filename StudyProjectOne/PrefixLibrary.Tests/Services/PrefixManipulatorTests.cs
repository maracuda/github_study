using NUnit.Framework;
using PrefixLibrary.Services;

namespace PrefixLibrary.Tests.Services
{
    [TestFixture()]
    public class PrefixValidatorTests
    {
        [TestCase("10.0.0.0/24")]
        [TestCase("0.0.0.1/32")]
        [TestCase("10.0.0.84/30")]
        public void ValidatePrefixString_CorrectString_Success(string input_prefix)
        {
            var expected = "";

            var result = PrefixValidator.ValidatePrefixString(input_prefix);

            Assert.AreEqual(expected, result);
        }

        [Test()]
        public void ValidatePrefixString_InvalidNetwork_InvalidFormatMessage()
        {
            var prefix_to_test = "abc/24";
            var expected = "Неверный формат, используйте x.x.x.x/y";

            var result = PrefixValidator.ValidatePrefixString(prefix_to_test);

            Assert.AreEqual(expected, result);
        }

        [Test()]    
        public void ValidatePrefixString_InvalidPrefixLength_InvalidFormatMessage()
        {
            var prefix_to_test = "10.10.10.0/34";
            var expected = "Неверная длина префикса"; ;

            var result = PrefixValidator.ValidatePrefixString(prefix_to_test);

            Assert.AreEqual(expected, result);
        }

        [TestCase("10.0.0.12/24")]
        [TestCase("0.0.23.1/1")]
        [TestCase("10.0.0.85/30")]
        public void ValidatePrefixString_InvalidNetworkValue_InvalidFormatMessage(string input_prefix)
        {
            var expected = "Такой подсети не существует"; ;

            var result = PrefixValidator.ValidatePrefixString(input_prefix);

            Assert.AreEqual(expected, result);
        }

    }
}
