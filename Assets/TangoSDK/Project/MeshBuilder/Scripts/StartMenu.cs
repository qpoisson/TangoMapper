using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour {

    public DataWriter dataWriter;
    public GameObject dynamicMesh;

    public Text poseText;
    public Text depthText;
    public Text imageText;

    private string posePath;
    private string depthPath;
    private string imagePath;

	// Use this for initialization
	void Start () {
        // A default path
        posePath = depthPath = imagePath = "/sdcard/";
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

        // Fill in datawriter
        dataWriter.posePath = posePath;
        dataWriter.depthPath = depthPath;
        dataWriter.imagePath = imagePath;

        dataWriter.createDirectory();

        // Activate dynamic mesh
        dynamicMesh.SetActive(true);

        // Deactivate this canvas
        gameObject.SetActive(false);
    }

}
