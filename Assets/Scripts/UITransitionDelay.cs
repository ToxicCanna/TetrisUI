using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UnityFigmaBridge.Runtime.UI
{
    public class UITransitionDelay : MonoBehaviour
    {
        // The screen prefab to transition to
        public GameObject TargetScreenPrefab
        {
            get => m_TargetScreenPrefab;
            set => m_TargetScreenPrefab = value;
        }

        private bool _isTransitioning = false;
        private bool _isButtonPressed = false;

        [SerializeField] private GameObject m_TargetScreenPrefab;
        [SerializeField] private float delay;

        private GameObject _currentScreen;

        protected void Start()
        {
            _currentScreen = m_TargetScreenPrefab;
            _isButtonPressed = false;
            StartCoroutine(WaitThenTransition());
        }

        private void TransitionToScene()
        {
            if (_isTransitioning || _isButtonPressed) return;

            _isTransitioning = true;
            _isButtonPressed = true;

            if (m_TargetScreenPrefab != null)
            {
                var prototypeFlowController =GetComponentInParent<Canvas>().rootCanvas?.GetComponent<PrototypeFlowController>();
                if (prototypeFlowController != null)
                {
                    if (_currentScreen != null)
                    {
                        m_TargetScreenPrefab.SetActive(false);
                        _currentScreen.SetActive(false);
                    }
                    _currentScreen = Instantiate(m_TargetScreenPrefab, prototypeFlowController.transform);
                    _currentScreen.SetActive(true);

                    RectTransform rectTransform = _currentScreen.GetComponent<RectTransform>();

                    if (rectTransform != null)
                    {
                        rectTransform.pivot = new Vector2(0.5f, 0.5f);
                        rectTransform.anchoredPosition = Vector2.zero;
                        rectTransform.anchorMin = new Vector2(0f, 0f);
                        rectTransform.anchorMax = new Vector2(1f, 1f);
                        rectTransform.sizeDelta = Vector2.zero;
                    }
                }
            }
            StartCoroutine(ResetFlags());
        }
        private IEnumerator ResetFlags()
        {
            yield return new WaitForSeconds(0.5f);
            _isButtonPressed = false;
            _isTransitioning = false;
        }

        IEnumerator WaitThenTransition()
        {
            if (_isTransitioning) yield break;

            _isTransitioning = true;
            yield return new WaitForSeconds(delay);
            TransitionToScene();
            _isTransitioning = false;
        }

        public void KeyPressed()
        {
            _isButtonPressed = true;
        }


    }

}