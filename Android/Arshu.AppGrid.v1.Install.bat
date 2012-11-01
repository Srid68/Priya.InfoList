adb devices

aapt dump badging Arshu.AppGrid.v1-Signed.apk

adb install -r Arshu.AppGrid.v1-Signed.apk

adb shell am start -a android.intent.action.MAIN -n Arshu.AppGrid.v1/arshu.appgrid.StartAndroidActivity

pause 

