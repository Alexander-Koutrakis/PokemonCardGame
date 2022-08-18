using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


/*@summary 
 * This class contains the data of a pokemon card in Gameobject
 * Can Move/Scale/Destroy the Gameobject Card
 */
public class CardContainer : MonoBehaviour
{
    public PokemonCardData pokemonCardData { private set; get; }
    public void Initialise(PokemonCardData pokemonCardData)
    {
        this.pokemonCardData = pokemonCardData;
        Image cardImage = GetComponent<Image>();
        cardImage.sprite = this.pokemonCardData.Sprite;
        gameObject.name = pokemonCardData.Name;

    }

    public void SelectCard()
    {
        CardFocus.Instance.FocusCard(this);
    }

    public void MoveToPosition(Transform targetTransformParent,bool setparentfirst)
    {
    
        transform.SetAsFirstSibling();
        if (setparentfirst)
        {
            transform.SetParent(targetTransformParent);
            transform.DOMove(targetTransformParent.position, 0.5f);
        }
        else
        {
            transform.DOMove(targetTransformParent.position, 0.5f).OnComplete(() => transform.SetParent(targetTransformParent));
        }
       
    }


 

    public void ScaleTransform(Vector2 scale)
    {
        transform.DOScale(scale, 0.5f).OnComplete(() => DestroyIfSizeTooSmall());
    }

    private void DestroyIfSizeTooSmall()
    {
        if (transform.localScale.magnitude <= 0)
        {
            Destroy(gameObject);
        }
    }
}
