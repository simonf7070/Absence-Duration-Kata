using System;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace Absence_Duration_Kata
{
    [Binding]
    public class BookingFeatureSteps
    {
        [Given(@"I work Monday to Friday pattern specified in days")]
        public void GivenIWorkMondayToFridayInDays()
        {
            ScenarioContext.Current.Set("XXXXXOOXXXXXOO", "pattern");
        }

        [Given(@"I work Monday to Friday pattern specified in minutes")]
        public void GivenIWorkMondayToFridayInHours()
        {
            var hoursBeforeWork = new string('0', 60 * 9);
            var hoursWorking = new string('X', 60 * 8);
            var hoursAfterWork = new string('0', 60 * 7);
            var workingDay = hoursBeforeWork + hoursWorking + hoursAfterWork;
            var nonWorkingDay = new string('0', 60 * 24);
            var workingWeek = workingDay + workingDay + workingDay + workingDay + workingDay + nonWorkingDay + nonWorkingDay;
            ScenarioContext.Current.Set(workingWeek + workingWeek, "pattern");
        }

        [Given(@"a pattern of one ferry crossing every 3 days")]
        public void GivenAPatternOfFerryCrossingsPerDay()
        {
            ScenarioContext.Current.Set("00X00X00X00X00X00", "pattern");
        }

        [Given(@"a pattern of available delivery slots")]
        public void GivenAPatternOfDeliverySlotsPerDay()
        {
            ScenarioContext.Current.Set("00000000X000X0XXX0X000X000000000000000", "pattern");
        }

        [Given(@"I have booked off (.*) to (.*) in days")]
        [Given(@"I have want to book a ferry between (.*) to (.*)")]
        public void GivenIHaveBookedOffToDays(DateTime start, DateTime end)
        {
            var dateRange = new DateRange(start, end);
            ScenarioContext.Current.Set(dateRange.ToDayString(), "booking");
        }

        [Given(@"I want to get my shopping delivered on (.*)")]
        public void GivenIWantToGetMyShoppingDeliveredOn(DateTime date)
        {
            var deliveryDate = new string('X', 24);
            ScenarioContext.Current.Set(deliveryDate, "booking");
        }

        [Given(@"I have booked off (.*) to (.*) in minutes")]
        public void GivenIHaveBookedOffToHours(DateTime start, DateTime end)
        {
            var dateRange = new DateRange(start, end);
            ScenarioContext.Current.Set(dateRange.ToMinuteString(), "booking");
        }
        
        [When(@"I calculate the duration")]
        public void WhenICalculateTheDuration()
        {
            var pattern = ScenarioContext.Current.Get<string>("pattern");
            var booking = ScenarioContext.Current.Get<string>("booking");
            var bookingCalculator = new BookingCalculator(booking, pattern);
            ScenarioContext.Current.Set(bookingCalculator, "bookingCalculator");
        }

        [Then(@"the result should be (.*) days")]
        [Then(@"the result should be (.*) minutes")]
        [Then(@"the result should be (.*) crossings")]
        [Then(@"the result should be (.*) possible slots")]
        public void ThenTheResultDurationShouldBe(int duration)
        {
            var bookingCalculator = ScenarioContext.Current.Get<BookingCalculator>("bookingCalculator");
            Assert.That(bookingCalculator.Duration, Is.EqualTo(duration));
        }
    }
}
