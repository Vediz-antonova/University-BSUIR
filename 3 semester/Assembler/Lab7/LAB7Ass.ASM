.model small 
.data
    duration dw 0
    startTime dw 0 
    endTime dw 0 
	color db 7
    n db 0 
    m db 0 
    row db 0 
    column db 0 
    buff db 2000 dup(?)   
    
    text1 db "Macro list: $"
    text2 db "S + D: Make a sound with the duration.$"
    text3 db "S + C: Change text or background color.$"
    text4 db "S + R: Starts rotating text in console.$"
    text5 db "S + E: Exit.$"
    msgDur db "Duration: $"
    msgLineEnd db 0Ah, 0Dh, '$'
    
.code  

public _func
_func proc
    push di
    mov di, @data
    
waitKey:
    mov ah, 01h        
    int 16h            
    jz waitKey       
    
    mov ah, 00h        
    int 16h            

    cmp al, 's'
    je sPressed

    cmp ah, 3Bh
    je f1Pressed
    
    jmp waitKey  
                    
f1Pressed:
    mov ax, 1
    jmp endCheck
        
sPressed:
    mov ah, 0 
    int 1ah
    mov startTime, dx
    
    call checkIsKeyPress
    jmp endCheck 

endCheck:
	pop di
    ret
_func endp

checkIsKeyPress proc
    mov ah, 00h          
    int 16h 

    cmp al, 'd'
    je dPressed
    
    cmp al, 'c'
    je cPressed
    
    cmp al, 'r'
    je rPressed
    
    cmp al, 'e'
    je ePressed
    
    ret
    
dPressed:
    mov ah, 0 
    int 1ah
    mov endTime, dx
    
    mov ax, endTime
    sub ax, startTime
    mov duration, ax
    
    mov ax, 2
    ret
    
cPressed:
    mov ax, 3
    ret
    
rPressed:
    mov ax, 4
    ret
    
ePressed:
    mov ax, 5
    ret
checkIsKeyPress endp

public _printHelp
_printHelp proc
    push di
    mov di,@data     
    
    mov ah, 09h
    lea dx, text1
    int 21h   
    
    lea dx, msgLineEnd
    int 21h

    lea dx, text2
    int 21h 

    lea dx, msgLineEnd
    int 21h

    lea dx, text3
    int 21h 

    lea dx, msgLineEnd
    int 21h

    lea dx, text4
    int 21h 

    lea dx, msgLineEnd
    int 21h

    lea dx, text5
    int 21h 

    lea dx, msgLineEnd
    int 21h
	
	dec color
	call _colorize

    pop di
    ret 
_printHelp endp

public _soundKey
_soundKey proc
    push di
    mov di, @data
    push si
	
    mov ah, 09h
    lea dx, msgDur
    int 21h

    mov ax, duration    
    lea di, buff  
    call convertToStr
        
    mov ah, 09h
    mov dx, di
    int 21h
        
    mov ah, 09h
    lea dx, msgLineEnd
    int 21h
    
    mov si, duration
    ; mov ax, 10
    ; mul si
    ; mov si, ax
    
generateSound:
    in al, 61h
    or al, 2
    out 61h, al

    call delay

    in al, 61h
    and al, 11111100b
    out 61h, al

    call delay
    
    dec si
    cmp si, 0
    jg generateSound
    
	mov ax, duration
	dec color
	call _colorize
	pop si
	pop di
    ret
_soundKey endp

convertToStr proc
    mov bx, 10
    add di, 5         
    mov byte ptr [di], '$'
    
convertLoop:
    dec di           
    mov dx, 0       
    div bx           
    add dl, '0'     
    mov byte ptr [di], dl     

    test ax, ax      
    jnz convertLoop 
    ret
convertToStr endp

delay proc
    mov cx, 65535

waitDelay:
    loop waitDelay
    ret
delay endp

public _colorize
_colorize proc  
;Arg color:byte
	push di
    mov di, @data
	inc color

    mov ah, 03h
    mov bh, 0 
    int 10h

    mov byte ptr n, dh
    mov byte ptr m, dl
    
    mov byte ptr row, 0
    mov byte ptr column, 0

colorLoop:
    mov ah, 02h    
    mov bh, 0     
    mov dh, byte ptr row     
    mov dl, byte ptr column    
    int 10h
    
    mov ah, 08h
    mov bh, 0
    int 10h 
    
    mov ah, 09h
    mov bh, 0
    mov bl, byte ptr color
    mov cx, 1
    int 10h
    
    inc byte ptr column
    cmp byte ptr column, 80
    jge nextRowColor       

    jmp colorLoop
    
nextRowColor:
    inc byte ptr row
    mov byte ptr column, 0
    cmp byte ptr row, 25
    jl colorLoop
    
    mov ah, 02h    
    mov bh, 0     
    mov dh, byte ptr n     
    mov dl, byte ptr m    
    int 10h

	pop di
    ret
_colorize endp

public _rotate
_rotate proc
;Arg color:byte
	push di
    mov di, @data
	
    mov ah, 03h
    mov bh, 0 
    int 10h

    mov byte ptr n, dh
    mov byte ptr m, dl

mainLoop:
    lea si, buff
    mov byte ptr row, 0
    mov byte ptr column, 0

readLoop:
    mov ah, 02h    
    mov bh, 0     
    mov dh, byte ptr row     
    mov dl, byte ptr column    
    int 10h
    
    mov ah, 08h
    mov bh, 0
    int 10h 
    
    mov byte ptr [si], al
    inc si 
    
    inc byte ptr column
    cmp byte ptr column, 80
    jge nextRowRotate       

    jmp readLoop

nextRowRotate:
    inc byte ptr row
    mov byte ptr column, 0
    cmp byte ptr row, 25
    jge endRead
    jmp readLoop

endRead: 
    lea si, buff
    mov byte ptr row, 0 
    mov byte ptr column, 1 
    mov di, 0 
    
printLoop: 
    mov ah, 02h 
    mov bh, 0 
    mov dh, byte ptr row 
    mov dl, byte ptr column 
    int 10h 
    
    mov ah, 09h 
    mov bh, 0 
    mov bl, byte ptr color 
    mov cx, 1 
    mov al, byte ptr [si] 
    int 10h 
    inc si 
    inc di   
    
    inc byte ptr column 
    cmp byte ptr column, 80 
    jge _nextRow      
    
    jmp printLoop 
    
_nextRow: 
    cmp di, 80 
    jl waited 
    inc byte ptr row 
    mov byte ptr column, 1 
    cmp byte ptr row, 25 
    jge endPrint 
    jmp printLoop 
    
waited: 
    mov ah, 02h 
    mov bh, 0 
    mov dh, byte ptr row 
    mov dl, 0 
    int 10h    
    
    mov ah, 09h 
    mov bh, 0 
    mov bl, byte ptr color 
    mov cx, 1 
    mov al, byte ptr [si] 
    int 10h 
    inc si 
    
    mov di, 0 
    inc byte ptr row 
    mov byte ptr column, 1 
    cmp byte ptr row, 25 
    jge endPrint 
    jmp printLoop 
    
endPrint: 
    mov ah, 01h 
    int 16h 
    jnz endRotate 
    
    call delay 
    call delay 
    call delay 
    call delay 
    call delay 
    call delay 
    jmp mainLoop 
    
endRotate: 
    mov ah, 02h 
    mov bh, 0 
    mov dh, byte ptr n 
    mov dl, byte ptr m 
    int 10h 
	
	pop di
    ret 
_rotate endp    
end