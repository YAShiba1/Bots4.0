using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class GoldDisplay : MonoBehaviour
{
    [SerializeField] private Base _goldBase;

    private TMP_Text _amountOfGold;

    private void Awake()
    {
        _amountOfGold = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        _goldBase.GoldChanged += OnGoldChanged;
    }

    private void OnDisable()
    {
        _goldBase.GoldChanged -= OnGoldChanged;
    }

    private void OnGoldChanged(int amount)
    {
        _amountOfGold.text = amount.ToString("F0");
    }
}
