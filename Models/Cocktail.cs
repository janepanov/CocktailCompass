using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CocktailCompass.Models
{
    public class Cocktail
    {
        public Cocktail() {
            Ingredients = new List<IngredientCocktail>();
        }

        public int CocktailId { get; set; }
        public string Name { get; set; }
        public string Instructions { get; set; }
        public string ImageURL { get; set; }
        public virtual ICollection<IngredientCocktail> Ingredients { get; set; }
    }
}