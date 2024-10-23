#include "logic.h"
#include "gtest/gtest.h"

TEST(task3, FindMultiplay) {
    const int n = 3;
    int arr[n] = {6, 2, 4};

    int result = findMultiply(arr, n);

    ASSERT_EQ(result, 48);
}

TEST(task3, DiagonalElements) {
    const int n = 3;
    const int m = 2;
    //int arr[3][2] = {1, 2, 2, 5, 5, 7};
    int **arr = new int *[n]{
            new int[m]{1, 2},
            new int[m]{4, 5},
            new int[m]{6, 7}
    };

    int size;
    int *diagonal = diagonalElements(arr, n, m, size);
    ASSERT_EQ(diagonal[0], 1);
    ASSERT_EQ(diagonal[1], 5);
}

TEST (task3, FindOddDiagonalElements){
    const int n = 5;
    int* arr = new int[n] {1, 2, 4, 5, 8};

    int size = 0;
    int* result = findOddElements(arr, n, size);

    ASSERT_EQ(result[0], 2);
    ASSERT_EQ(result[1], 4);
    ASSERT_EQ(result[2], 8);
}

int main(int argc, char **argv) {
    ::testing::InitGoogleTest(&argc, argv);
    return RUN_ALL_TESTS();
}