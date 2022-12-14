using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/*This class is responsible for showing all the Player Decks
 * and Select one of them as Active
 */
public class DeckSelector : MonoBehaviour
{
    private VerticalLayoutGroup verticalLayoutGroup;
    private GameObject deckHolderPrefab;
    private GameObject deckLinePrefab;
    private Sprite createdDeckSprite;
    public List<Deck> AvailableDecks { private set; get; }
    public static DeckSelector Instance { private set; get; }
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            verticalLayoutGroup = GetComponentInChildren<VerticalLayoutGroup>();
            deckHolderPrefab = Resources.Load<GameObject>("DeckContainer");
            deckLinePrefab = Resources.Load<GameObject>("DeckLine");
            createdDeckSprite = Resources.Load<Sprite>("DeckSprite");

           
            Save save = SaveSystem.LoadGame();
            if (save == null)
            {
                AvailableDecks = new List<Deck>();
            }
            else
            {
                AvailableDecks = save.ToDeckList();
            }   
        }
    }

    private void Start()
    {
        ShowDecks();
    }


    public void ShowDecks()
    {
        for (int i = 0; i < AvailableDecks.Count; i++)
        {
                AddDeck(AvailableDecks[i]);
        }
        Deck newDeck = new Deck();
        AddDeck(newDeck);
    }
    

    public void AddDeck(Deck deck)
    {
        HorizontalLayoutGroup[] horizontalLayoutGroups = verticalLayoutGroup.GetComponentsInChildren<HorizontalLayoutGroup>();
        HorizontalLayoutGroup horizontalLayoutGroup;
        if (horizontalLayoutGroups.Length<= 0)
        {
            GameObject newLine = Instantiate(deckLinePrefab, verticalLayoutGroup.transform);
            horizontalLayoutGroup = newLine.GetComponent<HorizontalLayoutGroup>();
        }
        else
        {
            horizontalLayoutGroup = horizontalLayoutGroups[horizontalLayoutGroups.Length - 1];
        }
        GameObject newDeckHolder;
 
        if (horizontalLayoutGroup.GetComponentsInChildren<Transform>().Length >= 5)
        {
            GameObject newLine = Instantiate(deckLinePrefab, verticalLayoutGroup.transform);
            newDeckHolder = Instantiate(deckHolderPrefab, newLine.transform);
        }
        else
        {
            newDeckHolder = Instantiate(deckHolderPrefab, horizontalLayoutGroup.transform);
        }
        AddDeckToButton(newDeckHolder, deck);
    }

    private void AddnewDeck(Deck deck)
    {
        if (!AvailableDecks.Contains(deck))
        {
            AvailableDecks.Add(deck);
        }
    }
    private void AddDeckToButton(GameObject deckHolder,Deck deck)
    {
        Button deckButton = deckHolder.GetComponent<Button>();
        UnityEvent showDeckEvent = new UnityEvent();
        Deck deckToShow = deck;
        deckButton.onClick.AddListener(delegate { DeckController.Instance.SetActiveDeck(deckToShow); });
        deckButton.onClick.AddListener(delegate { DeckController.Instance.ShowDeck(deckToShow); });
        deckButton.onClick.AddListener(delegate { gameObject.SetActive(false);});
        deckButton.onClick.AddListener(delegate { AddnewDeck(deckToShow); });
        deckButton.onClick.AddListener(delegate { DeckController.Instance.ChangeStatus(DeckState.RemovingCard); });

        if (deck.PokemonCards.Count > 0)
        {
            Image buttonImage = deckHolder.GetComponent<Image>();
            buttonImage.sprite = createdDeckSprite;
        }
    }

    private void Clear()
    {
        HorizontalLayoutGroup[] horizontalLayoutGroups = verticalLayoutGroup.GetComponentsInChildren<HorizontalLayoutGroup>();       
        foreach(HorizontalLayoutGroup horizontalLayoutGroup in horizontalLayoutGroups)
        {
            Destroy(horizontalLayoutGroup.gameObject);
        }
    }

    private void OnDisable()
    {
        Clear();
    }

    private void OnDestroy()
    {
        Instance = null;
    }
}
