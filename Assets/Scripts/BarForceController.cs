using UnityEngine;
using UnityEngine.UI;

public class BarForceController : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    private float forceValue;
    enum FORCE_BAR {
        MAX = 100,
        MIN = 0,
    }

    public float speedFill = 1;
    public float lastForce;
    bool isFillForward = false;

    private void Start()
    {  
        forceValue = ChangeForceBarValue(slider.value);        
    }
  
    private float ChangeForceBarValue(float incomingValue)
    {
        return ((forceValue) * 100) + incomingValue;
    }

    public void ChangeShotForce()
    { 
        float ChangeValue (float speedFill) { return forceValue += speedFill *Time.deltaTime; }

        if (isFillForward)
        {
            ChangeValue(isFillForward ? speedFill : -speedFill);
        }

        if (FORCE_BAR.MAX.Equals(forceValue) && !isFillForward)
        {
            isFillForward = true;            
        }        
    }

    public void ResetForceBarValue()
    {
        isFillForward = false;
        lastForce = forceValue;
        forceValue = (float)FORCE_BAR.MIN;
    }


}
