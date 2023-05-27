namespace Dreamteck.Forever
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class EndScreen : MonoBehaviour
    {
        public delegate void EmptyHandler();
        static EndScreen instance;
        public CanvasGroup screenGroup;
        public static event EmptyHandler onRestartClicked;

        public Animator animatorPocong;
        public Animator animatorGirl;
        public GameObject timelineDeath;
        public int waitBeforeEnd = 5;

        private void Awake()
        {
            instance = this;
        }

        public void Restart()
        {
            if (onRestartClicked != null) 
            {
                animatorPocong.SetBool("isDeath", false);
                animatorGirl.SetBool("isDeath", false);
                timelineDeath.SetActive(false);
                onRestartClicked();
            }
            StopAllCoroutines();
            screenGroup.gameObject.SetActive(false);
            Time.timeScale = 1f;
        }

        public static void Open()
        {
            instance.StartCoroutine(instance.FadeIn());
        }

        // IEnumerator Waitss()
        // {
            
        // }

        IEnumerator FadeIn()
        {
            timelineDeath.SetActive(true);
            animatorPocong.SetBool("isDeath", true);
            animatorGirl.SetBool("isDeath", true);

            yield return new WaitForSeconds(waitBeforeEnd);

            screenGroup.gameObject.SetActive(true);
            screenGroup.alpha = 0f;
            while (screenGroup.alpha < 1f)
            {
                screenGroup.alpha = Mathf.MoveTowards(screenGroup.alpha, 1f, Time.unscaledDeltaTime);
                Time.timeScale = 1f - screenGroup.alpha;
                yield return null;
            }
        }

    }
}
