.model small    
.stack 100h  
.data                   
    hello db 'Hello!', 0Dh, 0Ah, '$'
    str db 128 dup(0)    
    
.code      
start: 
    push es
    mov ax, @data
    mov es, ax     
    
    mov si, 81h          
    lea di, str          
    mov cx, 128         

copyParams:
    lodsb               
    cmp al, 0          
    je endCopy         
    stosb              
    loop copyParams     

endCopy:
    mov byte ptr es:[di], '$'
    pop es
            
    mov ax, @data
    mov ds, ax   
                                     
    lea dx, hello
    mov ah, 09h
    int 21h
                                 
    lea dx, str
    mov ah, 09h
    int 21h

    mov ax, 4C00h
    int 21h    
end start
