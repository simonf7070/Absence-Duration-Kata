using NUnit.Framework;

namespace Absence_Duration_Kata
{
    [TestFixture]
    public class AbsenceTests
    {
        [TestCase("", "XXXXX00", 0)]
        [TestCase("X", "XXXXX00", 1)]
        [TestCase("XX", "XXXXX00", 2)]
        [TestCase("XXX", "XXXXX00", 3)]
        [TestCase("XXXX", "XXXXX00", 4)]
        [TestCase("XXXXX", "XXXXX00", 5)]
        [TestCase("XXXXXX", "XXXXX00", 5)]
        [TestCase("XXXXXXXXXX", "XXXXX00XXXXX00", 8)]
        [TestCase(" X", "XXXXX00", 1)]
        [TestCase("", "XXXX000", 0)]
        [TestCase("X", "XXXX000", 1)]
        public void GivenShiftsBookingDurationIsCalculated(string input, string pattern, int matches)
        {
            var durationCalculator = new DurationCalculator(input, pattern);
            Assert.That(durationCalculator.Duration(), Is.EqualTo(matches));
        }
    }

    public class DurationCalculator
    {
        private readonly string _booking;
        private readonly string _shifts;

        public DurationCalculator(string booking, string shifts)
        {
            _booking = booking;
            _shifts = shifts;
        }

        public int Duration()
        {
            var duration = 0;
            for (int i = 0; i < _booking.Length; i++)
            {
                if (_booking[i] == _shifts[i])
                {
                    duration++;
                }
            }
            return duration;
        }
    }
}
