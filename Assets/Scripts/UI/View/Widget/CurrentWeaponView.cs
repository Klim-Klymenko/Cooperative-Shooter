using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.View
{
    public sealed class CurrentWeaponView : MonoBehaviour
    {
        [SerializeField]
        private Image _currentWeaponImage;
        
        [SerializeField]
        private TextMeshProUGUI _chargesText;
        
        public void ChangeImage(Sprite weaponSprite)
        {
            _currentWeaponImage.sprite = weaponSprite;
        }

        public void ChangeChargesText(string ammo)
        {
            _chargesText.text = ammo;
        }
    }
}