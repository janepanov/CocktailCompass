using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CocktailCompass.Models
{
    public class CocktailFavorite
    {
        public int CocktailFavoriteId { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public int CocktailId { get; set; }
        public virtual Cocktail Cocktail { get; set; }
        
    }
}