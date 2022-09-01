using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Threading.Tasks;
using TMPro;

/* This Class is responsible for controlling and showing a Deck
 * It holds an Player Deck to View/Remove/Add Cards
 * And an All Cards Deck for the player to select a Card to Add
 * Sorts a Decks by HP/Rarity/Type
 */
public class DeckController : MonoBehaviour
{
    public  Deck allCardsDeck { private set; get; }
    private Deck activeDeck;

    private GameObject cardLinePrefab;
    private GameObject cardContainerPrefab;
    private Transform cardSlotPrefab;

    private VerticalLayoutGroup verticalLayoutGroup;
    private Transform[] cardGridTransforms;

    //use transform position to visualise adding and removing a Card
    private Transform activeDeckTransform;
    private Transform garbageBinTransform;

    private TMP_Text deckStatusText;// Show the Deck state to the player

    // navigation button Image to swap from adding cards to removing card and vice versa
    private Image addRemoveButtonImage;
    private Sprite toAddCardsSprite;
    private Sprite toRemoveCardsSprite;

    public static DeckController Instance { private set; get; }
    public DeckState DeckState { private set; get; }
   
    
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            cardLinePrefab = Resources.Load<GameObject>("CardsLine");
            cardContainerPrefab = Resources.Load<GameObject>("CardContainer");
            cardSlotPrefab = Resources.Load<Transform>("CardSlot");
            verticalLayoutGroup = GetComponentInChildren<VerticalLayoutGroup>();
            activeDeckTransform = GameObject.FindGameObjectWithTag("Deck").transform;
            garbageBinTransform = GameObject.FindGameObjectWithTag("Garbage").transform;
            deckStatusText = GetComponentInChildren<TMP_Text>();
            toRemoveCardsSprite = Resources.Load<Sprite>("removeCard");
            toAddCardsSprite = Resources.Load<Sprite>("addCard");

            Button addRemoveButton = GetComponentsInChildren<Button>()[3];
            addRemoveButtonImage = addRemoveButton.GetComponent<Image>();

