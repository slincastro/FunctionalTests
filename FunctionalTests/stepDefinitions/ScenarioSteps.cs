using System;
using System.Linq;
using System.Runtime;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;
using Xunit;

namespace FunctionalTests.stepDefinitions
{
    [Binding]
    public class SalesOrdersSpecDefinitions : IDisposable
    {
        private IWebDriver _driver;
        private const string BasePath = "path of the application";

        [Given(@"I navigate to ""(.*)""")]
        public void GivenINavigateToTheSearch(string page)
        {
            _driver = new ChromeDriver(".");

           _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
           
           _driver.Navigate().GoToUrl(BasePath + page);
        }
        
        [Given(@"I have entered an sales number ""(.*)"" in the Search component")]
        public void GivenIHaveEnteredAValidOrderNumberInTheSearchComponentInLabNewApp(string orderNumber)
        {
            var searchOrder = _driver.FindElements(By.Id("txtSearchOrder")).FirstOrDefault();
            searchOrder?.SendKeys(orderNumber);
        }

        [When(@"I hit the Search")]
        public void WhenIHitTheSearch()
        {
            var searchOrder = _driver.FindElements(By.Id("btnSearchOrder")).FirstOrDefault();
            searchOrder?.Click();
        }
        
        [Then(@"I see all the items that belong to that order with the fields : ""(.*)""")]
        public void ThenISeeAllTheItemsThatBelongToThatInvoice()
        {
            var resultTable = _driver.FindElement(By.Id("gridSearchOrder"));
            Assert.True(resultTable.Enabled);
        }
        
        [Then(@"I see the message ""(.*)""")]
        public void ThenISeeTheMessage(string message)
        {
            var resultMessage = _driver.FindElement(By.Id("lblResultMessage"));
            Assert.Equal(resultMessage.Text,message);
        }
        
        [Then(@"I see the error message ""(.*)""")]
        public void ThenISeeTheErrorMessage(string message)
        {
            var resultMessage = _driver.FindElement(By.Id("lblValidationMessage"));
            Assert.Equal(resultMessage.Text,message);
        }
        
        public void Dispose()
        {
            _driver?.Dispose();
        }
    }
}