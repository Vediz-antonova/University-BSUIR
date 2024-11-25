.model small
.stack 100h
.data
    cmdLength dw 0
    
    char db 0
    fileName db 32 dup(0) 
    fileID dw 0
    
    size dw 0
    lines dw 0    
    sizeLine dw 0
    lineCounter dw 0
    
    msgEnter db 0Dh, 0Ah, '$'     
    msgSize db 'Input size of word: $'   
    msgEmpty db 'String is empty.$'
    msgLines db 'Number of lines: $'
    msgCount db 'Number of lines longer than size: $'
    msgRead db 'Read file proc$'
    
.code
start:
    mov ax, @data
    mov ds, ax
    
    call getSize
    
    cmp size, 50
    jg notFoundBigger  
    
    call getCmdLine
    
    call openFile 
    call readFile
    call closeFile    
    
getCmdLine proc
    mov cx, 0   
    mov cl, ES:[80h]
    mov cmdLength, cx   
        
    cld
    mov di, 81h
    mov al, ' '
    rep scasb
    dec di
    lea si, fileName 
    
copy:
    mov al, ES:[DI]
    cmp al, 0Dh 
    je endCmd
    cmp al, 20h 
    je endCmd
    cmp al, 9h 
    je endCmd
    
    mov DS:[SI], al
    inc di
    inc si
    jmp copy  
       
endCmd:
    ret        
getCmdLine endp   

getSize: 
    lea dx, msgSize 
    call output 
    lea si, size 
    call input 
    lea dx, msgEnter 
    call output 
    ret 
    
notFoundBigger: 
    mov lineCounter, 0 
    call showResult 
    
openFile:                    
    mov ah, 3Dh
    mov al, 0
    lea dx, fileName
    int 21h
    jc exit
    mov fileID, ax  
    ret
    
readFile proc 
    mov si, size
    mov bx, fileID  
    mov cx, 1     
    lea dx, char   
    
nextChar: 
    mov ah, 3Fh
    int 21h 
    jc exitReadFile 
     
    cmp ax, 0
    je endOfFile
    
    cmp char, 13  
    je newLine
    
    inc sizeLine
    cmp sizeLine, si
    jle nextChar
    
    inc lineCounter
    
longLine:   
    mov ah, 3Fh
    int 21h 
    jc exitReadFile 
     
    cmp ax, 0
    je endOfFile
    
    cmp char, 13  
    je newLine
    
    jmp longLine
    
newLine:
    inc lines
    mov sizeLine, 0
    jmp nextChar    
    
endOfFile: 
    inc lines 
    
exitReadFile: 
    ret 
readFile endp    
    
closeFile: 
    mov ah, 3Eh 
    int 21h    
    
showResult: 
    ;lea dx, msgLines 
    ;call output 
    ;mov ax, lines 
    ;lea di, lines 
    ;call convertToStr 
    ;lea dx, di 
    ;call output  
    ;lea dx, msgEnter 
    ;call output 
    
    lea dx, msgCount 
    call output 
    mov ax, lineCounter 
    lea di, lineCounter 
    call convertToStr 
    lea dx, di 
    call output 
    
exit: 
    mov ax, 4C00h 
    int 21h  
    
output: 
    mov ah, 09h 
    int 21h 
    ret 
    
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

input proc uses ax, bx, cx,dx 
    mov ax, 0 
    mov bx, 0 
    mov cx, 0 
    mov dx, 0 
    
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
    lea dx, msgEnter 
    call output 
    lea dx, msgEmpty 
    call output 
    jmp exit 
end start