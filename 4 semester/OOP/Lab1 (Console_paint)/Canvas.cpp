#include "Canvas.h"

Canvas::Canvas() {
    figures.clear();
}

void Canvas::addFigure(std::unique_ptr<Figure> figure) {
    figures.push_back(std::move(figure));
}

void Canvas::drawAll() const {
    for (const auto& figure : figures) {
        figure->draw();
    }
}

void Canvas::moveFigure(int id, int deltaX, int deltaY) {
    for (auto& figure : figures) {
        if (figure->getId() == id) {
            figure->move(deltaX, deltaY);
            break;
        }
    }

    drawAll();
}

void Canvas::removeFigure(int id) {
    figures.erase(
            std::remove_if(figures.begin(), figures.end(),
                           [id](const std::unique_ptr<Figure>& figure) {
                               return figure->getId() == id;
                           }),
            figures.end());

    drawAll();
}

const std::vector<std::unique_ptr<Figure>>& Canvas::getFigures() const {
    return figures;
}

void Canvas::clear() {
    figures.clear();
}