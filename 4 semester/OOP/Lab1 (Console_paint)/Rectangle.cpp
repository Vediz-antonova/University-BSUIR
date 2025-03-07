#include "Rectangle.h"
#include <iostream>

Rectangle::Rectangle(int id, int x, int y, const std::string& color, int width, int height)
        : Figure(id, x, y, color), width(width), height(height) {}


void Rectangle::draw(char grid[][40], int heightGrid, int widthGrid) const {
    for (int i = 0; i < height; ++i) {
        for (int j = 0; j < width; ++j) {
            int newX = x + j;
            int newY = y + i;
            if (newX >= 0 && newX < widthGrid && newY >= 0 && newY < heightGrid) {
                grid[newY][newX] = '#';
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