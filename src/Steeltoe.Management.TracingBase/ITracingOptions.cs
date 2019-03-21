﻿// Copyright 2017 the original author or authors.
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

namespace Steeltoe.Management.Tracing
{
    public interface ITracingOptions
    {
        string Name { get; }

        string IngressIgnorePattern { get; }

        string EgressIgnorePattern { get; }

        int MaxNumberOfAttributes { get; }

        int MaxNumberOfAnnotations { get; }

        int MaxNumberOfMessageEvents { get; }

        int MaxNumberOfLinks { get; }

        bool AlwaysSample { get; }

        bool NeverSample { get;  }

        bool UseShortTraceIds { get; }
    }
}
