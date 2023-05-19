using System;
using _MainAssets.Scripts.Player;
using _MainAssets.Scripts.UI;
using UnityEngine;

namespace _MainAssets.Scripts.Coin
{
    public class CurrencyStorage : MonoBehaviour
    {
        [SerializeField] private CurrencyView[] _currencyViews;
        private int[] _currency;

        private void Start()
        {
            _currency = new int[2];
            for (int i = 0; i < _currency.Length; i++)
            {
                _currency[i] = 0;
            }

            UpdateView();
            CoinFollow.pickedUp += OnPicked;
            PlayerMoveController.OnBase += OnBase;
        }


        private void OnDestroy()
        {
            CoinFollow.pickedUp -= OnPicked;
            PlayerMoveController.OnBase -= OnBase;
        }

        private void OnBase()
        {
            UpdateView();
        }

        private void UpdateView()
        {
            foreach (var _currencyView in _currencyViews)
            {
                _currencyView.UpdateView(_currency[(int)_currencyView.CurrencyType]);
            }
        }

        private void OnPicked(CurrencyType obj)
        {
            _currency[(int)obj]++;
        }
    }
}