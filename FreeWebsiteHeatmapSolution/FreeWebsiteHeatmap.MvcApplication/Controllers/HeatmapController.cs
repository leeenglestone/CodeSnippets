using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace FreeWebsiteHeatmap.MvcApplication.Controllers
{
    

    public class HeatmapController : Controller
    {
        //
        // GET: /Heatmap/

        private static string lastX = "0";
        private static string lastY = "0";
        private static int lastC = 0;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SaveCoordinate(string x, string y, string c)
        //public ActionResult SaveCoordinate()
        {
            string path = Server.MapPath("~/App_Data/coordinates.txt");

            // This text is added only once to the file. 
            if (!System.IO.File.Exists(path))
            {
                // Create a file to write to. 
                using (StreamWriter sw = System.IO.File.CreateText(path))
                {
                   
                    sw.WriteLine(String.Format("x={0},y={1},c={2}", x, y, c));
                }
            }
            else
            {
                // This text is always added, making the file longer over time 
                // if it is not deleted. 
                using (StreamWriter sw = System.IO.File.AppendText(path))
                {
                    sw.WriteLine(String.Format("x={0},y={1},c={2}", x, y, c));
                }	    
            }

            return Content("");

        }

    }
}
