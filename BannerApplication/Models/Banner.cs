using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BannerApplication.Models
{
    public class Banner
    {
        public int Id { get; set; }
        public string Html { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }

    public class MarkupErrors
    {
        public string ErrorCode { get; set; }
        public string ErrorReason { get; set; }
    }
}