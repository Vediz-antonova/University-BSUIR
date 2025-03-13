#ifndef CONSOLEPAINT_SQUARE_H
#define CONSOLEPAINT_SQUARE_H

#pragma once
#include "Figure.h"

class Square : public Figure {
private:
    int length;

public:
    Square(int id, int x, int y, bool isFilling, int length);

    std::string info() const override;
    void draw(char** grid, int heightGrid, int widthGrid) override;
    void fill(char** grid, int heightGrid, int widthGrid) override;
    int getLength() const;

    void saveToFile(std::ostream& os) const override;
    void loadFromFile(std::istream& is) override;
};

#endif //CONSOLEPAINT_SQUARE_H
