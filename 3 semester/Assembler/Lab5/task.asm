.model small
.stack 100h

.data  
    buffer db 255 dup(0)  
    fileName db 100h dup(0)
    
    num dw 0            
    lines dw 0
    lineCounter dw 0
    fileHandle dw ?
    bytesRead dw ?
    
    msgFileName db 'Enter the file name: $'  
    msgNumber db 'Input size of word: $'  
    Enter db 0Dh, 0Ah, '$'   
    msgEmpty db 'String is empty.$'               
    msgLines db 'Number of lines: $'
    msgCount db 'Number of lines longer than specified: $'
    
.code
start:
    mov ax, @data
    mov ds, ax  
    
    call parseCmdLineParams ; ??????? ?????????? ????????? ??????
    cmp num, 50
    jg notFoundBigger
         
    call openFile
    call readFile
    call closeFile
    call showResult
    
    jmp exit   
    
notFoundBigger:
    mov lineCounter, 0
    call showResult
    jmp exit
    
openFile:
    mov dx, offset fileName
    mov ah, 3Dh 
    mov al, 0 
    int 21h 
    jc exit 
    mov fileHandle, ax 
    ret  
    
readFile proc 
    mov ah, 3Fh 
    mov bx, fileHandle
    mov cx, 255 
    lea dx, buffer 
    int 21h 
    jc exitFileRead
    cmp ax, 0 
    je exitFileRead
    mov bytesRead, ax
    
    mov si, 0 
    mov bx, 0 
    
nextLine: 
    cmp si, bytesRead 
    je readMore
    
    mov al, [buffer + si] 
    cmp al, 0Dh 
    je endOfLine 
    cmp al, 0 
    je endOfFile
    inc bx 
    inc si 
    jmp nextLine 
    
endOfLine: 
    inc lines
    
    cmp bx, num 
    jle resetCounter 
    inc lineCounter 
    
resetCounter: 
    add si, 2
    mov bx, 0
    jmp nextLine 
    
readMore:
    call readFile
    ret
    
endOfFile:  
    inc lines
    cmp bx, num
    jle exitFileRead
    inc lineCounter

exitFileRead: 
    ret
readFile endp
    
closeFile:
    mov ah, 3Eh
    mov bx, fileHandle
    int 21h  
    ret
    
showResult:     
    lea dx, msgLines
    call output
    
    mov ax, lines   
    lea di, lines
    call convertToStr 
    lea dx, di 
    call output 
    lea dx, Enter 
    call output
    
    lea dx, msgCount
    call output
    
    mov ax, lineCounter   
    lea di, lineCounter
    call convertToStr 
    lea dx, di 
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
    
parseCmdLineParams proc 
    lea di, fileName 
    mov si, 80h 
    
paramLoop: 
    lodsb 
    cmp al, ' ' 
    je checkNext 
    stosb 
    jmp paramLoop 

checkNext: 
    cmp byte ptr [si], ' '
    jne numberParse 
    jmp paramLoop 

numberParse: 
    xor ax, ax 
    lodsb 
    sub al, '0' 
    mov num, ax 
    ret   
parseCmdLineParams endp

convertToStr proc
    mov bx, 10 
    add di, 10
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
