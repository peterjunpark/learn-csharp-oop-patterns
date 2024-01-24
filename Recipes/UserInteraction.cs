using Peter.CookiesCookbook.Ingredients;

namespace Peter.CookiesCookbook.Recipes
{
    public interface IRecipesUserInteraction
    {
        void ShowMessage(string message);
        void Quit();
        void PrintExistingRecipes(IEnumerable<Recipe> allRecipes);
        void PromptToCreateRecipe();
        IEnumerable<Ingredient> ReadIngredientsFromUser();
    }

    public class RecipesConsoleUserInteraction(IIngredientsStore ingredientsStore)
        : IRecipesUserInteraction
    {
        private readonly IIngredientsStore _ingredientsStore = ingredientsStore;

        public void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void Quit()
        {
            Console.WriteLine("Press any key to close.");
            _ = Console.ReadKey();
        }

        public void PrintExistingRecipes(IEnumerable<Recipe> allRecipes)
        {
            if (!allRecipes.Any())
            {
                return;
            }

            int counter = 1;
            Console.WriteLine("Existing recipes:" + Environment.NewLine);
            foreach (Recipe recipe in allRecipes)
            {
                Console.WriteLine($" * {counter}");
                Console.WriteLine(recipe);
                counter++;
            }
        }

        public void PromptToCreateRecipe()
        {
            Console.WriteLine("Create a new cookie recipe.\nAvailable ingredients are:");

            foreach (Ingredient ingredient in _ingredientsStore.All)
            {
                Console.WriteLine(ingredient);
            }
        }

        public IEnumerable<Ingredient> ReadIngredientsFromUser()
        {
            bool shouldStop = false;
            List<Ingredient> ingredients = [];

            while (!shouldStop)
            {
                Console.WriteLine(
                    "Add an ingredient by ID. Or type anything else to finish adding ingredients."
                );
                string userInput = Console.ReadLine();

                if (int.TryParse(userInput, out int ingredientId))
                {
                    Ingredient selectedIngredient = _ingredientsStore.GetById(ingredientId);

                    if (selectedIngredient is not null)
                    {
                        ingredients.Add(selectedIngredient);
                    }
                }
                else
                {
                    shouldStop = true;
                }
            }

            return ingredients;
        }
    }
}
