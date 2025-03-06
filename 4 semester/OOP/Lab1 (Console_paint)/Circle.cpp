#include "Circle.h"

Circle::Circle(int id, int x, int y, const std::string& color, int radius)
        : Figure(id, x, y, color), radius(radius) {}

void Circle::draw() const {
    std::cout << "Drawing Circle at (" << x << ", " << y
              << ") with radius " << radius
              << " and color " << color << std::endl;
}

void Circle::setRadius(int newRadius) {
    radius = newRadius;
}

int Circle::getRadius() const {
    return radius;
}