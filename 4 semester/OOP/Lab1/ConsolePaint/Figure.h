#ifndef CONSOLEPAINT_FIGURE_H
#define CONSOLEPAINT_FIGURE_H

#pragma once
#include <iostream>

class Figure {
protected:
    int id;
    int x, y;
    bool isFilling;

public:
    Figure(int id, int x, int y, bool isFilling);
    virtual ~Figure();

    virtual std::string info() const;
    virtual void draw(char** grid, int heightGrid, int widthGrid) = 0;
    virtual void fill(char** grid, int heightGrid, int widthGrid) = 0;
    void move(int deltaX, int deltaY);

    int getId() const;
    int getX() const;
    int getY() const;
    bool getIsFilling() const;
    void setIsFilling(bool filled);

    virtual void saveToFile(std::ostream& os) const = 0;
    virtual void loadFromFile(std::istream& is) = 0;
};

#endif //CONSOLEPAINT_FIGURE_H
