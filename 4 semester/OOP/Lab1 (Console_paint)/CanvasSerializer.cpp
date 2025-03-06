#include "CanvasSerializer.h"

void CanvasSerializer::saveToFile(const Canvas& canvas, const std::string& filename) {
    std::ofstream outFile(filename, std::ios::binary);
    if (!outFile) {
        std::cerr << "Error opening file for writing: " << filename << std::endl;
        return;
    }

    const auto& figures = canvas.getFigures();
    for (const auto& figure : figures) {
        outFile << figure->getId() << " "
                << figure->getX() << " "
                << figure->getY() << " "
                << figure->getColor() << "\n";

        if (auto circle = dynamic_cast<Circle*>(figure.get())) {
            outFile << "CIRCLE " << circle->getRadius() << "\n";
        }
    }

    std::cout << "Canvas saved to file: " << filename << std::endl;
}

void CanvasSerializer::loadFromFile(Canvas& canvas, const std::string& filename) {
    std::ifstream inFile(filename, std::ios::binary);
    if (!inFile) {
        std::cerr << "Error opening file for reading: " << filename << std::endl;
        return;
    }

    canvas.clear();

    int id, x, y;
    std::string color, type;

    while (inFile >> id >> x >> y >> color >> type) {
        if (type == "CIRCLE") {
            int radius;
            inFile >> radius;
            std::unique_ptr<Circle> circle = std::unique_ptr<Circle>(new Circle(id, x, y, color, radius));
            canvas.addFigure(std::move(circle));
        }
    }

    std::cout << "Canvas loaded from file: " << filename << std::endl;
}