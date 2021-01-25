using UnityEngine;
using UnityEngine.UI;

namespace Golf
{
    public class PowerBarController : MonoBehaviour
    {
        [SerializeField] private Image powerBarImage;

        public void setValue(float value)
        {
            
            if (!(powerBarImage is null) && value <= 1 && value >= 0)
            {
                powerBarImage.fillAmount = value;
                powerBarImage.color = new Color(value, 1 -value, 0);
            }

        }
    }
}