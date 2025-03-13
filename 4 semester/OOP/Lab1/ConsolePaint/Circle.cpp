#include "Circle.h"

Circle::Circle(int id, int x, int y, bool isFilling, int radius)
        : Figure(id, x, y, isFilling), radius(radius) {}

std::string Circle::info() const {
    return "Circle. \n" + Figure::info() + ", Radius: " + std::to_string(radius);
}

void Circle::draw(char** grid, int heightGrid, int widthGrid) {
    if (getIsFilling()){
        fill(grid, heightGrid, widthGrid);
        return;
    }

    for (int dy = -radius; dy <= radius; ++dy) {
        for (int dx = -radius; dx <= radius; ++dx) {
            int distanceSquared = dx * dx + dy * dy;
            if (distanceSquared >= radius * radius - radius && distanceSquared <= radius * radius + radius) {
                int newX = getX() + dx;
                int newY = getY() + dy;
                if (newX >= 0 && newX < widthGrid && newY >= 0 && newY < heightGrid) {
                    grid[newY][newX] = '*';
                }
            }
        }
    }
}

void Circle::fill(char** grid, int heightGrid, int widthGrid) {
    for (int dy = -radius; dy <= radius; ++dy) {
        for (int dx = -radius; dx <= radius; ++dx) {
            if (dx * dx + dy * dy <= radius * radius) {
                int newX = getX() + dx;
                int newY = getY() + dy;
                if (newX >= 0 && newX < widthGrid && newY >= 0 && newY < heightGrid) {
                    grid[newY][newX] = '*';
                }
            }
        }
    }

    setIsFilling(true);
}

int Circle::getRadius() const {
    return radius;
}

void Circle::saveToFile(std::ostream& os) const {
    os << "Circle " << getId() << " " << getX() << " " << getY() << " " << getIsFilling() << " " << radius << "\n";
}

void Circle::loadFromFile(std::istream& is) {
    is >> id >> x >> y >> isFilling >> radius;
}