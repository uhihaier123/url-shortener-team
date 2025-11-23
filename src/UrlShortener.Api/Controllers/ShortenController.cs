using Microsoft.AspNetCore.Mvc;

namespace UrlShortener.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShortenController : ControllerBase
    {
        // POST /api/shorten
        public record ShortenRequest(string Url);
        public record ShortenResponse(string ShortCode, string ShortUrl);

        [HttpPost]
        public IActionResult Post([FromBody] ShortenRequest req)
        {
            if (string.IsNullOrWhiteSpace(req?.Url))
                return BadRequest(new { error = "Missing url" });

            // naive short code: take a hash of the url and keep first 6 chars
            var code = Math.Abs(req.Url.GetHashCode()).ToString("X");
            if (code.Length > 6) code = code.Substring(0, 6);

            var shortUrl = $"{Request.Scheme}://{Request.Host}/{code}";
            return Ok(new ShortenResponse(code, shortUrl));
        }
    }
}
