using Peter.CookiesCookbook.Ingredients;

namespace Peter.CookiesCookbook.Recipes
{
    public class Recipe(IEnumerable<Ingredient> ingredients)
    {
        public IEnumerable<Ingredient> Ingredients { get; } = ingredients;

        public override string ToString()
        {
            IEnumerable<string> steps = Ingredients.Select(ingredient =>
                $"{ingredient.Name}. {ingredient.PreparationInstructions}"
            );

            return string.Join(Environment.NewLine, steps);
        }
    }
}
