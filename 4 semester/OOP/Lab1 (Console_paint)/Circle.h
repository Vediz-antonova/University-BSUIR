#ifndef LAB1__CONSOLE_PAINT__CIRCLE_H
#define LAB1__CONSOLE_PAINT__CIRCLE_H

#pragma once
#include "Figure.h"

class Circle : public Figure {
private:
    int radius;

public:
    Circle(int id, int x, int y, const std::string& color, int radius);

    void draw(char grid[][40], int height, int width) const override;
    void setRadius(int newRadius);
    int getRadius() const;
};

#endif //LAB1__CONSOLE_PAINT__CIRCLE_H
