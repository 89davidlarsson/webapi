using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BannerApplication.Models
{
    public interface IBannerRepository
    {
        IEnumerable<Banner> GetAll();
        Banner Get(int id);
        Banner Add(Banner item);
        void Remove(int id);
        bool Update(Banner item);
    }
}
