cmake_minimum_required(VERSION 3.26)
project(5)

set(CMAKE_CXX_STANDARD 17)

add_executable(5 task5.cpp
        logic.cpp
        logic.h
        logic.cpp
        logic.h)

add_executable(tests5
        test_task5.cpp
        logic.cpp
        logic.h)

target_include_directories(tests5 PUBLIC
        "${PROJECT_SOURCE_DIR}"
        "${PROJECT_SOURCE_DIR}/googletest/googletest/include")

target_link_libraries(tests5 PRIVATE gtest_main)

add_test(NAME unit_tests COMMAND tests5)