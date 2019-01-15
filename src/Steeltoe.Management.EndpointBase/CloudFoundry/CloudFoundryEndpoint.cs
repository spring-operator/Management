﻿// Copyright 2017 the original author or authors.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using Microsoft.Extensions.Logging;
using Steeltoe.Management.Endpoint.Discovery;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Steeltoe.Management.Endpoint.CloudFoundry
{
    public class CloudFoundryEndpoint : AbstractEndpoint<Links, string>
    {
        private ILogger<CloudFoundryEndpoint> _logger;
        private IManagementOptions _mgmtOption;

        public CloudFoundryEndpoint(ICloudFoundryOptions options, IEnumerable<IManagementOptions> mgmtOptions, ILogger<CloudFoundryEndpoint> logger = null)
        : base(options)
        {
            _mgmtOption = mgmtOptions.OfType<CloudFoundryManagementOptions>().Single();
            _logger = logger;
        }

        [Obsolete]
        public CloudFoundryEndpoint(ICloudFoundryOptions options, ILogger<CloudFoundryEndpoint> logger = null)
            : base(options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            _logger = logger;
        }

        protected new ICloudFoundryOptions Options => options as ICloudFoundryOptions;

        public override Links Invoke(string baseUrl)
        {
            DiscoveryService discoveryService = new DiscoveryService(_mgmtOption, _logger);
            return discoveryService.Invoke(baseUrl);
        }
    }
}
