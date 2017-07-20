using Microsoft.AspNetCore.Mvc;
using AspNetCoreVideo.Entities;
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
                Genre = video.Genre.ToString()
            });
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(VideoEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                 var video = new Video
                 {
                    Title = model.Title,
                    Genre = model.Genre
                 };

                _videos.Add(video);
                _videos.Commit();  // step 7 but causing error for some reason NOTE: 7/20/17 now working with no changes?
                return RedirectToAction("Details", new { id = video.Id });
            }

            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var video = _videos.Get(id);

            if (video == null) return RedirectToAction("Index");
            return View(video);
        }

        [HttpPost]
        public IActionResult Edit(int id, VideoEditViewModel model)
        {
            var video = _videos.Get(id);

            if (video == null || !ModelState.IsValid) return View(model);

            video.Title = model.Title;
            video.Genre = model.Genre;

            _videos.Commit();

            return RedirectToAction("Details", new {id = video.Id});
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
                    Genre = model.Genre.ToString()
                }
            );
        }
    }
}
