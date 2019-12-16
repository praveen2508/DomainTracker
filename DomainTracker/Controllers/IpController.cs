using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Mapper;
using DomainModel;
using System.Diagnostics;

namespace DomainTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IpController : ControllerBase
    {
        private readonly IValidator<DomainRequest> _ipRequestValidator;

        private readonly IErrorMapper _errorMapper;
        private readonly IDomainService _domainService;
        private readonly IConfiguration _configuration;
        private readonly IIPMapper _ipMapper;
        private readonly IPingMapper _ipingMapper;
        private readonly IDnsMapper _idnsMapper;
        private readonly IRdapMapper _irdapMapper;

        public IpController(IValidator<DomainRequest> ipRequestValidator, IErrorMapper errorMapper, IDomainService domainService,
            IConfiguration iConfig, IIPMapper ipMapper, IDnsMapper idnsMapper, IPingMapper ipingMapper, IRdapMapper irdapMapper)
        {
            //_logger = logger;
            _ipRequestValidator = ipRequestValidator;
            _errorMapper = errorMapper;
            _domainService = domainService;
            _configuration = iConfig;
            _ipMapper = ipMapper;
            _idnsMapper = idnsMapper;
            _ipingMapper = ipingMapper;
            _irdapMapper = irdapMapper;
        }

        [HttpPost("DomainDetails")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IpResponse>> Post([FromBody] DomainRequest domain)
        {
            List<ResultDetail> resultDetailList;
            IpResponse ipResponse = new IpResponse();

            ValidationResult validationResult = _ipRequestValidator.Validate(domain);
            // Errors will never be null.
            if (validationResult.Errors.Any())
            {
                resultDetailList = _errorMapper.Map(validationResult);
                ipResponse = new IpResponse
                {
                    Result = new Result
                    {
                        ResultCode = "Warning",
                        ResultDetailList = resultDetailList
                    }
                };

                return StatusCode(400, ipResponse);
            }

            try
            {
                var geoUrl = _configuration.GetSection("ServicesUrl").GetSection("GeoUrl").Value;
                switch (domain.services)
                {
                    case ServiceData.Dns:
                        var dnsData = await _domainService.GetDnsDetails(domain.IpAddress);
                        ipResponse.DnsInfo = _idnsMapper.Map(dnsData);
                        break;
                    case ServiceData.Geo:
                        var geoData = await _domainService.GetGeoDetails(domain.IpAddress, geoUrl);
                        ipResponse.GeoData = _ipMapper.Map(geoData);
                        break;
                    case ServiceData.Ping:
                        var data = await _domainService.GetPingDetails(domain.IpAddress);
                        ipResponse.PingInfo = _ipingMapper.Map(data);
                        break;
                    case ServiceData.Rdap:
                        var rdapData = await _domainService.GetRdapDetails(domain.IpAddress);
                        ipResponse.RdapInfo = _irdapMapper.Map(rdapData);
                        break;
                    default:
                        ipResponse = await ExecuteAllServiceAsync(domain.IpAddress, geoUrl);
                        break;
                }
            }
            catch (Exception exception)
            {

                if (exception?.InnerException?.Data?.Values.Count > 0)
                {
                    var exceptionData = (List<Error>)(exception.InnerException.Data["Error"]);
                    resultDetailList = _errorMapper.Map(exceptionData);
                }
                else
                {
                    ResultDetail resultDetail = new ResultDetail
                    {
                        Message = exception.Message
                    };
                    resultDetailList = new List<ResultDetail>
                    {
                        resultDetail
                    };
                }

                ipResponse = SetErrorResponseForPost(resultDetailList);

                return StatusCode(500, ipResponse);
            }

            return ipResponse;
        }
        private static IpResponse SetErrorResponseForPost(List<ResultDetail> resultDetailList)
        {
            return new IpResponse
            {
                Result = new Result
                {
                    ResultCode = "ERROR",
                    ResultDetailList = resultDetailList
                }
            };
        }

        private Task<IpResponse> ExecuteAllServiceAsync(string ipAddress, string url)
        {
            IpResponse response = new IpResponse();

            //var dnsTask = _domainService.GetDnsDetails(ipAddress).Result;
            IpData geoTask;
            DnsData dnsTask;
            PingData pingTask;
            Timestamp timestamp = new Timestamp();

            Parallel.Invoke(
                () =>
                {
                    geoTask = _domainService.GetGeoDetails(ipAddress, url).Result;
                    response.GeoData = _ipMapper.Map(geoTask);
                    timestamp.GeoTime = DateTime.Now;
                },
                () =>
                {
                    dnsTask = _domainService.GetDnsDetails(ipAddress).Result;
                    response.DnsInfo = _idnsMapper.Map(dnsTask);
                    timestamp.DnsTime = DateTime.Now;
                },
                () =>
                {
                    pingTask = _domainService.GetPingDetails(ipAddress).Result;
                    response.PingInfo = _ipingMapper.Map(pingTask);
                    timestamp.PingTime = DateTime.Now;
                }
            );

           
            return Task.FromResult<IpResponse>(response) ;

        }
    }

    public class Timestamp
    {
        public DateTime GeoTime { get; set; }
        public DateTime PingTime { get; set; }
        public DateTime DnsTime { get; set; }
    }
}
