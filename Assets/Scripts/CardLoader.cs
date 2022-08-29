using System.Collections.Generic;
using System.Threading.Tasks;
using PokemonTcgSdk;
using PokemonTcgSdk.Models;

//This class is responsible for loading the data from pokemonTGC.io
//in json format and save them in PokemonCardData
public static class CardLoader
{
    private static int cardsLoaded = 0;
    public static string Status { private set; get; }
    public static async Task LoadPokemonCards()
    {       
        Pokemon pokemon = await GetCards();
        List<PokemonCardData> pokemonCardData = await SaveCardsinPokemonCardData(pokemon);
        CachedCards.Instance.LoadCards(pokemonCardData);
    }

   

    //Get cards in Json format and save them in Pokemon format
    private static async Task<Pokemon> GetCards()
    {
        Dictionary<string, string> query = new Dictionary<string, string>()
        {
            { "supertype", "Pokémon" },
        };

        Pokemon pokemon = null;
        do
        {
            Status = "Connecting";
            pokemon = await Card.GetAsync(query);
        } while (pokemon.Cards == null);
        Status = "Connected";
        return pokemon;
    }

    //Save data in pokemonCardData and download card Image
    private static async Task<List<PokemonCardData>> SaveCardsinPokemonCardData(Pokemon pokemon)
    {
        List<PokemonCardData> pokemonCardDatas = new List<PokemonCardData>();

        foreach (PokemonCard pokemonCard in pokemon.Cards)
        {
            PokemonCardData newCard = new PokemonCardData(pokemonCard);
            await ImageLoader.SetSpriteAsync(pokemonCard.ImageUrl, newCard);
            pokemonCardDatas.Add(newCard);
            cardsLoaded++;
            
            Status = "Loading "+cardsLoaded+" %";
        }
        return pokemonCardDatas;
    }
}
