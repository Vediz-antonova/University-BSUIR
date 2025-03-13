#include "ConsoleUI.h"

void ConsoleUI::showMenu() const {
    std::cout << "\n===== Console Paint =====\n";
    std::cout << "1. Add Figure\n";
    std::cout << "2. Delete Figure\n";
    std::cout << "3. Move Figure\n";
    std::cout << "4. Save Canvas\n";
    std::cout << "5. Load Canvas\n";
    std::cout << "6. Undo\n";
    std::cout << "7. Redo\n";
    std::cout << "0. Exit\n";
    std::cout << "Choose an option: ";
}

void ConsoleUI::addFigure() {
    std::cout << "Choose figure to add:\n1. Circle\n2. Rectangle\n3. Rhombus\n4. Square\n5. Triangle";
    int choice;
    checkingForInput(choice, false);
    int id, x, y;
    bool isFilling;

    auto display = [&id, &x, &y, &isFilling, this]() {
        std::cout << "Enter ID: ";
        checkingForInput(id, true);
        std::cout << "Enter X: ";
        checkingForInput(x, true);
        std::cout << "Enter Y: ";
        checkingForInput(y, true);
        std::cout << "Enter isFilling (0 or 1): ";
        bool validInput = false;
        do {
            if (std::cin >> isFilling) {
                validInput = (isFilling == 0 || isFilling == 1 ? true : false);
            } else {
                std::cout << "Invalid input. Please enter a valid integer." << '\n';
                std::cin.clear();
                std::cin.ignore(std::numeric_limits<std::streamsize>::max(), '\n');
            }
        } while (!validInput);
    };

    switch (choice) {
        case 1:  // Круг
            int radius;
            display();
            std::cout << "Enter Radius: ";
            checkingForInput(radius, true);

            canvas.addFigure(std::make_unique<Circle>(id, x, y, isFilling, radius));
            actions.addAction({"add", id, 0, 0, 0, 0, std::make_unique<Circle>(id, x, y, isFilling, radius)});
            break;
        case 2: // Прямоугольник
            int width, height;
            display();
            std::cout << "Enter Width: ";
            checkingForInput(width, true);
            std::cout << "Enter Height: ";
            checkingForInput(height, true);

            canvas.addFigure(std::make_unique<Rectangle>(id, x, y, isFilling, width, height));
            actions.addAction({"add", id, 0, 0, 0, 0, std::make_unique<Rectangle>(id, x, y, isFilling, width, height)});
            break;
        case 3: // Ромб
            int d1, d2;
            display();
            std::cout << "Enter diagonal1: ";
            checkingForInput(d1, true);
            std::cout << "Enter diagonal2: ";
            checkingForInput(d2, true);

            canvas.addFigure(std::make_unique<Rhombus>(id, x, y, isFilling, d1, d2));
            actions.addAction({"add", id, 0, 0, 0, 0, std::make_unique<Rhombus>(id, x, y, isFilling, d1, d2)});
            break;
        case 4: // Квадрат
            int lengthS;
            display();
            std::cout << "Enter length: ";
            checkingForInput(lengthS, true);

            canvas.addFigure(std::make_unique<Square>(id, x, y, isFilling, lengthS));
            actions.addAction({"add", id, 0, 0, 0, 0, std::make_unique<Square>(id, x, y, isFilling, lengthS)});
            break;
        case 5:  // Треугольник
            int sideA, sideB, sideC;
            display();
            std::cout << "Enter sideA: ";
            checkingForInput(sideA, true);
            std::cout << "Enter sideB: ";
            checkingForInput(sideB, true);
            std::cout << "Enter sideC: ";
            checkingForInput(sideC, true);

            canvas.addFigure(std::make_unique<Triangle>(id, x, y, isFilling, sideA, sideB, sideC));
            actions.addAction({"add", id, 0, 0, 0, 0, std::make_unique<Triangle>(id, x, y, isFilling, sideA, sideB, sideC)});
            break;
        default:
            std::cout << "Invalid option!\n";
            break;
    }
}

