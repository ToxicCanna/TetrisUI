using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UnityFigmaBridge.Runtime.UI
{
    public class UITransitionKeydown : MonoBehaviour
    {
        // The screen prefab to transition to
        public GameObject TargetScreenPrefab
        {
            get => m_TargetScreenPrefab;
            set => m_TargetScreenPrefab = value;
        }

        [SerializeField] private GameObject m_TargetScreenPrefab;
        private UITransitionDelay _transitionDelayScript;
        [SerializeField] private string InputType;

        private GameObject _currentScreen;
        private bool _isTransitioning = false;
        private bool _isButtonPressed = false;

        private void Start()
        {
            _transitionDelayScript = GetComponent<UITransitionDelay>();
            _currentScreen = m_TargetScreenPrefab;
        }
        private void TransitionToScene()
        {
            if (_isTransitioning || _isButtonPressed) return;

            _isTransitioning = true;
            _isButtonPressed = true;

            var prototypeFlowController = GetComponentInParent<Canvas>().rootCanvas?.GetComponent<PrototypeFlowController>();
            if (prototypeFlowController != null)
            {
                if (_currentScreen != null)
                {
                    m_TargetScreenPrefab.SetActive(false);
                    _currentScreen.SetActive(false);
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
                _currentScreen.SetActive(true);
            }

            StartCoroutine(ResetFlags());
        }

        private IEnumerator ResetFlags()
        {
            yield return new WaitForSeconds(0.5f);
            _isButtonPressed = false;
            _isTransitioning = false;
        }

        private void Update()
        {
            if (Input.GetButtonDown(InputType))
            {
                _transitionDelayScript?.KeyPressed();
                TransitionToScene();
            }
        }

    }

}