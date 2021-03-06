// Copyright (c) Microsoft Corporation.  All rights reserved.
// MIT License
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the ""Software""), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED *AS IS*, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using Microsoft.HealthVault.ItemTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NodaTime;

namespace Microsoft.HealthVault.UnitTest.ItemTypes
{
    [TestClass]
    public class ApproximateDateTimeTests
    {
        [TestMethod]
        public void WhenWrongTypePassedToEquals_ThenFalseReturned()
        {
            ApproximateDateTime dateTime = new ApproximateDateTime();

            Assert.IsFalse(dateTime.Equals(this), "Equals should return false.");
        }

        [TestMethod]
        public void WhenNullPassedToEquals_ThenFalseReturned()
        {
            ApproximateDateTime dateTime = new ApproximateDateTime();

            Assert.IsFalse(dateTime.Equals(null), "Equals should return false.");
        }

        [TestMethod]
        public void WhenSameDatePassedToEquals_ThenTrueReturned()
        {
            ApproximateDate date = new ApproximateDate(2017);

            ApproximateDateTime dateTime1 = new ApproximateDateTime(date);
            ApproximateDateTime dateTime2 = new ApproximateDateTime(date);

            Assert.IsTrue(dateTime1.Equals(dateTime2), "Equals should return true.");
        }

        [TestMethod]
        public void WhenDifferentDatePassedToEquals_ThenFalseReturned()
        {
            ApproximateDateTime dateTime1 = new ApproximateDateTime(new LocalDateTime(2017, 5, 18, 5, 28, 20));
            ApproximateDateTime dateTime2 = new ApproximateDateTime(new LocalDateTime(2017, 5, 18, 5, 27, 20));

            Assert.IsFalse(dateTime1.Equals(dateTime2), "Equals should return false.");
        }

        [TestMethod]
        public void WhenSameDateTimePassedToEquals_ThenTrueReturned()
        {
            ApproximateDateTime approximateDateTime = new ApproximateDateTime(new LocalDateTime(2017, 5, 18, 5, 28, 20));
            LocalDateTime dateTime = new LocalDateTime(2017, 5, 18, 5, 28, 20);

            Assert.IsTrue(approximateDateTime.Equals(dateTime), "Equals should return true");
        }

        [TestMethod]
        public void WhenDifferentDateTimePassedToEquals_ThenFalseReturned()
        {
            ApproximateDateTime approximateDateTime = new ApproximateDateTime(new LocalDateTime(2017, 5, 18, 5, 28, 20));
            LocalDateTime dateTime = new LocalDateTime(2017, 5, 18, 5, 28, 24);

            Assert.IsFalse(approximateDateTime.Equals(dateTime), "Equals should return false");
        }
    }
}
