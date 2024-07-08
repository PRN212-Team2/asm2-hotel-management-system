using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Models
{
    public static class BasketManager
    {
        public static ObservableCollection<BasketItem> BasketItems = new ObservableCollection<BasketItem>();

        public static ObservableCollection<BasketItem> GetBasketItems() { return BasketItems; }

        public static void AddBasketItem(BasketItem basketItem)
        {
            BasketItems.Add(basketItem);
        }

        public static void DeleteBasketItem(int roomId)
        {
            var basketItem = BasketItems.FirstOrDefault(i => i.RoomID == roomId);
            BasketItems.Remove(basketItem);
        }

    }
}
