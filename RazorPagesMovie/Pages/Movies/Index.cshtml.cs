using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Models;

namespace RazorPagesMovie.Pages.Movies
{
    public class IndexModel : PageModel
    {
        private readonly RazorPagesMovie.Models.MovieContext _context;

        public IndexModel(RazorPagesMovie.Models.MovieContext context)
        {
            _context = context;
        }

        public IList<Movie> Movie { get;set; }

        public SelectList Genres;
        public string MovieGenre { get; set; }

        /// <summary>
        /// ページの状態を初期化
        /// </summary>
        /// <param name="movieGenre"></param>
        /// <param name="searchString"></param>
        /// <returns></returns>
        public async Task OnGetAsync(string movieGenre, string searchString)
        {
            IQueryable<string> genreQuery = from m in _context.Movie
                                            orderby m.Genre
                                            select m.Genre;

            var movies = from m in _context.Movie
                         select m;

            // 映画名または映画ジャンルの絞り込み設定されている場合
            if ((!String.IsNullOrEmpty(searchString)) || (!String.IsNullOrEmpty(movieGenre)))
            {
                // 検索
                IsSearch(searchString, movieGenre, movies);
            }

            Genres = new SelectList(await genreQuery.Distinct().ToListAsync());
            Movie = await movies.ToListAsync();
        }

        /// <summary>
        /// 映画の絞り込み
        /// </summary>
        private void IsSearch(string searchString, string movieGenre, IQueryable<Movie> movies)
        {
            // 映画名の絞り込み設定されている場合
            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title.Contains(searchString));
            }

            // 映画ジャンルの絞り込み設定されている場合
            if (!String.IsNullOrEmpty(movieGenre))
            {
                movies = movies.Where(x => x.Genre == movieGenre);
            }
        }
    }
}
