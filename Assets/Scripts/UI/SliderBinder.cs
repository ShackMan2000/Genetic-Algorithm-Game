using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderBinder : MonoBehaviour 
{
    [SerializeField]
    private StatSlider minValueSlider, maxValueSlider;



    private void Start()
    {
        minValueSlider.slider.onValueChanged.AddListener(delegate { adjustMaxValue(); });
        maxValueSlider.slider.onValueChanged.AddListener(delegate { adjustMinValue(); });
    }


    public void adjustMaxValue()
    {
        if (minValueSlider.slider.value > maxValueSlider.slider.value)
        {
            maxValueSlider.slider.value = minValueSlider.slider.value;
        }
    }

    public void adjustMinValue()
    {
        if (minValueSlider.slider.value > maxValueSlider.slider.value)
        {
            minValueSlider.slider.value = maxValueSlider.slider.value;
        }
    }



}
