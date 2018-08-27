# AppControl #

AppControl sample application demonstrates how to launch an application and get a result for the launch request.

<p align="center">
 <img src="./AppControl_Snapshot.png" width=150 height=150>
 <img src="./AppControl_Snapshot_1.png" width=150 height=150>
</p>

When you press `Launch` button, *AppInformation* sample app will be launched.


### Prerequisites
First of all, you need to install [AppInformation sample app](https://github.com/Samsung/Tizen-CSharp-Samples/tree/master/Wearable/AppInformation) 
because it is what would be launched by AppControl sample application. 

You can install it by using `Visual Studio` or the `sdb` command line as follows:

```
$ sdb install org.tizen.example.AppInformation-1.0.0.tpk
```

This application uses Tizen.Application API.

* [Class AppControl][AppControl]
* [Class AppControlLaunchMode][AppControlLaunchMode]

In addition, there is similar native/web sample applications.

* [Tizen Native version](https://developer.tizen.org/development/sample/native/AppFW/Application_control)
* [Tizen Web app version](https://developer.tizen.org/development/sample/web/Application/App_Control)


   [AppControl]: <https://developer.tizen.org/dev-guide/csapi/api/Tizen.Applications.AppControl.html>
   [AppControlLaunchMode]: <https://developer.tizen.org/dev-guide/csapi/api/Tizen.Applications.AppControlLaunchMode.html>

