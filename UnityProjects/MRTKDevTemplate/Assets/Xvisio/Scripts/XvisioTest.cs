using UnityEngine;

public class XvisioTest : MonoBehaviour
{
    public Transform[] targets;

    private float[] rawPose = new float[13];
    private bool poseReset = false;

    void Start()
    {
        Xvisio.Start();
        Debug.Log("Started");
    }

    void Update()
    {
        if (Xvisio.GetPose(rawPose) == true)
        {
            if (poseReset == false)
            {
                poseReset = true;
                Xvisio.ResetSlam();
                Debug.Log("Reset");
            }
            Vector3 pos = new Vector3(rawPose[10], -rawPose[11], rawPose[12]);
            Matrix4x4 m = Matrix4x4.identity;
            m.m00 = rawPose[1];
            m.m01 = rawPose[2];
            m.m02 = rawPose[3];
            m.m10 = rawPose[4];
            m.m11 = rawPose[5];
            m.m12 = rawPose[6];
            m.m20 = rawPose[7];
            m.m21 = rawPose[8];
            m.m22 = rawPose[9];
            Vector3 rot = Vector3.Scale(m.rotation.eulerAngles, new Vector3(-1f, 1f, -1f));

            foreach (var target in targets)
            {
                target.position = pos;
                target.eulerAngles = rot;
            }
        }
    }

    private void OnDestroy()
    {
        Xvisio.Stop();
        Debug.Log("Stopped");
    }
}
