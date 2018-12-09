using UnityEngine;
using System.Collections;

public static class ImmersiveMode
{
#if UNITY_ANDROID
    /// <summary>
    /// Contains a reference to the associated Java class.
    /// </summary>
    public static AndroidJavaClass JavaClass { get; private set; }

    /// <summary>
    /// Is true when the navigation bar was detected.
    /// This property is NOT reliable.
    /// </summary>
    public static bool IsNavigationBarDetected { get; private set; }
    
    static ImmersiveMode()
    {
        try
        {
            JavaClass = new AndroidJavaClass("com.ruudlenders.immersivemode.ImmersiveMode");
            IsNavigationBarDetected = JavaClass.CallStatic<bool>("isNavigationBarDetected");
        }
        catch { }
    }

    /// <summary>
    /// Adds an activity to make it enter full-screen mode.
    /// </summary>
    public static bool Add(AndroidJavaObject activity)
    {
        return JavaClass != null && JavaClass.CallStatic<bool>("add", activity);
    }

    /// <summary>
    /// Adds the current activity to make it enter full-screen mode.
    /// </summary>
    public static bool AddCurrentActivity()
    {
        return JavaClass != null && JavaClass.CallStatic<bool>("addCurrentActivity");
    }

    /// <summary>
    /// Removes all activities to make them exit full-screen mode.
    /// </summary>
    public static bool Clear()
    {
        return JavaClass != null && JavaClass.CallStatic<bool>("clear");
    }

    /// <summary>
    /// Checks if an activity has entered full-screen mode.
    /// </summary>
    public static bool Contains(AndroidJavaObject activity)
    {
        return JavaClass != null && JavaClass.CallStatic<bool>("contains", activity);
    }

    /// <summary>
    /// Checks if the current activity has entered full-screen mode.
    /// </summary>
    public static bool ContainsCurrentActivity()
    {
        return JavaClass != null && JavaClass.CallStatic<bool>("containsCurrentActivity");
    }

    /// <summary>
    /// Removes an activity to make it exit full-screen mode.
    /// </summary>
    public static bool Remove(AndroidJavaObject activity)
    {
        return JavaClass != null && JavaClass.CallStatic<bool>("remove", activity);
    }

    /// <summary>
    /// Removes the current activity to make it exit full-screen mode.
    /// </summary>
    public static bool RemoveCurrentActivity()
    {
        return JavaClass != null && JavaClass.CallStatic<bool>("removeCurrentActivity");
    }
#endif
}
