﻿using MemcacheClient;
using Peacock.InWork4.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peacock.InWork4.Services
{
    public class ParametersServices:SingModel<OuterTaskService>
    {
        private static readonly ICacheClient MemcachedClient = CacheClient.GetInstance();

        private static readonly string ParameterCacheKey = "InworkParameters";
    }
}
