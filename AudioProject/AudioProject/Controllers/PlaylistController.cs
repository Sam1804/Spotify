using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AudioProject.Models;
using AudioProject.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace AudioProject.Controllers
{
    [Authorize]
    public class PlaylistController : Controller{

        private readonly AuthDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public PlaylistController(AuthDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        // GET: PlaylistController
        public ActionResult Index()
        {
            ClaimsPrincipal currentUser = this.User;
            var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            List<PlaylistModel> data = (from x in _context.Playlist where x.UserID == currentUserID select x).ToList();
            ViewData["Playlist"] = data;
            return View();
        }

        // GET: PlaylistController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PlaylistController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PlaylistController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PlaylistModel Model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ClaimsPrincipal currentUser = this.User;
                    var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
                    Model.UserID = currentUserID;
                    _context.Playlist.Add(Model);
                    _context.SaveChanges();
                    return View();
                }
                catch
                {
                    ModelState.AddModelError("Playlist", "Playlist");
                }
            }
            return View();
        }

        // GET: PlaylistController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
            
        }

        // POST: PlaylistController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit()
        {
            var playlistid = Request.Form["PlaylistId"];
            try
            {
                List<PlaylistModelAudioModel> playlistSong = (from x in _context.PlaylistAudio where x.PlaylistId == Int32.Parse(playlistid) select x).Include(q => q.Audio).ToList();
                ViewData["PlaylistSongs"] = playlistSong;
                return View();
            }
            catch
            {
                return View();
            }

        }

        // GET: PlaylistController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteOnlySong()
        {
            var playlistid = Int32.Parse(Request.Form["PlaylistID"]);
            var songid = Int32.Parse(Request.Form["AudioId"]);
 
            var removingRow = _context.Set<PlaylistModelAudioModel>().Where(x => x.PlaylistId == playlistid && x.AudioId == songid).FirstOrDefault();

            if (removingRow != null)
                {
                    _context.Set<PlaylistModelAudioModel>().Remove(removingRow);
                    _context.SaveChanges();
                }
                Edit();
                return View("Edit");
        }

        // POST: PlaylistController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete()
        {
            var playlistid = Int32.Parse(Request.Form["PlaylistId"]);
            try
            {
                var removingRow = _context.Playlist.Find(playlistid);

                if (removingRow != null)
                {
                    _context.Playlist.Remove(removingRow);
                    _context.SaveChanges();
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
