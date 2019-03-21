﻿//
// Copyright 2017 the original author or authors.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// https://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.


using System;
using Xunit;
using Microsoft.Extensions.Configuration;
using Steeltoe.Management.Endpoint.Test;
using System.IO;

namespace Steeltoe.Management.Endpoint.Health.Contributor.Test
{
    public class DiskSpaceContributorOptionsTest
    {
        [Fact]
        public void Constructor_InitializesWithDefaults()
        {
            var opts = new DiskSpaceContributorOptions();
            Assert.Equal(".", opts.Path);
            Assert.Equal(10 * 1024 * 1024, opts.Threshold);
        }
        [Fact]
        public void Contstructor_ThrowsIfConfigNull()
        {
            IConfiguration config = null;
            Assert.Throws<ArgumentNullException>(() => new DiskSpaceContributorOptions(config));
        }

        [Fact]
        public void Contstructor_BindsConfigurationCorrectly()
        {
            var appsettings = @"
{
    'management': {
        'endpoints': {
            'health' : {
                'enabled': true, 
                'diskspace' : {
                    'path': 'foobar',
                    'threshold': 5
                }
            }
        }
    }
}";
            var path = TestHelpers.CreateTempFile(appsettings);
            string directory = Path.GetDirectoryName(path);
            string fileName = Path.GetFileName(path);
            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.SetBasePath(directory);

            configurationBuilder.AddJsonFile(fileName);
            var config = configurationBuilder.Build();

            var opts = new DiskSpaceContributorOptions(config);
            Assert.Equal("foobar", opts.Path);
            Assert.Equal(5, opts.Threshold);

        }
    }
}
