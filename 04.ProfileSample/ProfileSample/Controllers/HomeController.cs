using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ProfileSample.DAL;
using ProfileSample.Models;

namespace ProfileSample.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            using (var context = new ProfileSampleEntities())
            {
                // Retrieve the first 20 ImgSources with their data in one query
                var sources = await context.ImgSources
                    .AsNoTracking()
                    .Take(20)
                    .Select(x => new ImageModel
                    {
                        Name = x.Name,
                        Data = x.Data
                    })
                    .ToListAsync(); // Execute the query and convert to a list asynchronously

                return View(sources);
            }
        }

        public async Task<ActionResult> Convert()
        {
            var files = Directory.GetFiles(Server.MapPath("~/Content/Img"), "*.jpg");

            using (var context = new ProfileSampleEntities())
            {
                foreach (var file in files)
                {
                    using (var stream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, useAsync: true))
                    {
                        byte[] buff = new byte[stream.Length];
                        await stream.ReadAsync(buff, 0, (int)stream.Length);

                        var entity = new ImgSource()
                        {
                            Name = Path.GetFileName(file),
                            Data = buff,
                        };

                        context.ImgSources.Add(entity);
                        await context.SaveChangesAsync();
                    }
                }
            }

            return RedirectToAction("Index");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}