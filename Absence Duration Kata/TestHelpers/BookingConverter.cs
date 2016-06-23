using System;

namespace Absence_Duration_Kata.TestHelpers
{
    public static class BookingConverter
    {
        public static DateRange ToDateRange(string booking)
        {
            var mondayDate = new DateTime(2016, 5, 2);

            var start = mondayDate.AddDays(booking.IndexOf('X'));
            var end = mondayDate.AddDays(booking.LastIndexOf('X'));

            return new DateRange(start, end);
        }
    }
}