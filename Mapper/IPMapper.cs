using DomainModel;
using Models;
using System;

namespace Mapper
{
    public interface IIPMapper {
        GeoIpInfo Map(IpData data);
    }

    public class IPMapper : IIPMapper
    {
        public GeoIpInfo Map(IpData data)
        {
            GeoIpInfo geoData = new GeoIpInfo();
            if (data.status == "success")
            {
                geoData.Ip = data.query;
                geoData.Latitude = data.lat;
                geoData.Longititude = data.lon;
                geoData.Region = data.region;
                geoData.RegionName = data.regionName;
                geoData.TimeZone = data.timezone;
                geoData.Postal = data.zip;
                geoData.Country = data.country;
                geoData.CountryCode = data.countryCode;
                geoData.City = data.city;
;
            }
            return geoData;
        }
    }
}
