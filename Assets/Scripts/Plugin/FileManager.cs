using UnityEngine;

public static class FileManager
{
    private const string packName = "com.ramosmarin.mylibrary";
    private const string loggerClassName = "FileManager";

    private static AndroidJavaClass fileManager = null;
    private static AndroidJavaObject fileManagerInstance = null;

    public static string ReadFile()
    {
        if (fileManagerInstance == null)
        {
            Init();
        }
        string txt = fileManagerInstance?.Call<string>("ReadFile");

        return txt;
    }

    public static void WriteFile(string data)
    {
        if (fileManagerInstance == null)
        {
            Init();
        }
        fileManagerInstance?.Call("WriteFile", data + "\n");
    }

    public static void DeleteFile()
    {
        PopUp.Init();
        PopUp.ShowAlertDialog(new string[] { "Are you sure you want to delete logs?", "deleting logs.txt", "Delete", "Cancel" },
            (index) =>
            {
                Debug.Log("Index of button: " + index);
                if (index == -3)
                {
                    if (fileManagerInstance == null)
                    {
                        Init();
                    }
                    fileManagerInstance?.Call("DeleteFiles");
                }
            });
    }

    private static void Init()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        fileManager = new AndroidJavaClass(packName + "." + loggerClassName);
        AndroidJavaClass unityJC = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject activity = unityJC.GetStatic<AndroidJavaObject>("currentActivity");
        fileManager.SetStatic("mainAct", activity);

        fileManagerInstance = fileManager.CallStatic<AndroidJavaObject>("GetInstance");
#endif
    }
}