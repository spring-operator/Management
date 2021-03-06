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

using OpenCensus.Trace.Propagation;

namespace Steeltoe.Management.Census.Trace.Propagation
{
    public sealed class B3PropagationComponent : PropagationComponentBase
    {
        private readonly ThrowsBinaryFormat binaryFormat = new ThrowsBinaryFormat();
        private readonly B3Format textFormat = new B3Format();

        /// <inheritdoc/>
        public override IBinaryFormat BinaryFormat
        {
            get
            {
                return this.binaryFormat;
            }
        }

        /// <inheritdoc/>
        public override ITextFormat TextFormat
        {
            get
            {
                return this.textFormat;
            }
        }
    }
}
