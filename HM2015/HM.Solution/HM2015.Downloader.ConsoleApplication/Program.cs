using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HM2015.Downloader.ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "http://www.eventbrite.com/e/hackmanchester-2015-tickets-15514426066";

            using (var driver = new ChromeDriver())
            {
                driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
                driver.Navigate().GoToUrl(url);
                

                //var userNameField = driver.FindElementById("usr");
                //var userPasswordField = driver.FindElementById("pwd");
                //var uls = driver.FindElementByXPath("//td[@id='pagedown_1']/ul");

                var uls = driver.FindElements(By.XPath("//td[@id='pagedown_1']/ul"));

                foreach(var ul in uls)
                {
                    var bolds = ul.FindElements(By.TagName("b"));

                    string name = "";
                    string handle = "";

                    name = bolds[0].Text;

                    if(bolds.Count == 2)
                    {
                        handle = bolds[1].Text.Replace("@", "");
                    }

                    Console.WriteLine("Name : {0}", name);
                    Console.WriteLine("Handle : {0}", handle);
                }
            }

            Console.ReadLine();

        }
    }
}
