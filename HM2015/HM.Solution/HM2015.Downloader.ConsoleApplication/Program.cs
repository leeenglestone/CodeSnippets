using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace HM2015.Downloader.ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            //ProcessAttendees();

            //ShowFacialRecognition(@"C:\test\HM2015\HM\hack_manchester.jpg");

            //ShowFacialRecognition(@"C:\test\HM2015\HM\1.jpg");
            //ShowFacialRecognition(@"C:\test\HM2015\HM\2.jpg");
            //ShowFacialRecognition(@"C:\test\HM2015\HM\3.jpg");
            //ShowFacialRecognition(@"C:\test\HM2015\HM\4.jpg");

            //ShowFacialRecognition(@"C:\test\HM2015\HM\5.jpg");
            //ShowFacialRecognition(@"C:\test\HM2015\HM\6.jpg");
            ShowFacialRecognition(@"C:\test\HM2015\HM\7.jpg");

            Console.ReadLine();

        }

        static void ShowFacialRecognition(string imagePath)
        {
            FacialRecognition.Library.FacialRecognition.Run(imagePath);
        }

        private static void ProcessAttendees()
        {
            string url = "http://www.eventbrite.com/e/hackmanchester-2015-tickets-15514426066";

            var people = new List<Person>();

            using (var driver = new ChromeDriver())
            {
                driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
                driver.Navigate().GoToUrl(url);

                var uls = driver.FindElements(By.XPath("//td[@id='pagedown_1']/ul"));

                foreach (var ul in uls)
                {
                    var bolds = ul.FindElements(By.TagName("b"));

                    string name = "";
                    string handle = "";
                    string location = "";

                    name = bolds[0].Text;

                    if (bolds.Count == 2)
                    {
                        handle = bolds[1].Text.Replace("@", "");
                    }

                    Console.WriteLine("Name : {0}", name);
                    Console.WriteLine("Handle : {0}", handle);

                    people.Add(new Person()
                    {
                        Name = name,
                        TwitterProfile = new TwitterProfile(handle, location)
                    });
                }
            }

            foreach (var person in people)
            {
                if (!String.IsNullOrWhiteSpace(person.TwitterProfile.Handle))
                {
                    person.TwitterProfile = GetTwitterProfile(person.TwitterProfile.Handle);
                    Console.WriteLine(person.TwitterProfile.ProfileImageUrl);
                }
            }
        }

        static TwitterProfile GetTwitterProfile(string twitterHandle)
        {
            var twitterProfile = new TwitterProfile(twitterHandle);

            string url = String.Format("http://www.twitter.com/{0}", twitterHandle);

            using (var driver = new ChromeDriver())
            {
                driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
                driver.Navigate().GoToUrl(url);
                var image = driver.FindElement(By.XPath("//img[@class='ProfileAvatar-image ']"));
                twitterProfile.ProfileImageUrl= image.GetAttribute("src");

                string profileImageSaveLocation = String.Format(@"c:\test\hm2015\{0}.jpg", twitterProfile.Handle);

                if(!File.Exists(profileImageSaveLocation))
                {
                    using (WebClient webClient = new WebClient())
                    {
                        webClient.DownloadFile(twitterProfile.ProfileImageUrl, profileImageSaveLocation);
                    }
                }
            }

            return twitterProfile;
        }
    }

    internal class Person
    {
        public String Name { get; set; }
        public TwitterProfile TwitterProfile { get; set; }
    }

    internal class TwitterProfile
    {
        public TwitterProfile(string handle, string location)
        {
            Handle = handle;
            Location = location;
        }

        public TwitterProfile(string twitterHandle)
        {
            Handle = twitterHandle;
        }

        public string ProfileImageUrl { get; set; }
        public string Handle { get; set; }
        public string Location { get; set; }


    }
}
