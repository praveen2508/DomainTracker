using DomainModel;
using Mapper;
using Shouldly;
using System;
using Xunit;

namespace UnitTests
{
    public class GeoIpMapperTest
    {
        [Fact]
        public void ShouldReturnIpInfomationWhenIpIsValid()
        {
            // Arrange
            IpData data = new IpData
            {
                city = "Landon",
                country = "UK",
                countryCode = "UK",
                lon = "21.21",
                lat = "78.28",
                query = "123.32.32.21",
                region = "North-West",
                status = "success",
                timezone = "Western",
                zip = "12345"
            };
            var ipMapper = new IPMapper();

            // Act
            var response = ipMapper.Map(data);

            // Assert
            response.ShouldNotBeNull();
            response.City.ShouldBe(data.city);
        }
    }
}
