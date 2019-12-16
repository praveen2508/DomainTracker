using DomainTracker.Controllers;
using FluentValidation;
using Mapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models;
using Services;
using Shouldly;
using System;
using Xunit;

namespace IntegrationTests
{
    public class IpControllerTest
    {
        private readonly IpController _ipController;
        private readonly IValidator<DomainRequest> _ipRequestValidator;
        private readonly IErrorMapper _errorMapper;
        private readonly IDomainService _domainService;
        private readonly IIPMapper _ipMapper;
        private readonly IPingMapper _ipingMapper;
        private readonly IDnsMapper _idnsMapper;
        private readonly IRdapMapper _irdapMapper;
        private readonly IConfiguration _iconfig;
        public IpControllerTest()
        {
            _domainService = new DomainService();
            _ipRequestValidator = new DomainValidator();
            _ipMapper = new IPMapper();
            _errorMapper = new ErrorMapper();
            _ipingMapper = new PingMapper();
            _idnsMapper = new DnsMapper();
            _irdapMapper = new RdapMapper();
            _ipController = new IpController(_ipRequestValidator, _errorMapper, _domainService
                , _iconfig, _ipMapper, _idnsMapper, _ipingMapper, _irdapMapper);


            _ipController.ControllerContext = new ControllerContext();
            _ipController.ControllerContext.HttpContext = new DefaultHttpContext();
        }

        [Fact]
        public void ShouldReturnDnsIPAndPingData()
        {
            // Arrange
            DomainRequest request = new DomainRequest
            {
                IpAddress = "172.217.160.142",
                services = ServiceData.Geo
            };
            // Act
            var response = _ipController.Post(request);

            // Assert
            response.ShouldNotBeNull();
        }
    }
}
