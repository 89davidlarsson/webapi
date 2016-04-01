using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BannerApplication.Models
{
    public class BannerRepository : IBannerRepository
    {
        private List<Banner> banners = new List<Banner>();
        private int _nextId = 1;

        public BannerRepository()
        {
            Add(new Banner { Html = "<!DOCTYPE html><html><head></head><body>Hello World, 123</body></html>", Created = DateTime.Today, Modified = DateTime.Today });
            Add(new Banner { Html = "</html>", Created = DateTime.Now, Modified = DateTime.Today.AddYears(5) });
        }

        public IEnumerable<Banner> GetAll()
        {
            return banners;
        }

        public Banner Get(int id)
        {
            return banners.FirstOrDefault(b => b.Id == id);
        }

        public Banner Add(Banner item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            item.Id = _nextId++;
            item.Created = DateTime.Today;
            banners.Add(item);
            return item;
        }

        public void Remove(int id)
        {
            banners.RemoveAll(b => b.Id == id);
        }

        public bool Update(Banner item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            int index = banners.FindIndex(b => b.Id == item.Id);
            if (index == -1)
            {
                return false;
            }
            banners.RemoveAt(index);
            banners.Add(item);
            return true;
        }        
    }
}