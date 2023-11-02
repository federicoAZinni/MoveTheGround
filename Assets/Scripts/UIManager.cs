using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
//sdfsf
public class UIManager : MonoBehaviour
{
    public static UIManager INS;
    [SerializeField] Image imgFadeInOut;
    [SerializeField] GameObject tutorialText;
    [SerializeField] GameObject endImage;
    [SerializeField] Text textEndGame;
    public Image border1;
    public Image handle1;

    [SerializeField] Button changePespective;
    [SerializeField] Sprite button3d;
    [SerializeField] Sprite button2d;

    bool perspective;
    [SerializeField] Button jump;

    public static Action OnChangePerspective;
    public static Action OnJump;

    private void Awake()
    {
        INS = this;
        changePespective.onClick.AddListener(() => {
            OnChangePerspective?.Invoke();
            if (perspective) changePespective.image.sprite = button3d;
            else changePespective.image.sprite = button2d;
            perspective = !perspective;
        });
        jump.onClick.AddListener(() => {
            OnJump?.Invoke();
        });
    }
    private void Start()
    {
        FadeInOut();
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
