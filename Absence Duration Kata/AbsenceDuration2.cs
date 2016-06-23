using System;
using System.Collections.Generic;
using System.Linq;
using Absence_Duration_Kata.TestHelpers;
using NUnit.Framework;

namespace Absence_Duration_Kata
{
    [TestFixture]
    public class Class2Tests
    {
        [Test]
        public void ListMondayToFriday()
        {
            var shifts = ShiftFactory.MonToFri().Take(7).ToList();

            Assert.IsInstanceOf<WorkingDay>(shifts[0]);
            Assert.IsInstanceOf<WorkingDay>(shifts[1]);
            Assert.IsInstanceOf<WorkingDay>(shifts[2]);
            Assert.IsInstanceOf<WorkingDay>(shifts[3]);
            Assert.IsInstanceOf<WorkingDay>(shifts[4]);
            Assert.IsInstanceOf<DayOff>(shifts[5]);
            Assert.IsInstanceOf<DayOff>(shifts[6]);

            Assert.That(shifts.Sum(shift => shift.Duration), Is.EqualTo(5));
        }

        [Test]
        public void HolidayBookingIsSevenDays()
        {
            var holidayBooking = new HolidayBooking(new DateTime(2016, 5, 2), new DateTime(2016, 5, 8));
            Assert.That(holidayBooking.Days.Count(), Is.EqualTo(7));
        }

        [Test]
        public void HolidayBookingHasCorrectDuration()
        {
            var holidayBooking = new HolidayBooking(new DateTime(2016, 5, 2), new DateTime(2016, 5, 17));
            var monToFriShift = ShiftFactory.MonToFri();

            var bookingWithShifts = holidayBooking.WithShiftsApplied(monToFriShift);

            Assert.That(bookingWithShifts.Sum(x => x.Duration), Is.EqualTo(12));
        }

        [TestCase("X", 1)]
        [TestCase("XX", 2)]
        [TestCase(" XX", 2)]
        public void BookingShouldReturnCorrectDuration(string booking, int duration)
        {
            var dateRange = BookingConverter.ToDateRange(booking);
            var holidayBooking = new HolidayBooking(dateRange.Start, dateRange.End);
            var monToFriShift = ShiftFactory.MonToFri();

            var bookingWithShifts = holidayBooking.WithShiftsApplied(monToFriShift);

            Assert.That(bookingWithShifts.Sum(x => x.Duration), Is.EqualTo(duration));
        }

        [TestCase("X", 1)]
        [TestCase("XXXXXXX", 5)]
        [TestCase("XXXXXXXXXXXXXX", 10)]
        [TestCase("   XXX", 2)]
        [TestCase("  XXXXXXX", 5)]
        public void BookingWithMonFriShiftReturnsCorrectDuration(string booking, int duration)
        {
            var dateRange = BookingConverter.ToDateRange(booking);
            var holidayBooking = new HolidayBooking(dateRange.Start, dateRange.End);
            var monToFriShift = ShiftFactory.MonToFri().Skip(booking.IndexOf('X'));

            var bookingWithShifts = holidayBooking.WithShiftsApplied(monToFriShift);

            Assert.That(bookingWithShifts.Sum(x => x.Duration), Is.EqualTo(duration));
        }

        [TestCase("X", 1)]
        [TestCase("XXXXXXX", 4)]
        [TestCase("XXXXXXXXXXXXXX", 8)]
        [TestCase("   XXX", 1)]
        [TestCase("  XXXXXXX", 3)]
        public void BookingWith4On4OffShiftReturnsCorrectDuration(string booking, int duration)
        {
            var dateRange = BookingConverter.ToDateRange(booking);
            var holidayBooking = new HolidayBooking(dateRange.Start, dateRange.End);
            var fourOnFourOffShifts = ShiftFactory.FourOnFourOffShifts().Skip(booking.IndexOf('X'));

            var bookingWithShifts = holidayBooking.WithShiftsApplied(fourOnFourOffShifts);

            Assert.That(bookingWithShifts.Sum(x => x.Duration), Is.EqualTo(duration));
        }
    }

    public static class ShiftFactory
    {
        public static IEnumerable<Shift> MonToFri()
        {
            while (true)
            {
                yield return new WorkingDay();
                yield return new WorkingDay();
                yield return new WorkingDay();
                yield return new WorkingDay();
                yield return new WorkingDay();
                yield return new DayOff();
                yield return new DayOff();
            }
        }

        public static IEnumerable<Shift> FourOnFourOffShifts()
        {
            while (true)
            {
                yield return new WorkingDay();
                yield return new WorkingDay();
                yield return new WorkingDay();
                yield return new WorkingDay();
                yield return new DayOff();
                yield return new DayOff();
                yield return new DayOff();
                yield return new DayOff();
            }
        }
    }

    public abstract class Shift
    {
        public int Duration;
    }

    public class WorkingDay : Shift
    {
        public WorkingDay()
        {
            Duration = 1;
        }
    }

    public class DayOff : Shift
    {
        public DayOff()
        {
            Duration = 0;
        }
    }

    public class HolidayBooking
    {
        private DateTime Start { get; set; }
        private DateTime End { get; set; }

        public HolidayBooking(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }

        public IEnumerable<DateTime> Days
        {
            get
            {
                for (var day = Start; day <= End; day = day.AddDays(1))
                {
                    yield return day;
                }
            }
        }
    }

    public class Day
    {
        public DateTime Date;
        public int Duration;
    }

    public static class DateTimeExtension
    {
        public static IEnumerable<Day> WithShiftsApplied(this HolidayBooking holidayBooking, IEnumerable<Shift> shift)
        {
            return holidayBooking.Days.Zip(shift, (booking, shiftDays) => new Day { Date = booking.Date, Duration = shiftDays.Duration });
        }
    }
}
