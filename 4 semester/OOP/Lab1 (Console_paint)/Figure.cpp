#include "Figure.h"

Figure::Figure(int id, int x, int y, const std::string& color) : id(id), x(x), y(y), color(color) {}
Figure::~Figure() {}

void Figure::move(int deltaX, int deltaY) {
    x += deltaX;
    y += deltaY;
}

int Figure::getId() const {
    return id;
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
