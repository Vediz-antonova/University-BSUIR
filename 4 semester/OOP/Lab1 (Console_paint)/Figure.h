#ifndef LAB1__CONSOLE_PAINT__FIGURE_H
#define LAB1__CONSOLE_PAINT__FIGURE_H

#pragma once
#include <iostream>

class Figure {
protected:
    int x, y;
    std::string color;

public:
    Figure(int x, int y, const std::string& color);
    virtual ~Figure();

    virtual void draw() const = 0;
    virtual void move(int deltaX, int deltaY);

    int getX() const;
    int getY() const;

    void setColor(const std::string& newColor);
    std::string getColor() const;
};


#endif //LAB1__CONSOLE_PAINT__FIGURE_H
