using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Absence_Duration_Kata
{
    [TestFixture]
    public class InputParserTests
    {
        [Test]
        public void ParseDateRange()
        {
            var dateRange = new DateRange(DateTime.Today, DateTime.Today.AddDays(5));
            Assert.That(dateRange.ToInputString(), Is.EqualTo("XXXXX"));
        }
    }

    public static class InputParser
    {
        public static string ToInputString(this DateRange dateRange)
        {
            var numberOfDays = (int)(dateRange.End - dateRange.Start).TotalDays;
            return new string('X', numberOfDays);
        }
    }
}
