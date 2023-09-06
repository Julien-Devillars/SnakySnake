using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderHandler : MonoBehaviour
{

    public void Start()
    {
        GetComponent<Slider>().value = ES3.Load<float>(gameObject.name, 0.5f);
    }
    
    public void setValue(float value)
    {
        Debug.Log($"{gameObject.name} changed to {value}");
        ES3.Save<float>(gameObject.name, value);
    }
}
