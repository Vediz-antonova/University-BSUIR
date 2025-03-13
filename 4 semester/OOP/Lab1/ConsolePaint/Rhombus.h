#ifndef CONSOLEPAINT_RHOMBUS_H
#define CONSOLEPAINT_RHOMBUS_H

#pragma once
#include "Figure.h"

class Rhombus : public Figure {
private:
    int diagonal1;
    int diagonal2;

public:
    Rhombus(int id, int x, int y, bool isFilling, int diagonal1, int diagonal2);
    
    std::string info() const override;
    void draw(char** grid, int heightGrid, int widthGrid) override;
    void fill(char** grid, int heightGrid, int widthGrid) override;
    
    int getDiagonal1() const;
    int getDiagonal2() const;

    void saveToFile(std::ostream& os) const override;
    void loadFromFile(std::istream& is) override;
};

#endif //CONSOLEPAINT_RHOMBUS_H
