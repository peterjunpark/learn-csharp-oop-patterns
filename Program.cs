using Peter.CookiesCookbook.App;
using Peter.CookiesCookbook.Ingredients;
using Peter.CookiesCookbook.Recipes;
using Peter.CookiesCookbook.Utils;

const string fileName = "recipes";
FileMetadata fileMetadata = new(fileName, FileFormat.Json);

IStringsRepository stringsRepository =
    fileMetadata.Format == FileFormat.Json
        ? new StringsJsonRepository()
        : new StringsTextRepository();
IIngredientsStore ingredientsStore = new IngredientsStore();

CookiesRecipesApp cookiesRecipesApp =
    new(
        new RecipesRepository(stringsRepository, ingredientsStore),
        new RecipesConsoleUserInteraction(ingredientsStore)
    );

cookiesRecipesApp.Run(fileMetadata.ToPath());
