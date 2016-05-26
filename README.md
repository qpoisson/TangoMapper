# TangoMapper
3D reconstruction android app built using Google Tango Unity API for UCSD CSE 145.  
  
Team Lead: Quentin Gautier  
Team: Steven Lee, Jin Woong Yu, Sung Wook Son
  
### 1. What is Unity
---
[Unity](https://unity3d.com/) is a 3D game engine.  
The Google Tango has Unity support for AR and VR applications.

### 2. Source Code
---
Unity adds a lot of their own files.  
Find the actual C# scripts [here](/Assets/TangoSDK/Project/MeshBuilder/Scripts).  
The most important script is the TangoDynamicMesh.cs script since that contains all the actual code to create the meshes.  
Ignore the .meta files as those are Unity's organization files.

### 3. Building without Unity
---
Currently there is no way to build the project without the Unity editor.  
There will be a .apk file in the repository which can then be installed through the Google Tango.

### 4. Getting Started with Unity
---
Unity can build for many platforms including Android which the Google Tango uses.  
#####Follow these instructions from the [Google Unity getting started website](https://developers.google.com/project-tango/apis/unity/).

##### What version should I use?
This project was built and tested using Unity version 3.4.1.f on Windows 10.  
Windows and Mac OS versions are recommended.  
The Linux version is fairly new so might be unstable.  

##### For some reason I can't do anything Android related on Unity.
Make sure you are installing from Unity's Download Assistant and not a specific Unity version x.x.x installer.  
The Unity download website should have the Unity Download Assistant as the default download.  
You should be getting the options in the image below during your installation.  
Install with Android build support.  
Microsoft Visual Studio is the preferred text editor.  
  
![alt tag](https://raw.githubusercontent.com/ssl024/TangoMapper/master/images/unity_install_components.PNG?token=AIseTDOSsc2W0s8zvFfZoPII5n6sUQEtks5XS3HTwA%3D%3D)
  
### 5. Installing on the Google Tango
---
Take the .apk that Unity builds and move it over to the Google Tango.  
Use a file explorer application on the Tango and install the .apk.  

# How do I use the application?
---

### 1. App menu
---
You have the application and a file explorer I am using in conjunction.  
  
![alt tag](https://raw.githubusercontent.com/ssl024/TangoMapper/master/images/tablet_apps.jpg)

### 2. The App
---
Once you start the application, make sure you hold still until the "Hold Still" window disappears.  
  
You will see this menu.  Place the directory where each of these file types will be saved in.  
For example: if I type in "/tangoPoses/" for the pose directory, then my "pose.txt" will be created in "/tangoPoses/pose.txt"  
You may decide not to put in a directory, then by default no files of that type will be saved even if they are recorded.  

![alt tag](https://raw.githubusercontent.com/ssl024/TangoMapper/master/images/startmenu_numbered.jpg)  

### 3. In App
---
This is where all the action happens.  
The top-left corner shows your debug info.  If your backlog gets too big, then you can pause to give the device a chance to finish up.  
![alt tag](https://raw.githubusercontent.com/ssl024/TangoMapper/master/images/sidebar_newoptions.jpg)  

Your Pose, Depth, and Image buttons on the right start and stop recording each type of file.  

Pose is saved as a lines in a text file of format: timestamp orientation[0] orientation[1] orientation[2] orientation[3] translation[0] translation[1] translation[2]  

Depth is saved as two different files: a .depth file (txt file) that contains an array of packed coordinate triplets, x,y,z as floating point values where each index of the array is a line in the file and a .indices file (also a txt file) that is a buffer ordering that contains the index of a point in the xyz array that was generated at this "ij" location where each index in the array is a line in the file.  [Depth reference](https://developers.google.com/project-tango/apis/unity/reference/class/tango/tango-unity-depth#constructors-and-destructors).

Image is saved as a stream of bytes representing the pixels.  It is in the TANGO_HAL_PIXEL_FORMAT_YV12
YCrCb 4:2:0 Planar format.  [Image reference](https://developers.google.com/project-tango/apis/unity/reference/class/tango/tango-unity-image-data#public-attributes).  

### 4. Where are my recordings?
---
Open up a file explorer app (I am using file commander)  
On the sidebar go to internal storage.  
In the main scroll bar find the directory you typed.  For instance if I had typed "/poses/" for my pose directory, then I would have this folder.  


![alt tag](https://github.com/ssl024/TangoMapper/blob/master/images/fileexplorer_pose.jpg)
