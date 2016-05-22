using System.Collections;
using Tango;
using UnityEngine;

/// <summary>
/// GUI Controls at the top-right.
/// 
/// Features include Clear, Pause/Resume, Export
///
/// </summary>

public class MeshBuilderGUIWithExport : MonoBehaviour
{
    /// <summary>
    /// Debug info: If the mesh is being updated.
    /// </summary>
    private bool m_isEnabled = true;

    private TangoApplication m_tangoApplication;
    private TangoDynamicMesh m_dynamicMesh;

    /// <summary>
    /// Start is used to initialize.
    /// </summary>
    public void Start()
    {
        m_tangoApplication = FindObjectOfType<TangoApplication>();
        m_dynamicMesh = FindObjectOfType<TangoDynamicMesh>();
    }

    /// <summary>
    /// Updates UI and handles player input.
    /// </summary>
    public void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    /// <summary>
    /// Draws the Unity GUI.
    /// </summary>
    public void OnGUI()
    {
        GUI.color = Color.white;
        if (GUI.Button(new Rect(Screen.width - 160, 20, 140, 80), "<size=30>Clear</size>"))
        {
            m_dynamicMesh.Clear();
            m_tangoApplication.Tango3DRClear();
        }
        
        string text = m_isEnabled ? "Pause" : "Resume";
        if (GUI.Button(new Rect(Screen.width - 160, 120, 140, 80), "<size=30>" + text + "</size>"))
        {
            m_isEnabled = !m_isEnabled;
            m_tangoApplication.Set3DReconstructionEnabled(m_isEnabled);
        }

        if (GUI.Button(new Rect(Screen.width - 160, 220, 140, 80), "<size=30>Export</size>"))
        {
            string filepath = string.Format("/sdcard/DemoMesh.obj");
            m_dynamicMesh.ExportMeshToObj(filepath);
            Debug.Log(filepath);
        }
    }
}
