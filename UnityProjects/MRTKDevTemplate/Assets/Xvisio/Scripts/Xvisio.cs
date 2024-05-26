using System;
using System.Runtime.InteropServices;

public static class Xvisio
{
    [DllImport("XvisioService.dll")]
    public static extern void CStart(string json, byte mode);

    [DllImport("XvisioService.dll")]
    public static extern void CStop();
    [DllImport("XvisioService.dll")]
    public static extern void CResetSlam();

    [DllImport("XvisioService.dll")]
    public static extern IntPtr CGetPose();

    public static void Start(string json = "")
    {
        CStart(json, 0); //edge mode
    }

    public static void Stop()
    {
        CStop();
    }

    public static void ResetSlam()
    {
        CResetSlam();
    }

    public static bool GetPose(float[] pose)
    {
        IntPtr ptr = CGetPose();
        if (ptr != IntPtr.Zero)
        {
            Marshal.Copy(ptr, pose, 0, 13); //1 float - confidence, 9 floats - rot mat, 3 floats pos
            return true;
        }
        return false;
    }
}
