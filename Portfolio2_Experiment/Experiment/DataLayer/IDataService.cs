using DataLayer.Models;
using System.Collections.Generic;

namespace DataLayer
{
    public interface IDataService
    {
        // --USER--
        User AddUser(string username, string password, string email);
        User GetUser(string username);
        User GetUser(int userId);
        void DeleteUser(int userId);

        // --BOOKMARK--
        IList<Bookmark> GetBookmarks(int userId);
        Bookmark GetBookmark(int userId, int bookmarkId);
        Bookmark AddBookmark(int userId, string tconst, string nconst, string note);
        void DeleteBookmark(int userId, int bookmarkId);

        // --SEARCH HISTORY--
        IList<SearchHistory> GetSearchHistory(int userId);
        SearchHistory GetSearchHistory(int userId, string searchQuery, DateTime createdAt);
        SearchHistory AddSearchHistory(int userId, string searchQuery);
        void DeleteSearchHistory(int userId, string searchQuery, DateTime createdAt);

        // --USER RATING--
        IList<UserRating> GetUserRatings(int userId);
        UserRating GetUserRating(int userId, int tconst);
        UserRating AddUserRating(int userId, int tconst, int rating);
        void DeleteUserRating(int userId, int tconst);


        // --PERSON--
        Person GetPersonById(int personId);
        Person GetPersonByNConst(string nconst);
        IList<Person> GetAllPersons();
        Person AddPerson(string actualName, string birthYear, string deathYear, string primaryProfession, string knownForTitles);
        void DeletePerson(string nconst);

        IList<KnownForTitle> GetKnownForTitlesByPerson(string nconst);
        KnownForTitle AddKnownForTitle(string nconst, string knownForTitle);
        void DeleteKnownForTitle(string nconst, string knownForTitle);

        // --TITLE CHARACTERS--
        IList<TitleCharacter> GetTitleCharactersByPerson(string nconst);
        TitleCharacter AddTitleCharacter(string nconst, string tconst, string character, int ordering);
        void DeleteTitleCharacter(string nconst, string tconst, string character);

        // --TITLE PRINCIPALS--
        IList<TitlePrincipal> GetTitlePrincipalsByTitle(string tconst);
        TitlePrincipal AddTitlePrincipal(string tconst, string nconst, int ordering, string category, string job);
        void DeleteTitlePrincipal(string tconst, string nconst, int ordering);
    }
}