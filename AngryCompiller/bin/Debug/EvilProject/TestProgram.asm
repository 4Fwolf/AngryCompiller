.686
.model flat, stdcall 
option casemap: none

include \masm32\include\windows.inc
include \masm32\macros\macros.asm
include \masm32\include\kernel32.inc 
include \masm32\include\masm32.inc 
include \masm32\include\user32.inc 
include \masm32\include\gdi32.inc 
include \masm32\include\msvcrt.inc

includelib \masm32\lib\kernel32.lib 
includelib \masm32\lib\masm32.lib 
includelib \masm32\lib\gdi32.lib 
includelib \masm32\lib\user32.lib
includelib \masm32\lib\msvcrt.lib

.data
format1 db "%d",10,13
tmp dd 0
_Dig dd 0
_Dig1 dd 0

.code
start:
invoke  crt_scanf,ADDR format1,ADDR _Dig
invoke  crt_scanf,ADDR format1,ADDR _Dig1
mov eax,_Dig
.if  eax > _Dig1
mov eax,10
mov ebx,10
add eax,ebx
mov ebx,eax
mov _Dig,ebx
.else
mov eax,_Dig
mov ecx,10
sub eax,ecx
mov ecx,eax
mov _Dig1,ecx
.endif
invoke  crt_printf,ADDR format1,_Dig
invoke  crt_printf,ADDR format1,_Dig1
invoke  crt_scanf,ADDR format1,ADDR _Dig
invoke  ExitProcess,0
END start
