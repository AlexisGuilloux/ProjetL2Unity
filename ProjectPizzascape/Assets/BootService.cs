using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class BootService : MonoBehaviour
{
    [SerializeField] private CanvasGroup loadingCG;

    public bool homeLoaded = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadingAnimationSequence());

        loadingCG.alpha = 1f;
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        
    }


    private IEnumerator LoadingAnimationSequence()
    {
        yield return new WaitForSeconds(1f);
        Sequence sequence = DOTween.Sequence();
        sequence.Append(loadingCG.DOFade(0f, 1f));
        sequence.AppendCallback(delegate { loadingCG.gameObject.SetActive(false); });

        yield return null;

    }
}
