using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Xml;
namespace BadmintonBookingApp.Models.Facilities
{
    public static class Status
    {
        public static Dictionary<int, string> myDictionary = new Dictionary<int, string>()
        {
            { 0, " Không Hoạt Động" }, { 1, "Đang Hoạt Động" } 
        };

        public static string GetValue(int key) 
        {
            if (myDictionary.ContainsKey(key))
            {
                return myDictionary[key];
            }
            else
            {
                return null;
            }
        }

        public static IEnumerable<SelectListItem> GetValue()
        {
            List<SelectListItem> listSelListItem = new List<SelectListItem>();
            SelectListItem tmp;
            foreach (var item in  myDictionary)
            {
                tmp = new SelectListItem()
                {
                    Text = item.Value,
                    Value = item.Key.ToString(),
                    Selected = true
                };
                listSelListItem.Add(tmp);
            }
            return listSelListItem;

        }
    }
}
