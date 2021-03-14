using AudioProject.Data;
using AudioProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace AudioProject.Controllers
{
    [Authorize]
    public class AudioController : Controller
    {
        
        private readonly AuthDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public AudioController(AuthDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: AudioController
        public ActionResult Index()
        {
            return View();
        }

        // GET: AudioController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AudioController/Create
        public ActionResult Create()
        { 
            return View();
        }

        [HttpGet]
        public ActionResult Display()
        {
            List<AudioModel> songs = _context.Audio.ToList();
            ViewData["AudioList"] = songs;
            List<PlaylistModel> data = _context.Playlist.ToList();
            ViewData["Playlist"] = data;
            return View("Display");

        }

        // POST: AudioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AudioModel Model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var files = Request.Form.Files;
                    foreach (var file in files)
                    {
                        var uploads = Path.Combine(_environment.WebRootPath, "Song");
                        if (file.Length > 0) {
                            string FileName = Guid.NewGuid().ToString();
                            string[] extent = file.FileName.Split('.');
                            Model.FilePath = FileName + "." + extent.Last();
                            using (var fileStream = new FileStream(Path.Combine(uploads, Model.FilePath), FileMode.Create))
                            {
                                file.CopyTo(fileStream);
                            }
                        }
                    }
                    //AudioModel Audio = new AudioModel();
                    _context.Audio.Add(Model);
                    _context.SaveChanges();
                    return View();
                }
                catch
                {
                    ModelState.AddModelError("Test","Test");
                }
            }
            return View();    
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddData(PlaylistModelAudioModel Model1)
        {

            if (ModelState.IsValid)
            {
                var playlistid = Request.Form["Playlist"];
                var songid = Request.Form["AudioId"];

                try
                {
                    Model1.PlaylistId = Int32.Parse(playlistid);
                    Model1.AudioId = Int32.Parse(songid);
                    _context.PlaylistAudio.Add(Model1);
                    _context.SaveChanges();
                    Display();
                    return View("Display");
                }
                catch
                {
                    ModelState.AddModelError("Playlist", "Playlist");
                }
            }
            Display();
            return View("Display");
        }

        // GET: AudioController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AudioController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AudioController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AudioController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete()
        {
            var audioid = Int32.Parse(Request.Form["AudioId"]);
            try
            {
                var removingRow = _context.Audio.Find(audioid);

                if (removingRow != null)
                {
                    _context.Audio.Remove(removingRow);
                    _context.SaveChanges();
                }
                Display();
                return View("Display");
            }
            catch
            {
                Display();
                return View("Display");
            }
        }
    }
}
