#ifndef CONSOLEPAINT_RECTANGLE_H
#define CONSOLEPAINT_RECTANGLE_H

#pragma once
#include "Figure.h"

class Rectangle : public Figure {
private:
    int width;
    int height;

public:
    Rectangle(int id, int x, int y, bool isFilling, int width, int height);

    std::string info() const override;
    void draw(char** grid, int heightGrid, int widthGrid) override;
    void fill(char** grid, int heightGrid, int widthGrid) override;

    int getWidth() const;
    int getHeight() const;

    void saveToFile(std::ostream& os) const override;
    void loadFromFile(std::istream& is) override;
};

#endif //CONSOLEPAINT_RECTANGLE_H
