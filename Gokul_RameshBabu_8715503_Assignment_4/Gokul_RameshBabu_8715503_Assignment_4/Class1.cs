using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using SeleniumExtras.WaitHelpers;
using NUnit.Framework;

namespace Gokul_RameshBabu_8715503_Assignment_4
{
    [TestFixture]
    public class SampletestTest
    {
        private IWebDriver driver;
        public IDictionary<string, object> vars { get; private set; }
        private IJavaScriptExecutor js;
        private ChromeOptions options = new ChromeOptions();

        //Modify the URL to test the application
        private static string BASE_URL = "http://127.0.0.1:5500/Assignment%204/";
        [SetUp]
        public void SetUp()
        {


            js = (IJavaScriptExecutor)driver;
            vars = new Dictionary<string, object>();
            options.AddArgument("--disable-notifications");
            driver = new ChromeDriver(options);
        }
        [TearDown]
        protected void TearDown()
        {
            driver.Quit();
        }

        [Test, Order(1)]
        public void newUserRegistrationWithEmptyFields()
        {
            driver.Navigate().GoToUrl(BASE_URL + "index.html");
            driver.FindElement(By.Name("submit")).Click();
            var expectedAlertText = "All fields are mandatory!";
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IAlert alert = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());
            Assert.That(alert.Text, Is.EqualTo(expectedAlertText));
            alert.Accept();
        }

