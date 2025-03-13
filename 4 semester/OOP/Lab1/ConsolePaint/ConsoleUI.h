#ifndef CONSOLEPAINT_CONSOLEUI_H
#define CONSOLEPAINT_CONSOLEUI_H

#pragma once
#include "Canvas.h"
#include "CanvasSerializer.h"
#include "Actions.h"
#include "Circle.h"
#include "Rectangle.h"
#include "Rhombus.h"
#include "Square.h"
#include "Triangle.h"

class ConsoleUI {
private:
    Canvas canvas;
    Actions actions;

    void showMenu() const;

    void deleteFigure();
    void moveFigure();
    void saveCanvas();
    void loadCanvas();

    void undoAction();
    void redoAction();

public:
    void run();
    void checkingForInput(int &number, bool isPositive);

    void addFigure();
};

#endif //CONSOLEPAINT_CONSOLEUI_H
