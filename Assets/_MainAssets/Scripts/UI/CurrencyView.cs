using UnityEngine;
using UnityEngine.UI;

namespace _MainAssets.Scripts.UI
{
    public enum CurrencyType
    {
        Money = 0,
        Diamond = 1
    }

    public class CurrencyView : MonoBehaviour
    {
        [SerializeField] private CurrencyType _currencyType;
        [SerializeField] private Text _text;
        public CurrencyType CurrencyType => _currencyType;


        public void UpdateView(int obj)
        {
            _text.text = obj.ToString();
        }
    }
}