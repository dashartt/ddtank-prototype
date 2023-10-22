using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    private float healthValue = 100;
    enum HEALTH_BAR_VALUE
    {
        MAX = 100,
        MIN = 0,
    }

    private void Start()
    {
        healthValue = 1;
        healthValue = ChangeHealthBarValue(healthValue);
    }

    public float ChangeHealthBarValue(float incomingValue)
    {
        return ((healthValue) * 100) + incomingValue;
    } 


}
