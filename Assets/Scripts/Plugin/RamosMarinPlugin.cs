using UnityEngine;

public class RamosMarinPlugin : MonoBehaviour
{
#if UNITY_ANDROID
    private AndroidJavaClass unityClass;
    private AndroidJavaObject unityActivity;
    private AndroidJavaObject pluginInstance;

    void Start()
    {
        InitializePlugin("com.ramosmarin.mylibrary.RamosMarinPlugin");
    }

    public void Toast()
    {
        if (pluginInstance != null)
        {
            pluginInstance.Call("Toast", "Hello user!");
        }
    }

    private void InitializePlugin(string pluginName)
    {
        unityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        unityActivity = unityClass.GetStatic<AndroidJavaObject>("currentActivity");
        pluginInstance = new AndroidJavaObject(pluginName);
        if (pluginInstance == null)
        {
            Debug.Log("Failed to initialize plugin");
        }
        else
        {
            pluginInstance.CallStatic("receiveUnityActivity", unityActivity);
        }

    }
#endif
}