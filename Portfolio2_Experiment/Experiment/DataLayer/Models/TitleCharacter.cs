namespace DataLayer.Models
{
    public class TitleCharacter
    {
        public string NConst { get; set; }
        public string TConst { get; set; }
        public string Character { get; set; }
        public int Ordering { get; set; }

    
        public Person Person { get; set; }
    }
}
