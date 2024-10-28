﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Models;

namespace DataLayer
{
    public class MovieDbContext : DbContext
    {
        public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Bookmark> Bookmarks { get; set; }
        public DbSet<UserRating> UserRatings { get; set; }
        public DbSet<SearchHistory> SearchHistories { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<KnownForTitle> KnownForTitles { get; set; }
        public DbSet<TitleCharacter> TitleCharacters { get; set; }
        public DbSet<TitlePrincipal> TitlePrincipals { get; set; }
        public DbSet<TitleBasic> TitleBasics { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            MapUsers(modelBuilder);
            MapBookmarks(modelBuilder);
            MapUserRatings(modelBuilder);
            MapSearchHistories(modelBuilder);
            MapNameBasic(modelBuilder);
            MapKnownForTitles(modelBuilder);
            MapTitleCharacters(modelBuilder);
            MapTitlePrincipals(modelBuilder);
            MapTitleBasic(modelBuilder);
        }

        // User Table Mapping
        private static void MapUsers(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<User>().Property(u => u.Id).HasColumnName("userId");
            modelBuilder.Entity<User>().Property(u => u.Username).HasColumnName("username");
            modelBuilder.Entity<User>().Property(u => u.Email).HasColumnName("email");
            modelBuilder.Entity<User>().Property(u => u.Password).HasColumnName("password");
        }

        // Bookmark Table Mapping
        private static void MapBookmarks(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bookmark>().ToTable("bookmarks");
            modelBuilder.Entity<Bookmark>().HasKey(b => b.Id);
            modelBuilder.Entity<Bookmark>().Property(b => b.Id).HasColumnName("bookmarkId");
            modelBuilder.Entity<Bookmark>().Property(b => b.TConst).HasColumnName("tconst");
            modelBuilder.Entity<Bookmark>().Property(b => b.NConst).HasColumnName("nconst");
            modelBuilder.Entity<Bookmark>().Property(b => b.Note).HasColumnName("note");
            modelBuilder.Entity<Bookmark>().Property(b => b.CreatedAt).HasColumnName("bookmarkDate");
        }

        // User Ratings Table Mapping
        private static void MapUserRatings(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRating>().ToTable("user_ratings");
            modelBuilder.Entity<UserRating>().HasKey(ur => new { ur.TConst, ur.UserId });
            modelBuilder.Entity<UserRating>().Property(u => u.UserId).HasColumnName("userId");
            modelBuilder.Entity<UserRating>().Property(u => u.TConst).HasColumnName("tconst");
            modelBuilder.Entity<UserRating>().Property(u => u.Rating).HasColumnName("rating");
            modelBuilder.Entity<UserRating>().Property(u => u.CreatedAt).HasColumnName("ratingDate");
        }

        // Search History Table Mapping
        private static void MapSearchHistories(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SearchHistory>().ToTable("search_history");
            modelBuilder.Entity<SearchHistory>().HasKey(sh => new { sh.UserId, sh.SearchQuery, sh.CreatedAt });
            modelBuilder.Entity<SearchHistory>().Property(s => s.UserId).HasColumnName("userId");
            modelBuilder.Entity<SearchHistory>().Property(s => s.SearchQuery).HasColumnName("searchQuery");
            modelBuilder.Entity<SearchHistory>().Property(s => s.CreatedAt).HasColumnName("searchDate");
        }

        // Person Table Mapping
        private static void MapNameBasic(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().ToTable("namebasic");
            modelBuilder.Entity<Person>().HasKey(p => p.NConst);
            modelBuilder.Entity<Person>().Property(p => p.NConst).HasColumnName("nconst");
            modelBuilder.Entity<Person>().Property(p => p.BirthYear).HasColumnName("birthyear");
            modelBuilder.Entity<Person>().Property(p => p.DeathYear).HasColumnName("deathyear");
            modelBuilder.Entity<Person>().Property(p => p.ActualName).HasColumnName("primaryname");
        }

        // MapTitleCharacters method
        private static void MapTitleCharacters(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TitleCharacter>().ToTable("titlecharacters");
            modelBuilder.Entity<TitleCharacter>().HasKey(tc => new { tc.NConst, tc.TConst });
            modelBuilder.Entity<TitleCharacter>().Property(tc => tc.NConst).HasColumnName("nconst");
            modelBuilder.Entity<TitleCharacter>().Property(tc => tc.TConst).HasColumnName("tconst");
            modelBuilder.Entity<TitleCharacter>().Property(tc => tc.Character).HasColumnName("character");
            modelBuilder.Entity<TitleCharacter>().Property(tc => tc.Ordering).HasColumnName("ordering");

            modelBuilder.Entity<TitleCharacter>()
                .HasOne(tc => tc.TitleBasic)
                .WithMany()
                .HasForeignKey(tc => tc.TConst)
                .HasPrincipalKey(tb => tb.TConst);

            modelBuilder.Entity<TitleCharacter>()
                .HasOne(tc => tc.Person)
                .WithMany()
                .HasForeignKey(tc => tc.NConst)
                .HasPrincipalKey(p => p.NConst);
        }

        // MapKnownForTitles method
        private static void MapKnownForTitles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<KnownForTitle>().ToTable("nameknownfor");
            modelBuilder.Entity<KnownForTitle>().HasKey(k => new { k.NConst, k.KnownForTitles });
            modelBuilder.Entity<KnownForTitle>().Property(k => k.NConst).HasColumnName("nconst");
            modelBuilder.Entity<KnownForTitle>().Property(k => k.KnownForTitles).HasColumnName("knownfortitles");

            modelBuilder.Entity<KnownForTitle>()
                .HasOne(k => k.Person)
                .WithMany()
                .HasForeignKey(k => k.NConst)
                .HasPrincipalKey(p => p.NConst);

            modelBuilder.Entity<KnownForTitle>()
                .HasOne(k => k.TitleBasic)
                .WithMany()
                .HasForeignKey(k => k.KnownForTitles)
                .HasPrincipalKey(tb => tb.TConst); // Explicitly specify that 'knownfortitles' maps to 'tconst' in TitleBasic
        }

