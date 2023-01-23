using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BikeServiceCenter.Data
{
    public partial class TakeOutService
    {
    
        private static void SaveAll(List<TakeOutDetails> addinventory)
        {
            string appDataDirectoryPath = Utils.GetAppDirectoryPath();
            string appItemsFilePath = Utils.GetTakeOutFilePath();

            if (!Directory.Exists(appDataDirectoryPath))
            {
                Directory.CreateDirectory(appDataDirectoryPath);
            }

            var json = JsonSerializer.Serialize(addinventory);
            File.WriteAllText(appItemsFilePath, json);
        }

        public static List<TakeOutDetails> GetAll()
        {
            string appItemsFilePath = Utils.GetTakeOutFilePath();
            if (!File.Exists(appItemsFilePath))
            {
                return new List<TakeOutDetails>();
            }

            var json = File.ReadAllText(appItemsFilePath);

            return JsonSerializer.Deserialize<List<TakeOutDetails>>(json);
        }

        public static List<TakeOutDetails> Create(string item, int quantity, string staffname, string approvedby, DateTime dateitemtaken, DateTime approveddate)
        {
            List<TakeOutDetails> takeoutdetails = GetAll();

            if ((item == ""))
            {
                throw new Exception("Item name cannot be empty !! ");
            }
            
            if ((staffname == ""))
            {
                throw new Exception("Taken by cannot be empty !!");
            }
            

            takeoutdetails.Add(
                    new TakeOutDetails
                    {
                        itemname = item,
                        quantity = quantity,
                        staffname = staffname,
                        approvedby = approvedby,
                        datetaken = dateitemtaken,
                        dateapproved = approveddate
                  

                    }
                    );
            SaveAll(takeoutdetails);
            return takeoutdetails;
        }
        public static List<TakeOutDetails> Update(string item, int quantity, string staffname, string approvedby, DateTime dateapproved, DateTime dateitemtaken, bool isdone)
        {
            List<TakeOutDetails> itemss = GetAll();
            TakeOutDetails TakeOutUpdate = itemss.FirstOrDefault(x => x.itemname == item);

            if (TakeOutUpdate == null)
            {
                throw new Exception("Item not found.");
            }

            TakeOutUpdate.itemname = item;
            TakeOutUpdate.IsDone = isdone;
            TakeOutUpdate.quantity = quantity;
            TakeOutUpdate.staffname = staffname;
            TakeOutUpdate.datetaken = dateitemtaken;
            TakeOutUpdate.approvedby = "Admin";
            TakeOutUpdate.dateapproved = dateapproved;

            SaveAll(itemss);
            return itemss;
        }

        public static TakeOutDetails GetById(Guid id)
        {
            List<TakeOutDetails> items = GetAll();
            return items.FirstOrDefault(x => x.Id == id);
        }

        public static List<TakeOutDetails> Delete(Guid id)
        {
            List<TakeOutDetails> items = GetAll();
            TakeOutDetails item = items.FirstOrDefault(x => x.Id == id);

            if (item == null)
            {
                throw new Exception("Item not found.");
            }

            items.Remove(item);
            SaveAll(items);

            return items;
        }

        public static List<double> GetChartData()
        {
            List<TakeOutDetails> items = GetAll();
            List<double> quantity = new();
            foreach (var item in items)
            {
                if (item.quantity > 0)
                {
                    quantity.Add(item.quantity);
                }

            }

            return quantity;
        }
        public static List<string> GetChartLabel()
        {
            List<TakeOutDetails> items = GetAll();
            List<string> name = new();
            foreach (var item in items)
            {
                if (item.quantity > 0)
                {
                    name.Add(item.itemname);
                }
            }
            return name;
        }
    }
}

