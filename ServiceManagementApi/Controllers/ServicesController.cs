using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace ServiceManagementApi.Controllers
{
    public class ServicesController : ApiController
    {
        [HttpGet]
        [Route("api/Services")]
        public IHttpActionResult ExecuteService(string path)
        {
            var p = new Process();
            p.StartInfo.FileName = path;
            ServicesRepo.Services[path] = p;

            return Ok(p.Start());
        }

        [HttpGet]
        [Route("api/Services/Stop")]
        public IHttpActionResult StopService(string path)
        {
            ServicesRepo.Services[path].Kill();

            return Ok(ServicesRepo.Services[path].HasExited);
        }

        [HttpGet]
        [Route("api/Services/Stop")]
        public IHttpActionResult StopAllService()
        {
            foreach (var path in ServicesRepo.Services.Keys)
            {
                ServicesRepo.Services[path].Kill();
            }

            return Ok("It is done comrade general");
        }

        [HttpGet]
        [Route("api/Services/Restart")]
        public IHttpActionResult RestartAllService()
        {
            var status = new StringBuilder();
            foreach (var path in ServicesRepo.Services.Keys)
            {
                try
                {
                    ServicesRepo.Services[path].Kill();
                }
                catch (Exception ex)
                {
                    status.AppendLine($"'{path}' failed to terminate because: '{ex.Message}'");
                }
            }

            foreach (var path in ServicesRepo.Services.Keys)
            {
                try
                {
                    ServicesRepo.Services[path].Start();
                }
                catch (Exception ex)
                {
                    status.AppendLine($"'{path}' failed to start because: '{ex.Message}'");
                }
            }

            return Ok(status.ToString());
        }

        [HttpGet]
        [Route("api/Services/Status")]
        public IHttpActionResult StatusService(string path)
        {
            return Ok(ServicesRepo.Services[path].HasExited);
        }
    }
}
