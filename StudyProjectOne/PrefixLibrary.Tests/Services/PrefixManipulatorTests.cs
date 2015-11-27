using NUnit.Framework;
using PrefixLibrary.Services;

namespace PrefixLibrary.Tests.Services
{
    [TestFixture()]
    public class PrefixManipulatorTests
    {
        [Test()]
        public void ValidatePrefixString_CorrectString_Success()
        {
            var prefix_to_test = "0.0.0.1/32";
            var expected = "";

            var result = PrefixManipulator.ValidatePrefixString(prefix_to_test);

            Assert.AreEqual(expected, result);
        }

        [Test()]
        public void ValidatePrefixString_InvalidNetwork_InvalidFormatMessage()
        {
            var prefix_to_test = "abc/24";
            var expected = "Неверный формат, используйте x.x.x.x/y";

            var result = PrefixManipulator.ValidatePrefixString(prefix_to_test);

            Assert.AreEqual(expected, result);
        }

        [Test()]    
        public void ValidatePrefixString_InvalidPrefixLength_InvalidFormatMessage()
        {
            var prefix_to_test = "10.10.10.0/34";
            var expected = "Неверная длина префикса"; ;

            var result = PrefixManipulator.ValidatePrefixString(prefix_to_test);

            Assert.AreEqual(expected, result);
        }

        [Test()]
        public void ValidatePrefixString_InvalidNetworkValue_InvalidFormatMessage()
        {
            var prefix_to_test = "10.10.10.10/24";
            var expected = "Такой подсети не существует"; ;

            var result = PrefixManipulator.ValidatePrefixString(prefix_to_test);

            Assert.AreEqual(expected, result);
        }

    }
}
