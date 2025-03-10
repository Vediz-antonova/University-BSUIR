#include "Square.h"

Square::Square(int id, int x, int y, const std::string& color, int length)
        : Figure(id, x, y, color), length(length) {}


void Square::draw(char grid[][40], int heightGrid, int widthGrid) const {
    for (int i = 0; i < length; ++i) {
        for (int j = 0; j < length; ++j) {
            int newX = x + 1;
            int newY = y + 1;
            if (newX >= 0 && newX < widthGrid && newY >= 0 && newY < heightGrid) {
                grid[newY][newX] = '+';
            }
        }
    }
}

void Square::setLength(int newLength) {
    length = newLength;
}

int Square::getLength() const {
    return length;
}