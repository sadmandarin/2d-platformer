using UnityEngine;
using UnityEngine.UI;

public class BossHpView : MonoBehaviour
{
    private BossBase _bossArmor;
    private Image _image;

    private void OnEnable()
    {
        _bossArmor = GetComponentInParent<BossBase>();
        _image = GetComponent<Image>();
        _bossArmor.OnHealthTookDamage += UpdateHealthView;
    }

    private void OnDisable()
    {
        _bossArmor.OnHealthTookDamage -= UpdateHealthView;
    }

    private void UpdateHealthView()
    {
        _image.fillAmount = (float)_bossArmor.Health / (float)_bossArmor.MaxHealth;
    }
}
