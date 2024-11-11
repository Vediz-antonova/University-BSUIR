.model small
.stack 100h

.data
    str db 202 dup(0)      
    len db 0
    msgEmpty db 'String is empty.$'
    msgOverflow db 'String is overflow.$'
    msgInput db 'Input your string: $'
    msgOutput db 'Sorted words: $'    
    msgEnter db 0Dh, 0Ah, '$'

.code
start:
    mov ax, @data
    mov ds, ax

    mov dx, offset msgInput
    mov ah, 09h
    int 21h

    xor cx, cx 
    lea si, str,

inputLoop:
    mov ah, 01h
    int 21h
    cmp al, 0Dh
    je inputEnd
                     
    mov [si], al      
    inc si 
    inc cx       
    cmp cx, 200 
    jge inputEnd

    jmp inputLoop

inputEnd:   
    mov byte ptr[si], 0Dh     
    mov len, cl
    cmp cx, 0
    je empty
    cmp cx, 201
    jge overflow

    lea di, str 
    xor bx, bx    
    mov cx, 0   
    
goSort:
    xor dx, dx
    mov dl, len
    jmp sort
    
outputSting:    
    mov dx,offset msgEnter
    mov ah,09h
    int 21h
 
    lea si, str
     
    mov dx,offset msgOutput
    mov ah,09h
    int 21h
    
    lea si, str
    jmp outputSorted   
    
empty:
    mov dx, offset msgEmpty
    mov ah, 09h
    int 21h
    jmp exit

overflow:
    mov dx, offset msgOverflow
    mov ah, 09h
    int 21h
    jmp exit

exit:
    mov ax, 4C00h
    int 21h 
    
sort proc 
    mov cx, 0 
    xor bx, bx 
    lea si, str
    lea di, str   
    
nextChar:
    cmp cl, len
    je endSort   
    
    cmp byte ptr[di], ' '
    je spaceFound   
    
    cmp bx, 0
    je newWord
    
    jmp processChar
    
spaceFound:  
    mov bx, 0
    
processChar:   
    inc di 
    inc cx
    jmp nextChar

newWord:  
    inc bx  
    
    cmp si, di      
    jne sortWords
    jmp processChar

sortWords:   
    jmp compareWords 
    
endCompare: 
    cmp ax, di 
    je caseFirst  
    mov bx, 1
    cmp ax, si 
    je caseSecond 
    
caseFirst: 
    push di 
    push si 
    mov cx, 0
    jmp countWordLength
    
countWordLength: 
    cmp byte ptr [di], ' ' 
    je shiftStringRight 
    cmp byte ptr [di], 0Dh 
    je shiftStringRight 
    inc di 
    inc cx 
    jmp countWordLength 
    
shiftStringRight:
    pop si
    pop di
    push si 
    push di
    inc cx

shiftLoop:
    mov al, [di]
    push si  
    mov bl, [si]
    mov [si], ' '
    inc si
    
innerShiftLoop:  
    mov dl, [si]  
    mov [si], bl
    mov bl, dl
    inc si
    cmp si, di
    jl innerShiftLoop
    
    pop si
    mov [si], al 
    inc si
    inc di
    dec cx
    jnz shiftLoop
      
    mov [si-1], ' '      
    pop di
    pop si  
    mov cx, di 
    lea si, str 
    jmp nextChar
    
caseSecond:
    inc si 
    cmp si, di
    je next
    
    cmp byte ptr[si], ' '
    je findSpaceForSi
                
    cmp bx, 0 
    je sortWords  
    
    jmp caseSecond

findSpaceForSi:
    mov bx, 0
    jmp caseSecond    
    
next:
    lea si, str
    inc di
    inc cx
    jmp nextChar
            
endSort:  
    lea si, str
    jmp outputSting 
sort endp  

compareWords proc 
    ; si first
    ; di second     
    push bx
    push dx  
    push cx
    mov cx, si
    mov dx, di
    
compareLoop:
    mov al, [si]
    mov bl, [di]
    
    cmp al, 20h
    je returnFirst  
    cmp bl, 0Dh
    je returnSecond 
    cmp bl, 20h
    je returnSecond   
    
    cmp al, bl
    jne returnLess
    
    inc si
    inc di
    jmp compareLoop  
    
returnLess:
    cmp al, bl
    jb returnFirst
    ja returnSecond
    
returnFirst:    
    mov ax, cx  
    jmp endCompareWords
    
returnSecond: 
    mov ax, dx 
    jmp endCompareWords
    
endCompareWords:    
    mov si, cx
    mov di, dx
    pop bx
    pop dx   
    pop cx 
    jmp endCompare
compareWords endp         

outputSorted proc
    lea si, str            
    mov cl, len               

outputLoop:
    cmp cl, 0
    je endOutput

    mov ah, 02h
    mov dl, [si]
    int 21h

    inc si
    dec cl
    jmp outputLoop

endOutput:
    jmp exit 
outputSorted endp 

end start