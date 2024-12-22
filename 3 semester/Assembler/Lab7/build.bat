del prog.exe
tasm /ml lab7ass.asm
tcc -ms -Ic:\tc\include -c lab7.cpp
tlink c:\tc\lib\c0s.obj lab7ass.obj lab7.obj, prog.exe, , c:\tc\lib\cs.lib
