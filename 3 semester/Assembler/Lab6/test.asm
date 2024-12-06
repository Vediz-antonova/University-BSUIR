name "test", 0
.model tiny
.code
org 100h

start:
    mov cx, 80h 
    cld
    mov si, 81h          
    lea di, str          
    
copyParams:
    lodsb               
    cmp al, 0Dh        
    je endCopy
    stosb               
    loop copyParams   
    
endCopy:
    mov byte ptr [di], '$' 
    
    lea dx, str
    mov ah, 09h
    int 21h

    mov ax, 4C00h
    int 21h

.data
    str db 128 dup(0)    
end start
