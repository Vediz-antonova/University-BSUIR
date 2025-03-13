cmake_minimum_required(VERSION 3.30)
project(ConsolePaint)

set(CMAKE_CXX_STANDARD 20)

add_subdirectory(Tests/googletest)

add_library(ConsolePaintLib STATIC
        Figure.cpp
        Circle.cpp
        Rectangle.cpp
        Square.cpp
        Triangle.cpp
        Rhombus.cpp
        Canvas.cpp
        ConsoleUI.cpp
        CanvasSerializer.cpp
        Actions.cpp
)

add_executable(ConsolePaint main.cpp)

target_link_libraries(ConsolePaint PRIVATE ConsolePaintLib)

add_executable(MyProject_tests
        Tests/test_main.cpp
        Tests/test_actions.cpp
        Tests/test_canvas.cpp
        Tests/test_consoleui.cpp
        Tests/test_figures.cpp
        Tests/test_serializer.cpp
)

target_link_libraries(MyProject_tests PRIVATE ConsolePaintLib gtest gtest_main)