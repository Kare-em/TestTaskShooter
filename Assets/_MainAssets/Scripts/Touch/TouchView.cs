using System;
using UnityEngine;

namespace _MainAssets.Scripts.Touch
{
    public class TouchView : MonoBehaviour, IDraggable
    {
        [SerializeField] private GameObject panel;
        [SerializeField] private GameObject stick;

        private void OnEnable()
        {
            TouchHandler.OnBeginDrag += OnBeginDrag;
            TouchHandler.OnDrag += OnDrag;
            TouchHandler.OnEndDrag += OnEndDrag;
            GameController.enemiesDestroyed += OnEndDrag;
            GameController.gameEnded += OnEndDrag;
        }

        private void OnDisable()
        {
            TouchHandler.OnBeginDrag -= OnBeginDrag;
            TouchHandler.OnDrag -= OnDrag;
            TouchHandler.OnEndDrag -= OnEndDrag;
            GameController.enemiesDestroyed -= OnEndDrag;
            GameController.gameEnded -= OnEndDrag;
        }

        public void OnBeginDrag(Vector2 position)
        {
            panel.SetActive(true);
            panel.transform.position = position;
            stick.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        }

        public void OnDrag(Vector2 position)
        {
            stick.transform.position = position;
        }

        public void OnEndDrag()
        {
            panel.SetActive(false);
        }
    }
}