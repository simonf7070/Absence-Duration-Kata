using System;

namespace Absence_Duration_Kata
{
    public class DateRange
    {
        public readonly DateTime Start;
        public readonly DateTime End;

        public DateRange(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }
    }
}