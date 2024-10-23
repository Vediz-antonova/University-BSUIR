cmake_minimum_required(VERSION 3.26)
project(4)

set(CMAKE_CXX_STANDARD 17)

add_executable(4

        task4.cpp
        logic.cpp
        logic.h)

add_executable(tests4
        test_task4.cpp
        logic.cpp
        logic.h)

target_include_directories(tests4 PUBLIC
        "${PROJECT_SOURCE_DIR}"
        "${PROJECT_SOURCE_DIR}/googletest/googletest/include")

target_link_libraries(tests4 PRIVATE gtest_main)

add_test(NAME unit_tests COMMAND tests4)