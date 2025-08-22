using TMPro;
using UnityEngine;

namespace NecroMotMicon.Script.FightingPlan.UI
{
    public class PlayerHpDisplay : MonoBehaviour
    {
        private TMP_Text hpText;
        public PlayerArea _playerArea;

        private void Awake()
        {
            hpText = GetComponent<TMP_Text>();
            
            UpdateHpDisplay(_playerArea.Hp);
        }

        private void OnEnable()
        {
            _playerArea.OnDamageTaken += UpdateHpDisplay;
        }

        private void OnDisable()
        {
            _playerArea.OnDamageTaken -= UpdateHpDisplay;
        }

        private void UpdateHpDisplay(float obj)
        {
            hpText.text = obj.ToString();
        }
    }
}
