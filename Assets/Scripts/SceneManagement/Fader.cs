using System.Collections;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        CanvasGroup canvasGroup;

        private void Start() {
            canvasGroup = GetComponent<CanvasGroup>();
        }
        public IEnumerator FadeOut(float time) 
        {
            while (canvasGroup.alpha < 1) // alpha is not 1 
            {
                canvasGroup.alpha += Time.deltaTime / time;
                yield return null; // everyframe 
            }
        }

        public IEnumerator FadeIn(float time) 
        {
            while (canvasGroup.alpha > 0) // alpha is not 1 
            {
                canvasGroup.alpha -= Time.deltaTime / time;
                yield return null; // everyframe 
            }
        }
    }
}
