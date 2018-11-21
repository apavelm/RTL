using System;
using System.Collections.Generic;

namespace RTLTestTask.Models
{
    public class Cast
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime? DoB { get; set; }

        public virtual ICollection<ShowCast> ShowCasts { get; set; }
    }
}
