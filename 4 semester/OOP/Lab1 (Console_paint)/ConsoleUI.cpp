#include "ConsoleUI.h"
#include "Circle.h"
#include <iostream>
#include <limits>

void ConsoleUI::showMenu() const {
    std::cout << "\n===== Console Paint =====\n";
    std::cout << "1. Add Figure\n";
    std::cout << "2. Delete Figure\n";
    std::cout << "3. Move Figure\n";
    std::cout << "4. Save Canvas\n";
    std::cout << "5. Load Canvas\n";
    std::cout << "0. Exit\n";
    std::cout << "Choose an option: ";
}

void ConsoleUI::addFigure() {
    std::cout << "Choose figure to add:\n1. Circle\n2. Rectangle\n";
    int choice;
    std::cin >> choice;

    if (choice == 1) { // Круг
        int id, x, y, radius;
        std::string color;

        std::cout << "Enter ID: ";
        std::cin >> id;
        std::cout << "Enter X: ";
        std::cin >> x;
        std::cout << "Enter Y: ";
        std::cin >> y;
        std::cout << "Enter Radius: ";
        std::cin >> radius;
        std::cout << "Enter Color: ";
        std::cin >> color;

        canvas.addFigure(std::make_unique<Circle>(id, x, y, color, radius));
    } else if (choice == 2) { // Прямоугольник
        int id, x, y, width, height;
        std::string color;

        std::cout << "Enter ID: ";
        std::cin >> id;
        std::cout << "Enter X (Top-Left): ";
        std::cin >> x;
        std::cout << "Enter Y (Top-Left): ";
        std::cin >> y;
        std::cout << "Enter Width: ";
        std::cin >> width;
        std::cout << "Enter Height: ";
        std::cin >> height;
        std::cout << "Enter Color: ";
        std::cin >> color;

        canvas.addFigure(std::make_unique<Rectangle>(id, x, y, color, width, height));
    } else {
        std::cout << "Invalid option!\n";
    }
}

void ConsoleUI::moveFigure() {
    int id, deltaX, deltaY;
    std::cout << "Enter ID of the figure to move: ";
    std::cin >> id;
    std::cout << "Enter Delta X: ";
    std::cin >> deltaX;
    std::cout << "Enter Delta Y: ";
    std::cin >> deltaY;

    canvas.moveFigure(id, deltaX, deltaY);
}

void ConsoleUI::deleteFigure() {
    int id;
    std::cout << "Enter ID of the figure to delete: ";
    std::cin >> id;

    canvas.removeFigure(id);
}

void ConsoleUI::saveCanvas() {
    std::string filename;
    std::cout << "Enter filename to save: ";
    std::cin >> filename;

    CanvasSerializer::saveToFile(canvas, filename);
    std::cout << "Canvas saved to " << filename << ".\n";
}

void ConsoleUI::loadCanvas() {
    std::string filename;
    std::cout << "Enter filename to load: ";
    std::cin >> filename;

    CanvasSerializer::loadFromFile(canvas, filename);
    std::cout << "Canvas loaded from " << filename << ".\n";
}

void ConsoleUI::run() {
    while (true) {
        showMenu();

        int choice;
        std::cin >> choice;

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

