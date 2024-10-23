cmake_minimum_required(VERSION 3.26)
project(1)

set(CMAKE_CXX_STANDARD 17)

add_executable(1 task1.cpp)
add_subdirectory(static-lib)
target_link_libraries(1 PUBLIC static)
target_include_directories(1 PUBLIC
        "${PROJECT_SOURCE_DIR}/static-lib")