#include "Circle.h"

Circle::Circle(int id, int x, int y, const std::string& color, int radius)
        : Figure(id, x, y, color), radius(radius) {}

void Circle::draw(char grid[][40], int height, int width) const {
    for (int dy = -radius; dy <= radius; ++dy) {
        for (int dx = -radius; dx <= radius; ++dx) {
            if (dx * dx + dy * dy <= radius * radius) {
                int newX = x + dx;
                int newY = y + dy;
                if (newX >= 0 && newX < width && newY >= 0 && newY < height) {
                    grid[newY][newX] = 'O';
                }
            }
        }
    }
}

void Circle::setRadius(int newRadius) {
    radius = newRadius;
}

int Circle::getRadius() const {
    return radius;
}