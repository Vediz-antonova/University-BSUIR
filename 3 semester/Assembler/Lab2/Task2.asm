 org $8000
 ldaa #$7F
 ldx #$8200

start_loop:
 staa ,x
 deca
 inx
 cpx #$8300    
 bmi start_loop

 ldx #$8200

main_loop:
 ldy #$8200

sec_loop:
 ldaa ,y
 ldab 1,y
 cba
 blo skip_exchange

 staa $8199
 tba
 ldab $8199
 staa ,y
 stab 1,y

skip_exchange:
 iny
 cpy #$82FF
 bmi sec_loop
 ldx #$8200
 cpx #$82FF
 bmi main_loop

end: 
 bra end