#ifndef CONSOLEPAINT_CANVAS_H
#define CONSOLEPAINT_CANVAS_H

#pragma once
#include "Figure.h"
#include <vector>
#include <memory>
#include <fstream>
#include <algorithm>

class Canvas {
private:
    std::vector<std::shared_ptr<Figure>> figures;

    static constexpr int width = 75;
    static constexpr int height = 15;

    char** grid;

public:
    Canvas();
    ~Canvas();

    void addFigure(std::shared_ptr<Figure> figure);
    void removeFigure(int id);
    void moveFigure(int id, int deltaX, int deltaY);
    void repaint();
    void clearGrid();

    const std::vector<std::shared_ptr<Figure>>& getFigures() const;
};

#endif //CONSOLEPAINT_CANVAS_H
