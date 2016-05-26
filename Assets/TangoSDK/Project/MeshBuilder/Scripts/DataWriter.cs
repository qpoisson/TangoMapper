using Tango;
using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class DataWriter : MonoBehaviour, ITangoPose, ITangoDepth, ITangoVideoOverlay {

    public bool poseIsRecording = false;
    public bool depthIsRecording = false;
    public bool imageIsRecording = false;

    public string posePath;
    public string depthPath;
    public string imagePath;
    public string exportPath = "/sdcard/TangoUnityMeshes";

    private double last_timestamp;

    private TangoApplication m_tangoApplication;

    Queue<TangoPoseData> pose_queue;
    Queue<TangoUnityDepth> depth_queue;
    Queue<ImagePair> image_queue;

	// Use this for initialization
	void Start () {
        last_timestamp = -1.0;

        pose_queue = new Queue<TangoPoseData>();
        depth_queue = new Queue<TangoUnityDepth>();
        image_queue = new Queue<ImagePair>();

        m_tangoApplication = GameObject.FindObjectOfType<TangoApplication>();
        if (m_tangoApplication != null)
        {
            m_tangoApplication.Register(this);
        }
	}
	
	// Update is called once per frame
	void Update () {

        // Process queues
        save_poses_task();
        save_depth_task();
        save_image_task();
	}

    // If we are recording pose, then queue up poses
    public void OnTangoPoseAvailable(TangoPoseData poseData)
    {
        if (poseIsRecording)
        {
            pose_queue.Enqueue(poseData);
        }
    }

    // If we are recording depth, then queue up depths
    public void OnTangoDepthAvailable(TangoUnityDepth tangoDepth)
    {
        if(depthIsRecording)
        {
            depth_queue.Enqueue(tangoDepth);
        }
    }

    public void OnTangoImageAvailableEventHandler(TangoEnums.TangoCameraId cameraId, TangoUnityImageData imageBuffer)
    {
        if(imageIsRecording)
        {
            ImagePair ip = new ImagePair(cameraId, imageBuffer);
            image_queue.Enqueue(ip);
        }
    }

    void saveImage(TangoUnityImageData im, string scan_name)
    {
        //double t = im->timestamp;
        double t = im.timestamp;

        //size_t height = im->height+im->height/2;
        //size_t width  = im->width;
        uint height = im.height + im.height/2;
        uint width = im.width;

        if(scan_name == "fisheye")
        {
            //height = im->height;
            //width  = im->stride; // stride > width for fisheye
            height = im.height;
            width = im.stride; // stride > width for fisheye
        }

        // Path to the file
        string filePath = imagePath + "/" + scan_name + "_" + t + ".yv12";

        // Create a file and write all bytes to it
        File.WriteAllBytes(filePath, im.data);
    }

    void save_poses_task()
    {
        string filepath = posePath + "/pose.txt";

        if (pose_queue.Count > 0)
        {
            // Read the front of the Queue
            TangoPoseData current_pose = pose_queue.Peek();

            string poseData = current_pose.timestamp + " ";
            poseData += current_pose.orientation[0] + " " + current_pose.orientation[1] + " ";
            poseData += current_pose.orientation[2] + " " + current_pose.orientation[3] + " ";
            poseData += current_pose.translation[0] + " " + current_pose.translation[1] + " ";
            poseData += current_pose.translation[2] + "\n";

            // Append data to file
            File.AppendAllText(filepath, poseData);

            // We're done with the pose so pop it out
            pose_queue.Dequeue();

            //if(poses_timestamps != null){ poses_timestamps.Add(current_pose.timestamp); }
        }
    }

    void save_image_task()
    {
       if(image_queue.Count > 0)
       {
           ImagePair ip = image_queue.Peek();

           // Not a new image
           if(ip.imageBuffer.timestamp == last_timestamp)
           {
               return;
           }

           last_timestamp = ip.imageBuffer.timestamp;

           saveImage(ip.imageBuffer, ip.cameraName);

           image_queue.Dequeue();
       }
    }


   /*
    * TangoUnityDepth: https://developers.google.com/project-tango/apis/unity/reference/class/tango/tango-unity-depth#public-static-attributes
    */
   void save_depth_task()
    {
        string scan_name = "depth";

        // Save depth
        if(depth_queue.Count > 0)
        {
            TangoUnityDepth depth = depth_queue.Peek();

            string depthFilePath = depthPath + "/" + scan_name + "_" + depth.m_timestamp + ".depth";
            string depthIndicesFilePath = depthPath + "/" + scan_name + "_" + depth.m_timestamp + ".indices";

            float[] points = depth.m_points;
            int[] indices = depth.m_ij;

            using(StreamWriter writer = new StreamWriter(depthFilePath))
            {
                for (int i = 0; i < depth.m_pointCount; ++i)
                {
                    writer.WriteLine(points[i]);
                }

                
            }

            using(StreamWriter writer = new StreamWriter(depthIndicesFilePath))
            {
                int numIndices = depth.m_ijRows * depth.m_ijColumns;

                for (int i = 0; i < numIndices; ++i)
                {
                    writer.WriteLine(indices[i]);
                }
            }

            //if(depth_timestamps != null){depth_timestamps.Add(depth.m_timestamp);}

            depth_queue.Dequeue();
        }
    }

    // Create the directories for each of the paths
    public void createDirectory()
    {
        posePath = "/sdcard" + posePath;
        depthPath = "/sdcard" + depthPath;
        imagePath = "/sdcard" + imagePath;
        Directory.CreateDirectory(posePath);
        Directory.CreateDirectory(depthPath);
        Directory.CreateDirectory(imagePath);
        
        // Create mesh directory
        Directory.CreateDirectory(exportPath);
    }



   private class ImagePair
   {
       public TangoEnums.TangoCameraId cameraId;
       public TangoUnityImageData imageBuffer;
       public string cameraName;

       public ImagePair(TangoEnums.TangoCameraId cameraId, TangoUnityImageData imageBuffer)
       {
           this.cameraId = cameraId;
           this.imageBuffer = imageBuffer;

           if(cameraId == TangoEnums.TangoCameraId.TANGO_CAMERA_FISHEYE)
           {
               cameraName = "fisheye";
           }
           else if(cameraId == TangoEnums.TangoCameraId.TANGO_CAMERA_COLOR)
           {
               cameraName = "colorcamera";
           }
           else if(cameraId == TangoEnums.TangoCameraId.TANGO_CAMERA_DEPTH)
           {
               cameraName = "depthcamera";
           }
           else if(cameraId == TangoEnums.TangoCameraId.TANGO_CAMERA_RGBIR)
           {
               cameraName = "rgbircamera";
           }
           else
           {
               cameraName = "unknowncamera";
           }
       }
   }


}
