//
// Created by User on 13.11.2023.
//

#include "logic.h"

int *diagonalElements(int **arr, int n, int m, int& resultSize) {
    resultSize = n <= m ? n : m;
    int *result = new int[resultSize];
    int k = 0;

    for (int i = 0; i < resultSize; ++i) {
        result[k++] = arr[i][i];
    }

    return result;
}

int* findOddElements(int* arr, int n, int& resultSize){
    int* result = new int[n];
    resultSize = 0;

    for(int i = 0; i < n; ++i){
        if(arr[i] % 2 == 0){
            result[resultSize++] = arr[i];
        }
    }

    return result;
}

int findMultiply(int *arr, int n) {
    int ans = 1;

    for (int i = 0; i < n; ++i) {
        ans *= arr[i];
    }

    return ans;
}