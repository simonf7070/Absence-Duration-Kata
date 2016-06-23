using System.Linq;
using NUnit.Framework;

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
