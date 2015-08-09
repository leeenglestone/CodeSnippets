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

            var people = new List<Person>();

            using (var driver = new ChromeDriver())
            {
                driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
                driver.Navigate().GoToUrl(url);


                //var userNameField = driver.FindElementById("usr");
                //var userPasswordField = driver.FindElementById("pwd");
                //var uls = driver.FindElementByXPath("//td[@id='pagedown_1']/ul");

                var uls = driver.FindElements(By.XPath("//td[@id='pagedown_1']/ul"));

                foreach (var ul in uls)
                {
                    var bolds = ul.FindElements(By.TagName("b"));

                    string name = "";
                    string handle = "";

                    name = bolds[0].Text;

                    if (bolds.Count == 2)
                    {
                        handle = bolds[1].Text.Replace("@", "");
                    }

                    Console.WriteLine("Name : {0}", name);
                    Console.WriteLine("Handle : {0}", handle);

                    people.Add(new Person() { Name = name, Handle = handle });
                }
            }

            foreach(var person in people)
            {
                if(!String.IsNullOrWhiteSpace(person.Handle))
                {
                    string profileImageUrl = GetTwitterProfile(person.Handle);
                    person.ProfileImageUrl = profileImageUrl;
                    Console.WriteLine(profileImageUrl);
                }
            }

            Console.ReadLine();

        }

        static string GetTwitterProfile(string twitterHandle)
        {
            string url = String.Format("http://www.twitter.com/{0}", twitterHandle);
            string imageUrl = "";

            using (var driver = new ChromeDriver())
            {
                driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));

                driver.Navigate().GoToUrl(url);

                var image = driver.FindElement(By.XPath("//img[@class='ProfileAvatar-image ']"));

                imageUrl = image.GetAttribute("src");
            }

            return imageUrl;
        }
    }

    class Person
    {
        public String Name { get; set; }
        public string Handle { get; set; }
        public string ProfileImageUrl { get; set; }
    }
}
