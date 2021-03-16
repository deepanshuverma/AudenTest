using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace AudenTest.Pages.Credit.ShortTermLoan
{
    public class ShortTermLoan
    {
        private IWebDriver Driver;

        private By Slider = By.CssSelector(".loan-amount__range-slider__input");
        private By DateSlector = By.CssSelector(".date-selector > *");
        private By LoanSummary = By.CssSelector(".loan-summary > *");

        private By rdoBtn_FirstRepaymentCurrentMonth = By.CssSelector("label.loan-schedule__tab__panel__detail__tag__label:nth-child(1)");
        private By rdoBtn_FirstRepaymentNextMonth = By.CssSelector("label.loan-schedule__tab__panel__detail__tag__label:nth-child(2)");

        public ShortTermLoan(IWebDriver driver)
        {
            this.Driver = driver;
        }

        public bool SelectDate(string selectedDate)
        {
            var dates = Driver.FindElements(DateSlector);
            foreach (var date in dates)
            {
                if (!string.Equals(date.Text, selectedDate))
                {
                    continue;
                }
                date.Click();
                return true;
            }
            return false;
        }

        public (int, int) SetSliderAmount(int amount)
        {
            var slider = Driver.FindElement(Slider);

            new WebDriverWait(Driver, new TimeSpan(0, 0, 10)).
                Until(d => Convert.ToInt32(d.FindElement(Slider).GetAttribute("max")) > 0);

            var sliderMaxAmount = Convert.ToInt32(slider.GetAttribute("max"));

            var sliderMinAmount = Convert.ToInt32(slider.GetAttribute("min"));
            var pixels = GetPixelsToMove(slider, amount, sliderMaxAmount, sliderMinAmount);

            Driver.Manage().Window.Maximize();

            var sliderAction = new Actions(Driver);

            sliderAction.ClickAndHold(slider)
                .MoveByOffset((-(int)slider.Size.Width) / 2, 0)
                .MoveByOffset(pixels, 0).Release().Perform();

            return (sliderMaxAmount, sliderMinAmount);
        }

        public DateTime GetFirstPaymentDate()
        {
            return DateTime.Parse(Driver.FindElement(rdoBtn_FirstRepaymentNextMonth).Text);
        }

        public string GetLoanSummaryMatrix(string summaryAmount)
        {
            var summaryTable = Driver.FindElement(LoanSummary);
            var cols = Driver.FindElements(By.CssSelector("div.loan-summary__column"));
            foreach (var col in cols)
            {
                var rows = col.FindElements(By.CssSelector("span.loan-summary__column__amount__label"));
                foreach (var row in rows)
                {
                    if (string.Equals(row.Text, summaryAmount, StringComparison.CurrentCultureIgnoreCase))
                    {
                        var amount = col.FindElement(By.CssSelector("div.loan-summary__column__amount")).Text;
                        return amount;
                    }

                }
            }
            return null;
        }

        private int GetPixelsToMove(IWebElement element, decimal amount, decimal sliderMax, decimal sliderMin)
        {
            new WebDriverWait(Driver, new TimeSpan(0, 0, 10)).
                Until(d => d.FindElement(Slider).Size.Width > 0);

            decimal tempPixels = element.Size.Width;
            tempPixels /= sliderMax - sliderMin;
            tempPixels *= amount - sliderMin;
            return Convert.ToInt32(tempPixels);
        }




    }
}
