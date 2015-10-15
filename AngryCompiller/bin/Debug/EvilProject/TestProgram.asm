.586
.model flat, stdcall 
option casemap: none

include \masm32\include\windows.inc
include \masm32\include\kernel32.inc 
include \masm32\include\masm32.inc 
include \masm32\include\debug.inc
include \masm32\include\user32.inc 

includelib \masm32\lib\kernel32.lib 
includelib \masm32\lib\masm32.lib 
includelib \masm32\lib\debug.lib
includelib \masm32\lib\user32.lib

.data
tmp dd 0
_Dig dd 0
_Dig1 dd 0

.code
start:
