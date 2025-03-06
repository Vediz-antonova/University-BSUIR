#ifndef LAB1__CONSOLE_PAINT__CANVASSERIALIZER_H
#define LAB1__CONSOLE_PAINT__CANVASSERIALIZER_H

#pragma once
#include "Canvas.h"
#include "Circle.h"
#include <string>

class CanvasSerializer {
public:
    static void saveToFile(const Canvas& canvas, const std::string& filename);

    static void loadFromFile(Canvas& canvas, const std::string& filename);
};

#endif //LAB1__CONSOLE_PAINT__CANVASSERIALIZER_H
