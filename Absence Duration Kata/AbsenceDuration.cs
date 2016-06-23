using System.Linq;
using NUnit.Framework;
using System;

namespace Absence_Duration_Kata
{
    [TestFixture]
    public class AbsenceDurationTests
    {
        const string MonFriShifts = "XXXXXOOXXXXXOO";
        const string FourOnFourOffShifts = "XXXXOOOOXXXXOOOO";

        [TestCase("", 0)]
        [TestCase("X", 1)]
        [TestCase("XX", 2)]
        [TestCase(" ", 0)]
        [TestCase(" XX", 2)]
        public void BookingShouldReturnCorrectDuration(string booking, int duration)
        {
            var bookingCalculator = new BookingCalculator(booking, MonFriShifts);
            Assert.That(bookingCalculator.Duration, Is.EqualTo(duration));
        }

        [TestCase("X", 1)]
        [TestCase("XXXXXXX", 5)]
        [TestCase("XXXXXXXXXXXXXX", 10)]
        [TestCase("   XXX", 2)]
        [TestCase("  XXXXXXX", 5)]
        [TestCase("XX XX", 4)]
        public void BookingWithMonFriShiftReturnsCorrectDuration(string booking, int duration)
        {
            var bookingCalculator = new BookingCalculator(booking, MonFriShifts);
            Assert.That(bookingCalculator.Duration, Is.EqualTo(duration));
        }

        [TestCase("2016-01-01", "2016-01-01", 1)]
        [TestCase("2016-01-01", "2016-01-07", 5)]
        [TestCase("2016-01-01", "2016-01-14", 10)]
        public void ParsedBookingWithMonFriShiftReturnsCorrectDuration(DateTime start, DateTime end, int duration)
        {
            var booking = new DateRange(start, end).ToInputString();
            var bookingCalculator = new BookingCalculator(booking, MonFriShifts);
            Assert.That(bookingCalculator.Duration, Is.EqualTo(duration));
        }

        [TestCase("2016-01-01", "2016-01-03", "2016-01-04", "2016-01-06", 2)]
        [TestCase("2016-01-01", "2016-01-02", "2016-01-03", "2016-01-09", 5)]
        public void DelayedParsedBookingWithMonFriShiftReturnsCorrectDuration(DateTime delayStart, DateTime delayEnd, DateTime start, DateTime end, int duration)
        {
            var delay = new DateRange(delayStart, delayEnd).ToInputString(' ');
            var booking = new DateRange(start, end).ToInputString('X');
            var completeBooking = delay + booking;
            var bookingCalculator = new BookingCalculator(completeBooking, MonFriShifts);
            Assert.That(bookingCalculator.Duration, Is.EqualTo(duration));
        }

        [TestCase("X", 1)]
        [TestCase("XXXXXXX", 4)]
        [TestCase("XXXXXXXXXXXXXX", 8)]
        [TestCase("   XXX", 1)]
        [TestCase("  XXXXXXX", 3)]
        [TestCase("  X XX   XX", 3)]
        public void BookingWith4On4OffShiftReturnsCorrectDuration(string booking, int duration)
        {
            var bookingCalculator = new BookingCalculator(booking, FourOnFourOffShifts);
            Assert.That(bookingCalculator.Duration, Is.EqualTo(duration));
        }
    }

    public class BookingCalculator
    {
        private readonly string _booking;
        private readonly string _shifts;

        public BookingCalculator(string booking, string shifts)
        {
            _booking = booking;
            _shifts = shifts;
        }

        public int Duration
        {
            get
            {                
                return _booking.Where((day, i) => day == _shifts[i]).Count();
            }
        }
    }
}
