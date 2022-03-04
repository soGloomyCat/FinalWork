using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShopItemView : MonoBehaviour
{
    [SerializeField] private TMP_Text _label;
    [SerializeField] private TMP_Text _price;
    [SerializeField] private Image _icon;
    [SerializeField] private Button _sellButton;

    private ShopItem _shopItem;

    public event UnityAction<ShopItem, ShopItemView> SellButtonClick;

    private void OnEnable()
    {
        _sellButton.onClick.AddListener(OnButtonClick);
        _sellButton.onClick.AddListener(TryLockItem);
    }

    private void OnDisable()
    {
        _sellButton.onClick.RemoveListener(OnButtonClick);
        _sellButton.onClick.RemoveListener(TryLockItem);
    }

    public void Render(ShopItem item)
    {
        _shopItem = item;
        _label.text = _shopItem.Label;
        _price.text = _shopItem.Price.ToString();
        _icon.sprite = _shopItem.Icon;
    }

    private void OnButtonClick()
    {
        SellButtonClick?.Invoke(_shopItem, this);
    }

    private void TryLockItem()
    {
        if (_shopItem.IsBuyed)
        {
            _sellButton.interactable = false;
            _price.text = "Sold";
        }
    }
}
