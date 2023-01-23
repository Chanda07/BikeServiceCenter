using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeServiceCenter.Data
{
    public class TakeOutDetails
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public int id { get; set; }
        public string itemname { get; set; }
        public int quantity { get; set; }
        public string staffname { get; set; }
        public string approvedby { get; set; }
        public DateTime datetaken { get; set; }
        public DateTime dateapproved { get; set; }

        public bool IsDone { get; set; }

        public int totalquantity { get; set; }
    }
}
