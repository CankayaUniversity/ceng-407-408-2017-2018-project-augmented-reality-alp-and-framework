# Augmented Reality Based Continuous Onboarding Framework

* İpek OHRİ 201311038
* Bora ORKUN 201311039
* H. İrem ÖGE 201311040

##### Advisor: Assist. Prof. Dr. Murat YILMAZ
##### Co-Advisor: Dr. Eray TÜZÜN

This page contains information about the project titled as “Augmented Reality Based Continuous Onboarding Framework”. This system is an application that assists developers to adopt more easily to their working environment, and ultimately improve the software development process and work more efficiently.

Augmented Reality Based Continuous Onboarding Framework project is developed using Unity 3D Game Engine and Vuforia tool for augmented reality support. Language that is used for coding is C#. This project is developed to run on Android mobile devices.

# Installation & Compilation Guide

### Prerequisites / Tools
	
* Unity 2017.3.1f1 should be installed on the computer to run and compile the project. Other versions of Unity can cause some problems, since the project is developed with the features of 2017.3.1f1. 
* This version of Unity is integrated with Vuforia, so while installing that Unity version, Vuforia Augmented Reality Support should be chosen on the Components section.
* Android Studio should be installed to create .apk file from Unity, since the project will run on Android devices.


* Final version of the project is available at the link: https://github.com/CankayaUniversity/ceng-407-408-project-augmented-reality-alp-and-framework/releases/tag/v1.0.1 It can be downloaded as a .zip file. Extract the files from .zip folder.


### Opening the Project in Unity
* Open Unity.
* When Unity is opened, at home page click Open and then choose the SeniorProject-AR folder from the downloaded project folder.
* After the project is set and opened in Unity, from Assets folder, as seen in Figure 1, choose Login scene to start using the program from the beginning.

