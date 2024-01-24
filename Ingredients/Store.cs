namespace Peter.CookiesCookbook.Ingredients
{
    public interface IIngredientsStore
    {
        IEnumerable<Ingredient> All { get; }

        Ingredient GetById(int ingredientId);
    }

    public class IngredientsStore : IIngredientsStore
    {
        public IEnumerable<Ingredient> All { get; } =
            new List<Ingredient>
            {
                new WheatFlour(),
                new SpeltFlour(),
                new Butter(),
                new Chocolate(),
                new Cardamom(),
                new Cinnamon(),
                new CocoaPowder(),
                new Sugar(),
            };

        public Ingredient GetById(int ingredientId)
        {
            foreach (Ingredient ingredient in All)
            {
                if (ingredient.Id == ingredientId)
                {
                    return ingredient;
                }
            }
            return null;
        }
    }
}
