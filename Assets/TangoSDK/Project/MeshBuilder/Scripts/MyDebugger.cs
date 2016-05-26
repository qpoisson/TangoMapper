using System;
using System.Text;
using Tango;
using UnityEngine;

public class MyDebugger : MonoBehaviour, ITangoPose {

    /// <summary>
    /// If set, debugging info is displayed.
    /// </summary>
    public bool m_enableDebugUI = true;

    private TangoApplication m_tangoApplication;

	// Use this for initialization
	void Start () {
        m_tangoApplication = GameObject.FindObjectOfType<TangoApplication>();
        if (m_tangoApplication != null)
        {
            // Register for callbacks
            m_tangoApplication.Register(this);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnTangoPoseAvailable(Tango.TangoPoseData poseData)
    {
        if (poseData.framePair.baseFrame ==
        TangoEnums.TangoCoordinateFrameType.TANGO_COORDINATE_FRAME_AREA_DESCRIPTION &&
        poseData.framePair.targetFrame ==
        TangoEnums.TangoCoordinateFrameType.TANGO_COORDINATE_FRAME_START_OF_SERVICE &&
        poseData.status_code == TangoEnums.TangoPoseStatusType.TANGO_POSE_VALID)
        {
            AndroidHelper.ShowAndroidToastMessage("Loop detected");
        }
    }

}
