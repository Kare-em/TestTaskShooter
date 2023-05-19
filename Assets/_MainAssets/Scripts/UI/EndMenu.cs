using UnityEngine;
using UnityEngine.SceneManagement;

namespace _MainAssets.Scripts.UI
{
    public class EndMenu : MonoBehaviour
    {
        [SerializeField] private GameObject _repeatWindow;

        private void OnEnable()
        {
            GameController.levelFailed += ShowRepeatWindow;
        }

        private void OnDisable()
        {
            GameController.levelFailed -= ShowRepeatWindow;
        }

        private void ShowRepeatWindow()
        {
            _repeatWindow.SetActive(true);
        }

        public void Repeat()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}