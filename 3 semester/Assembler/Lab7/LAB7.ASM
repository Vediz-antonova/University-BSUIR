	ifndef	??version
?debug	macro
	endm
$comm	macro	name,dist,size,count
	comm	dist name:BYTE:count*size
	endm
	else
$comm	macro	name,dist,size,count
	comm	dist name[size]:BYTE:count
	endm
	endif
	?debug	S "lab7.cpp"
	?debug	C E97AAC9559086C6162372E637070
	?debug	C E9F29D955920633A5C74635C696E636C7564655C2E2E5C696E636C+
	?debug	C 7564655C737464696F2E68
	?debug	C E9F29D955921633A5C74635C696E636C7564655C2E2E5C696E636C+
	?debug	C 7564655C7374646C69622E68
	?debug	C E9F29D95591F633A5C74635C696E636C7564655C2E2E5C696E636C+
	?debug	C 7564655C74696D652E68
	?debug	C E9F29D955921633A5C74635C696E636C7564655C2E2E5C696E636C+
	?debug	C 7564655C7374646172672E68
LAB7_TEXT	segment byte public 'CODE'
LAB7_TEXT	ends
DGROUP	group	_DATA,_BSS
	assume	cs:LAB7_TEXT,ds:DGROUP
_DATA	segment word public 'DATA'
d@	label	byte
d@w	label	word
_DATA	ends
_BSS	segment word public 'BSS'
b@	label	byte
b@w	label	word
_BSS	ends
LAB7_TEXT	segment byte public 'CODE'
   ;	
   ;	void log(FILE* file, const char* format, ...) {
   ;	
	assume	cs:LAB7_TEXT
@log$qn4FILEnxzce	proc	far
	push	bp
	mov	bp,sp
	sub	sp,22
   ;	
   ;	    time_t currentTime;
   ;	    time(&currentTime);
   ;	
	push	ss
	lea	ax,word ptr [bp-4]
	push	ax
	call	far ptr _time
	pop	cx
	pop	cx
   ;	
   ;	    struct tm* timeInfo = localtime(&currentTime);
   ;	
	push	ss
	lea	ax,word ptr [bp-4]
	push	ax
	call	far ptr _localtime
	pop	cx
	pop	cx
	mov	word ptr [bp-8],ax
	mov	word ptr [bp-6],dx
   ;	
   ;	    char timeString[9];
   ;	    strftime(timeString, sizeof(timeString), "%H.%M.%S", timeInfo);
   ;	
	push	word ptr [bp-6]
	push	word ptr [bp-8]
	push	ds
	mov	ax,offset DGROUP:s@
	push	ax
	mov	ax,9
	push	ax
	push	ss
	lea	ax,word ptr [bp-18]
	push	ax
	call	far ptr _strftime
	add	sp,14
   ;	
   ;	    fprintf(file, "[%s]: ", timeString);
   ;	
	push	ss
	lea	ax,word ptr [bp-18]
	push	ax
	push	ds
	mov	ax,offset DGROUP:s@+9
	push	ax
	push	word ptr [bp+8]
	push	word ptr [bp+6]
	call	far ptr _fprintf
	add	sp,12
   ;	
   ;	
   ;	    va_list args;
   ;	    va_start(args, format);
   ;	
	lea	ax,word ptr [bp+14]
	mov	word ptr [bp-22],ax
	mov	word ptr [bp-20],ss
   ;	
   ;	    vfprintf(file, format, args);
   ;	
	push	word ptr [bp-20]
	push	word ptr [bp-22]
	push	word ptr [bp+12]
	push	word ptr [bp+10]
	push	word ptr [bp+8]
	push	word ptr [bp+6]
	call	far ptr _vfprintf
	add	sp,12
   ;	
   ;	    va_end(args);
   ;	
   ;	    fprintf(file, "\n");
   ;	
	push	ds
	mov	ax,offset DGROUP:s@+16
	push	ax
	push	word ptr [bp+8]
	push	word ptr [bp+6]
	call	far ptr _fprintf
	add	sp,8
   ;	
   ;	}
   ;	
	mov	sp,bp
	pop	bp
	ret	
@log$qn4FILEnxzce	endp
   ;	
   ;	short test() {
   ;	
	assume	cs:LAB7_TEXT
@test$qv	proc	far
	push	bp
	mov	bp,sp
   ;	
   ;		return 42;
   ;	
	mov	ax,42
	jmp	short @2@50
@2@50:
   ;	
   ;	}
   ;	
	pop	bp
	ret	
@test$qv	endp
   ;	
   ;	void test2(short& param) {
   ;	
	assume	cs:LAB7_TEXT
@test2$qms	proc	far
	push	bp
	mov	bp,sp
   ;	
   ;		param = 42;
   ;	
	les	bx,dword ptr [bp+6]
	mov	word ptr es:[bx],42
   ;	
   ;	}
   ;	
	pop	bp
	ret	
