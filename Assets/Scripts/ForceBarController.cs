using UnityEngine;
using UnityEngine.UI;

public class ForceBarController : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private float forceValue;
    [SerializeField]
    private SystemController system;
    enum FORCE_BAR {
        MAX = 1,
        MIN = 0,
    }

    public float speedFill = 0.05f;
    public float lastForce;
    public bool isFillForward = false;

    private void Start()
    {
        slider.value = 0;   
        system = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SystemController>();   
    }

    public void Update()
    {
        //if (slider.value == 0) { system.PassRound(); }
    }

    private float GetForceBarValue() { return slider.value; } 

    private void ChangeValue(float speedFill) { slider.value += speedFill * Time.deltaTime; }

    public void ChangeShotForce()
    {
        if (slider.value > 0.99f) { isFillForward = false; } 

        ChangeValue(isFillForward ? speedFill : -speedFill);                
    }

    public void ResetForceBarValue()
    {
        isFillForward = true;
        lastForce = GetForceBarValue();
        slider.value = 0;
    }


}
