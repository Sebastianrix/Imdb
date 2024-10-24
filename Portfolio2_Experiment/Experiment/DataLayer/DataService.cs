using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using DataLayer.Models;
using System.Collections.Generic;
using System.Linq;

namespace DataLayer
{
    public class DataService : IDataService
    {
        private readonly MovieDbContext _context;

        public DataService(IConfiguration configuration)
        {
            var options = new DbContextOptionsBuilder<MovieDbContext>()
              .UseNpgsql(configuration.GetConnectionString("MovieDb"))
              .Options;

            _context = new MovieDbContext(options);
        }





        // --USER--
        public User AddUser(string username, string password, string email)
        {
            var user = new User
            {
                Username = username,
                Password = password,
                Email = email
            };
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public User GetUser(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username);
        }

        public User GetUser(int userId)
        {
            return _context.Users.FirstOrDefault(u => u.Id == userId);
        }

        public void DeleteUser(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        // --BOOKMARK--
        public IList<Bookmark> GetBookmarks(int userId)
        {
            return _context.Bookmarks.Where(b => b.UserId == userId).ToList();
        }

        public Bookmark GetBookmark(int userId, int bookmarkId)
        {
            return _context.Bookmarks.FirstOrDefault(b => b.UserId == userId && b.Id == bookmarkId);
        }

        public Bookmark AddBookmark(int userId, string tconst, string nconst, string note)
        {
            var bookmark = new Bookmark
            {
                UserId = userId,
                TConst = tconst,
                NConst = nconst,
                Note = note
            };
            _context.Bookmarks.Add(bookmark);
            _context.SaveChanges();
            return bookmark;
        }

        public void DeleteBookmark(int userId, int bookmarkId)
        {
            var bookmark = _context.Bookmarks.FirstOrDefault(b => b.UserId == userId && b.Id == bookmarkId);
            if (bookmark != null)
            {
                _context.Bookmarks.Remove(bookmark);
                _context.SaveChanges();
            }
        }

        // --SEARCH HISTORY--
        public IList<SearchHistory> GetSearchHistory(int userId)
        {
            return _context.SearchHistories
                           .Where(sh => sh.UserId == userId)
                           .ToList();
        }

        public SearchHistory GetSearchHistory(int userId, string searchQuery, DateTime createdAt)
        {
            return _context.SearchHistories
                           .FirstOrDefault(sh => sh.UserId == userId
                                              && sh.SearchQuery == searchQuery
                                              && sh.CreatedAt == createdAt);
        }

        public SearchHistory AddSearchHistory(int userId, string searchQuery)
        {
            var searchHistory = new SearchHistory
            {
                UserId = userId,
                SearchQuery = searchQuery,
                CreatedAt = DateTime.UtcNow
            };
            _context.SearchHistories.Add(searchHistory);
            _context.SaveChanges();
            return searchHistory;
        }

        public void DeleteSearchHistory(int userId, string searchQuery, DateTime createdAt)
        {
            var searchHistory = _context.SearchHistories
                                        .FirstOrDefault(sh => sh.UserId == userId
                                                           && sh.SearchQuery == searchQuery
                                                           && sh.CreatedAt == createdAt);
            if (searchHistory != null)
            {
                _context.SearchHistories.Remove(searchHistory);
                _context.SaveChanges();
            }
        }

        // --USER RATING--
        public IList<UserRating> GetUserRatings(int userId)
        {
            return _context.UserRatings
                           .Where(ur => ur.UserId == userId)
                           .ToList();
        }

        public UserRating GetUserRating(int userId, int tconst)
        {
            return _context.UserRatings
                           .FirstOrDefault(ur => ur.UserId == userId
                                              && ur.TConst == tconst);
        }

        public UserRating AddUserRating(int userId, int tconst, int rating)
        {
            var userRating = new UserRating
            {
                UserId = userId,
                TConst = tconst,
                Rating = rating,
                CreatedAt = DateTime.UtcNow
            };
            _context.UserRatings.Add(userRating);
            _context.SaveChanges();
            return userRating;
        }

        public void DeleteUserRating(int userId, int tconst)
        {
            var userRating = _context.UserRatings
                                     .FirstOrDefault(ur => ur.UserId == userId
                                                        && ur.TConst == tconst);
            if (userRating != null)
            {
                _context.UserRatings.Remove(userRating);
                _context.SaveChanges();
            }
        }


        // --PERSON-- (Actors, Directors, Writers)
        public Person GetPersonById(int personId)
        {
            return _context.Persons.FirstOrDefault(p => p.Id == personId);
        }

        public Person GetPersonByNConst(string nconst)
        {
            return _context.Persons.FirstOrDefault(p => p.NConst == nconst);
        }

        public IList<Person> GetAllPersons()
        {
            return _context.Persons.ToList();
        }

        public Person AddPerson(string actualName, string birthYear, string deathYear, string primaryProfession, string knownForTitles)
        {
            var person = new Person
            {
                ActualName = actualName,
                BirthYear = birthYear,
                DeathYear = deathYear,
                PrimaryProfession = primaryProfession,
                KnownForTitles = knownForTitles
            };
            _context.Persons.Add(person);
            _context.SaveChanges();
            return person;
        }

        public void DeletePerson(string nconst)
        {
            var person = _context.Persons.FirstOrDefault(p => p.NConst == nconst);
            if (person != null)
            {
                _context.Persons.Remove(person);
                _context.SaveChanges();
            }
        }
        // --TITLE CHARACTERS--
        public IList<TitleCharacter> GetTitleCharactersByPerson(string nconst)
        {
            return _context.TitleCharacters
                           .Where(tc => tc.NConst == nconst)
                           .ToList();
        }

        public TitleCharacter AddTitleCharacter(string nconst, string tconst, string character, int ordering)
        {
            var titleCharacter = new TitleCharacter
            {
                NConst = nconst,
                TConst = tconst,
                Character = character,
                Ordering = ordering
            };
            _context.TitleCharacters.Add(titleCharacter);
            _context.SaveChanges();
            return titleCharacter;
        }

        public void DeleteTitleCharacter(string nconst, string tconst, string character)
        {
            var titleCharacter = _context.TitleCharacters
                                         .FirstOrDefault(tc => tc.NConst == nconst && tc.TConst == tconst && tc.Character == character);
            if (titleCharacter != null)
            {
                _context.TitleCharacters.Remove(titleCharacter);
                _context.SaveChanges();
            }
        }
        // --TITLE PRINCIPALS--
        public IList<TitlePrincipal> GetTitlePrincipalsByTitle(string tconst)
        {
            return _context.TitlePrincipals
                           .Where(tp => tp.TConst == tconst)
                           .ToList();
        }

        public TitlePrincipal AddTitlePrincipal(string tconst, string nconst, int ordering, string category, string job)
        {
            var titlePrincipal = new TitlePrincipal
            {
                TConst = tconst,
                NConst = nconst,
                Ordering = ordering,
                Category = category,
                Job = job
            };
            _context.TitlePrincipals.Add(titlePrincipal);
            _context.SaveChanges();
            return titlePrincipal;
        }

        public void DeleteTitlePrincipal(string tconst, string nconst, int ordering)
        {
            var titlePrincipal = _context.TitlePrincipals
                                         .FirstOrDefault(tp => tp.TConst == tconst && tp.NConst == nconst && tp.Ordering == ordering);
            if (titlePrincipal != null)
            {
                _context.TitlePrincipals.Remove(titlePrincipal);
                _context.SaveChanges();
            }
        }

        // --KNOWN FOR TITLES--
        public IList<KnownForTitle> GetKnownForTitlesByPerson(string nconst)
        {
            return _context.KnownForTitles
                           .Where(k => k.NConst == nconst)
                           .ToList();
        }

        public KnownForTitle AddKnownForTitle(string nconst, string knownForTitle)
        {
            var knownFor = new KnownForTitle
            {
                NConst = nconst,
                KnownForTitles = knownForTitle
            };
            _context.KnownForTitles.Add(knownFor);
            _context.SaveChanges();
            return knownFor;
        }

        public void DeleteKnownForTitle(string nconst, string knownForTitle)
        {
            var knownFor = _context.KnownForTitles
                                   .FirstOrDefault(k => k.NConst == nconst && k.KnownForTitles == knownForTitle);
            if (knownFor != null)
            {
                _context.KnownForTitles.Remove(knownFor);
                _context.SaveChanges();
            }
        }


    }
}