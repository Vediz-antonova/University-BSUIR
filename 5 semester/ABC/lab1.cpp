#include <iostream>
#include <cstdint>

void SolveF_MMX(const int8_t* A, const int8_t* B, const int8_t* C, const int16_t* D, int16_t* F) {
    asm volatile (
        // A + B
            "movq (%0), %%mm0;"             // mm0 ← A
            "movq (%1), %%mm1;"             // mm1 ← B
            "paddb %%mm1, %%mm0;"           // mm0 ← A + B

            // Сравнение с нулём: создаём маску отрицательных байтов
            "pxor %%mm7, %%mm7;"            // mm7 ← 0
            "movq %%mm0, %%mm6;"            // mm6 ← копия A+B
            "pcmpgtb %%mm7, %%mm6;"         // mm6 ← (A+B < 0) ? 0xFF : 0x00

            // Распаковка (A + B) в слова
            "pxor %%mm2, %%mm2;"            // mm2 ← 0
            "movq %%mm0, %%mm3;"            // mm3 ← копия A+B

            "punpcklbw %%mm0, %%mm2;"       // mm2 ← нижние байты A+B
            "movq %%mm2, %%mm0;"            // mm0 ← результат

            "punpckhbw %%mm3, %%mm2;"       // mm2 ← верхние байты A+B
            "movq %%mm2, %%mm3;"            // mm3 ← результат

            // Восстановление знака: арифметический сдвиг
            "psraw $8, %%mm0;"              // mm0 ← знаковая распаковка low
            "psraw $8, %%mm3;"              // mm3 ← знаковая распаковка high

            // Загрузка C и сравнение с нулём
            "movq (%2), %%mm4;"             // mm4 ← C
            "movq %%mm4, %%mm5;"            // mm5 ← копия C

            "pxor %%mm7, %%mm7;"            // mm7 ← 0
            "movq %%mm4, %%mm6;"            // mm6 ← копия C
            "pcmpgtb %%mm7, %%mm6;"         // mm6 ← (C < 0) ? 0xFF : 0x00

            // Распаковка C
            "pxor %%mm2, %%mm2;"            // mm2 ← 0

            "punpcklbw %%mm4, %%mm2;"       // mm2 ← нижние байты C
            "movq %%mm2, %%mm4;"            // mm4 ← результат

            "punpckhbw %%mm5, %%mm2;"       // mm2 ← верхние байты C
            "movq %%mm2, %%mm5;"            // mm5 ← результат

            // Восстановление знака C
            "psraw $8, %%mm4;"              // mm4 ← знаковая распаковка C[0..3]
            "psraw $8, %%mm5;"              // mm5 ← знаковая распаковка C[4..7]

            // Умножение: (A + B) * C
            "pmullw %%mm4, %%mm0;"          // mm0 ← (A+B)[0..3] * C[0..3]
            "pmullw %%mm5, %%mm3;"          // mm3 ← (A+B)[4..7] * C[4..7]

            // Загрузка D и прибавление
            "movq (%3), %%mm6;"             // mm6 ← D[0..3]
            "paddw %%mm6, %%mm0;"           // mm0 += D[0..3]

            "movq 8(%3), %%mm6;"            // mm6 ← D[4..7]
            "paddw %%mm6, %%mm3;"           // mm3 += D[4..7]

            // Сохранение результата
            "movq %%mm0, (%4);"             // F[0..3]
            "movq %%mm3, 8(%4);"            // F[4..7]

            "emms;"                         // Очистка MMX-состояния
            :
            : "r"(A), "r"(B), "r"(C), "r"(D), "r"(F)
            : "mm0", "mm1", "mm2", "mm3", "mm4", "mm5", "mm6", "mm7"
            );
}

int main() {
    int8_t A[8] = {12, -5, 7, 0, 23, -18, 4, 9};
    int8_t B[8] = {-3, 8, 15, -7, 2, 0, -12, 6};
    int8_t C[8] = {1, 1, 2, 3, 5, -8, 13, -21};
    int16_t D[8] = {100, 200, -150, 0, 327, -38, 42, -42};
    int16_t F[8];

    SolveF_MMX(A, B, C, D, F);

    std::cout << " A(i) | B(i) | C(i) | D(i) | F(i) " << std::endl;
    std::cout << "-------------------------------------------------" << std::endl;
    for (int i = 0; i < 8; i++) {
        std::cout << static_cast<int>(A[i]) << " | "
                  << static_cast<int>(B[i]) << " | "
                  << static_cast<int>(C[i]) << " | "
                  << D[i] << " | "
                  << F[i] << "\n";
    }

    return 0;
}