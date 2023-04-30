using Microsoft.EntityFrameworkCore;
using PhoneStore.BLL.Messages;
using PhoneStore.BLL.Services;
using PhoneStore.DAL.Data;
using PhoneStore.DAL.Models;
using System;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.Configuration;

namespace PhoneStore.UnitTests.BLL
{
    public class PhonesServiceTests
    {
        private readonly PhonesService _phonesService;

        public PhonesServiceTests()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            var dbContext = new ApplicationDbContext(dbOptions);

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            _phonesService = new PhonesService(dbContext, configuration);
        }
        [Fact]
        public async Task CreatePhone_Test()
        {
            var request = new SavePhoneRequest()
            {
                Phone = new Phone()
                {
                    Brand = "Samsung",
                    Model = "Galaxy",
                    Camera = 10,
                    Color = PhoneColor.Pink,
                    Memory = 60,
                    RAM = 16,
                    OS = "Android 10",
                    Price = 100,
                }
            };

            var isCreated = await _phonesService.CreatePhone(request);

            Assert.True(isCreated);
        }
    }
}
