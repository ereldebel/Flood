using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Screens
{
    public class HowToPlay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private GameObject backGround;

        [SerializeField] private GameObject enemies;
    
        public void OnPointerEnter(PointerEventData eventData)
        {
            backGround.SetActive(true);
            enemies.SetActive(true);
        }


        public void OnPointerExit(PointerEventData eventData)
        {
            backGround.SetActive(false);
            enemies.SetActive(false);
        }
    }
}
