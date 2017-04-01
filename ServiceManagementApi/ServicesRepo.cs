using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace ServiceManagementApi
{
    public class ServicesRepo
    {
        public static Dictionary<string, Process> Services { get; set; } = new Dictionary<string, Process>();
    }
}