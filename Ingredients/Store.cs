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
            var allIngredientsWithId = All.Where(ingredient => ingredient.Id = ingredientId);

            if (allIngredientsWithId.Count() > 1)
            {
                throw new InvalidOperationException(
                    $"More than one ingredient with id equal to {ingredientId}"
                );
            }

            // if (All.Select(ingredient => ingredient.Id).Distinct().Count() != All.Count())
            // {
            //     throw new InvalidOperationException($"Some ingredients have duplciate ids.");
            // }

            return All.FirstOrDefault();
        }
    }
}
