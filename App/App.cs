using Peter.CookiesCookbook.Ingredients;
using Peter.CookiesCookbook.Recipes;

namespace Peter.CookiesCookbook.App
{
    public class CookiesRecipesApp(
        IRecipesRepository recipesRepository,
        IRecipesUserInteraction recipesUserInteraction
    )
    {
        private readonly IRecipesRepository _recipesRepository = recipesRepository;
        private readonly IRecipesUserInteraction _recipesUserInteraction = recipesUserInteraction;

        public void Run(string filePath)
        {
            List<Recipe> allRecipes = _recipesRepository.Read(filePath);
            _recipesUserInteraction.PrintExistingRecipes(allRecipes);
            _recipesUserInteraction.PromptToCreateRecipe();

            IEnumerable<Ingredient> ingredients = _recipesUserInteraction.ReadIngredientsFromUser();

            if (ingredients.Any())
            {
                Recipe recipe = new(ingredients);
                allRecipes.Add(recipe);
                _recipesRepository.Write(filePath, allRecipes);

                _recipesUserInteraction.ShowMessage("Recipe added:");
                _recipesUserInteraction.ShowMessage(recipe.ToString());
            }
            else
            {
                _recipesUserInteraction.ShowMessage(
                    "No ingredients have been selected. " + "Recipe will not be saved."
                );
            }

            _recipesUserInteraction.Quit();
        }
    }
}
