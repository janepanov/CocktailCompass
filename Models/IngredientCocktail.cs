using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CocktailCompass.Models
{
    public class IngredientCocktail
    {
        public int IngredientCocktailId { get; set; }
        public int IngredientId { get; set; }
        public virtual Ingredient Ingredient { get; set; }
        public int CocktailId { get; set; }
        public virtual Cocktail Cocktail { get; set; }
        public decimal Amount { get; set; }
        public string Unit { get; set; }

    }
}