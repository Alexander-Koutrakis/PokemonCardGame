using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

/*This Class is responisble for Adding or Removing the Focused Card
 * It moves the card to Garbage Bin or to Deck Accordingly
 */
public class CardFocus : MonoBehaviour
{
    private Image fadeImage;
    private Button focusUtilityButton;
    private Button backButton;

    private Image addRemoveCardImage;
    private Sprite removeCardSprite;
    private Sprite addCardSprite;

    private DeckController deckController;
    private Transform cardParent;
    private CardContainer focusedCard;

    private Vector2 focusScale = new Vector2(1.5f, 1.5f);
    private Vector2 unfocusedScale = new Vector2(1, 1);
    public static CardFocus Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            fadeImage = GetComponent<Image>();
            HorizontalLayoutGroup horizontalLayoutGroup = GetComponentInChildren<HorizontalLayoutGroup>();
            focusUtilityButton = horizontalLayoutGroup.GetComponentsInChildren<Button>()[0];
            backButton = horizontalLayoutGroup.GetComponentsInChildren<Button>()[1];
            addRemoveCardImage = focusUtilityButton.GetComponent<Image>();
            deckController = FindObjectOfType<DeckController>();
            addCardSprite = Resources.Load<Sprite>("DeckSprite");
            removeCardSprite = Resources.Load<Sprite>("GarbageBin");
        }
       
    }

    private void Start()
    {
        fadeImage.enabled = false;
        backButton.gameObject.SetActive(false);
        focusUtilityButton.gameObject.SetActive(false);
    }

    //Focus the CardContainer and change the utility of the focused button
    public void FocusCard(CardContainer cardContainer)
    {
        //Ignore if already focused
        if (focusedCard != cardContainer)
        {
            focusedCard = cardContainer;

            //fade background
            fadeImage.enabled = true;
            fadeImage.DOFade(0.8f, 0.5f);

            //Scale CardContainer and set it to foreground
            cardParent = cardContainer.transform.parent;
            focusedCard.ScaleTransform(focusScale);
            focusedCard.MoveToPosition(transform,true);
           
            //Change focusedButton to be able to Add or Remove cards
            if (deckController.DeckState == DeckState.AddingCard)
            {
                SetButtonToAddCard(focusedCard);
            }
            else if (deckController.DeckState == DeckState.RemovingCard)
            {
                SetButtonToRemoveCard(focusedCard);
            }

            backButton.gameObject.SetActive(true);
            focusUtilityButton.gameObject.SetActive(true);
        }
    }
    public void UnfocusCard()
    {
        fadeImage.DOFade(0, 0.5f).OnComplete(() => fadeImage.enabled = false);
        focusedCard.ScaleTransform(unfocusedScale);
        focusedCard.MoveToPosition(cardParent,true);

        backButton.gameObject.SetActive(false);
        focusUtilityButton.gameObject.SetActive(false);
    }

    public void CloseFocus()
    {
        backButton.gameObject.SetActive(false);
        focusUtilityButton.gameObject.SetActive(false);
        fadeImage.DOFade(0, 0.5f).OnComplete(() => fadeImage.enabled = false);
    }
    private void SetButtonToAddCard(CardContainer cardContainer)
    {
        focusUtilityButton.onClick.RemoveAllListeners();
        focusUtilityButton.onClick.AddListener(delegate { deckController.AddCardToActive(cardContainer); });
        addRemoveCardImage.sprite = addCardSprite;
        
    }
    private void SetButtonToRemoveCard(CardContainer cardContainer)
    {
        focusUtilityButton.onClick.RemoveAllListeners();
        focusUtilityButton.onClick.AddListener(delegate { deckController.RemoveCardFromActive(cardContainer); });
        focusUtilityButton.onClick.AddListener(delegate { CloseFocus(); });
        addRemoveCardImage.sprite = removeCardSprite;
    }
}
