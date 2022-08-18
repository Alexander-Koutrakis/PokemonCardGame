using System.Collections.Generic;
using System.Linq;
public class Deck 
{
    public List<PokemonCardData> PokemonCards { get { return this.pokemonCards; } }
    private List<PokemonCardData> pokemonCards;
    public Deck(List<PokemonCardData> pokemonCards)
    {
        this.pokemonCards = pokemonCards;
    }

    public Deck()
    {
        this.pokemonCards =new List<PokemonCardData>();
    }

    public void AddCard(PokemonCardData pokemonCardData)
    {
        pokemonCards.Add(pokemonCardData);
        SaveSystem.SaveGame();
    }

    public void RemoveCard(PokemonCardData pokemonCardData)
    {
        if (pokemonCards.Contains(pokemonCardData))
        {
            pokemonCards.Remove(pokemonCardData);
            SaveSystem.SaveGame();
        }
    }
}
