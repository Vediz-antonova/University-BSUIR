[BITS 16]
[ORG 0x7C00]

jmp start

; ---------------- GDT ----------------
gdt_start:
gdt_null:      dq 0
gdt_code:      dw 0xFFFF, 0x0000, 0x9A00, 0x00CF
gdt_data:      dw 0xFFFF, 0x0000, 0x9200, 0x00CF
gdt_end:

gdt_descriptor:
    dw gdt_end - gdt_start - 1
    dd gdt_start

; ---------------- IDT ----------------
idt_start:
    dq 0
idt_end:

idt_descriptor:
    dw idt_end - idt_start - 1
    dd idt_start

; ---------------- Real Mode Entry ----------------
start:
    mov si, menu_rm
    call print_bios

.wait_rm:
    mov ah, 0
    int 16h
    cmp al, 'P'
    je enter_pm
    jmp .wait_rm

enter_pm:
    cli
    lgdt [gdt_descriptor]
    lidt [idt_descriptor]
    mov eax, cr0
    or eax, 1
    mov cr0, eax
    jmp 0x08:pm_entry

; ---------------- Protected Mode ----------------
[BITS 32]
pm_entry:
    mov ax, 0x10
    mov ds, ax
    mov es, ax
    mov ss, ax
    mov esp, 0x90000

    mov esi, msg_pm
    mov edi, 0xB8000
    call print_pm

    mov esi, menu_pm
    call print_pm

.wait_pm:
    call read_key_pm
    cmp al, 0x13        ; R
    je return_rm
    jmp .wait_pm

read_key_pm:
.wait_press:
    in al, 0x64
    test al, 1
    jz .wait_press
    in al, 0x60
    cmp al, 0xF0
    je .wait_press
    ret

return_rm:
    mov eax, cr0
    and eax, 0xFFFFFFFE
    mov cr0, eax
    mov ax, 0
    mov ds, ax
    mov es, ax
    mov ss, ax
    db 0xEA
    dw rm_entry
    dw 0x0000

; ---------------- Real Mode Return ----------------
[BITS 16]
rm_entry:
    mov ax, 0
    mov ds, ax
    mov es, ax
    mov ss, ax
    mov si, msg_back
    call print_bios
    jmp start

; ---------------- Output Routines ----------------
print_bios:
    mov ah, 0x0E
.next:
    lodsb
    cmp al, 0
    je .done
    int 10h
    jmp .next
.done:
    ret

print_pm:
.next:
    lodsb
    cmp al, 0
    je .done
    mov ah, 0x07
    stosw
    jmp .next
.done:
    ret

; ---------------- Messages ----------------
menu_rm db "Real Mode Menu:", 13, 10, "P - Protected Mode", 13, 10, 0
menu_pm db 13, 10, "Protected Mode Menu:", 13, 10, "R - Return to Real Mode", 13, 10, 0
msg_pm   db "You are in Protected Mode", 13, 10, 0
msg_back db "Returned to Real Mode", 13, 10, 0

times 510-($-$$) db 0
dw 0xAA55