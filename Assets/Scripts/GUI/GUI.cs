using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;

using Valve.VR.InteractionSystem;

static class GUI
{
    private static ColorSchemeList CSList;

    public static int ColorScheme = 0;
    public static int NColorScheme;

    public static void LoadColorScheme()
    {
        // Load the JSON file containing all the information
        string filename = Application.streamingAssetsPath + "/colorscheme.json";
        if (System.IO.File.Exists(filename))
        {
            string str = System.IO.File.ReadAllText(filename);
            CSList = JsonUtility.FromJson<ColorSchemeList>(str);
            NColorScheme = CSList.List.Count;
            Debug.Log("Color scheme with " + NColorScheme + " types.");
        }
        else
        {
            Debug.Log("colorscheme file is Null.");
        }
    }

    public static void SetNextColorScheme()
    {
        ColorScheme++;
        ColorScheme = ColorScheme % NColorScheme;
    }

    public static string GetColorSchemeName()
    {
        return CSList.List[ColorScheme].Name;
    }

    // Background Unselected: Color 
    // Background Selected: Monochromatic 


    // Label: Analogous 1
    // Value: Analogous 2

    // For the buttons

    // Default background color
    public static Color SetNormalColorUnSelected() // The button is NOT SELECTED (default background)
    {
        return new Color(CSList.List[ColorScheme].ColorBG1[0], CSList.List[ColorScheme].ColorBG1[1], CSList.List[ColorScheme].ColorBG1[2], CSList.List[ColorScheme].ColorBG1[3]);
    }

    // Monochromatic color
    public static Color SetNormalColorSelected() // The button is SELETCED
    {
        return new Color(CSList.List[ColorScheme].ColorBG3[0], CSList.List[ColorScheme].ColorBG3[1], CSList.List[ColorScheme].ColorBG3[2], CSList.List[ColorScheme].ColorBG3[3]);
    }


    public static Color SetFontColorUnSelectedButton() // The color of the font for the buttons (unselected)
    {
        return new Color(CSList.List[ColorScheme].ColorFT1[0], CSList.List[ColorScheme].ColorFT1[1], CSList.List[ColorScheme].ColorFT1[2], CSList.List[ColorScheme].ColorFT1[3]);
    }

    public static Color SetFontColorSelectedButton() // The color of the font for the buttons (selected)
    {
        return new Color(CSList.List[ColorScheme].ColorFT3[0], CSList.List[ColorScheme].ColorFT3[1], CSList.List[ColorScheme].ColorFT3[2], CSList.List[ColorScheme].ColorFT3[3]);
    }

    // For the label (Analogous 1)
    public static Color SetBGLabelColor() // The color of the background for the label
    {
        return new Color(CSList.List[ColorScheme].ColorBG4[0], CSList.List[ColorScheme].ColorBG4[1], CSList.List[ColorScheme].ColorBG4[2], CSList.List[ColorScheme].ColorBG4[3]);
    }

    public static Color SetFontColorLabel() // The color of the font for the label (they dont change)
    {
        return new Color(CSList.List[ColorScheme].ColorFT4[0], CSList.List[ColorScheme].ColorFT4[1], CSList.List[ColorScheme].ColorFT4[2], CSList.List[ColorScheme].ColorFT4[3]);
    }

    // For the values (Analogous 2)
    public static Color SetBGValueColor() // The color of the background for the values (they can change)
    {
        return new Color(CSList.List[ColorScheme].ColorBG5[0], CSList.List[ColorScheme].ColorBG5[1], CSList.List[ColorScheme].ColorBG5[2], CSList.List[ColorScheme].ColorBG5[3]);
    }

    public static Color SetFontColorValue() // The color of the font for the values (they can change)
    {
        return new Color(CSList.List[ColorScheme].ColorFT5[0], CSList.List[ColorScheme].ColorFT5[1], CSList.List[ColorScheme].ColorFT5[2], CSList.List[ColorScheme].ColorFT5[3]);
    }
}
