using DomainModel;
using Mapper;
using Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;


namespace Services
{
    public interface IDomainService
    {
        Task<IpData> GetGeoDetails(string ipAddress,string url);
        Task<PingData> GetPingDetails(string ipAddress);
        Task<RdapData> GetRdapDetails(string ipAddress);
        Task<DnsData> GetDnsDetails(string ipAddress);
    }
    public class DomainService : IDomainService
    {       
        public async Task<DnsData> GetDnsDetails(string ipAddress)
        {
            IPAddress address = IPAddress.Parse(ipAddress);
            DnsData dnsData = new DnsData();
            IPHostEntry host =await Dns.GetHostEntryAsync(address);
            dnsData.HostName=host.HostName;
            return dnsData;
        }

        public  async Task<IpData> GetGeoDetails(string ipAddress,string Url)
        {
            IpData geo = null;
            using (var client = new HttpClient()) {
                var result =await  client.GetAsync(Url+ipAddress);
                var response = JObject.Parse(await result.Content.ReadAsStringAsync());
                geo = JsonConvert.DeserializeObject<IpData>(response.ToString());            
            };
            return geo;


          
        }

        public async Task<PingData> GetPingDetails(string ipAddress)
        {
            PingData data = null;
            using (Ping pingSender = new Ping())
            {
                //var a = pingSender.SendPingAsync(ipAddress).GetAwaiter();
                PingReply reply = await pingSender.SendPingAsync(ipAddress);
                data = new PingData();
                data.Address = reply.Address.ToString();
                data.RoundtripTime = reply.RoundtripTime;
                data.DontFragment = reply.Options.DontFragment;
                data.Status = reply.Status.ToString();
                data.TTL = reply.Options.Ttl;
                data.BufferLength = reply.Buffer.Length;

            }

            return data;

        }

        public Task<RdapData> GetRdapDetails(string ipAddress)
        {
            return null;
        }


        static string  DoGetHostEntry(IPAddress address)
        {
            IPHostEntry host = Dns.GetHostEntry(address);
            return host.HostName;             
        }
    }
}
