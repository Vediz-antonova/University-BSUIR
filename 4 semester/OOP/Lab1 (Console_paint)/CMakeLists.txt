cmake_minimum_required(VERSION 3.30)
project(Lab1__Console_paint_)

set(CMAKE_CXX_STANDARD 20)

add_executable(Lab1__Console_paint_ main.cpp
        Figure.cpp
        Figure.h
        Square.cpp
        Square.h
        Circle.cpp
        Circle.h
        Triangle.cpp
        Triangle.h
        Rectangle.cpp
        Rectangle.h
        Canvas.cpp
        Canvas.h
        CanvasSerializer.cpp
        CanvasSerializer.h
        ConsoleUI.cpp
        ConsoleUI.h)