@test2$qms	endp
   ;	
   ;	void test3(int& param) {
   ;	
	assume	cs:LAB7_TEXT
@test3$qmi	proc	far
	push	bp
	mov	bp,sp
   ;	
   ;		param = 42;
   ;	
	les	bx,dword ptr [bp+6]
	mov	word ptr es:[bx],42
   ;	
   ;	}
   ;	
	pop	bp
	ret	
@test3$qmi	endp
   ;	
   ;	int main() {
   ;	
	assume	cs:LAB7_TEXT
_main	proc	far
	push	bp
	mov	bp,sp
	sub	sp,8
	push	si
   ;	
   ;	    int duration = 0;
   ;	
	mov	word ptr [bp-2],0
   ;	
   ;	    short key = 0;
   ;	
	xor	si,si
   ;	
   ;	    short color = 0;
   ;	
	mov	word ptr [bp-4],0
   ;	
   ;	
   ;		key = test();
   ;	
	push	cs
	call	near ptr @test$qv
	mov	si,ax
   ;	
   ;	
   ;	    FILE* file = fopen("logs.txt", "w");
   ;	
	push	ds
	mov	ax,offset DGROUP:s@+27
	push	ax
	push	ds
	mov	ax,offset DGROUP:s@+18
	push	ax
	call	far ptr _fopen
	add	sp,8
	mov	word ptr [bp-8],ax
	mov	word ptr [bp-6],dx
   ;	
   ;	
   ;	    if (file == NULL) {
   ;	
	mov	ax,word ptr [bp-8]
	or	ax,word ptr [bp-6]
	jne	short @5@74
   ;	
   ;	        printf("Failed to open logs.txt\n");
   ;	
	push	ds
	mov	ax,offset DGROUP:s@+29
	push	ax
	call	far ptr _printf
	pop	cx
	pop	cx
   ;	
   ;	        return 0;
   ;	
	xor	ax,ax
	jmp	@5@506
@5@74:
   ;	
   ;	    }
   ;	
   ;	    while (1) {
   ;	        key = 0;
   ;	
	xor	si,si
   ;	
   ;			printf("1234");
   ;	
	push	ds
	mov	ax,offset DGROUP:s@+54
	push	ax
	call	far ptr _printf
	pop	cx
	pop	cx
   ;	
   ;			printf("before func\n");
   ;	
	push	ds
	mov	ax,offset DGROUP:s@+59
	push	ax
	call	far ptr _printf
	pop	cx
	pop	cx
   ;	
   ;	        key = func();
   ;	
	call	far ptr _func
	mov	si,ax
   ;	
   ;			printf("key: %d\n", key);
   ;	
	push	si
	push	ds
	mov	ax,offset DGROUP:s@+72
	push	ax
	call	far ptr _printf
	add	sp,6
   ;	
   ;	        if (key == 0) {
   ;	
	or	si,si
	jne	short @5@146
	jmp	@5@458
	jmp	@5@386
@5@146:
   ;	
   ;	            //func(key);
   ;	            continue;
   ;	        } else if (key == 1) {
   ;	
	cmp	si,1
	jne	short @5@194
   ;	
   ;				printf("'F1' pressed\n`");
   ;	
	push	ds
	mov	ax,offset DGROUP:s@+81
	push	ax
	call	far ptr _printf
	pop	cx
	pop	cx
   ;	
   ;	            log(file, "'F1' pressed");
   ;	
	push	ds
	mov	ax,offset DGROUP:s@+96
	push	ax
	push	word ptr [bp-6]
	push	word ptr [bp-8]
	push	cs
	call	near ptr @log$qn4FILEnxzce
	add	sp,8
   ;	
   ;	            key1(color);
   ;	
	push	ss
	lea	ax,word ptr [bp-4]
	push	ax
	call	far ptr _key1
	pop	cx
	pop	cx
	jmp	@5@386
@5@194:
   ;	
   ;	        } else if (key == 2) {
   ;	
	cmp	si,2
	jne	short @5@242
   ;	
   ;	            log(file, "'s + d' pressed. Duration: %d", duration);
   ;	
	push	word ptr [bp-2]
	push	ds
	mov	ax,offset DGROUP:s@+109
	push	ax
	push	word ptr [bp-6]
	push	word ptr [bp-8]
	push	cs
	call	near ptr @log$qn4FILEnxzce
	add	sp,10
   ;	
   ;	            key2(duration);
   ;	
	push	ss
	lea	ax,word ptr [bp-2]
	push	ax
	call	far ptr _key2
	pop	cx
	pop	cx
	jmp	short @5@386
@5@242:
   ;	
   ;	        } else if (key == 3) {
   ;	
	cmp	si,3
	jne	short @5@290
   ;	
   ;	            log(file, "'s + c' pressed");
   ;	
	push	ds
	mov	ax,offset DGROUP:s@+139
	push	ax
	push	word ptr [bp-6]
	push	word ptr [bp-8]
	push	cs
	call	near ptr @log$qn4FILEnxzce
	add	sp,8
   ;	
   ;	            color++;
   ;	
	inc	word ptr [bp-4]
   ;	
   ;	            colorize(color);
   ;	
	push	ss
	lea	ax,word ptr [bp-4]
	push	ax
	call	far ptr _colorize
	pop	cx
	pop	cx
	jmp	short @5@386
@5@290:
   ;	
   ;	        } else if (key == 4) {
   ;	
	cmp	si,4
	jne	short @5@338
   ;	
   ;	            log(file, "'s + r' pressed");
   ;	
	push	ds
	mov	ax,offset DGROUP:s@+155
	push	ax
	push	word ptr [bp-6]
	push	word ptr [bp-8]
	push	cs
	call	near ptr @log$qn4FILEnxzce
	add	sp,8
   ;	
   ;	            rotate(color);
   ;	
	push	ss
	lea	ax,word ptr [bp-4]
	push	ax
	call	far ptr _rotate
	pop	cx
	pop	cx
	jmp	short @5@386
@5@338:
   ;	
   ;	        } else if (key == 5) {
   ;	
	cmp	si,5
	jne	short @5@386
   ;	
   ;	            log(file, "'s + e' pressed");
   ;	
	push	ds
	mov	ax,offset DGROUP:s@+171
	push	ax
	push	word ptr [bp-6]
	push	word ptr [bp-8]
	push	cs
	call	near ptr @log$qn4FILEnxzce
	add	sp,8
   ;	
   ;	            return 0;
   ;	
	xor	ax,ax
	jmp	short @5@506
@5@386:
   ;	
   ;	        }   
   ;	
   ;	        if (!color) {
   ;	
	cmp	word ptr [bp-4],0
	jne	short @5@434
   ;	
   ;	            color = 7;
   ;	
	mov	word ptr [bp-4],7
@5@434:
   ;	
   ;	        }
   ;	        colorize(color);
   ;	
	push	ss
	lea	ax,word ptr [bp-4]
	push	ax
	call	far ptr _colorize
	pop	cx
	pop	cx
   ;	
   ;	        key = 0;
   ;	
	xor	si,si
@5@458:
	jmp	@5@74
   ;	
   ;	    }
   ;	
   ;	    fclose(file);
   ;	
	push	word ptr [bp-6]
	push	word ptr [bp-8]
	call	far ptr _fclose
	pop	cx
	pop	cx
   ;	
   ;	    return 0;
   ;	
	xor	ax,ax
	jmp	short @5@506
@5@506:
   ;	
   ;	}
   ;	
	pop	si
	mov	sp,bp
	pop	bp
	ret	
