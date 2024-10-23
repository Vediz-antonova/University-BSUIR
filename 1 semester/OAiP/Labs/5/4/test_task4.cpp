#include "logic.h"
#include "gtest/gtest.h"

TEST(task4, CountOfO){
    const int n = 3;
    const int m = 4;
    double **arr = new double *[n]{
            new double[m]{1, 2, 0, 5},
            new double[m]{4, 0, 5, 1},
            new double[m]{0, 6, 0, 7}
    };

    int result = CountOfO(arr, n, m);

    ASSERT_EQ(result, 4);
}

TEST(task4, CoordinatesOfO){
    const int n = 3;
    const int m = 4;
    double **arr = new double *[n]{
            new double[m]{1, 2, 0, 5},
            new double[m]{4, 0, 5, 1},
            new double[m]{0, 6, 0, 7}
    };

    int size;
    coordinates* array = CoordinatesOfO(arr, n, m, size);

    ASSERT_EQ(array[0].x, 0);
    ASSERT_EQ(array[0].y, 2);
    ASSERT_EQ(array[1].x, 1);
    ASSERT_EQ(array[1].y, 1);
    ASSERT_EQ(array[2].x, 2);
    ASSERT_EQ(array[2].y, 0);
    ASSERT_EQ(array[3].x, 2);
    ASSERT_EQ(array[3].y, 2);
}

TEST(task4, Reverse){
    const int n = 2;
    const int m = 3;
    double **arr = new double *[n]{
            new double[m]{1, 2, 6},
            new double[m]{4, 0, 5}
    };

    double** result = Reverse(arr, n, m);

    ASSERT_EQ(result[0][0], 5);
    ASSERT_EQ(result[0][1], 0);
    ASSERT_EQ(result[0][2], 4);
    ASSERT_EQ(result[1][0], 6);
    ASSERT_EQ(result[1][1], 2);
    ASSERT_EQ(result[1][2], 1);
}

int main(int argc, char **argv) {
    ::testing::InitGoogleTest(&argc, argv);
    return RUN_ALL_TESTS();
}