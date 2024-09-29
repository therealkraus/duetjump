using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class that keeps a template of all materials color that need to be changed anytime scene is loaded
[System.Serializable]
public class ColorSchemes{

    public Color32 leftSphereColor;
    public Color32 rightSphereColor;
    public Color32 noDeathPadColor;
    public Color32 deathPadColor;
    public Color32 skyGradientColorTop;
    public Color32 skyGradientColorBottom;

    public ColorSchemes(Color32 aLeftSphereColor, Color32 aRightSphereColor, Color32 aNoDeathPadColor, Color32 aDeathPadColor, Color32 aSkyGradientColorTop, Color32 aSkyGradientColorBottom)
    {
        leftSphereColor = aLeftSphereColor;
        rightSphereColor = aRightSphereColor;
        noDeathPadColor = aNoDeathPadColor;
        deathPadColor = aDeathPadColor;
        skyGradientColorTop = aSkyGradientColorTop;
        skyGradientColorBottom = aSkyGradientColorBottom;
    }

   
}
