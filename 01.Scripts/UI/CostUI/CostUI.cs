using TMPro;
using UnityEngine;
public class CostUI : MonoBehaviour
{
    public TextMeshProUGUI _costText;
    public Oxygen Oxygen;

    private void Start()
    {
        Oxygen.OnOxygenChanged += HandleOxygenChanged;
    }


    public void HandleOxygenChanged(int pre, int cur)
    {
        _costText.SetText($"{cur}/{Oxygen.maxOxygen}");
    }
}
