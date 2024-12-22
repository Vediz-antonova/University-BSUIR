#include "stdio.h"
#include "stdlib.h"
#include "time.h"
#include "stdarg.h"

void log(FILE* file, const char* format, ...) {
	time_t currentTime;
	time(&currentTime);
	struct tm* timeInfo = localtime(&currentTime);
	char timeString[9];
	strftime(timeString, sizeof(timeString), "%H.%M.%S", timeInfo);
	fprintf(file, "[%s]: ", timeString);

	va_list args;
	va_start(args, format);
	vfprintf(file, format, args);
	va_end(args);

	fprintf(file, "\n");
}
        
int main() {
	int startTime = 0;
	int endTime = 0;
	int duration = 0;
	int key = 0;
	short n = 0, m = 0, row = 0, column = 0;
	short color = 0;
	char buffer[2000];

	FILE* file = fopen("logs.txt", "w");

	if (file == NULL) {
		printf("Failed to open logs.txt\n");
		return 0;
	}

	while (1) {
	waitKey:
	asm {
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
	}
	f1Pressed:
	asm {
		mov word ptr key, 1
		jmp keysPress
	}
	sPressed:
	asm {
		mov word ptr key, 0
		mov ah, 0 
		int 1ah
		mov startTime, dx
		
		call checkIsKeyPress
		jmp keysPress
	}
	asm {
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
	}
	dPressed:
	asm {
		mov ah, 0 
		int 1ah
		mov endTime, dx
		
		mov ax, endTime
		sub ax, startTime
		mov duration, ax
		
		mov word ptr key, 2
		ret
	}
	cPressed:
	asm {
		mov word ptr key, 3
		ret
	}
	rPressed:
	asm {
		mov word ptr key, 4
		ret
	}
	ePressed:
	asm {
		mov word ptr key, 5
		ret
	checkIsKeyPress endp
	}
	keysPress:
		char text1[] = "Macro list: $";
		char text2[] = "S + D: Make a sound with the duration.$";
		char text3[] = "S + C: Change text or background color.$";
		char text4[] = "S + R: Starts rotating text in console.$";
		char text5[] = "S + E: Exit.$";
		char msgDur[] = "Duration: $";
		char buff[6];
		char msgEnter[3] = {(char)10, (char)13, '$'};

		if (key == 0) {
			goto waitKey;
	} else if (key == 1) {
			log(file, "'F1' pressed");
			asm {
                mov ah, 09h
                lea dx, text1
                int 21h   
                
                lea dx, msgEnter
                int 21h

                lea dx, text2
                int 21h 

                lea dx, msgEnter
                int 21h

                lea dx, text3
                int 21h 

                lea dx, msgEnter
                int 21h

                lea dx, text4
                int 21h 

                lea dx, msgEnter
                int 21h

                lea dx, text5
                int 21h 

                lea dx, msgEnter
                int 21h

                cmp byte ptr color, 0
                jne skip
                mov byte ptr color, 7
            }
			skip:
            asm {
                call colorize
                jmp waitKey
            }
		} else if (key == 2) {
			log(file, "'s + d' pressed. Duration: %d", duration);
			asm {                
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
                lea dx, msgEnter
                int 21h
				
				mov si, duration
		mov ax, 10
                mul si
                mov si, ax
            }
			generateSound:
            asm {
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
            }   
		} else if (key == 3) {
			log(file, "'s + c' pressed");
			asm {
                inc color
                call colorize
            }
		} else if (key == 4) {
			log(file, "'s + r' pressed");
			asm {
                call rotate
            }
		} else if (key == 5) {
			log(file, "'s + e' pressed");
			return 0;
		}	

		if (!color) {
            color = 7;
        }
        asm {
            call colorize
        }
		key = 0;
		startTime = 0;
		endTime = 0;
	}

	fclose(file);
    return 0;
	asm {
	convertToStr proc
		mov bx, 10
		add di, 5         
		mov byte ptr [di], '$'
	}
	convertLoop:
	asm {
		dec di           
		mov dx, 0       
		div bx           
		add dl, '0'     
		mov byte ptr [di], dl     

		test ax, ax      
		jnz convertLoop 
		ret
	convertToStr endp
	}
	asm {
	delay proc
		mov cx, 65535
	}
	waitDelay:
	asm {
		loop waitDelay
		ret
	delay endp
	}
	
	asm {
	colorize proc
		mov ah, 03h
		mov bh, 0 
		int 10h

		mov byte ptr n, dh
		mov byte ptr m, dl
		
		mov byte ptr row, 0
		mov byte ptr column, 0
	}  
	colorLoop:
	asm {
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
	}
	nextRowColor:
	asm {
		inc byte ptr row
		mov byte ptr column, 0
		cmp byte ptr row, 25
		jl colorLoop
		
		mov ah, 02h    
		mov bh, 0     
		mov dh, byte ptr n     
		mov dl, byte ptr m    
		int 10h
		
		ret
	colorize endp
	}
	
	asm {
	rotate proc
		mov ah, 03h
		mov bh, 0 
		int 10h

		mov byte ptr n, dh
		mov byte ptr m, dl
	}    
	mainLoop:
	asm {
		lea si, buffer
		mov byte ptr row, 0
		mov byte ptr column, 0
	}    
	readLoop:
	asm {
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
	}
	nextRowRotate:
	asm {
		inc byte ptr row
		mov byte ptr column, 0
		cmp byte ptr row, 25
		jge endRead
		jmp readLoop
	}  
	endRead:
	asm {
		lea si, buffer
		mov byte ptr row, 0
		mov byte ptr column, 1
		
		mov di, 0
	}  
	printLoop:
	asm {
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
	}
	_nextRow:
	asm {
		cmp di, 80
		jl wait
		inc byte ptr row
		mov byte ptr column, 1
		cmp byte ptr row, 25
		jge endPrint
		jmp printLoop
	}
	wait:
	asm { 
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
	}   
	endPrint:    
	asm { 
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
	}
	endRotate:
	asm {
		mov ah, 02h    
		mov bh, 0     
		mov dh, byte ptr n     
		mov dl, byte ptr m    
		int 10h
		ret
	rotate endp
	}
}