using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeServiceCenter.Data
{
    public class AddInventoryDetails
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string itemname { get; set; }

        public int quantity { get; set; }

        public string addedby { get; set; }

        public DateTime dateitemadded { get; set; } 


    }
}
