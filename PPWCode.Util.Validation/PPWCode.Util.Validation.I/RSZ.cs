﻿// Copyright 2017-2017 by PeopleWare n.v..
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
// 

namespace PPWCode.Util.Validation.I
{
    public class RSZ : AbstractIdentification
    {
        public RSZ(string rawVersion) : base(rawVersion)
        {
        }

        public override char PaddingCharacter => '0';

        public override int StandardLength => 10;

        public override string PaperVersion =>
            IsValid
                ? $"{CleanedVersion.Substring(0, CleanedVersion.Length - 2)}-{CleanedVersion.Substring(CleanedVersion.Length - 2)}"
                : null;

        protected override bool Validate(string identification)
        {
            if (identification != null && identification.Length == StandardLength)
            {
                var rest = 96 - long.Parse(identification.Substring(0, identification.Length - 2)) * 100 % 97;
                if (rest == 0)
                    rest = 97;

                return rest == long.Parse(identification.Substring(identification.Length - 2));
            }

            return false;
        }
    }
}