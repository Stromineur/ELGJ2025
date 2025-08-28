using Script.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NecroMotMicon.Script.FightingPlan.UI
{
    public class LosePanelDisplay : MonoBehaviour
    {
        [SerializeField] private PlayerArea playerArea;
        [SerializeField] private GameObject content;

        private void OnEnable()
        {
            ServiceLocator.Instance.PlayerArea.OnDamageTaken += OnDamageTaken;
        }

        private void OnDisable()
        {
            if (ServiceLocator.Instance.PlayerArea != null)
                ServiceLocator.Instance.PlayerArea.OnDamageTaken -= OnDamageTaken;
        }

        private void OnDamageTaken(float f)
        {
            if (f > 0)
                return;
            
            content.SetActive(true);
            Time.timeScale = 0;
        }

        public void Restart()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("Main_Scene");
        }
    }
}
