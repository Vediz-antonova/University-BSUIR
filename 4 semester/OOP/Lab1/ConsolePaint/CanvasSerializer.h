#ifndef CONSOLEPAINT_CANVASSERIALIZER_H
#define CONSOLEPAINT_CANVASSERIALIZER_H

#pragma once
#include "Canvas.h"
#include <fstream>
#include "Circle.h"
#include "Rectangle.h"
#include "Rhombus.h"
#include "Square.h"
#include "Triangle.h"

class CanvasSerializer {
public:
    static void saveToFile(const Canvas& canvas, const std::string& filename);
    static void loadFromFile(Canvas& canvas, const std::string& filename);
};

#endif //CONSOLEPAINT_CANVASSERIALIZER_H
