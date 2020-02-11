using System;
using System.Collections.Generic;
using System.Text;
using oyasumi_lazer.Objects;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using static oyasumi_lazer.Objects.User;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace oyasumi_lazer.Database
{
    class OyasumiDbContext : DbContext
    {
        public OyasumiDbContext() 
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           optionsBuilder.UseMySql($"server=localhost;database={Config.Get().Database};user={Config.Get().Username};password={Config.Get().Password}");
        }
        
        public DbSet<Users> DBUsers { get; set; }
        [Table("Users")]
        public class Users
        {
            [Key]
            [Required]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public long Id { get; set; }

            public DateTimeOffset JoinDate { get; set; }
            [Required]
            public string Username { get; set; }
            public string PreviousUsernames { get; set; }
            [Required]
            public string Country { get; set; }
            public string Colour { get; set; }
            public string AvatarUrl { get; set; }
            public string CoverUrl { get; set; }
            public bool PMFriendsOnly { get; set; }
            public string Interests { get; set; }
            public string Occupation { get; set; }
            public string Title { get; set; }
            public string Location { get; set; }
            public DateTimeOffset? LastVisit { get; set; }
            public string Twitter { get; set; }
            public string Lastfm { get; set; }
            public string Skype { get; set; }
            public string Discord { get; set; }
            public string Website { get; set; }
            public int PostCount { get; set; }
            public int FollowerCount { get; set; }
            public int FavouriteBeatmapsetCount { get; set; }
            public int GraveyardBeatmapsetCount { get; set; }
            public int LovedBeatmapsetCount { get; set; }
            public int RankedAndApprovedBeatmapsetCount { get; set; }
            public int UnrankedBeatmapsetCount { get; set; }
            public int ScoresFirstCount { get; set; }
            public string Playstyle { get; set; }
            public string ProfileOrder { get; set; }
            [Required]
            public string Password { get; set; }
        }
    }
}
