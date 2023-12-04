using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CocktailCompass.Models
{
    public class Ingredient
    {
        public Ingredient() { 
            Cocktails = new List<IngredientCocktail>();
        }
        public int IngredientId { get; set; }
        public string Name { get; set; }
        public string ImageURL { get; set; }
        public virtual ICollection<IngredientCocktail> Cocktails { get; set; }
    }
}