using System;
using NUnit.Framework;

namespace Absence_Duration_Kata
{
    [TestFixture]
    public class InputParserTests
    {
        [Test]
        public void ParseDateRangeFor1Day()
        {
            var startDate = new DateTime(2016, 1, 1);
            var endDate = new DateTime(2016, 1, 1);
            var dateRange = new DateRange(startDate, endDate);
            Assert.That(dateRange.ToInputString('X'), Is.EqualTo("X"));
        }

        [Test]
        public void ParseDateRangeFor5Days()
        {
            var startDate = new DateTime(2016, 1, 1);
            var endDate = new DateTime(2016, 1, 5);
            var dateRange = new DateRange(startDate, endDate);
            Assert.That(dateRange.ToInputString('X'), Is.EqualTo("XXXXX"));
        }
    }

    public static class InputParser
    {
        public static string ToInputString(this DateRange dateRange, char character)
        {
            var numberOfDays = (int)(dateRange.End - dateRange.Start).TotalDays + 1;
            return new string(character, numberOfDays);
        }
    }
}
