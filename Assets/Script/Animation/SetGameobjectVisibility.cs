using System.Threading;
using UnityEngine;

namespace NecroMotMicon
{
    public class SetGameobjectVisibility : MonoBehaviour
    {
        public void SwitchVisibility()
        {
            if (gameObject.activeInHierarchy)
            {
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(true);
            }
        }
    }
}
