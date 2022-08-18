using System.Collections.Generic;


//Save the list of Player Decks as an Array of SavedDeck
//Each SavedDeck is a string[] of CardIDs
[System.Serializable]
public class Save 
{
    public SavedDeck[] savedDecks;

    public Save(List<Deck> decks)
    {
        savedDecks = new SavedDeck[decks.Count];
        for (int i = 0; i < decks.Count; i++)
        {
            savedDecks[i] = new SavedDeck(decks[i]);
        }
        
    }

    public List<Deck> SaveToList()
    {
        List<Deck> decks = new List<Deck>();
        for(int i = 0; i < savedDecks.Length; i++)
        {
            Deck loadedDeck = savedDecks[i].SavedDeckToDeck();
            decks.Add(loadedDeck);
        }

        return decks;
    }
}
