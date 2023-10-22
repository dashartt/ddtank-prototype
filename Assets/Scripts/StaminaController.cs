using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaController : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    private float staminaValue;
    enum STAMINA_BAR
    {
        MAX = 100,
        MIN = 0,
    }

    // Start is called before the first frame update
    void Start()
    { 
        slider.value = 1;
        staminaValue = ChangeStaminaBarValue(slider.value);        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float ChangeStaminaBarValue(float incomingValue)
    {
        return ((staminaValue) * 100) + incomingValue;
    }    
}
