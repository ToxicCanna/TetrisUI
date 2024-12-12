using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UnityFigmaBridge.Runtime.UI
{
    public class UITransitionKeydown : MonoBehaviour
    {
        /// <summary>
        /// The screen prefab to transition to
        /// </summary>
        public GameObject TargetScreenPrefab
        {
            get => m_TargetScreenPrefab;
            set => m_TargetScreenPrefab = value;
        }

        [SerializeField] private GameObject m_TargetScreenPrefab;
        private UITransitionDelay _transitionDelayScript;
        [SerializeField] private string InputType;

        private GameObject _currentScreen;

        private void Start()
        {
            _transitionDelayScript = GetComponent<UITransitionDelay>();
        }
        private void TransitionToScene()
        {
            var prototypeFlowController = GetComponentInParent<Canvas>().rootCanvas?.GetComponent<PrototypeFlowController>();
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