            List<PokemonCardData> allCards = CachedCards.GetInstance.AllPokemonCardData.Values.ToList();
            allCardsDeck = new Deck(allCards);
            DeckState = DeckState.RemovingCard;
            Instance = this;
        }
        
    }

   
    public async void ShowDeck(Deck deck)
    {
        await ClearDeckAsync();
        CreateGrid(deck);
        CreateDeckCards(deck);
    }

    public void SetActiveDeck(Deck deck)
    {
        activeDeck = deck;
    }
    public void ClearDeck()
    {
        HorizontalLayoutGroup[] horizontalLayoutGroups = verticalLayoutGroup.GetComponentsInChildren<HorizontalLayoutGroup>();
        foreach (HorizontalLayoutGroup horizontalLayoutGroup in horizontalLayoutGroups)
        {
            Destroy(horizontalLayoutGroup.gameObject);
        }
    }

    //Clear deck and wait one frame to destroy the objects
    private async Task ClearDeckAsync()
    {
        ClearDeck();
        await Task.Yield();
    }

    //Create a grid of Positions depending on the size of the screen
    private void CreateGrid(Deck deck)
    {
        int cardNumber = deck.PokemonCards.Count;
        ScrollRect scrollRect = GetComponentInChildren<ScrollRect>();
        scrollRect.verticalNormalizedPosition = 1;
        int lineSize = 4;
        int lines = (int)(cardNumber / lineSize)+1;
        cardGridTransforms = new Transform[lineSize * lines];
        int index = 0;
        for (int i = 0; i < lines; i++)
        {
            GameObject cardLine = Instantiate(cardLinePrefab, verticalLayoutGroup.transform);
            for(int j = 0; j < lineSize; j++)
            {
                Transform newCardSlot = Instantiate(cardSlotPrefab, cardLine.transform);
                cardGridTransforms[index] = newCardSlot;
                index++;
            }
        }
 
        RectTransform verticalLayoutGroupRectTransform = verticalLayoutGroup.GetComponent<RectTransform>();
        LayoutRebuilder.ForceRebuildLayoutImmediate(verticalLayoutGroupRectTransform);

    }


    private void CreateDeckCards(Deck deck)
    {
        int index = 0;
        foreach(PokemonCardData pokemonCardData in deck.PokemonCards)
        {
            GameObject newCardContainerGO = Instantiate(cardContainerPrefab, verticalLayoutGroup.transform);
            RectTransform rectTransform = newCardContainerGO.GetComponent<RectTransform>();
            CardContainer newCardContainer = newCardContainerGO.GetComponent<CardContainer>();
            newCardContainer.Initialise(pokemonCardData);
            newCardContainer.MoveToPosition(cardGridTransforms[index].transform,true);
            index++;
        }
    }

    public void AddCardToActiveDeck(CardContainer cardContainer)
    {
        activeDeck.AddCard(cardContainer.pokemonCardData);
        GameObject newCardContainerGO = Instantiate(cardContainerPrefab, cardContainer.transform);
        CardContainer newCardContainer = newCardContainerGO.GetComponent<CardContainer>();
        newCardContainer.Initialise(cardContainer.pokemonCardData);
        newCardContainer.MoveToPosition(activeDeckTransform,false);
        newCardContainer.ScaleTransform(Vector2.zero);
    }

    public void RemoveCardFromActiveDeck(CardContainer cardContainer)
    {
        activeDeck.RemoveCard(cardContainer.pokemonCardData);
        cardContainer.ScaleTransform(Vector2.zero);
        cardContainer.MoveToPosition(garbageBinTransform,false);
    }

    public void ChangeStatus(DeckState deckState)
    {
        DeckState = deckState;
        ChangeAddRemoveButtonText(DeckState);
        ChangeDeckStatusText(DeckState);
    }

    public void AddRemoveCardButton()
    {
        if (DeckState == DeckState.AddingCard)
        {
            ChangeStatus(DeckState.RemovingCard);
            ShowDeck(activeDeck);
        }
        else
        {
            ChangeStatus(DeckState.AddingCard);
            ShowDeck(allCardsDeck);
        }    
    }
    private void ChangeDeckStatusText(DeckState deckState)
    {
        if (deckState == DeckState.AddingCard)
        {
            deckStatusText.text = "Select a Card to Add";
        }
        else
        {
            deckStatusText.text = "Select a Card to Remove";
        }
    }
    private void ChangeAddRemoveButtonText(DeckState deckState)
    {
        if (deckState == DeckState.AddingCard)
        {
            addRemoveButtonImage.sprite = toRemoveCardsSprite;
        }
        else
        {
            addRemoveButtonImage.sprite = toAddCardsSprite;
        }
    }
    private void RearangeCardContainers(List<PokemonCardData> pokemonCardDatas)
    {
        List<CardContainer> cardContainers = GetComponentsInChildren<CardContainer>().ToList();

        for (int i = 0; i < pokemonCardDatas.Count; i++)
        {
            for(int j=cardContainers.Count-1; j>=0; j--)
            {
                if(pokemonCardDatas[i] == cardContainers[j].pokemonCardData)
                {
                    cardContainers[j].MoveToPosition(cardGridTransforms[i].transform,true);
                    cardContainers.RemoveAt(j);
                    break;
                }
            }
            
        }
    }
    public void SortByHP()
    {
        List<PokemonCardData> sortedPokemonCardDatas = GetShowingPokemonCardData();
        sortedPokemonCardDatas=sortedPokemonCardDatas.OrderBy(n => n.HP).ToList();
        RearangeCardContainers(sortedPokemonCardDatas);
    } 
    public void SortByRarity()
    {
        List<PokemonCardData> sortedPokemonCardDatas = GetShowingPokemonCardData();
        sortedPokemonCardDatas=sortedPokemonCardDatas.OrderBy(n => n.Rarity.INTRarity()).ToList();
        RearangeCardContainers(sortedPokemonCardDatas);
    }
    public void SortByType()
    {
        List<PokemonCardData> sortedPokemonCardDatas = GetShowingPokemonCardData();
        sortedPokemonCardDatas=sortedPokemonCardDatas.OrderBy(n => n.Type).ToList();
        RearangeCardContainers(sortedPokemonCardDatas);
    }
    private List<PokemonCardData> GetShowingPokemonCardData()
    {
        CardContainer[] showingCardContainers = GetComponentsInChildren<CardContainer>();
        List<PokemonCardData> showingPokemoncardDatas = new List<PokemonCardData>();
        foreach (CardContainer cardContainer in showingCardContainers)
        {
            showingPokemoncardDatas.Add(cardContainer.pokemonCardData);
        }
        return showingPokemoncardDatas;
    }

    private void OnDestroy()
    {
        Instance = null;
    }
}
[System.Serializable]
public enum DeckState { AddingCard,RemovingCard}
