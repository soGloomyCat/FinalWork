using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private List<ShopItem> _items;
    [SerializeField] private SurvivorsShopOpportunities _buyer;
    [SerializeField] private ShopItemView _itemPattern;
    [SerializeField] private GameObject _itemsContainer;

    private void Start()
    {
        for (int i = 0; i < _items.Count; i++)
        {
            AddItem(_items[i]);
        }
    }

    private void AddItem(ShopItem item)
    {
        var view = Instantiate(_itemPattern, _itemsContainer.transform);
        view.SellButtonClick += OnSellClick;
        view.Render(item);
    }

    private void OnSellClick(ShopItem item, ShopItemView itemView)
    {
        TrySellItem(item, itemView);
    }

    private void TrySellItem(ShopItem item, ShopItemView view)
    {
        if (item.Price <= _buyer.WalletBalance)
        { 
            _buyer.BuyItem(item);
            item.Buy();
            view.SellButtonClick -= OnSellClick;
        }
    }
}
