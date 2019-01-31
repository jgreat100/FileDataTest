using System;
using System.IO;
using Moq;
using Xunit;

namespace FileData.Tests
{
    public class ProgramTests
    {
        private readonly Mock<IFileDetailsService> mockFileDetailsService;

        public ProgramTests()
        {
            mockFileDetailsService = new Mock<IFileDetailsService>();
        }

        [Theory]
        [InlineData("Arg1")]
        [InlineData("Arg1", "Arg2", "Arg3")]
        public void Main_IncorrectNumberOfArgmentsSupplied_UserInformed(params string[] args)
        {
            // Arrange
            var expectedOutput = "Incorrect number of arguments specified, please supply an action and a file path\r\n";

            // Act
            var actualOutput = ActAndRetrieveMessage(args);

            // Assert
            Assert.Equal(expectedOutput, actualOutput);
        }

        [Fact]
        public void Main_UnknownActionSupplied_UserInformed()
        {
            // Arrange
            var action = "-t";
            var filePath = "c:\test.txt";
            var args = new string[] { action, filePath };

            var expectedOutput = "Action not recognised, allowed actions include -v, --v, /v, --version for the file version, " +
                "and -s, --s, /s, --size for the file size\r\n";

            // Act
            var actualOutput = ActAndRetrieveMessage(args);

            // Assert
            Assert.Equal(expectedOutput, actualOutput);
        }

        [Theory]
        [InlineData("-v")]
        [InlineData("--v")]
        [InlineData("/v")]
        [InlineData("--version")]
        public void Main_ValidSizeActionSupplied_SizeOutputToConsole(string action)
        {
            // Arrange
            var filePath = "c:\test.txt";
            var args = new string[] { action, filePath };
            var version = "1.0.0";

            mockFileDetailsService.Setup(x => x.Version(filePath)).Returns(version);
            var expectedOutput = $"File {filePath} is at version {version}\r\n";

            // Act
            var actualOutput = ActAndRetrieveMessage(args);

            // Assert
            Assert.Equal(expectedOutput, actualOutput);
        }

        [Theory]
        [InlineData("-s")]
        [InlineData("--s")]
        [InlineData("/s")]
        [InlineData("--size")]
        public void Main_ValidVersionActonSupplied_VersionOutputToConsole(string action)
        {
            // Arrange
            var filePath = "c:\test.txt";
            var args = new string[] { action, filePath };
            var size = 100;

            mockFileDetailsService.Setup(x => x.Size(filePath)).Returns(size);
            var expectedOutput = $"File {filePath} has size of {size} bytes\r\n";

            // Act
            var actualOutput = ActAndRetrieveMessage(args);

            // Assert
            Assert.Equal(expectedOutput, actualOutput);
        }


        private string ActAndRetrieveMessage(string[] args)
        {
            var message = string.Empty;
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                Program.Main(args);

                message = sw.ToString();
            }

            return message;
        }
    }
}
