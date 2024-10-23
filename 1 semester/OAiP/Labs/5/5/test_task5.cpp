#include "logic.h"
#include "gtest/gtest.h"

TEST(task5, NewArray){
    const int n = 3;
    const int m = 4;
    int **arr = new int *[n]{
            new int[m]{1, 2, 0, 5},
            new int[m]{4, 0, 5, 1},
            new int[m]{0, 6, 7, 7}
    };

    int size;
    int* array = NewArray(arr, n, m, size);

    ASSERT_EQ(array[0], 1);
    ASSERT_EQ(array[1], 5);
    ASSERT_EQ(array[2], 7);
}

TEST(task5, AverageMean){
    const int n = 2;
    const int m = 3;
    int **arr = new int *[n]{
            new int[m]{1, 2, 0},
            new int[m]{4, 0, 5},
    };

    double ans = AverageMean(arr, n, m);

    ASSERT_EQ(ans, 2.0);
}

TEST(task5, AverageMeanNewArray){
    const int n = 3;
    int *arr = new int [n] {1, 3, 5};

    double ans = AverageMeanNewArray(arr, n);

    ASSERT_EQ(ans, 3.0);
}

int main(int argc, char **argv) {
    ::testing::InitGoogleTest(&argc, argv);
    return RUN_ALL_TESTS();
}