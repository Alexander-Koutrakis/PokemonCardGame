
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
        Deck allCardsDeck = DeckController.allCardsDeck;
        for(int i=0;i< pokemonCardID.Length; i++)
        {
            for(int j = 0; j < allCardsDeck.PokemonCards.Count; j++)
            {
                if (allCardsDeck.PokemonCards[j].ID == pokemonCardID[i])
                {
                    pokemonCardDatas.Add(allCardsDeck.PokemonCards[j]);
                }
            }
         
        }
        Deck deck = new Deck(pokemonCardDatas);
        return deck;
    }
}
