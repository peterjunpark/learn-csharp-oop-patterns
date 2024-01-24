using Peter.CookiesCookbook.Ingredients;

namespace Peter.CookiesCookbook.Recipes
{
    public class Recipe(IEnumerable<Ingredient> ingredients)
    {
        public IEnumerable<Ingredient> Ingredients { get; } = ingredients;

        public override string ToString()
        {
            List<string> steps = [];

            foreach (Ingredient ingredient in Ingredients)
            {
                steps.Add($"{ingredient.Name}. {ingredient.PreparationInstructions}");
            }

            return string.Join(Environment.NewLine, steps);
        }
    }
}
