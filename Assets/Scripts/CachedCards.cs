
using System.Collections.Generic;


public class CachedCards 
{
    public Dictionary<string, PokemonCardData> AllPokemonCardData{ private set; get;}
    private static CachedCards instance;
    public static CachedCards GetInstance {get
        {
            if (instance == null)
            {
                instance = new CachedCards();
            }
            return instance;
        } }

    public void LoadCards(List<PokemonCardData> pokemonCardDatas)
    {
        AllPokemonCardData = new Dictionary<string, PokemonCardData>();
        foreach (PokemonCardData pokemonCardData in pokemonCardDatas)
        {
            AllPokemonCardData.Add(pokemonCardData.ID, pokemonCardData);
        }
    }
}
