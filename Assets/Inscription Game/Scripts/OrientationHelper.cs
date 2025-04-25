using UnityEngine;

public static class OrientationHelper
{
    public static bool IsPortrait()
    {
        return Screen.height >= Screen.width;
    }

    public static bool IsLandscape()
    {
        return Screen.width > Screen.height;
    }

    public static string GetCurrentOrientation()
    {
        if (IsLandscape())
            return "Landscape";
        else
            return "Portrait";
    }
}
