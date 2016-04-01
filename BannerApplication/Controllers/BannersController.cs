using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BannerApplication.Models;

namespace BannerApplication.Controllers
{
    public class BannersController : ApiController
    {
        static readonly IBannerRepository repository = new BannerRepository();

        public IEnumerable<Banner> GetAllBanners()
        {
            return repository.GetAll();
        }
        // Create
        public HttpResponseMessage PostBanner(Banner item)
        {
            item = repository.Add(item);
            var response = Request.CreateResponse<Banner>(HttpStatusCode.Created, item);

            string uri = Url.Link("DefaultApi", new { id = item.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }
        // Read
        public Banner GetBanner(int id)
        {
            Banner item = repository.Get(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return item;
        }
        // Update
        public void PutBanner(int id, Banner banner)
        {
            banner.Id = id;
            if (!repository.Update(banner))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }
        // Delete
        public void DeleteBanner(int id)
        {
            Banner item = repository.Get(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        // Validate Banner HTML, using HTML Agility Pack
        // Provides hints, e.g. a missing tag, and runs a very basic check if the string is plain text or potentially HTML.
        public List<MarkupErrors> IsMarkupValid(string html)
        {
            var document = new HtmlAgilityPack.HtmlDocument();
            document.OptionFixNestedTags = true;
            document.LoadHtml(html);

            var parseErrors = new List<MarkupErrors>();

            if (!html.EndsWith(">"))
            {
                parseErrors.Add(new MarkupErrors
                {
                    ErrorCode = "",
                    ErrorReason = "Invalid HTML."
                });
                return parseErrors;
            }
            foreach (var error in document.ParseErrors)
            {
                parseErrors.Add(new MarkupErrors
                {
                    ErrorCode = error.Code.ToString(),
                    ErrorReason = error.Reason
                });
            }
            return parseErrors;
        }

        // Get the HTML string stored in the banner and return it as string
        public string RenderHtml(int id)
        {
            Banner banner = repository.Get(id);
            var htmlString = new HtmlAgilityPack.HtmlDocument();
            htmlString.LoadHtml(banner.Html);
            return htmlString.DocumentNode.OuterHtml;
        }
    }
}
