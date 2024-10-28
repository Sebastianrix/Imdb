namespace DataLayer.Models
{
    public class KnownForTitle
    {
        public string NConst { get; set; }
        public string KnownForTitles { get; set; }


        public Person Person { get; set; }
        public TitleBasic TitleBasic { get; set; }
    }
}
