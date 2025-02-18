using System.ComponentModel;
using UnityEngine;
using UnityEngine.EventSystems;
public class WordBookAnims : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Animator animator;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        animator.SetBool("CanStart", true);
        animator.SetBool("CanStop", false);
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        animator.SetBool("CanStart", false);
        animator.SetBool("CanStop", true);
    }
}
