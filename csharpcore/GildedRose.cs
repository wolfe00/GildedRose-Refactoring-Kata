using System;
using System.Collections.Generic;
using System.Linq;

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
            var increment = 0;
            switch (ItemType(item))
            {
                case "Sulfuras, Hand of Ragnaros":
                    break;
                
                case "Aged Brie":
                    increment = !HasExpired(item)
                        ? 1
                        : 2;
                    break;
                
                case "Backstage passes to a TAFKAL80ETC concert":
                    if (item.SellIn > 10)
                    {
                        increment = 1;
                    }
                    else if (item.SellIn > 5)
                    {
                        increment = 2;
                    }
                    else if (item.SellIn > 0)
                    {
                        increment = 3;
                    }
                    else
                    {
                        increment = MinQuality - MaxQuality;
                    }
                    break;
                
                default:
                    increment = !HasExpired(item)
                        ? -1
                        : -2;
                    break;
            }

            if (IsConjured(item))
            {
                increment *= 2;
            }

            if (item.Name != "Sulfuras, Hand of Ragnaros")
            {
                IncrementQuality(item, increment);
                item.SellIn -= 1;
            }
        }
        static bool HasExpired(Item item)
        {
            return item.SellIn <= 0;
        }

        static string ItemType(Item item)
        {
            return IsConjured(item) ? item.Name.Substring("Conjured ".Length, item.Name.Length - "Conjured ".Length) : item.Name;
        }

        static bool IsConjured(Item item)
        {
            return item.Name.Substring(0, "Conjured ".Length) == "Conjured ";
        }

        static void IncrementQuality(Item item, int increment)
        {
            item.Quality += increment;
            item.Quality = Math.Min(MaxQuality, item.Quality);
            item.Quality = Math.Max(MinQuality, item.Quality);
        }
    }
}
