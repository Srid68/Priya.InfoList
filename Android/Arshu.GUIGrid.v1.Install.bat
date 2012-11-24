adb devices

aapt dump badging Arshu.GUIGrid.v1-Signed.apk

adb install -r Arshu.GUIGrid.v1-Signed.apk

adb shell am start -a android.intent.action.MAIN -n Arshu.GUIGrid.v1/arshu.guigrid.StartAndroidGUIActivity

pause 

