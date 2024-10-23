//
// Created by User on 14.11.2023.
//

#include "logic.h"

int *NewArray(int **arr, int n, int k, int &size) {
    size = 0;
    int *res = new int[n];
    for (int i = 0; i < n; ++i) {
        for (int j = 0; j < k; j += 2) {
            if (arr[i][j] % 2 == 1) {
                res[size++] = arr[i][j];
            }
        }
    }

    return res;
}

double AverageMean(int **arr, int n, int k) {
    int sum = 0, size = 0;
    for (int i = 0; i < n; ++i) {
        for (int j = 0; j < k; ++j) {
            sum += arr[i][j];
            ++size;
        }
    }
    return sum / size;
}

double AverageMeanNewArray(int *arr, int size) {
    if(size == 0){
        return -1;
    }

    int sum = 0;
    for (int i = 0; i < size; ++i) {
        sum += arr[i];
    }
    return sum / size;
}
