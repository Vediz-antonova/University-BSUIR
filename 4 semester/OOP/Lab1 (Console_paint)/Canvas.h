#ifndef LAB1__CONSOLE_PAINT__CANVAS_H
#define LAB1__CONSOLE_PAINT__CANVAS_H

#pragma once
#include "Figure.h"
#include <vector>
#include <memory>
#include <fstream>
#include <algorithm>

class Canvas {
private:
    std::vector<std::unique_ptr<Figure>> figures;

    static constexpr int width = 40;
    static constexpr int height = 20;

    char grid[height][width];

public:
    Canvas();

    void addFigure(std::unique_ptr<Figure> figure);
    void repaint();
    void drawCanvas();
    void clearGrid();
    void moveFigure(int id, int deltaX, int deltaY);
    void removeFigure(int id);
    const std::vector<std::unique_ptr<Figure>>& getFigures() const;

    void clear();
};

#endif //LAB1__CONSOLE_PAINT__CANVAS_H
