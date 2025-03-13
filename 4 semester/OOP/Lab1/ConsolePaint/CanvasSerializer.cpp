#include "CanvasSerializer.h"

void CanvasSerializer::saveToFile(const Canvas& canvas, const std::string& filename) {
    std::ofstream file(filename);
    if (!file.is_open()) {
        std::cerr << "Error: Unable to open file for saving: " << filename << "\n";
        return;
    }

    for (const auto& figure : canvas.getFigures()) {
        figure->saveToFile(file);
    }

    std::cout << "Canvas successfully saved to " << filename << "\n";
}

void CanvasSerializer::loadFromFile(Canvas& canvas, const std::string& filename) {
    std::ifstream file(filename);
    if (!file.is_open()) {
        std::cerr << "Error: Unable to open file for loading: " << filename << "\n";
        return;
    }

    canvas.clearGrid();

    std::string figureType;
    while (file >> figureType) {
        if (figureType == "Circle") {
            auto circle = std::make_unique<Circle>(0, 0, 0, false, 0);
            circle->loadFromFile(file);
            canvas.addFigure(std::move(circle));
        } else if (figureType == "Rectangle") {
            auto rectangle = std::make_unique<Rectangle>(0, 0, 0, false, 0, 0);
            rectangle->loadFromFile(file);
            canvas.addFigure(std::move(rectangle));
        } else if (figureType == "Rhombus") {
            auto rhombus = std::make_unique<Rhombus>(0, 0, 0, false, 0, 0);
            rhombus->loadFromFile(file);
            canvas.addFigure(std::move(rhombus));
        } else if (figureType == "Square") {
            auto square = std::make_unique<Square>(0, 0, 0, false, 0);
            square->loadFromFile(file);
            canvas.addFigure(std::move(square));
        } else if (figureType == "Triangle") {
            auto triangle = std::make_unique<Triangle>(0, 0, 0, false, 0, 0, 0);
            triangle->loadFromFile(file);
            canvas.addFigure(std::move(triangle));
        }
    }
    canvas.repaint();
    std::cout << "Canvas successfully loaded from " << filename << "\n";
}