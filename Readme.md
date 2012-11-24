# PhoneGap/Cordova alternative - Easiest way to package Html5 Apps into Android/IOS

Simply Copy the zip of your Html5App into the assets/Apps directory (Case Sensitive) of the APK

* Copy the APK to be packaged with the Html5 App into a Directory
* Copy the Zip of the Html5 App having index.html in the root folder of the zip file into /assets/Apps directory under the APK directory
* Execute this command "aapt add apkFileName assets/Apps/html5AppName"
* Sign and Zip Align and deploy to device/emulator.

Kindly note that this cannot be used for packaging C# Apps since you need to compile it using Xamarin Mono for Android profile. If you require this, then contact me so that i can help you to configure the development environment for enabling packaging C# Apps.

You can download the APK of a Hello world App [Android Apk Download][] as well as a C# Web App Apk (PreAlpha) and a OPEN GL based APK (PreAlpha).

# Major Difference between PhoneGap/Cordova and this Framework

The architecture of PhoneGap is based on Frontend Technologies (Html & Js) where as this facilitates both usage of Frontend and Backend Technologies (Html Mustache Templates, C#, RPC Service, ORM, SQLite database etc)  

The clear advantage is that this provides a easy migration from Html5 to purely Native Implementation (MonoTouch/MonoDroid) or a Common OpenGL (Both Android/IOS) based Implementation with high reusablity of the code base.

Using Backend Technologies enables usage of POCO/SQL based MicroORM to access Data (Petapoco), Usage of Typed View Templates and exposure of behaviour using Testable API Service Methods.

# Early Adopter Packaging Service (Standalone Apps)

Any early adopter who wants to validate suitability of this framework for packaging their Html5 and C# App can send (mailto:sri@arshu.com) with the source code required for me to package and send back to you for your verification of suitability.

This initial packaging Service will be provided **FREE** of cost for a limited time. 

If you find the packaged app after through testing is suitable for deployment, then you can send any custom branding (Background/Icons etc) (should include my branding also) (Design Assets/Layout/Navigation Flow)
for me to implement the branding changes and then you will have to pay only **~~1000~~/100 USD for Android** / **~~2500~~/250 USD for IOS** (For a Limited Time) through cheque drawn on Arshu Consultancy Private Limited for a license to deploy per App per OS.

Custom Freelance Service using the above framework will be provided at a latter date using [IdeaToMvp][] website

# Priya.InfoList Reference Implementation (Can Run in Webserver/Android/IOS)
 
An Example JQuery Mobile Single Page Info List App which captures information hierarchically as pages/sections/details with the option to enable comments from users having appropriate roles. 
The Info created are by default only viewable by the author. The author can assign access groups to enable other users to view the info created.

A example demo website running this app is hosted at http://www.priyadoc.com. 

Please Auto Register and enable Author Mode to add Info Pages. Advanced Options are visible only after saving and editing a page/section/detail.

The Info List App has be architected so that it can be deployed in Shared Host Servers/EC2/Iron Foundry/Android and IOS Devices with appropriate Platform Wrappers.

# Why do this

There is a major pain experienced by Companies when they want to develop mobile apps because of OS Fragmentation between three major OS (IOS/Android/Win8) leading to increased cost with unknown benefits. 

It is better to validate the usefulness/adoptability/need of the App by creating a Mobile Web App and then if it makes business sense to convert it to native app for enhanced performance/user experience. A Mobile Web has too much difference with a Native App, where as a Mobile Web App packaged as a Native App (Hybrid App) reduces the differences substantially.

To do this at the minimum cost, if we can adopt a architecture which enables seamless transfer of knowledge/effort from a web architecture to a native architecture with minimum rework it will be beneficial from a cost point of view.

## Different In Architecture

Every other Mobile Hybrid architecture is trying to provide the App functionality by targeting the Browser as the main component. This constraints all the architectures to using JavaScript and anyone who has worked on JavaScript for big projects knows the increased complexity/lack of tools to implement good separation of concerns and reduced performance due to Single Threaded JavaScript/Browser Complexity.

The Proposed Architecture is again providing the App Functionality by targeting the Browser, but also including a embedded socket server (Non Compliant Statefull Http/RPC/Rest Server) to distribute app functionality between the Browser and Socket Server. This leads to taking all the knowledge acquired in implementing scalable server side web app architecture to be used in the Mobile Hybrid Apps.

This Proposed Architecture also does not solve the inherent performance problem of the Browser, but solves the architecting of web apps using proven architectures already implemented in the server side of any web app. 

Also it provides a seamless way to migrate to a fully native app (without the Browser as the GUI) using OpenGL or Native UI, without reworking a lot of the application functionality.

The Meta Data Development architecture of the App will also enable in the future to develop apps in the Native Languages (Objective-C for IPhone, and Java for Android) using a higher level abstraction to generate common architectural code and only make developers implment the glue code/enhance the architectural code leading to reduced cost.

# Platform/GUI/Language Agnostic Architecture

A Architectural development workflow for building solutions to problems (Apps) in a Language Agnostic (Recode able in another language with minimum rework) and which can be deployed in multiple OS supported environments using appropriate native OS wrappers.

![Code Architectrue](https://raw.github.com/Srid68/Priya.InfoList/master/Document/Architecture2.png "Code Architecture")

The vision for implementing this code is to develop a methodology/architecture which is Platform/Language/GUI agnostic and also leads to Separation of Skills for various participants involved in the development of the code base

![Architectrue](https://raw.github.com/Srid68/Priya.InfoList/master/Document/Architecture3.png "Architecture")

Separation of Skills as well as Concerns in the Code base leads to parallel and efficient outsourcing of development

![Separation of Skills/Concerns](https://raw.github.com/Srid68/Priya.InfoList/master/Document/Architecture1.png "Separation of Skills/Concerns")

# Functional Screens of Info List Web App

## Auto Register 
To login immediately and Latter can enter the User Id and Password if required

![Augo Registeration](https://raw.github.com/Srid68/Priya.InfoList/master/Document/AutoRegister.png "Auto Registration")

## Guest Top Action Menu [LogOff/User Details/Setting - Guest Role] 
Access by clicking AppMaker Menu

![Guest Role Action Menu](https://raw.github.com/Srid68/Priya.InfoList/master/Document/GuestRoleActionMenu.png "Guest Role Action Menu")

## Enable Author Role - User Details 
Access by clicking User Name Link Button. The Application architecture has roles for controlling the access of functionality. 
The default roles provided are Guest, Author and Admin 

![User Details - Enable Author Role](https://raw.github.com/Srid68/Priya.InfoList/master/Document/UserDetailEnableAuthorRole.png "User Details - Enable Author Role")

## Change Password - User Details 
Access by clicking User Name Link Button

## User Groups - User Details 
Access by clicking User Name Link Button

## Author Top Action Menu [LogOff/User Details/Setting/API List/Tracking - Author Role Enabled] (Access by clicking AppMaker Menu)

![Author Role Enabled Action Menu](https://raw.github.com/Srid68/Priya.InfoList/master/Document/AuthorRoleEnabledActionMenu.png "Author Role Enabled Action Menu")


## API Details (Help Screen) 
Access by clicking API Link Button


## Setting Details 
Access by clicking Setting Link Button


## Tracking Details 
Access by clicking Tracking Link Button


## Admin Details 
Access by clicking Admin Link Button


## MIT License
The Source with the supporting binary dlls is published using MIT License

[IdeatoMvp]: http:www.ideatomvp.com "Idea to Mvp"