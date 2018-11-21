using System.Collections.Generic;

namespace RTLTestTask.Models
{
    public class TVShow
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<ShowCast> ShowCasts { get; set; }
    }
}