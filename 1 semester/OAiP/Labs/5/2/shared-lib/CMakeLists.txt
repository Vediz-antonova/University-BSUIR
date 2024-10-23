cmake_minimum_required(VERSION 3.26)
project(shared_library)

set(CMAKE_CXX_STANDARD 17)

add_library(shared_library SHARED shared_library.cpp)
