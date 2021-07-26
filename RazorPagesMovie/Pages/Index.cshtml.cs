using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMDbLib.Client;
using TMDbLib.Objects.Movies;

namespace RazorPagesMovie.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public string Title;

        public Task OnGetAsync()
        {
            // TMDbのAPIキー
            TMDbClient client = new TMDbClient("c807e25e9945dcb331636165896edb32");

            Movie movie = client.GetMovieAsync(47964).Result;

            Title = movie.Title;

            return Task.CompletedTask;
        }
    }
}
