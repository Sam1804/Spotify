using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AudioProject.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AudioProject.Models;

namespace AudioProject.Data
{
    public class AuthDbContext : IdentityDbContext<ApplicationUser>
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options)
            : base(options)
        {
        }

        public DbSet<PlaylistModel> Playlist { get; set; }
        public DbSet<AudioModel> Audio { get; set; }
        public DbSet<PlaylistModelAudioModel> PlaylistAudio { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<PlaylistModelAudioModel>().HasKey(sc => new { sc.PlaylistId, sc.AudioId });
        }
    }
}
