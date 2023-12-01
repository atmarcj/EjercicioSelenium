using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace EjercicioPractico
{
    public class CompletarFormulario
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }

        [Test]
        [Description("Se completan todos los del formulario.")]
        public void IngresoCompletoCampos()
        {
            driver.Navigate().GoToUrl("https://demo.seleniumeasy.com/input-form-demo.html");
            Assert.That(driver.Title, Is.EqualTo("Selenium Easy - Input Form Demo with Validations"));

            driver.FindElement(By.Name("first_name")).SendKeys("Leonardo");
            driver.FindElement(By.Name("last_name")).SendKeys("Perez");
            driver.FindElement(By.Name("email")).SendKeys("lperez@ces.com.uy");
            driver.FindElement(By.Name("phone")).SendKeys("(845)555-1212");
            driver.FindElement(By.Name("address")).SendKeys("Direccion");
            driver.FindElement(By.Name("city")).SendKeys("Ciudad");
            new SelectElement(driver.FindElement(By.Name("state"))).SelectByText("Virginia");
            driver.FindElement(By.Name("zip")).SendKeys("98370");
            driver.FindElement(By.Name("website")).SendKeys("ces.com.uy");
            driver.FindElements(By.Name("hosting"))[0].Click();
            driver.FindElement(By.Name("comment")).SendKeys("Esto es un comentario.");
            driver.FindElement(By.CssSelector(".btn")).Click();

            IList<IWebElement> listaEtiquetasSmall = driver.FindElements(By.TagName("small"));
            bool invalid = false;

            foreach (var item in listaEtiquetasSmall)
                if (item.GetAttribute("data-bv-result").Equals("INVALID"))
                    invalid = true;

            Assert.IsFalse(invalid, "Se encuentra una verificacion pendiente.");

        }

        [Test]
        [Description("Se completan todos los del formulario menos nombre.")]
        public void NoIngresamosNombre()
        {
            driver.Navigate().GoToUrl("https://demo.seleniumeasy.com/input-form-demo.html");
            Assert.That(driver.Title, Is.EqualTo("Selenium Easy - Input Form Demo with Validations"));

            driver.FindElement(By.Name("last_name")).SendKeys("Perez");
            driver.FindElement(By.Name("email")).SendKeys("lperez@ces.com.uy");
            driver.FindElement(By.Name("phone")).SendKeys("(845)555-1212");
            driver.FindElement(By.Name("address")).SendKeys("Direccion");
            driver.FindElement(By.Name("city")).SendKeys("Ciudad");
            new SelectElement(driver.FindElement(By.Name("state"))).SelectByText("Virginia");
            driver.FindElement(By.Name("zip")).SendKeys("98370");
            driver.FindElement(By.Name("website")).SendKeys("ces.com.uy");
            driver.FindElements(By.Name("hosting"))[0].Click();
            driver.FindElement(By.Name("comment")).SendKeys("Esto es un comentario.");
            driver.FindElement(By.CssSelector(".btn")).Click();

            //Otra forma en la que podemos validar
            IList<IWebElement> listaEtiquetasSmall = driver.FindElements(By.TagName("small"));
            List<string> listaDataBvResult = new List<string>();

            foreach (var item in listaEtiquetasSmall)
                listaDataBvResult.Add(item.GetAttribute("data-bv-result"));


            Assert.IsFalse(listaDataBvResult.Contains("INVALID"), "Se encuentra una verificacion pendiente.");

        }
    }
}