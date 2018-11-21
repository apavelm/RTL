namespace RTLTestTask.Models
{
    public class ShowCast
    {
        public int ShowId { get; set; }
        public TVShow Show { get; set; }
        public int CastId { get; set; }
        public Cast Cast { get; set; }
    }
}