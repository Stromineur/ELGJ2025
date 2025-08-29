using LucidFactory.UI.Panels;
using Script.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NecroMotMicon.Script.FightingPlan.UI
{
    public class LosePanelDisplay : MonoBehaviour
    {
        [SerializeField] private PlayerArea playerArea;
        [SerializeField] private LF_TabManager tabManager;

        private void Awake()
        {
            if (tabManager == null)
                tabManager = GetComponentInParent<LF_TabManager>();
        }

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
            
            tabManager.OpenTab(name);
            Time.timeScale = 0;
        }

        public void Restart()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("Main_Scene");
        }
    }
}
