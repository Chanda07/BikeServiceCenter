using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BikeServiceCenter.Data
{
	public partial class AddInventoryService
	{
		//method to save the added inventory items
		private static void SaveAll(List<AddInventoryDetails> addinventory)
		{
			string appDataDirectoryPath = Utils.GetAppDirectoryPath();
			string appItemsFilePath = Utils.GetAddInventoryFilePath();

			if (!Directory.Exists(appDataDirectoryPath))
			{
				Directory.CreateDirectory(appDataDirectoryPath);
			}

			var json = JsonSerializer.Serialize(addinventory);
			File.WriteAllText(appItemsFilePath, json);
		}

		//method to save the items details in a list
		public static List<AddInventoryDetails> GetAll()
		{
			string appItemsFilePath = Utils.GetAddInventoryFilePath();
			if (!File.Exists(appItemsFilePath))
			{
				return new List<AddInventoryDetails>();
			}

			var json = File.ReadAllText(appItemsFilePath);

			return JsonSerializer.Deserialize<List<AddInventoryDetails>>(json);
		}

		//method to add items details 
		public static List<AddInventoryDetails> Create(string item, int quantity, string addedby, DateTime dateitemadded)
		{
			List<AddInventoryDetails> addinventorydetails = GetAll();
			AddInventoryDetails addinventory = addinventorydetails.FirstOrDefault(x => x.itemname == item);
			bool itemExists = addinventorydetails.Any(x => x.itemname == item);

			if ((item == ""))
			{
				throw new Exception("Item name cannot be empty !! ");
			}
			if ((quantity == 0))
			{
				throw new Exception("Quantity must be is greater than 0 !!");
			}

			if (itemExists)
			{
				throw new Exception("Item Name already exists.");

			}
			addinventorydetails.Add(
					new AddInventoryDetails
					{
						itemname = item,
						quantity = quantity,
						addedby = addedby,
						dateitemadded = dateitemadded
					}
				);
			SaveAll(addinventorydetails);
			return addinventorydetails;
		}

		public static AddInventoryDetails GetById(Guid id)
		{
			List<AddInventoryDetails> items = GetAll();
			return items.FirstOrDefault(x => x.Id == id);
		}

		//method to delete itemdetails
		public static List<AddInventoryDetails> Delete(Guid id)
		{
			List<AddInventoryDetails> items = GetAll();
			AddInventoryDetails item = items.FirstOrDefault(x => x.Id == id);

			if (item == null)
			{
				throw new Exception("Item not found.");
			}


			items.Remove(item);
			SaveAll(items);

			return items;
		}
		//method to update item details
		public static List<AddInventoryDetails> UpdateItem(string name, int quantity)
		{
			List<AddInventoryDetails> items = GetAll();
			AddInventoryDetails item = items.FirstOrDefault(x => x.itemname == name);
			if (item == null)
			{
				throw new Exception("Item not found.");
			}
			item.quantity += quantity;
			SaveAll(items);
			return items;
		}
	}
}

