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
            Assert.That(dateRange.ToDayString(), Is.EqualTo("X"));
        }

        [Test]
        public void ParseDateRangeFor5Days()
        {
            var startDate = new DateTime(2016, 1, 1);
            var endDate = new DateTime(2016, 1, 5);
            var dateRange = new DateRange(startDate, endDate);
            Assert.That(dateRange.ToDayString(), Is.EqualTo("XXXXX"));
        }

        [Test]
        public void ParseDateRangeFor1Minute()
        {
            var startDate = new DateTime(2016, 1, 1);
            var endDate = new DateTime(2016, 1, 1, 0, 1, 0);
            var dateRange = new DateRange(startDate, endDate);
            var expected = new string('X', 1);
            Assert.That(dateRange.ToMinuteString(), Is.EqualTo(expected));
        }

        [Test]
        public void ParseDateRangeFor68Minutes()
        {
            var startDate = new DateTime(2016, 1, 1);
            var endDate = new DateTime(2016, 1, 1, 1, 8, 0);
            var dateRange = new DateRange(startDate, endDate);
            var expected = new string('X', 68);
            Assert.That(dateRange.ToMinuteString(), Is.EqualTo(expected));
        }
    }

    public static class InputParser
    {
        public static string ToDayString(this DateRange dateRange)
        {
            var numberOfDays = (int)Math.Truncate((dateRange.End - dateRange.Start).TotalDays + 1);
            return new string('X', numberOfDays);
        }

        public static string ToMinuteString(this DateRange dateRange)
        {
            var numberOfMinutes = (int)Math.Truncate((dateRange.End - dateRange.Start).TotalMinutes);
            return new string('X', numberOfMinutes);
        }
    }
}
