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

using System.Collections;
using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using PPWCode.Util.Validation.I.European.Belgium;

namespace PPWCode.Util.Validation.I.UnitTests.European.Belgium
{
    [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "Test")]
    [SuppressMessage("ReSharper", "UnusedMember.Local", Justification = "Test")]
    public class VATTests : BaseTests
    {
        private static IEnumerable InvalidVATs
        {
            get
            {
                yield return null;
                yield return string.Empty;
                yield return "1";
                yield return "12341234";
            }
        }

        private static IEnumerable StrictValidVATs
        {
            get { yield return "0453834195"; }
        }

        private static IEnumerable ValidVATs
        {
            get
            {
                foreach (object vat in StrictValidVATs)
                {
                    yield return vat;
                }

                yield return "453834195";
                yield return "0453.834.195";
                yield return "BE 0453.834.195";
                yield return "BE 0453.834.195 Antwerp";
            }
        }

        private static IEnumerable PaperVersions
        {
            get
            {
                yield return new TestCaseData("0420936943").Returns("BE 0420.936.943");
                yield return new TestCaseData("BE 0420936943").Returns("BE 0420.936.943");
            }
        }

        [Test]
        [TestCaseSource(nameof(PaperVersions))]
        public string check_paperversion(string identification)
        {
            // Arrange
            VAT vat = new VAT(identification);

            // Act

            // Assert
            Assert.That(vat.IsValid, Is.True);
            Assert.That(vat.ElectronicVersion, Is.Not.Null);
            return vat.PaperVersion;
        }

        [Test]
        [TestCaseSource(nameof(InvalidVATs))]
        public void vat_is_not_valid(string identification)
        {
            // Arrange
            VAT vat = new VAT(identification);

            // Act

            // Assert
            Assert.That(vat.IsValid, Is.False);
            Assert.That(vat.IsStrictValid, Is.False);
            Assert.That(vat.ElectronicVersion, Is.Null);
            Assert.That(vat.PaperVersion, Is.Null);
        }

        [Test]
        [TestCaseSource(nameof(StrictValidVATs))]
        public void vat_is_strict_valid(string identification)
        {
            // Arrange
            VAT vat = new VAT(identification);

            // Act

            // Assert
            Assert.That(vat.IsValid, Is.True);
            Assert.That(vat.IsStrictValid, Is.True);
            Assert.That(vat.ElectronicVersion, Is.EqualTo(vat.CleanedVersion));
            Assert.That(vat.ElectronicVersion, Is.EqualTo(vat.RawVersion));
            Assert.That(vat.PaperVersion, Is.Not.Null);
        }

        [Test]
        [TestCaseSource(nameof(ValidVATs))]
        public void vat_is_valid(string identification)
        {
            // Arrange
            VAT vat = new VAT(identification);

            // Act

            // Assert
            Assert.That(vat.IsValid, Is.True);
            Assert.That(vat.ElectronicVersion, Is.Not.Null);
            Assert.That(vat.PaperVersion, Is.Not.Null);
        }
    }
}