![](http://i66.tinypic.com/5lv1qd.png)

Figure 1: Unity page to run the program

* After the Login scene is chosen, press the Play button from the top, which is shown by a red circle in Figure 1. Then, program is built and run. (*)

### Creating .apk File for Android Devices
	
System is run on Android devices; therefore, to transfer the project into Android devices, .apk file should be created. Android Studio should be installed in the computer to create the .apk file from Unity. 


* Connect your Android device to the computer.
* From Unity, choose File -> Build Settings.
* All the scenes in Scenes In Build panel must be checked and ordered as seen in the Figure 3.
* From Platform panel, choose Android and press Switch Platform button. 
* After switch operation is completed, press Build and Run button. 
* In the opening window, enter name of the .apk file.

.apk file is created and application is run on the android device after building is completed.

![](http://i65.tinypic.com/295wvo4.png)

Figure 2: Build Settings


## Installation

* From the guide above, created .apk file can be transferred to an Android device by simply dragging and copying the file. 
* Also, from the link below, ARBCOF.apk file can be downloaded. After the file is downloaded, it can be extracted from. zip file and copied to the Android device and it is ready to run on the device.

https://drive.google.com/file/d/1TaGdJLUQHOwAT9EM8INsJBR7kiL5LVHY/view?usp=sharing


# User Manual

## Additional Documents
* For detailed information about the requirements of the system, see Augmented Reality Based Continuous Onboarding Framework Software Requirements Specification [(SRS)](https://github.com/CankayaUniversity/ceng-407-408-project-augmented-reality-alp-and-framework/wiki/Software-Requirement-Specifications)
* For detailed information about the design of the system, see Augmented Reality Based Continuous Onboarding Framework Software Design Document [(SDD)](https://github.com/CankayaUniversity/ceng-407-408-project-augmented-reality-alp-and-framework/wiki/Software-Design-Document)
* For detailed information about the test plan and test cases of the system, see Augmented Reality Based Continuous Onboarding Framework Test Design Specification [(TDS)](https://github.com/CankayaUniversity/ceng-407-408-project-augmented-reality-alp-and-framework/wiki/Test-Plan,-Test-Design-Specifications-and-Test-Cases)

All of these documents are available at: https://augmentedrealitybcof.wordpress.com/documentation/

## System Requirements
* Android OS 4.4+
* ARMv7 CPU with NEON support or Atom CPU
* OpenGL ES 2.0+
* Internet connection

Note: ‘+’ means later, higher versions

## Overview of the Product

Augmented Reality Based Continuous Onboarding Framework is an augmented reality application that assists developers to adopt more easily to their working environment, and ultimately improve the software development process and work more efficiently. This is a system that will be used in the office environment to increase the communication between the newcomers with former employees and provide effective and efficient orientation process. Also, this system is used especially by Scrum development teams, to make the meetings and processes more effective by using augmented reality technology.

This system runs on Android devices, especially on Android mobile devices of the company employees. Employees should login to the system by their company accounts. System has 2 main modes which are Orientation Mode and Operation Mode. Switch between these modes are done by using a menu. 

Orientation Mode is used for learning more information about the employees to create an easy communication with them.
Operation Mode has two different functions. One of them is to learn about people’s tasks and shared tasks with the person himself. The other one is to learn about the tasks of a project by combining the Scrum board of the project with the augmented reality components using the data on Team Foundation Server.

## Using the Augmented Reality Based Continuous Onboarding Framework
### Download Augmented Reality Based Continuous Onboarding Framework

If you have not already downloaded the Augmented Reality Based Continuous Onboarding Framework application, you can download the apk from [here](https://drive.google.com/file/d/1TaGdJLUQHOwAT9EM8INsJBR7kiL5LVHY/view?usp=sharing )


## Screens of the Project
### Login
The system is only available for authorized people. To control the user access, there is a login mechanism in the application. Every user has a unique e-mail address and password which exist in system database. In this system, e-mail addresses and passwords are used as credentials for users.

![](http://i68.tinypic.com/34xkmkz.png)

Figure 1: Login Screen

#### User Interactions

| **Name**   | **Type** | **Explanation** |
| ------------- | ------------- | ------------- |
| Email  | TextField |  To access the system, user should fill the correct e-mail address which belongs the him/her. |
| Password   | TextField | To access the system, user should fill the correct password which belongs the him/her. |
| Login   | Button | After filling the e-mail and password fields, user clicks this button to enter the system. |


Table 1: Login Screen User Interactions 

### Main Menu
After successful login operation, users are navigated to the main menu which includes mode selection buttons. 

![](http://i68.tinypic.com/2vv1a46.png)

Figure 2: Main Menu 

#### User Interactions

| **Name**   | **Type** | **Explanation** |
| ------------- | ------------- | ------------- |
| Orientation Mode  | Button | User can click this button to access the Orientation Mode of the system.  |
| Operation Mode   | Button | User can click this button to access the Operation Mode of the system. |
| Quit   | Button | User can click this button to exit from application. |

Table 2: Main Menu User Interactions

### Orientation Mode
In this mode, user can access the information about his/her teammates. To get this information, user should catch the correct position to scan the unique image of selected teammate. After scanning the image target, system will recognize the image and informative panel will appear through the device screen. User can learn about some professional and personal information about that person.

![](http://i68.tinypic.com/4tse1d.png)

Figure 3: Orientation Mode

#### User Interactions

| **Name**   | **Type** | **Explanation** |
| ------------- | ------------- | ------------- |
| Video Play/Pause  | Button | User can click this button to play or pause the informative video about the person.  |
| Pause   | Button | User can click this button to pause the Operation Mode and open the pause menu. |

Table 3: Orientation Mode User Interactions

### Operation Mode
In this mode, user can access the information about the tasks which are defined in the current sprint. Also, in this mode, user can see the teammates workload  To get this information, user should catch the correct position to scan the image target. After scanning the image target, system will recognize the image and informative panel will appear through the device screen. User can learn about task status or workload of his/her teammates depends on the image target.

![](http://i64.tinypic.com/2u59ico.png)						
	
Figure 4: Operation Mode Feature 1


![](http://i68.tinypic.com/20hwys2.png)	

Figure 5: Operation Mode Feature 2


| **Name**   | **Type** | **Explanation** |
| ------------- | ------------- | ------------- |
| Open in TFS  | Button | User can click this button to open TFS link of the selected task on the default web browser. |
| Pause   | Button | User can click this button to pause the Operation Mode and open the pause menu. |

Table 4: Operation Mode User Interactions	

### Pause
User can stop the running mode and change his/her current mode or quit from the application.

![](http://i64.tinypic.com/vpkdv7.png)

Figure 6: Pause Menu


| **Name**   | **Type** | **Explanation** |
| ------------- | ------------- | ------------- |
| Resume  | Button | User can click this button to turn back the last running application mode. |
| Main Menu  | Button | User can click this button to open the Main Menu. |
| Main Menu  | Button | User can click this button to exit from application. |

Table 5: Operation Mode User Interactions



