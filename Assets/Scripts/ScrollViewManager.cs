using System.Collections.Generic;
using UnityEngine;

public class ScrollViewManager : MonoBehaviour
{
    public WeaponsAndAbilitiesStorage myScriptableObject;
    public GameObject content;  
    public GameObject fieldPrefab;  
    public float maxHeight = 400f;  
    public float fieldHeight = 100f;  
    public RectTransform contentRectTransform;
    public RectTransform ScrollViewTransform;

    void OnEnable()
    {
        PopulateScrollView();
    }

    public void PopulateScrollView()
    {
        List<object> items = new List<object>();
        int fieldCount = 0;

        if (gameObject.name == "MagicAbilityPanel")
        {
            var abilityItems = myScriptableObject.GetListByType<AbilitySOBase>();
            fieldCount = abilityItems.Count;
            items.AddRange(abilityItems);
        }
        else if (gameObject.name == "WeaponPanel")
        {
            var weaponItems = myScriptableObject.GetListByType<WeaponsBase>();
            fieldCount = weaponItems.Count;
            items.AddRange(weaponItems);
        }
        else if (gameObject.name == "ArmorPanel")
        {
            var weaponItems = myScriptableObject.GetListByType<PlayerArmorBase>();
            fieldCount = weaponItems.Count;
            items.AddRange(weaponItems);
        }
        else if (gameObject.name == "LongRangeWeaponPanel")
        {
            var weaponItems = myScriptableObject.GetAllLongRangeWeapons();
            fieldCount = weaponItems.Count;
            items.AddRange(weaponItems);
        }

        float totalHeight = fieldCount * fieldHeight;
        float adjustedHeight = Mathf.Min(totalHeight, maxHeight);

        contentRectTransform.anchoredPosition = new Vector2(0, 0);
        contentRectTransform.sizeDelta = new Vector2(contentRectTransform.sizeDelta.x, adjustedHeight);

        foreach (Transform child in content.transform)
        {
            Destroy(child.gameObject);
        }

        ItemEquip previousField = null;

        for (int i = 0; i < fieldCount; i++)
        {
            GameObject newField = Instantiate(fieldPrefab, content.transform);
            var data = items[i];
            ItemEquip currentField = newField.GetComponent<ItemEquip>();

            if (data is WeaponsBase weapons)
            {
                newField.GetComponent<ItemView>().SetUp(weapons.Icon, weapons.ShortDescription, weapons);
            }

            if (previousField != null)
            {
                currentField._upButton = previousField.GetComponent<ItemEquip>();
                previousField._downButton = currentField.GetComponent<ItemEquip>();
            }

            previousField = currentField;

            if (i == 0)
            {
                currentField.SelectButton();
            }
        }

        if (previousField != null)
        {
            previousField.GetComponent<ItemEquip>()._downButton = null;
        }
    }
}
