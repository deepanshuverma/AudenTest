using AudenTest.Configuration;
using AudenTest.Extensions;
using AudenTest.Pages.Credit.ShortTermLoan;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using TechTalk.SpecFlow;

namespace AudenTest.Steps
{
    [Binding]
    public class LoanCalulatorSteps
    {
        private static IWebDriver _driver;
        private ShortTermLoan _shortTermLoan;
        private PrivacyPromptModal _privacyPromptModal;
        private DateTime selectedDate;
        private int sliderMaxAmount;
        private int sliderMinAmount;

        [BeforeFeature]
        public static void setup()
        {
            _driver = new ChromeDriver();
            _driver.Manage().Window.Maximize();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

        }

        [AfterFeature]
        public static void tearDown()
        {
            if (_driver != null)
            {
                _driver.Close();
                _driver.Quit();
            }
        }

        [Given(@"The amount of (.*) has been selected as loan amount in slider")]
        public void GivenTheAmountOfHasBeenSelectedAsLoanAmountInSlider(int amountToBeSelected)
        {
            _privacyPromptModal = new PrivacyPromptModal(_driver);
            _shortTermLoan = new ShortTermLoan(_driver);

            _driver.Url = SetupConfiguration.AppSettings.BaseUrl;
            _privacyPromptModal.AcceptCookies();

            (sliderMaxAmount, sliderMinAmount) = _shortTermLoan.SetSliderAmount(amountToBeSelected);
        }

        [When(@"the date selected in repayment date selector is a Weekend")]
        public void WhenTheDateSelectedInRepaymentDateSelectorIsAWeekend()
        {
            selectedDate = DateTime.Now.AddMonths(1).GetNextDayDate(DayOfWeek.Saturday);

            _shortTermLoan.SelectDate(selectedDate.Day.ToString());
        }

        [Then(@"First repayment option is shown to the user is friday by default")]
        public void ThenFirstRepaymentOptionIsShownToTheUserIsFridayByDefault()
        {
            var firstPaymentData = _shortTermLoan.GetFirstPaymentDate();

            Assert.That(firstPaymentData.Date.Day, Is.EqualTo(selectedDate.AddDays(-1).Day));
        }

        [Then(@"Loan Amount in summary is equal to (.*)")]
        public void ThenLoanAmountInSummaryIsEqualTo(int amount)
        {
            var loanAmount = _shortTermLoan.GetLoanSummaryMatrix("Loan");
            var amt = int.Parse(loanAmount, System.Globalization.NumberStyles.Currency);
            Assert.That(amount, Is.EqualTo(amt));
        }

    }
}
