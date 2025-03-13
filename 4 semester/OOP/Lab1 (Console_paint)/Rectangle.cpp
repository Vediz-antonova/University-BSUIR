#include "Rectangle.h"
#include <iostream>

Rectangle::Rectangle(int id, int x, int y, const std::string& color, int width, int height)
        : Figure(id, x, y, color), width(width), height(height) {}


void Rectangle::draw(char grid[][40], int heightGrid, int widthGrid) const {
    // Вершины ромба
    int x1 = getX();                     // Верхняя вершина
    int y1 = getY() - width / 2;

    int x2 = getX() + height / 2;     // Правая вершина
    int y2 = getY();

    int x3 = getX();                     // Нижняя вершина
    int y3 = getY() + width / 2;

    int x4 = getX() - height / 2;     // Левая вершина
    int y4 = getY();

    // Лямбда-функция для нахождения x для заданного y
    auto interpolateX = [](int x0, int y0, int x1, int y1, int y) -> int {
        if (y0 == y1) return x0; // Горизонтальная линия
        return x0 + (x1 - x0) * (y - y0) / (y1 - y0);
    };

    for (int py = y1; py <= y2; ++py) { // От верхней вершины до центра
        if (py < 0 || py >= heightGrid) continue; // Проверка границ
        int xLeft = interpolateX(x1, y1, x4, y4, py); // Левое ребро
        int xRight = interpolateX(x1, y1, x2, y2, py); // Правое ребро

        for (int px = xLeft; px <= xRight; ++px) {
            if (px >= 0 && px < widthGrid) {
                grid[py][px] = '*';
            }
        }
    }

    // Заполнение нижней половины ромба
    for (int py = y2; py <= y3; ++py) { // От центра до нижней вершины
        if (py < 0 || py >= heightGrid) continue; // Проверка границ
        int xLeft = interpolateX(x3, y3, x4, y4, py); // Левое ребро
        int xRight = interpolateX(x3, y3, x2, y2, py); // Правое ребро

        for (int px = xLeft; px <= xRight; ++px) {
            if (px >= 0 && px < widthGrid) {
                grid[py][px] = '*';
            }
        }
    }
}


void Rectangle::setWidth(int newWidth) {
    width = newWidth;
}

void Rectangle::setHeight(int newHeight) {
    height = newHeight;
}

int Rectangle::getWidth() const {
    return width;
}

int Rectangle::getHeight() const {
    return height;
}