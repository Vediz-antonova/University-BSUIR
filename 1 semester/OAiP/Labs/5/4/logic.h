//
// Created by User on 13.11.2023.
//

#ifndef LAB5_4_LOGIC_H
#define LAB5_4_LOGIC_H

struct coordinates{
    int x;
    int y;
};

int CountOfO(double** arr, int n, int k);
coordinates* CoordinatesOfO(double **arr, int n, int k, int& size);
double **Reverse(double **arr, int n, int k);

#endif //LAB5_4_LOGIC_H
