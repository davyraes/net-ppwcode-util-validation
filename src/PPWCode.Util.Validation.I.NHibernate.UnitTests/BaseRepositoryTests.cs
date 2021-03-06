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

using System;
using PPWCode.Vernacular.NHibernate.I.Interfaces;
using PPWCode.Vernacular.Persistence.II;

namespace PPWCode.Util.Validation.I.NHibernate.UnitTests
{
    public abstract class BaseRepositoryTests<T> : BaseQueryTests
        where T : class, IIdentity<int>
    {
        protected abstract Func<ILinqRepository<T, int>> RepositoryFactory { get; }

        protected ILinqRepository<T, int> Repository { get; private set; }

        protected override void OnSetup()
        {
            base.OnSetup();

            Repository = RepositoryFactory();
            SessionFactory.Statistics.Clear();
        }

        protected override void OnTeardown()
        {
            Repository = null;

            base.OnTeardown();
        }
    }
}
