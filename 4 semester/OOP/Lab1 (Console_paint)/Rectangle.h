#ifndef LAB1__CONSOLE_PAINT__RECTANGLE_H
#define LAB1__CONSOLE_PAINT__RECTANGLE_H

#pragma once
#include "Figure.h"

class Rectangle : public Figure {
private:
    int width;
    int height;

public:
    Rectangle(int id, int x, int y, const std::string& color, int width, int height);

    void draw(char grid[][40], int heightGrid, int widthGrid) const override;

    void setWidth(int newWidth);
    void setHeight(int newHeight);

    int getWidth() const;
    int getHeight() const;
};

#endif //LAB1__CONSOLE_PAINT__RECTANGLE_H
