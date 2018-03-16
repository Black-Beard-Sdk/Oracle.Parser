

[System.Environment]::SetEnvironmentVariable("CLASSPATH", "C:\src\Oracle.Parser\Src\Black.Beard.Oracle.Parser\grammar\antlr-4.7-complete.jar", [EnvironmentVariableTarget]::Machine);

# java.exe C:\src\Oracle.Parser\Src\Black.Beard.Oracle.Parser\grammar\antlr-4.7-complete.jar:org.antlr.v4.runtime.misc.TestRig


java org.antlr.v4.runtime.misc.TestRig