using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    private float healthValue = 100;
    enum HEALTH_BAR_VALUE
    {
        MAX = 100,
        MIN = 0,
    }

    private void Start()
    {
        healthValue = GetComponent<Slider>().value;   
    }

    private float ChangeHealthBar(float incomingValue)
    {
        return ((healthValue) * 100) + incomingValue;
    }
    public void ChangeHealthValue(float incomingValue)
    {
        healthValue = ChangeHealthBar(incomingValue);
    }


}
