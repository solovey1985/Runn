using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

using Runner.Services;
using Runner.Services.Models;

namespace Runner.Services.Test
{
    [TestFixture]
    public class ConfigurationServiceTests
    {
        private ConfigurationService configService;

        [OneTimeSetUp]
        public void Init()
        {
            configService = new ConfigurationService();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            configService.Dispose();
        }
        [Test]
        [TestCase("config.json")]
        public void ReadConfig_Should_GetValidConfig_When_FileExist(string path)
        {
            // Arrange


            // Act
            string fullPath = AppDomain.CurrentDomain.BaseDirectory;
            

            // Assert
            
            
        }

        [Test]
        public void WriteConfigToFile_Should_FileCreated_When_ConfigCorrect()
        {
            // Arrange
             

            // Act


            // Assert

        }
        [Test]
        public void UpdateConfigFile_Should_UpdateFile_When_ConfigWasChanged()
        {
            // Arrange


            // Act


            // Assert

        }

    }
}
