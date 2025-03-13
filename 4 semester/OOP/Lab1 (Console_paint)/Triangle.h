#ifndef LAB1__CONSOLE_PAINT__TRIANGLE_H
#define LAB1__CONSOLE_PAINT__TRIANGLE_H

#pragma once
#include "Figure.h"

class Triangle : public Figure {
private:
    int sideA; // Длина первой стороны
    int sideB; // Длина второй стороны
    int sideC; // Длина третьей стороны

public:
    Triangle(int id, int x, int y, const std::string& color, int sideA, int sideB, int sideC);

    void draw(char grid[][40], int heightGrid, int widthGrid) const override;
};

#endif //LAB1__CONSOLE_PAINT__TRIANGLE_H
