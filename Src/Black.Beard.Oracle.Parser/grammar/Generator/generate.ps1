# antlr-4.7-complete.jar must be preset 
# http://www.antlr.org/download/antlr-4.7-complete.jar

# java.exe -jar antlr-4.7-complete.jar -Dlanguage=CSharp oracle.g4 -visitor -no-listener -package Bb.Oracle.Parser

java.exe -jar antlr-4.7-complete.jar -Dlanguage=CSharp PlSqlLexer.g4 -visitor -no-listener -package Bb.Oracle.Parser
java.exe -jar antlr-4.7-complete.jar -Dlanguage=CSharp PlSqlParser.g4 -visitor -no-listener -package Bb.Oracle.Parser