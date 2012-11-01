# Instructions to Package Any Html App

* Create a [assets/Apps] directory under the APK Directory

* Ensure your Html5 App has index.html in the root folder

* Zip and Copy your Html5 App into /assets/Apps directory under the APK directory

* Copy and Rename the AppGrid Apk to Arshu.AppGrid.v1.apk 

* Add the Html5App as an Asset by Executing the following command in a command prompt
        aapt add Arshu.AppGrid.v1.apk assets/Apps/[html5AppName]

* Create a Priyate Key using the Keytool
	keytool -genkey -v -keystore appgrid.keystore -alias appgrid -keyalg RSA -keysize 2048 -validity 10000

* Sign the Apk by Executing the following command (Change your path)

"C:\Program Files (x86)\Java\jdk1.7.0_06\bin\jarsigner.exe" -verbose -sigalg MD5withRSA -digestalg SHA1 -keystore appgrid.keystore -signedjar Arshu.AppGrid.v1.apk appgrid

* ZipAling the Apk by Executing the following command 
	zipalign -v 4 Arshu.AppGrid.v1.apk Arshu.AppGrid.v1-Signed.apk

* Deploy to device/emulator using the provided batch file

