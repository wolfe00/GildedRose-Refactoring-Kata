using System;
using System.Collections.Generic;

namespace csharpcore
{
    public class GildedRose
    {
        IList<Item> Items;
        static int MinQuality = 0;
        static int MaxQuality = 50;
        
        public GildedRose(IList<Item> Items)
        {
            this.Items = Items;
        }

        public void UpdateQuality()
        {
            foreach (var item in Items)
            {
                UpdateItem(item);
            }
        }

        private static void UpdateItem(Item item)
        {
            switch (item.Name)
            {
                case "Sulfuras, Hand of Ragnaros":
                    break;
                
                case "Aged Brie":
                    item.Quality = !hasExpired(item)
                        ? Math.Min(MaxQuality, item.Quality + 1)
                        : Math.Min(MaxQuality, item.Quality + 2);
                    break;
                
                case "Backstage passes to a TAFKAL80ETC concert":
                    if (item.SellIn > 10)
                    {
                        item.Quality = Math.Min(MaxQuality, item.Quality + 1);
                    }
                    else if (item.SellIn > 5)
                    {
                        item.Quality = Math.Min(MaxQuality, item.Quality + 2);
                    }
                    else if (item.SellIn > 0)
                    {
                        item.Quality = Math.Min(MaxQuality, item.Quality + 3);
                    }
                    else
                    {
                        item.Quality = 0;
                    }
                    break;
                
                default:
                    item.Quality = !hasExpired(item)
                        ? Math.Max(MinQuality, item.Quality - 1)
                        : Math.Max(MinQuality, item.Quality - 2);
                    break;
            }
            
            if (item.Name != "Sulfuras, Hand of Ragnaros")
            {
                item.SellIn -= 1;
            }
        }
        static bool hasExpired(Item item)
        {
            return item.SellIn <= 0;
        }
    }
}
