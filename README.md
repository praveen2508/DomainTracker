# DomainTracker
Domain Tracker Application is mainly used for system tracking purpose.it is developed in Visual studio 2019 version.
Framework Used:

>Asp.Net Core 2.2
>Web api core
>Fluent Validation

Features:
>Swagger Implementation
>Request Limit 
>Unit & Integration Test using JUnit(not written whole test)
>Validation Of IpAddress using fluent validation.
Services:
> Geo Location by passing IpAddress
> Ping Details by passing IpAddess
>Dns Information by passing IpAddress.


for access service use Url:
for Swagger Url :http://localhost:60287/swagger/index.html

Api Service Call:
http://localhost:60287/api/Ip/DomainDetails
example:Input paramameter
{
  "ipAddress": "172.217.160.142",
  "services": 10005
}

Services parameter data is enum
public enum ServiceData
    { 
        [Description("Geo")]
        Geo = 10001,
        [Description("Ping")]
        Ping = 10002,
        [Description("Dns")]
        Dns = 10003,
        [Description("Rdap")]
        Rdap = 10004,
        [Description("All")]
        All = 10005
    }
    
    
Response of Service :
{
  "geoData": {
    "latitude": "13.0827",
    "longititude": "80.2707",
    "regionName": "Tamil Nadu",
    "timeZone": "Asia/Kolkata",
    "countryCode": "IN",
    "ip": "172.217.160.142",
    "hostname": null,
    "city": "Chennai",
    "region": "TN",
    "country": "India",
    "postal": "600001"
  },
  "pingInfo": {
    "address": "172.217.160.142",
    "roundtripTime": 57,
    "ttl": 54,
    "dontFragment": false,
    "bufferLength": 32,
    "status": "Success"
  },
  "rdapInfo": null,
  "dnsInfo": {
    "hostName": "maa03s29-in-f14.1e100.net"
  },
  "result": null,
  "warningMessage": null,
  "errorMessage": null,
  "totalCount": null
}
