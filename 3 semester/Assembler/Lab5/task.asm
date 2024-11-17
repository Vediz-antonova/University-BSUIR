.model small
.stack 100h

.data  
    buffer db 512 dup(0)  
    fileName db 100h dup(0)
    
    num dw 0  
    lineCounter dw 0
    res dw 11 dup(0)  
    
    msgFileName db 'Enter the file name: $'  
    msgNumber db 'Input size of word: $'  
    Enter db 0Dh, 0Ah, '$'   
    msgEmpty db 'String is empty.$' 
    msgCount db 'Number of lines longer than specified: $'  
    
.code
start:
    mov ax, @data
    mov ds, ax 
    
    call readSize
    call closeFile
    call showResult
    
    jmp exit      
    
closeFile:
    mov ah, 3Eh
    int 21h  
    ret
    
showResult:
    lea dx, msgCount
    call output
    
    lea bx, lineCounter 
    call convertToStr 
    lea dx, res 
    call output 
    lea dx, Enter 
    call output
    ret
    
exit:
    mov ax, 4C00h
    int 21h     
    
output:
    mov ah, 09h
    int 21h
    ret 
    
readSize:      
    lea dx, msgNumber
    call output   
    lea si, num
    call input 
    lea dx, Enter
    call output 
    ret 
    
input proc   
    mov ax, 0  
    mov bx, 0
    mov cx, 0 
    mov dx, 0
    mov bl, 0 
    
inputLoop: 
    mov ah, 01h 
    int 21h 
    
    cmp al, 0Dh 
    je inputEnd
    cmp al, '0' 
    jb invalidInput 
    cmp al, '9' 
    ja invalidInput 
    
    sub al, '0' 
    mov dl, al  
    mov al, bl
    mov cl, 10
    mul cl
    add ax, dx
        
    inc cx
    mov bx, ax 
    
    jmp inputLoop    
    
invalidInput:
    mov ah, 0Eh 
    mov al, 08h 
    int 10h 
    mov al, ' ' 
    int 10h 
    mov al, 08h 
    int 10h 
    jmp inputLoop 

inputEnd:     
    mov [si], bx     
    cmp cx, 0
    je empty   
    
    ret  
input endp 

empty:
    lea dx, Enter
    call output               
                   
    lea dx, msgEmpty
    call output  
    jmp exit  
    
convertToStr proc
    mov bx, 10
    add di, 5       
    mov byte ptr [di], '$' 

convertLoop:
    dec di           
    mov dx, 0       
    div bx           
    add dl, '0'     
    mov [di], dl

    test ax, ax      
    jnz convertLoop 
    ret
convertToStr endp
end start