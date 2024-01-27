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
            IEnumerable<Ingredient> ingredients = recipeFromFile
                .Split(Separator)
                .Select(int.Parse)
                .Select(_ingredientsStore.GetById);

            return new Recipe(ingredients);
        }

        public List<Recipe> Read(string filePath)
        {
            return _stringsRepository.Read(filePath).Select(RecipeFromString).ToList();
        }

        public void Write(string filePath, List<Recipe> allRecipes)
        {
            IEnumerable<string> recipesAsStrings = allRecipes.Select(recipe =>
            {
                IEnumerable<int> ingredientIds = recipe.Ingredients.Select(ingredient =>
                    ingredient.Id
                );

                return string.Join(
                    Separator,
                    recipe.Ingredients.Select(ingredient => ingredient.Id)
                );
            });

            _stringsRepository.Write(filePath, recipesAsStrings.ToList());
        }
    }
}
