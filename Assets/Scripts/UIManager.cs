using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    public static UIManager INS;
    [SerializeField] Image imgFadeInOut;
    [SerializeField] GameObject tutorialText;
    [SerializeField] GameObject endImage;
    [SerializeField] Text textEndGame;
    public Image border1;
    public Image handle1;



    private void Awake()
    {
        INS = this;
    }
    private void Start()
    {
        FadeInOut();
        LeanTween.delayedCall(2, () => { AnimTutorialTextIn(); });
    }

    public void AnimTutorialTextOut()
    {
        LeanTween.moveLocalY(tutorialText, -500, 2).setEaseInOutBack();
    }
    void AnimTutorialTextIn()
    {
        LeanTween.moveLocalY(tutorialText, -230, 2).setEaseInOutBack();
    }

    public void FinalAnim()
    {
        LeanTween.delayedCall(1, () => {
            endImage.SetActive(true);
            StartCoroutine(ShowCharacters("Coming soon... :D"));
            GameManager.INS.playerController.rb.isKinematic = true;
        });
    }

    public void FadeInOut()
    {
        imgFadeInOut.CrossFadeAlpha(1, 1f, false);
        LeanTween.delayedCall(2, () => { imgFadeInOut.CrossFadeAlpha(0, 2.0f, false); });
    }

    IEnumerator ShowCharacters(string text)
    {
        foreach (char item in text.ToCharArray())
        {
            textEndGame.text += item;
            yield return new WaitForSeconds(0.08f);
        }
    }

}
