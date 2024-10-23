cmake_minimum_required(VERSION 3.0)

project(3)

add_executable(3
        task3.cpp
        logic.cpp
        logic.h)

target_include_directories(3 PUBLIC
        "${PROJECT_SOURCE_DIR}")

#add_subdirectory(../googletest googletest)

add_executable(tests3
        test_task3.cpp
        logic.cpp
        logic.h)

target_include_directories(tests3 PUBLIC
        "${PROJECT_SOURCE_DIR}"
        "${PROJECT_SOURCE_DIR}/googletest/googletest/include")

target_link_libraries(tests3 PRIVATE gtest_main)

add_test(NAME unit_tests COMMAND tests3)