using UnityEngine;

namespace Plugins {
public class Clipboard
{
    public static void CopyToClipboard(string textToCopy)
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        // Use Unity's built-in clipboard for non-WebGL or in editor
        GUIUtility.systemCopyBuffer = textToCopy;
#else
        // For WebGL, use JavaScript interop
        CopyToClipboardWebGL(textToCopy);
#endif
    }

#if UNITY_WEBGL
    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern void CopyToClipboardWebGL(string text);
#endif
}
}