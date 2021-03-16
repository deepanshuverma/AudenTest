using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace AudenTest.Pages.Credit.ShortTermLoan
{
    public class PrivacyPromptModal
    {
        IWebDriver Driver;
        private By PrivacyPrompt = By.CssSelector(".privacy_prompt");
        private By btnAccept = By.CssSelector("#consent_prompt_submit");

        public PrivacyPromptModal(IWebDriver driver)
        {
            this.Driver = driver;
        }

        public void AcceptCookies()
        {
            try
            {
                Driver.FindElement(PrivacyPrompt).Click();
                Driver.FindElement(btnAccept).Click();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
