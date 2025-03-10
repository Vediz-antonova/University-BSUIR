#ifndef LAB1__CONSOLE_PAINT__SQUARE_H
#define LAB1__CONSOLE_PAINT__SQUARE_H

#pragma once
#include "Figure.h"

class Square : public Figure {
private:
    int length;

public:
    Square(int id, int x, int y, const std::string& color, int length);

    void draw(char grid[][40], int heightGrid, int widthGrid) const override;

    void setLength(int newLength);

    int getLength() const;

};

#endif //LAB1__CONSOLE_PAINT__SQUARE_H

