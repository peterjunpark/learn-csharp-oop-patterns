using Peter.CookiesCookbook.Ingredients;
using Peter.CookiesCookbook.Utils;

namespace Peter.CookiesCookbook.Recipes
{
    public interface IRecipesRepository
    {
        List<Recipe> Read(string filePath);
        void Write(string filePath, List<Recipe> allRecipes);
    }

    public class RecipesRepository(
        IStringsRepository stringsRepository,
        IIngredientsStore ingredientsStore
    ) : IRecipesRepository
    {
        private const string Separator = ",";
        private readonly IStringsRepository _stringsRepository = stringsRepository;
        private readonly IIngredientsStore _ingredientsStore = ingredientsStore;

        public Recipe RecipeFromString(string recipeFromFile)
        {
            string[] textualIds = recipeFromFile.Split(Separator);
            List<Ingredient> ingredients = [];

            foreach (string textualId in textualIds)
            {
                int id = int.Parse(textualId);
                Ingredient ingredient = _ingredientsStore.GetById(id);
                ingredients.Add(ingredient);
            }

            return new Recipe(ingredients);
        }

        public List<Recipe> Read(string filePath)
        {
            List<string> recipesFromFile = _stringsRepository.Read(filePath);
            List<Recipe> recipes = [];

            foreach (string recipeFromFile in recipesFromFile)
            {
                Recipe recipe = RecipeFromString(recipeFromFile);
                recipes.Add(recipe);
            }

            return recipes;
        }

        public void Write(string filePath, List<Recipe> allRecipes)
        {
            List<string> recipesAsStrings = [];

            foreach (Recipe recipe in allRecipes)
            {
                List<int> ingredientIds = [];
                foreach (Ingredient ingredient in recipe.Ingredients)
                {
                    ingredientIds.Add(ingredient.Id);
                }

                recipesAsStrings.Add(string.Join(Separator, ingredientIds));
            }

            _stringsRepository.Write(filePath, recipesAsStrings);
        }
    }
}
