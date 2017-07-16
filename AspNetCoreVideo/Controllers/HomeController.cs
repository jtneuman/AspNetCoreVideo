using Microsoft.AspNetCore.Mvc;
using AspNetCoreVideo.Services;
using AspNetCoreVideo.ViewModels;
using System;
using System.Linq;
using AspNetCoreVideo.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspNetCoreVideo.Controllers
{
    public class HomeController : Controller
    {
        private IVideoData _videos;

        public HomeController(IVideoData videos)
        {
            _videos = videos;
        }

        // GET: /<controller>/
        public ViewResult Index()
        {
            var model = _videos.GetAll().Select(video =>
            new VideoViewModel {
                Id = video.Id,
                Title = video.Title,
                Genre = Enum.GetName(typeof(Genres), video.GenreId)
            });
            return View(model);
        }

        public IActionResult Details(int id)
        {
            var model = _videos.Get(id);
            // check if exists
            if (model == null)
                return RedirectToAction("Index");
            //if so return 
            return View(new VideoViewModel
                {
                    Id = model.Id,
                    Title = model.Title,
                    Genre = Enum.GetName(typeof(Genres), model.GenreId)
                }
            );
        }
    }
}
