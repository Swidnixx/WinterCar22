using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Renderer carRenderer;

    public Slider redSlider;
    public Slider greenSlider;
    public Slider blueSlider;

    public Color color;

    private void Start()
    {
        color = IntToColor(
            PlayerPrefs.GetInt("Red"), 
            PlayerPrefs.GetInt("Green"),
            PlayerPrefs.GetInt("Blue"));

        redSlider.value = (int)(color.r * 255);
        greenSlider.value = (int)(color.g * 255);
        blueSlider.value = (int)(color.b * 255);
    }

    private void Update()
    {
        SetCarColor((int)redSlider.value, (int)greenSlider.value, (int)blueSlider.value);
    }

    void SetCarColor(int red, int green, int blue)
    {
        Color col = IntToColor(red, green, blue);
        carRenderer.material.color = col;

        PlayerPrefs.SetInt("Red", red);
        PlayerPrefs.SetInt("Green", green);
        PlayerPrefs.SetInt("Blue", green);

    }

    public static Color IntToColor(int red, int green, int blue)
    {
        float r = (float)red / 255;
        float g = (float)green / 255;
        float b = (float)blue / 255;

        return new Color(r, g, b);
    }
}