_main	endp
	?debug	C E9
LAB7_TEXT	ends
_DATA	segment word public 'DATA'
s@	label	byte
	db	'%H.%M.%S'
	db	0
	db	'[%s]: '
	db	0
	db	10
	db	0
	db	'logs.txt'
	db	0
	db	'w'
	db	0
	db	'Failed to open logs.txt'
	db	10
	db	0
	db	'1234'
	db	0
	db	'before func'
	db	10
	db	0
	db	'key: %d'
	db	10
	db	0
	db	39
	db	'F1'
	db	39
	db	' pressed'
	db	10
	db	'`'
	db	0
	db	39
	db	'F1'
	db	39
	db	' pressed'
	db	0
	db	39
	db	's + d'
	db	39
	db	' pressed. Duration: %d'
	db	0
	db	39
	db	's + c'
	db	39
	db	' pressed'
	db	0
	db	39
	db	's + r'
	db	39
	db	' pressed'
	db	0
	db	39
	db	's + e'
	db	39
	db	' pressed'
	db	0
_DATA	ends
LAB7_TEXT	segment byte public 'CODE'
LAB7_TEXT	ends
_s@	equ	s@
	public	@log$qn4FILEnxzce
	extrn	_time:far
	extrn	_localtime:far
	extrn	_strftime:far
	extrn	_fprintf:far
	extrn	_vfprintf:far
	public	@test$qv
	public	@test2$qms
	public	@test3$qmi
	public	_main
	extrn	_fopen:far
	extrn	_printf:far
	extrn	_func:far
	extrn	_key1:far
	extrn	_key2:far
	extrn	_colorize:far
	extrn	_rotate:far
	extrn	_fclose:far
	end
