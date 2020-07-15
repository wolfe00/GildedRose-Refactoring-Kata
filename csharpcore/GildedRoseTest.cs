using Xunit;
using System.Collections.Generic;

namespace csharpcore
{
    public class GildedRoseTest
    {
        [Fact]
        public void CheckItemAddsProperly()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Just a regular item, nothing to see here", SellIn = 10, Quality = 20 } };
            GildedRose app = new GildedRose(Items);
            Assert.Equal("Just a regular item, nothing to see here", Items[0].Name);
            Assert.Equal(10, Items[0].SellIn);
            Assert.Equal(20, Items[0].Quality);
        }

        [Fact]
        public void CheckRegularUnexpiredItemDecaysProperly()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Just a regular item, nothing to see here", SellIn = 10, Quality = 20 } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.Equal("Just a regular item, nothing to see here", Items[0].Name);
            Assert.Equal(9, Items[0].SellIn);
            Assert.Equal(19, Items[0].Quality);
        }
        
        [Fact]
        public void CheckRegularExpiredItemDecaysProperly()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Just a regular item, nothing to see here", SellIn = -5, Quality = 20 } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.Equal("Just a regular item, nothing to see here", Items[0].Name);
            Assert.True(Items[0].SellIn <= 0);
            Assert.Equal(18, Items[0].Quality);
        }

        [Fact]
        public void CheckRegularExpiryEdgeCase()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Just a regular item, nothing to see here", SellIn = 1, Quality = 20 } };
            GildedRose app = new GildedRose(Items);
            Assert.Equal("Just a regular item, nothing to see here", Items[0].Name);
            app.UpdateQuality();
            Assert.Equal(0, Items[0].SellIn);
            Assert.Equal(19, Items[0].Quality);
            app.UpdateQuality();
            Assert.True(Items[0].SellIn <= 0);
            Assert.Equal(17, Items[0].Quality);
        }

        [Fact]
        public void CheckRegularQualityNeverDropsBelowZero()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Just a regular item, nothing to see here", SellIn = 10, Quality = 1 } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            app.UpdateQuality();
            Assert.Equal(0, Items[0].Quality);
        }
        
        [Fact]
        public void CheckExpiredQualityDoesntJumpBelowZero()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Just a regular item, nothing to see here", SellIn = -5, Quality = 1 } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.Equal(0, Items[0].Quality);
        }

        [Fact]
        public void CheckEmptyInventoriesBehave()
        {
            IList<Item> Items = new List<Item> {};
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
        }
        
        [Fact]
        public void CheckMultiItemInventoriesBehave()
        {
            IList<Item> Items = new List<Item>
            {
                new Item { Name = "Just a regular item, nothing to see here", SellIn = -5, Quality = 1 },
                new Item { Name = "My friend I assure you this is also just a regular item", SellIn = 0, Quality = 4}
            };
            
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.Equal(0, Items[0].Quality);
            Assert.Equal(2, Items[1].Quality);
        }
        
        [Fact]
        public void CheckSulfurasNeverDecays()
        {
            IList<Item> Items = new List<Item>
            {
                new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = -5, Quality = 80 },
                new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80 },
                new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 10, Quality = 80 }
            };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.Equal(80, Items[0].Quality); 
            Assert.Equal(80, Items[1].Quality);
            Assert.Equal(80, Items[2].Quality); 
        }

        [Fact]
        public void CheckUnexpiredNonValuableAgedBrieBehavesCorrectly()
        {
            IList<Item> Items = new List<Item> {new Item { Name = "Aged Brie", SellIn = 10, Quality = 30 }};
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.Equal(31, Items[0].Quality);
        }
        
        [Fact]
        public void CheckExpiredNonValuableAgedBrieBehavesCorrectly()
        {
            IList<Item> Items = new List<Item> {new Item { Name = "Aged Brie", SellIn = -10, Quality = 30 }};
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.Equal(32, Items[0].Quality);
        }
        
        [Fact]
        public void CheckExpiredNonValuableAgedBrieEdgeCase()
        {
            IList<Item> Items = new List<Item>
            {
                new Item { Name = "Aged Brie", SellIn = 0, Quality = 30 },
                new Item { Name = "Aged Brie", SellIn = 1, Quality = 30 }
            };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.Equal(32, Items[0].Quality);
            Assert.Equal(31, Items[1].Quality);
        }
        
        [Fact]
        public void CheckAgedBrieValueNeverStepsOver50()
        {
            IList<Item> Items = new List<Item>
            {
                new Item { Name = "Aged Brie", SellIn = -5, Quality = 50 },
                new Item { Name = "Aged Brie", SellIn = 0, Quality = 50 },
                new Item { Name = "Aged Brie", SellIn = 1, Quality = 50 }
            };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.Equal(50, Items[0].Quality);
            Assert.Equal(50, Items[1].Quality);
            Assert.Equal(50, Items[2].Quality);
        }
        
        [Fact]
        public void CheckAgedBrieValueNeverJumpsOver50()
        {
            IList<Item> Items = new List<Item>
            {
                new Item { Name = "Aged Brie", SellIn = -5, Quality = 49 },
                new Item { Name = "Aged Brie", SellIn = 0, Quality = 49 },
            };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.Equal(50, Items[0].Quality);
            Assert.Equal(50, Items[1].Quality);
        }
        
        [Fact]
        public void CheckNonValuableBackstagePassesBehaveNormallyAwayFromExpiry()
        {
            IList<Item> Items = new List<Item> {new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 20, Quality = 30 }};
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.Equal(31, Items[0].Quality);
        }
        
        [Fact]
        public void CheckNonValuableBackstagePassesBehaveProperly6to10DaysFromExpiry()
        {
            IList<Item> Items = new List<Item> {new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 7, Quality = 30 }};
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.Equal(32, Items[0].Quality);
        }
        
        [Fact]
        public void CheckNonValuableBackstagePassesBehaveProperly1to5DaysFromExpiry()
        {
            IList<Item> Items = new List<Item> {new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 3, Quality = 30 }};
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.Equal(33, Items[0].Quality);
        }
        
        [Fact]
        public void CheckBackstagePassesExpireProperly()
        {
            IList<Item> Items = new List<Item> {new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 0, Quality = 30 }};
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.Equal(0, Items[0].Quality);
        }
        
        [Fact]
        public void CheckNonValuableBackstagePassEdgeDaysBehaveProperly()
        {
            IList<Item> Items = new List<Item>
            {
                new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 0, Quality = 30 },
                new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 1, Quality = 30 },
                new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 5, Quality = 30 },
                new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 6, Quality = 30 },
                new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 10, Quality = 30 },
                new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 11, Quality = 30 }
            };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.Equal(0, Items[0].Quality);
            Assert.Equal(33, Items[1].Quality);
            Assert.Equal(33, Items[2].Quality);
            Assert.Equal(32, Items[3].Quality);
            Assert.Equal(32, Items[4].Quality);
            Assert.Equal(31, Items[5].Quality);
        }
        
        [Fact]
        public void CheckBackstagePassValueNeverStepsOver50()
        {
            IList<Item> Items = new List<Item>
            {
                new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 2, Quality = 50 },
                new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 7, Quality = 50 },
                new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 12, Quality = 50 }
            };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.Equal(50, Items[0].Quality);
            Assert.Equal(50, Items[1].Quality);
            Assert.Equal(50, Items[2].Quality);
        }
        
        [Fact]
        public void CheckBackstagePassValueNeverJumpsOver50()
        {
            IList<Item> Items = new List<Item>
            {
                new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 2, Quality = 49 },
                new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 7, Quality = 49 },
            };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.Equal(50, Items[0].Quality);
            Assert.Equal(50, Items[1].Quality);
        }
    }
}