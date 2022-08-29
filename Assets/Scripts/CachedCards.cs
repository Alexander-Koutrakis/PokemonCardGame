using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CachedCards 
{
    public Dictionary<string, PokemonCardData> AllPokemonCardData{ private set; get;}
    private static CachedCards instance;
    public static CachedCards Instance {get
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
