using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour {

    public DataWriter dataWriter;
    public GameObject dynamicMesh;
    public MeshBuilderGUIWithExport meshBuilderGUI;

    public Text poseText;
    public Text depthText;
    public Text imageText;
    public Text exportText;

    private string posePath;
    private string depthPath;
    private string imagePath;
    private string exportFile;

	// Use this for initialization
	void Start () {
        // A default path
        posePath = depthPath = imagePath =  "/sdcard/";
        exportFile = "Demo";
	}
	
    // Will be called by start button on UI
    public void submit()
    {
        if(poseText.text != "")
        {
            posePath = poseText.text;
        }

        if(depthText.text != "")
        {
            depthPath = depthText.text;
        }

        if(imageText.text != "")
        {
            imagePath = imageText.text;
        }

        if(exportText.text != "")
        {
            exportFile = exportText.text;
        }

        // Fill in datawriter
        dataWriter.posePath = posePath;
        dataWriter.depthPath = depthPath;
        dataWriter.imagePath = imagePath;

        dataWriter.createDirectory();

        // Fill in export
        meshBuilderGUI.exportName = exportFile;

        // Activate dynamic mesh
        dynamicMesh.SetActive(true);

        // Deactivate this canvas
        gameObject.SetActive(false);
    }

}
