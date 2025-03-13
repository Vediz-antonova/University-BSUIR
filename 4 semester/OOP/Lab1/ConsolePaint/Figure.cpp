#include "Figure.h"

Figure::Figure(int id, int x, int y, bool isFilling) : id(id), x(x), y(y), isFilling(isFilling) {}
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

bool Figure::getIsFilling() const {
    return isFilling;
}

void Figure::setIsFilling(bool filled) {
    isFilling = filled;
}

std::string Figure::info() const {
    return "Figure ID: " + std::to_string(id) +
           ", Position: (" + std::to_string(x) + ", " + std::to_string(y) + ")" +
           ", isFilling: " + std::to_string(isFilling);
}