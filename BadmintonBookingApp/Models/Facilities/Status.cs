using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Xml;
namespace BadmintonBookingApp.Models.Facilities
{
    public static class Status
    {
        public static Dictionary<int, string> courtDictionary = new Dictionary<int, string>()
        {
            { 0, "Không Hoạt Động" }, { 1, "Đang Hoạt Động" } 
        };
        public static Dictionary<int, string> priceDictionary = new Dictionary<int, string>()
        {
            { 0, "Không áp dụng" }, { 1, "Đang áp dụng" }
        };
        public static Dictionary<int, string> serviceDictionary = new Dictionary<int, string>()
        {
            { 0, "Hết phục vụ" }, { 1, "Đang phục vụ" }
        };
        public static Dictionary<int, string> reservationDictionary = new Dictionary<int, string>()
        {
            { 0, "Chưa đặt cọc" }, { 1, "Đã đặt cọc" }, { 2, "Chưa thanh toán" }, { 3, "Chưa thanh toán" },
            { 4, "Đã thanh toán" }, { 5, "Quá giờ nhận sân" }, { 6, "Đã cọc và quá giờ nhận sân" }, { 7, "Đã hủy" }
        };
        public static string GetValue(int key,Dictionary<int,string> myDictionary) 
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

        public static IEnumerable<SelectListItem> GetValue(Dictionary<int, string> myDictionary)
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