        // MapTitlePrincipals method
        private static void MapTitlePrincipals(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TitlePrincipal>().ToTable("titleprincipals");
            modelBuilder.Entity<TitlePrincipal>().HasKey(tp => new { tp.TConst, tp.NConst }); 
            modelBuilder.Entity<TitlePrincipal>().Property(tp => tp.TConst).HasColumnName("tconst");
            modelBuilder.Entity<TitlePrincipal>().Property(tp => tp.NConst).HasColumnName("nconst");
            modelBuilder.Entity<TitlePrincipal>().Property(tp => tp.Ordering).HasColumnName("ordering");
            modelBuilder.Entity<TitlePrincipal>().Property(tp => tp.Category).HasColumnName("category");
            modelBuilder.Entity<TitlePrincipal>().Property(tp => tp.Job).HasColumnName("job");


            modelBuilder.Entity<TitlePrincipal>()
                .HasOne(tp => tp.Person)
                .WithMany()
                .HasForeignKey(tp => tp.NConst)
                .HasPrincipalKey(p => p.NConst);

            modelBuilder.Entity<TitlePrincipal>()
                .HasOne(tp => tp.TitleBasic)
                .WithMany()
                .HasForeignKey(tp => tp.TConst)
                .HasPrincipalKey(tb => tb.TConst);
        }

        // MapTitleBasic method
        private static void MapTitleBasic(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TitleBasic>().ToTable("titlebasic");
            modelBuilder.Entity<TitleBasic>().HasKey(tb => tb.TConst);
            modelBuilder.Entity<TitleBasic>().Property(tb => tb.TConst).HasColumnName("tconst");
            modelBuilder.Entity<TitleBasic>().Property(tb => tb.PrimaryTitle).HasColumnName("primarytitle");
            modelBuilder.Entity<TitleBasic>().Property(tb => tb.TitleType).HasColumnName("titletype");
            modelBuilder.Entity<TitleBasic>().Property(tb => tb.StartYear).HasColumnName("startyear");
        }
    }
}
