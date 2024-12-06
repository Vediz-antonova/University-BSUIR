.model small
.stack 100h 
.data  
    msg db "String", 0Dh, 0Ah, '$' 

.code 
    mov ax, @data 
    mov ds, ax      
    
    mov ah, 9
    mov dx, offset msg
    int 21h
    mov ax, 4C00h
    int 21h