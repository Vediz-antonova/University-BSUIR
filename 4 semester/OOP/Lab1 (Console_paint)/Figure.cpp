#include "Figure.h"

Figure::Figure(int x, int y, const std::string& color) : x(x), y(y), color(color) {}
Figure::~Figure() {}

void Figure::move(int deltaX, int deltaY) {
    x += deltaX;
    y += deltaY;
}

int Figure::getX() const {
    return x;
}
int Figure::getY() const {
    return y;
}

void Figure::setColor(const std::string& newColor) {
    color = newColor;
}
std::string Figure::getColor() const {
    return color;
}
