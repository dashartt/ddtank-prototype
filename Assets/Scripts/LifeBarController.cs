using UnityEngine;
using UnityEngine.UI;

public class LifeBarController : MonoBehaviour
{
    private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();   
    }

    public void ChangeValue(float incomingValue) 
    {
        //if (slider.value + incomingValue>=1)
        //{
        //    slider.value = 1;
        //    return;
        //}
        //if (slider.value - incomingValue <= 0)
        //{
        //    slider.value = 0;
        //    return;
        //}
        slider.value += incomingValue;
    }    
}
