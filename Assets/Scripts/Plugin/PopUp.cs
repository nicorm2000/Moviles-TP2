using UnityEngine;

public static class PopUp
{
    class AlertViewCallBack : AndroidJavaProxy
    {
        private System.Action<int> alertHandler = null;

        public AlertViewCallBack(System.Action<int> alertHandlerIn) : base(packName + "." + loggerClassName + "$AlertViewCallBack")
        {
            alertHandler = alertHandlerIn;
        }

        public void OnButtonTapped(int index)
        {
            Debug.Log("Button tapped: " + index);
            alertHandler?.Invoke(index);
        }
    }

    private const string packName = "com.ramosmarin.mylibrary";
    private const string loggerClassName = "PopUp";

    private static AndroidJavaClass popupManager = null;
    private static AndroidJavaObject popupManagerInstance = null;

    private static string title = "TitleText";
    private static string message = "MessageText";
    private static string button1 = "Close";

    public static void ShowPopup()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        if (popupManagerInstance == null)
        {
            Init();
        }
        ShowAlertDialog(new string[] { title + Application.version, message, button1 }, (int obj) =>
        {
            Debug.Log("Local Handler called: " + obj);
        });
#endif
    }

    public static void ShowAlertDialog(string[] strings, System.Action<int> handler = null)
    {
        if (strings.Length < 3)
        {
            Debug.LogError("AlertView requires at least 3 strings");
            return;
        }

        if (Application.platform == RuntimePlatform.Android)
        {
            popupManagerInstance?.Call("ShowAlertView", new object[] { strings, new AlertViewCallBack(handler) });
        }
        else
        {
            Debug.LogWarning("AlertView not supported on this platform");
        }
    }

    private static void Init()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        popupManager = new AndroidJavaClass(packName + "." + loggerClassName);
        AndroidJavaClass unityJC = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject activity = unityJC.GetStatic<AndroidJavaObject>("currentActivity");
        popupManager.SetStatic("mainAct", activity);

        popupManagerInstance = popupManager.CallStatic<AndroidJavaObject>("GetInstance");
#endif
    }
}