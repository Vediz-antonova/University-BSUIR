//
// Created by User on 13.11.2023.
//
#include <iostream>
#include "logic.h"

int CountOfO(double** arr, int n, int k){
    int ans = 0;
    for(int i = 0; i < n; ++i){
        for(int j = 0; j < k; ++j){
            if(arr[i][j] == 0){
                ++ans;
            }
        }
    }

    return ans;
}

coordinates* CoordinatesOfO(double **arr, int n, int k, int& size){
    coordinates* result;
    size = 0;
    for(int i = 0; i < n; ++i){
        for(int j = 0; j < k; ++j){
            if(arr[i][j] == 0){
                result[size].x = i;
                result[size++].y = j;
            }
        }
    }

    return result;
}

double** Reverse(double** arr, int n, int k){
    double temp;
    for(int i = 0; i < n; ++i){
        for(int j = 0; j < k/2; ++j){
            temp = arr[i][j];
            arr[i][j] = arr[i][k-1-j];
            arr[i][k-1-j] = temp;
        }
    }

    for(int i = 0; i < n/2; ++i){
        for(int j = 0; j < k; ++j){
            temp = arr[i][j];
            arr[i][j] = arr[n-1-i][j];
            arr[n-1-i][j] = temp;
        }
    }

    return arr;
}