        [Test, Order(2)]
        public void newUserRegistrationMandatoryNotes()
        {
            driver.Navigate().GoToUrl(BASE_URL + "index.html");
            driver.Manage().Window.Size = new System.Drawing.Size(1440, 900);
            driver.FindElement(By.Name("firstName")).Click();
            driver.FindElement(By.Name("firstName")).SendKeys("Gokul");
            driver.FindElement(By.Name("lastName")).SendKeys("Ramesh Babu");
            driver.FindElement(By.Name("address")).SendKeys("14 Beachsurf Road, Brampton, Ontario - L6R 2R4");
            {
                var dropdown = driver.FindElement(By.Name("city"));
                dropdown.FindElement(By.XPath("//option[. = 'Brampton']")).Click();
            }
            {
                var dropdown = driver.FindElement(By.Name("province"));
                dropdown.FindElement(By.XPath("//option[. = 'Yukon']")).Click();
            }
            driver.FindElement(By.Name("postalCode")).Click();
            driver.FindElement(By.Name("postalCode")).SendKeys("2R4L6R");
            driver.FindElement(By.Name("phoneNumber")).Click();
            driver.FindElement(By.Name("phoneNumber")).SendKeys("905-226-1853");
            driver.FindElement(By.Name("email")).Click();
            driver.FindElement(By.Name("email")).SendKeys("gokulpulari@gmail.com");
            driver.FindElement(By.Name("submit")).Click();
            var expectedAlertText = "All fields are mandatory!";
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IAlert alert = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());
            Assert.That(alert.Text, Is.EqualTo(expectedAlertText));
            alert.Accept();
        }

        [Test, Order(3)]
        public void newUserRegistrationWithIncorrectPostalCode()
        {
            driver.Navigate().GoToUrl(BASE_URL + "index.html");
            driver.FindElement(By.Name("firstName")).Click();
            driver.FindElement(By.Name("firstName")).SendKeys("Gokul");
            driver.FindElement(By.Name("lastName")).SendKeys("Ramesh Babu");
            driver.FindElement(By.Name("address")).SendKeys("14 Beachsurf Road, Brampton, Ontario - L6R 2R4");
            {
                var dropdown = driver.FindElement(By.Name("city"));
                dropdown.FindElement(By.XPath("//option[. = 'Brampton']")).Click();
            }
            {
                var dropdown = driver.FindElement(By.Name("province"));
                dropdown.FindElement(By.XPath("//option[. = 'Yukon']")).Click();
            }
            driver.FindElement(By.Name("postalCode")).SendKeys("L6R2R4");
            driver.FindElement(By.Name("phoneNumber")).Click();
            driver.FindElement(By.Name("phoneNumber")).SendKeys("905-226-1853");
            driver.FindElement(By.Name("email")).Click();
            driver.FindElement(By.Name("email")).SendKeys("gokulpulari@gmail.com");
            driver.FindElement(By.Name("notes")).Click();
            driver.FindElement(By.Name("notes")).SendKeys("Test notes.");
            driver.FindElement(By.Name("submit")).Click();

            var expectedAlertText = "Postal Code (Acceptable format: NAN ANA – where 'A' is any alphabetic character and 'N' is avalid numeric digit)";
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IAlert alert = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());
            Assert.That(alert.Text, Is.EqualTo(expectedAlertText));
            alert.Accept();
        }

        [Test, Order(4)]
        public void newUserRegistrationWithIncorrectPhoneNumber()
        {
            driver.Navigate().GoToUrl(BASE_URL + "index.html");
            driver.FindElement(By.Name("firstName")).Click();
            driver.FindElement(By.Name("firstName")).SendKeys("Gokul");
            driver.FindElement(By.Name("lastName")).SendKeys("Ramesh Babu");
            driver.FindElement(By.Name("address")).SendKeys("14 Beachsurf Road, Brampton, Ontario - L6R 2R4");
            {
                var dropdown = driver.FindElement(By.Name("city"));
                dropdown.FindElement(By.XPath("//option[. = 'Brampton']")).Click();
            }
            {
                var dropdown = driver.FindElement(By.Name("province"));
                dropdown.FindElement(By.XPath("//option[. = 'Yukon']")).Click();
            }
            driver.FindElement(By.Name("phoneNumber")).SendKeys("9052261853");
            driver.FindElement(By.Name("postalCode")).Click();
            driver.FindElement(By.Name("postalCode")).SendKeys("2R4L6R");
            driver.FindElement(By.Name("email")).Click();
            driver.FindElement(By.Name("email")).SendKeys("gokulpulari@gmail.com");
            driver.FindElement(By.Name("notes")).Click();
            driver.FindElement(By.Name("notes")).SendKeys("Test Notes.");
            driver.FindElement(By.Name("submit")).Click();
            var expectedAlertText = "Acceptable formats: 123-123-1234, or (123)123-1234";
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IAlert alert = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());
            Assert.That(alert.Text, Is.EqualTo(expectedAlertText));
            alert.Accept();
        }

        [Test, Order(5)]
        public void newUserRegistrationWithIncorrectEmail()
        {
            driver.Navigate().GoToUrl(BASE_URL + "index.html");
            driver.Manage().Window.Size = new System.Drawing.Size(1440, 900);
            driver.FindElement(By.Name("firstName")).Click();
            driver.FindElement(By.Name("firstName")).SendKeys("Gokul");
            driver.FindElement(By.Name("lastName")).SendKeys("Ramesh Babu");
            driver.FindElement(By.Name("address")).SendKeys("14 Beachsurf Road, Brampton, Ontario - L6R 2R4");
            {
                var dropdown = driver.FindElement(By.Name("city"));
                dropdown.FindElement(By.XPath("//option[. = 'Brampton']")).Click();
            }
            {
                var dropdown = driver.FindElement(By.Name("province"));
                dropdown.FindElement(By.XPath("//option[. = 'Yukon']")).Click();
            }
            driver.FindElement(By.Name("postalCode")).Click();
            driver.FindElement(By.Name("postalCode")).SendKeys("2R4L6R");
            driver.FindElement(By.Name("phoneNumber")).Click();
            driver.FindElement(By.Name("phoneNumber")).SendKeys("905-226-1853");
            driver.FindElement(By.Name("email")).Click();
            driver.FindElement(By.Name("email")).SendKeys("wwe@wee");
            driver.FindElement(By.Name("notes")).Click();
            driver.FindElement(By.Name("notes")).SendKeys("Test Notes.");
            driver.FindElement(By.Name("submit")).Click();
            Assert.That(driver.SwitchTo().Alert().Text, Is.EqualTo("Invalid Email!"));
        }

        [Test, Order(6)]
        public void successfulNewUserRegistration()
        {
            driver.Navigate().GoToUrl(BASE_URL + "index.html");
            driver.FindElement(By.Name("firstName")).Click();
            driver.FindElement(By.Name("firstName")).SendKeys("Gokul");
            driver.FindElement(By.Name("lastName")).SendKeys("Ramesh Babu");
            driver.FindElement(By.Name("address")).SendKeys("14 Beachsurf Road, Brampton, Ontario - L6R 2R4");
            {
                var dropdown = driver.FindElement(By.Name("city"));
                dropdown.FindElement(By.XPath("//option[. = 'Brampton']")).Click();
            }
            {
                var dropdown = driver.FindElement(By.Name("province"));
                dropdown.FindElement(By.XPath("//option[. = 'Yukon']")).Click();
            }
            driver.FindElement(By.Name("postalCode")).Click();
            driver.FindElement(By.Name("postalCode")).SendKeys("2R4L6R");
            driver.FindElement(By.Name("phoneNumber")).Click();
            driver.FindElement(By.Name("phoneNumber")).SendKeys("905-226-1853");
            driver.FindElement(By.Name("email")).Click();
            driver.FindElement(By.Name("email")).SendKeys("gokulpulari@gmail.com");
            driver.FindElement(By.Name("notes")).Click();
            driver.FindElement(By.Name("notes")).SendKeys("Test Notes.");
            driver.FindElement(By.Name("submit")).Click();
            var expectedAlertText = "User Data added successfully!";
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IAlert alert = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());
            Assert.That(alert.Text, Is.EqualTo(expectedAlertText));
            alert.Accept();
        }

        [Test, Order(7)]
        public void viewLastInsertedRecord()
        {
            driver.Navigate().GoToUrl(BASE_URL+"index.html");
            driver.FindElement(By.Name("firstName")).Click();
            driver.FindElement(By.Name("firstName")).SendKeys("Gokul");
            driver.FindElement(By.Name("lastName")).SendKeys("Ramesh Babu");
            driver.FindElement(By.Name("address")).SendKeys("14 Beachsurf Road, Brampton, Ontario - L6R 2R4");
            {
                var dropdown = driver.FindElement(By.Name("city"));
                dropdown.FindElement(By.XPath("//option[. = 'Brampton']")).Click();
            }
            {
                var dropdown = driver.FindElement(By.Name("province"));
                dropdown.FindElement(By.XPath("//option[. = 'Yukon']")).Click();
            }
            driver.FindElement(By.Name("postalCode")).Click();
            driver.FindElement(By.Name("postalCode")).SendKeys("2R4L6R");
            driver.FindElement(By.Name("phoneNumber")).Click();
            driver.FindElement(By.Name("phoneNumber")).SendKeys("905-226-1853");
            driver.FindElement(By.Name("email")).Click();
            driver.FindElement(By.Name("email")).SendKeys("gokulpulari@gmail.com");
            driver.FindElement(By.Name("notes")).Click();
            driver.FindElement(By.Name("notes")).SendKeys("Test Notes.");
            driver.FindElement(By.Name("submit")).Click();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IAlert alert = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());
            alert.Accept();
            driver.FindElement(By.LinkText("Last Saved Record")).Click();
            Assert.AreNotSame(driver.FindElement(By.Id("contactName")),"");
            Assert.AreNotSame(driver.FindElement(By.Id("address")),"");
            Assert.AreNotSame(driver.FindElement(By.Id("city")),"");
            Assert.AreNotSame(driver.FindElement(By.Id("province")),"");
            Assert.AreNotSame(driver.FindElement(By.Id("postalCode")),"");
            Assert.AreNotSame(driver.FindElement(By.Id("phoneNumber")),"");
            Assert.AreNotSame(driver.FindElement(By.Id("email")),"");
            Assert.AreNotSame(driver.FindElement(By.Id("notes")),"");
        }

        [Test]
        public void searchRecords()
        {
            driver.Navigate().GoToUrl(BASE_URL+"index.html");
            driver.FindElement(By.Name("firstName")).Click();
            driver.FindElement(By.Name("firstName")).SendKeys("Gokul");
            driver.FindElement(By.Name("lastName")).SendKeys("Ramesh Babu");
            driver.FindElement(By.Name("address")).SendKeys("14 Beachsurf Road, Brampton, Ontario - L6R 2R4");
            {
                var dropdown = driver.FindElement(By.Name("city"));
                dropdown.FindElement(By.XPath("//option[. = 'Brampton']")).Click();
            }
            {
                var dropdown = driver.FindElement(By.Name("province"));
                dropdown.FindElement(By.XPath("//option[. = 'Yukon']")).Click();
            }
            driver.FindElement(By.Name("postalCode")).Click();
            driver.FindElement(By.Name("postalCode")).SendKeys("2R4L6R");
            driver.FindElement(By.Name("phoneNumber")).Click();
            driver.FindElement(By.Name("phoneNumber")).SendKeys("905-226-1853");
            driver.FindElement(By.Name("email")).Click();
            driver.FindElement(By.Name("email")).SendKeys("gokulpulari@gmail.com");
            driver.FindElement(By.Name("notes")).Click();
            driver.FindElement(By.Name("notes")).SendKeys("Test Notes.");
            driver.FindElement(By.Name("submit")).Click();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IAlert alert = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());
            alert.Accept();
            driver.FindElement(By.LinkText("Search")).Click();
            var records = driver.FindElement(By.CssSelector(".item"));
            Assert.NotNull(records);
        }

        [Test, Order(9)]
        public void deleteLastInsertedRecord()
        {
            driver.Navigate().GoToUrl(BASE_URL+"index.html");
            driver.FindElement(By.Name("firstName")).Click();
            driver.FindElement(By.Name("firstName")).SendKeys("Gokul");
            driver.FindElement(By.Name("lastName")).SendKeys("Ramesh Babu");
            driver.FindElement(By.Name("address")).SendKeys("14 Beachsurf Road, Brampton, Ontario - L6R 2R4");
            {
                var dropdown = driver.FindElement(By.Name("city"));
                dropdown.FindElement(By.XPath("//option[. = 'Brampton']")).Click();
            }
            {
                var dropdown = driver.FindElement(By.Name("province"));
                dropdown.FindElement(By.XPath("//option[. = 'Yukon']")).Click();
            }
            driver.FindElement(By.Name("postalCode")).Click();
            driver.FindElement(By.Name("postalCode")).SendKeys("2R4L6R");
            driver.FindElement(By.Name("phoneNumber")).Click();
            driver.FindElement(By.Name("phoneNumber")).SendKeys("905-226-1853");
            driver.FindElement(By.Name("email")).Click();
            driver.FindElement(By.Name("email")).SendKeys("gokulpulari@gmail.com");
            driver.FindElement(By.Name("notes")).Click();
            driver.FindElement(By.Name("notes")).SendKeys("Test Notes.");
            driver.FindElement(By.Name("submit")).Click();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IAlert alert = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());
            alert.Accept();
            driver.FindElement(By.LinkText("Last Saved Record")).Click();
            driver.FindElement(By.Id("deleteBtn")).Click();
            var expectedAlertText = "Record deleted!";
            alert = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());
            Assert.That(alert.Text, Is.EqualTo(expectedAlertText));
            alert.Accept();
        }

        [Test, Order(10)]
        public void searchNoRecordsFound()
        {
            driver.Navigate().GoToUrl(BASE_URL + "index.html");
            driver.FindElement(By.LinkText("Search")).Click();
            var result = driver.FindElement(By.CssSelector(".no-records"));
            Assert.AreEqual(result.Text, "No Records Found!");
        }

    }
}