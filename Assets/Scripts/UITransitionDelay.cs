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

        private bool _keyDown = false;
        private bool _isTransitioning = false;


        [SerializeField] private GameObject m_TargetScreenPrefab;
        [SerializeField] private float delay;

        private GameObject _currentScreen;

        protected void Start()
        {
            _keyDown = false;
            StartCoroutine(WaitThenTransition());
        }

        private void TransitionToScene()
        {
            if (!_keyDown && m_TargetScreenPrefab != null)
            {
                var prototypeFlowController =GetComponentInParent<Canvas>().rootCanvas?.GetComponent<PrototypeFlowController>();
                if (prototypeFlowController != null)
                {
                    if (_currentScreen != null)
                    {
                        Destroy(_currentScreen);
                    }
                    _currentScreen = Instantiate(m_TargetScreenPrefab, prototypeFlowController.transform);

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
            _keyDown = true;
        }


    }

}