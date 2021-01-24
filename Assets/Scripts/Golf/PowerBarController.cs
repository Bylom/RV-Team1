using UnityEngine;
using UnityEngine.UI;

namespace Golf
{
    public class PowerBarController : MonoBehaviour
    {
        [SerializeField] private Image powerBarImage;
        [SerializeField] private float increment = 0.1f;
        private float _powerValue;
        private static float _tollerance = 0.1f;
        private void Update()
        {
            var value = Input.GetAxis("Vertical");
            if (value < -_tollerance)
            {
                _powerValue -= increment * Time.deltaTime;
                if (_powerValue < 0)
                    _powerValue = 0;
            }
            else if (value > _tollerance)
            {
                _powerValue += increment * Time.deltaTime;
                if (_powerValue > 1)
                    _powerValue = 1;
            }
            if (!(powerBarImage is null))
            {
                powerBarImage.fillAmount = _powerValue;
                powerBarImage.color = new Color(_powerValue, 1 -_powerValue, 0);
            }
            
        }
    }
}