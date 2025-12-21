REM Clears the native image cache in case you get error messages
REM when opening the project using VS Expres
REM http://blogs.msdn.com/b/astebner/archive/2005/11/09/491118.aspx

rd /s /q %windir%\assembly\NativeImages_v2.0.50727_32\

