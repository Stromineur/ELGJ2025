using Script.Core;
using TMPro;
using UnityEngine;

namespace NecroMotMicon.Script.FightingPlan.UI
{
    public class PlayerHpDisplay : MonoBehaviour
    {
        private TMP_Text hpText;
        private PlayerArea _playerArea;

        private void Awake()
        {
            hpText = GetComponent<TMP_Text>();
            _playerArea = ServiceLocator.Instance.PlayerArea;
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
        
        // Ajouter un fonction feedback pour la prise de dégâts
        
    }
}
