# TangoMapper
3D reconstruction android app built using Google Tango Unity API for UCSD CSE 145.
PHD Student: Quentin Gautier
Team: 

### What is Unity
---
[Unity](https://unity3d.com/) is a 3D game engine.
The Google Tango has Unity support for AR and VR applications.

### Source Code
---
Unity adds a lot of their own files.  Find the actual C# scripts [here](/Assets/TangoSDK/Project/MeshBuilder/Scripts).
The most important script is the TangoDynamicMesh.cs script since that contains all the actual code to create the meshes.
Ignore the .meta files as those are Unity's organization files.

### Building without Unity
---
Currently there is no way to build the project without the Unity editor.
There will be a .apk file in the repository which can then be installed through the Google Tango.

### Getting Started with Unity
---
Unity can build for many platforms including Android which the Google Tango uses.
Follow these instructions from the [Google Unity getting started website](https://developers.google.com/project-tango/apis/unity/).

##### What version should I use?
This project was built and tested using Unity version 3.4.1.f on Windows 10.
Windows and Mac OS versions are recommended.
The Linux version is fairly new so might be unstable.

##### For some reason I can't do anything Android related on Unity.
Make sure you are installing from Unity's Download Assistant and not a specific Unity Installer executable.
The Unity download website should have the Unity Download Assistant as the default download.
You should be getting the options in the image below during your installation.
Install with Android build support.
Microsoft Visual Studio is the preferred text editor.

![alt tag](https://raw.githubusercontent.com/ssl024/TangoMapper/master/images/unity_install_components.PNG?token=AIseTDOSsc2W0s8zvFfZoPII5n6sUQEtks5XS3HTwA%3D%3D)

### Installing on the Google Tango
---
Take the .apk that Unity builds and move it over to the Google Tango.
Use a file explorer application on the Tango and install the .apk.
