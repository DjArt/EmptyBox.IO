cd /d "M:\EmptyBox.IO\EmptyBox.IO.Android.Test" &msbuild "EmptyBox.IO.Android.Test.csproj" /t:sdvViewer /p:configuration="Release" /p:platform=Any CPU
exit %errorlevel% 