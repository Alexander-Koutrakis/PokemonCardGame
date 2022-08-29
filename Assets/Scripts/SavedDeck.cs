
using System.Collections.Generic;

//Save each cardID
[System.Serializable]
public struct SavedDeck 
{
    public string[] pokemonCardID;

    public SavedDeck(Deck deck)
    {
        pokemonCardID = new string[deck.PokemonCards.Count];
        for(int i = 0; i < pokemonCardID.Length; i++)
        {
            pokemonCardID[i] = deck.PokemonCards[i].ID;
        }
    }
   
    public Deck SavedDeckToDeck()
    {
        List<PokemonCardData> pokemonCardDatas = new List<PokemonCardData>();
        for (int i = 0; i < pokemonCardID.Length; i++)
        {
            PokemonCardData pokemonCardData = CachedCards.Instance.AllPokemonCardData[pokemonCardID[i]];
            pokemonCardDatas.Add(pokemonCardData);           
        }
        Deck deck = new Deck(pokemonCardDatas);
        return deck;
    }
}
