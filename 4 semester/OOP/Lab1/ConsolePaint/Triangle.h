#ifndef CONSOLEPAINT_TRIANGLE_H
#define CONSOLEPAINT_TRIANGLE_H

#pragma once
#include <cmath>
#include "Figure.h"

class Triangle : public Figure {
private:
    int sideA;
    int sideB;
    int sideC;

public:
    Triangle(int id, int x, int y, bool isFilling, int sideA, int sideB, int sideC);

    std::string info() const override;
    void draw(char** grid, int heightGrid, int widthGrid) override;
    void fill(char** grid, int heightGrid, int widthGrid) override;

    int getSideA() const;
    int getSideB() const;
    int getSideC() const;

    void saveToFile(std::ostream& os) const override;
    void loadFromFile(std::istream& is) override;
};

#endif //CONSOLEPAINT_TRIANGLE_H
