using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using System.Threading.Tasks;
using TMPro;
using UnityEngine.UI;

//Visualy show the loading of Card data and Image from PokemonTGC.io
public class LoadingScreen : MonoBehaviour
{
    private TMP_Text loadingText;
    private Image pokeball;
    private CanvasGroup loadingScreen;
    private Tween rotationTween;
    void Awake()
    {
        loadingScreen = GetComponentInChildren<CanvasGroup>();
        pokeball = GetComponentsInChildren<Image>()[1];
        loadingText = GetComponentInChildren<TMP_Text>();
    }

    private void Start()
    {
        rotationTween=pokeball.transform.DORotate(new Vector3(0, 0, 720), 1,RotateMode.FastBeyond360).SetLoops(-1);
        SceneLoading();
    }

    private async void SceneLoading()
    {
        var loadingTask = CardLoader.LoadPokemonCards();
        while (!loadingTask.IsCompleted)
        {
            loadingText.text = CardLoader.Status;
            await Task.Yield();
        }
      
        rotationTween.Kill();
        loadingScreen.DOFade(0, 1).OnComplete(() => Destroy(gameObject));
    }
 
}
