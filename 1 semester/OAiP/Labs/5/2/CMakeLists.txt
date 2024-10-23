cmake_minimum_required(VERSION 3.26)
project(2)

set(CMAKE_CXX_STANDARD 17)

add_executable(2 task2.cpp)
add_subdirectory(shared-lib)
target_link_libraries(2 PUBLIC shared_library)
target_include_directories(2 PUBLIC
        "${PROJECT_BINARY_DIR}"
        "${PROJECT_SOURCE_DIR}/shared-lib")

add_custom_command(TARGET 2 POST_BUILD
        COMMAND ${CMAKE_COMMAND} -E copy_if_different
        $<TARGET_FILE:shared_library>
        $<TARGET_FILE_DIR:2>)