namespace AffairList.Services.Models
{
    public class AffairsCollection
    {
        public List<Affair> Affairs { get; set; }
        public AffairsCollection()
        {
            Affairs = new List<Affair>();
        }
    }
}
