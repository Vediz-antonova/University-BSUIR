#include "shared_library.h"

#include <iostream>
#pragma
#include <cmath>

bool hasPositiveSin(double* A, int start, int end) {
    if (start == end) {
        return sin(A[start]) > 0;
    }

    int mid = (start + end) / 2;

    bool leftHalfHasPositiveSin = hasPositiveSin(A, start, mid);
    bool rightHalfHasPositiveSin = hasPositiveSin(A, mid + 1, end);

    return leftHalfHasPositiveSin || rightHalfHasPositiveSin;
}