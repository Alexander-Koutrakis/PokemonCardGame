using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

//play startScreen animations
//Load to startscreen
public class StartScreen : MonoBehaviour
{
    [SerializeField]private Image logoImage;
    [SerializeField]private Image fadeImage;
    private void Start()
    {
        PulseLogo();
    }

    private void PulseLogo()
    {
        logoImage.transform.DOScale(new Vector2(1.5f, 1.5f), 1).OnComplete(()=> logoImage.transform.DOScale(new Vector2(1f, 1f), 1).SetLoops(-1, LoopType.Yoyo));
    }

    public void LoadToSceneFlash(string scene)
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(fadeImage.DOFade(0.9f, 0.2f).SetEase(Ease.OutCirc));
        sequence.Append(fadeImage.DOFade(0, 0.2f));
        sequence.Append(fadeImage.DOFade(1, 1));
        sequence.Play().OnComplete(() => LoadSceneMode(scene,sequence));
    }

    private void LoadSceneMode(string scene,Sequence sequence)
    {
        DOTween.KillAll();
        SceneManager.LoadScene(scene);
    }

}
