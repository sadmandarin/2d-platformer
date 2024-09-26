using UnityEngine;
using UnityEngine.UI;

public class BossArmorView : MonoBehaviour
{
    private BossBase _bossArmor;
    private Image _image;

    private void OnEnable()
    {
        _bossArmor = GetComponentInParent<BossBase>();
        _image = GetComponent<Image>();
        _bossArmor.OnArmorTookDamage += UpdateArmorView;
    }

    private void OnDisable()
    {
        _bossArmor.OnArmorTookDamage -= UpdateArmorView;
    }

    private void UpdateArmorView()
    {
        _image.fillAmount = (float)(_bossArmor.BodyArmor + _bossArmor.HelmetArmor) / (float)_bossArmor.AllArmorHealth;
    }
}