void ConsoleUI::moveFigure() {
    int id, deltaX, deltaY;
    std::cout << "Enter ID of the figure to move: ";
    checkingForInput(id, true);
    std::cout << "Enter Delta X: ";
    checkingForInput(deltaX, false);
    std::cout << "Enter Delta Y: ";
    checkingForInput(deltaY, false);

    const auto& figures = canvas.getFigures();
    int oldX = 0, oldY = 0;

    for (const auto& fig : figures) {
        if (fig->getId() == id) {
            oldX = fig->getX();
            oldY = fig->getY();
            break;
        }
    }

    actions.addAction({"move", id, oldX, oldY, oldX + deltaX, oldY + deltaY, nullptr});

    canvas.moveFigure(id, deltaX, deltaY);
}

void ConsoleUI::deleteFigure() {
    int id;
    std::cout << "Enter ID of the figure to delete: ";
    checkingForInput(id, true);

    const auto& figures = canvas.getFigures();
    std::unique_ptr<Figure> removedFigure;

    for (const auto& figure : figures) {
        if (figure->getId() == id) {
            if (auto* circle = dynamic_cast<Circle*>(figure.get())) {
                removedFigure = std::make_unique<Circle>(*circle);
            } else if (auto* rectangle = dynamic_cast<Rectangle*>(figure.get())) {
                removedFigure = std::make_unique<Rectangle>(*rectangle);
            } else if (auto* rhombus = dynamic_cast<Rhombus*>(figure.get())) {
                removedFigure = std::make_unique<Rhombus>(*rhombus);
            } else if (auto* square = dynamic_cast<Square*>(figure.get())) {
                removedFigure = std::make_unique<Square>(*square);
            } else if (auto* triangle = dynamic_cast<Triangle*>(figure.get())) {
                removedFigure = std::make_unique<Triangle>(*triangle);
            }
        }
    }

    if (removedFigure) {
        actions.addAction({"remove", id, 0, 0, 0, 0, std::move(removedFigure)});

        canvas.removeFigure(id);
    } else {
        std::cout << "Figure with ID " << id << " not found.\n";
    }
}

void ConsoleUI::run() {
    while (true) {
        system("cls");
        canvas.repaint();
        showMenu();

        int choice;
        checkingForInput(choice, false);

        switch (choice) {
            case 1:
                addFigure();
                break;
            case 2:
                deleteFigure();
                break;
            case 3:
                moveFigure();
                break;
            case 4:
                saveCanvas();
                break;
            case 5:
                loadCanvas();
                break;
            case 6:
                undoAction();
                break;
            case 7:
                redoAction();
                break;
            case 0:
                std::cout << "Exiting program.\n";
                return;
            default:
                std::cout << "Invalid choice. Try again.\n";
        }

        std::cin.clear();
        std::cin.ignore(std::numeric_limits<std::streamsize>::max(), '\n');
    }
}

void ConsoleUI::saveCanvas() {
    std::string filename;
    std::cout << "Enter filename to save the canvas: ";
    std::cin >> filename;

    CanvasSerializer::saveToFile(canvas, filename);
}

void ConsoleUI::loadCanvas() {
    std::string filename;
    std::cout << "Enter filename to load the canvas: ";
    std::cin >> filename;

    CanvasSerializer::loadFromFile(canvas, filename);
}

void ConsoleUI::undoAction() {
    std::cout << "Performing Undo...\n";
    actions.undo(canvas);
}

void ConsoleUI::redoAction() {
    std::cout << "Performing Redo...\n";
    actions.redo(canvas);
}

void ConsoleUI::checkingForInput(int &number, bool isPositive) {
    bool validInput = false;
    do {
        if (std::cin >> number) {
            validInput = (isPositive ? (number >= 0 ? true : false) : true);
        } else {
            std::cout << "Invalid input. Please enter a valid integer." << '\n';
            std::cin.clear();
            std::cin.ignore(std::numeric_limits<std::streamsize>::max(), '\n');
        }
    } while (!validInput);
}