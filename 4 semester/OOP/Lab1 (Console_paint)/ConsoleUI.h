#ifndef LAB1__CONSOLE_PAINT__CONSOLEUI_H
#define LAB1__CONSOLE_PAINT__CONSOLEUI_H

#pragma once
#include "Canvas.h"
#include "CanvasSerializer.h"

class ConsoleUI {
private:
    Canvas canvas;

    void showMenu() const;
    void addFigure();
    void deleteFigure();
    void moveFigure();
    void saveCanvas();
    void loadCanvas();
    void drawCanvas() const;

public:
    void run();
};

#endif //LAB1__CONSOLE_PAINT__CONSOLEUI_H
