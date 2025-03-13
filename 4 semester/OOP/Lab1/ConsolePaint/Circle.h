#ifndef CONSOLEPAINT_CIRCLE_H
#define CONSOLEPAINT_CIRCLE_H

#pragma once
#include "Figure.h"

class Circle : public Figure {
private:
    int radius;

public:
    Circle(int id, int x, int y, bool isFilling, int radius);

    std::string info() const override;
    void draw(char** grid, int heightGrid, int widthGrid) override;
    void fill(char** grid, int heightGrid, int widthGrid) override;
    int getRadius() const;

    void saveToFile(std::ostream& os) const override;
    void loadFromFile(std::istream& is) override;
};

#endif //CONSOLEPAINT_CIRCLE_H
