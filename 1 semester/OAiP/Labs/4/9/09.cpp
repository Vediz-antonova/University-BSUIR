#include <iostream>
#include <vector>
#include <limits>

#define ll long long

int main() {
    // Шапка.
    std::cout << "9. Determine the diagonal with the largest sum of numbers." << '\n';
    std::cout << "Created by Antonava Lidia 353504" << "\n\n";

    int n;
    bool validInput = false;

    // Проверка на ввод.
    do {
        std::cout << "Enter size of array (nxnxn): ";
        if (std::cin >> n) {
            if (n >= 0) {
                validInput = true;
            }
        } else {
            std::cout << "Invalid input. Please enter a valid integer." << '\n';
            std::cin.clear();
            std::cin.ignore(std::numeric_limits<std::streamsize>::max(), '\n');
        }
    } while (!validInput);

    int ***arr = (int ***) malloc(n * sizeof(int **));
    for (int i = 0; i < n; ++i) {
        arr[i] = (int **) malloc(n * sizeof(int *));
        for (int j = 0; j < n; ++j) {
            arr[i][j] = (int *) malloc(n * sizeof(int));
        }
    }

    for (int i = 0; i < n; ++i) {
        for (int j = 0; j < n; ++j) {
            for (int k = 0; k < n; ++k) {
                std::cout << "Enter element [" << i << "][" << j << "][" << k << "]: ";
                if (std::cin >> arr[i][j][k]) {
                    if (arr[i][j][k] >= 0) {
                        validInput = true;
                    } else {
                        std::cout << "Invalid input. Please enter a valid integer." << std::endl;
                        std::cin.clear();
                        std::cin.ignore(std::numeric_limits<std::streamsize>::max(), '\n');
                        k--;
                    }
                } else {
                    std::cout << "Invalid input. Please enter a valid integer." << std::endl;
                    std::cin.clear();
                    std::cin.ignore(std::numeric_limits<std::streamsize>::max(), '\n');
                    k--;
                }
            }
        }
    }

    std::cout << '\n';
    for (int i = 0; i < n; ++i) {
        for (int j = 0; j < n; ++j) {
            for (int k = 0; k < n; ++k) {
                std::cout << arr[i][j][k] << ' ';
            }
            std::cout << '\n';
        }
        std::cout << "\n\n";
    }

    std::vector<ll> sum(n, 0);

    for (int i = 0; i < n; ++i) {
        sum[0] += arr[i][i][0];
        sum[1] += arr[i][i][n - 1];
        sum[2] += arr[i][0][i];
        sum[3] += arr[i][n - 1][i];
        sum[4] += arr[0][i][i];
        sum[5] += arr[n - 1][i][i];

        sum[6] += arr[n - i - 1][i][i];
        sum[7] += arr[i][n - i - 1][i];
        sum[8] += arr[i][i][n - i - 1];
        sum[9] += arr[i][n-i-1][n - i - 1];

        sum[10] += arr[n - i - 1][i][0];
        sum[11] += arr[n - i - 1][i][n-1];
        sum[12] += arr[0][i][n - i - 1];
        sum[13] += arr[n-1][i][n - i - 1];
        sum[14] += arr[n - i - 1][0][i];
        sum[15] += arr[n - i - 1][n-1][i];
    }

    ll max = sum[0];
    for(int i = 0; i < 16; ++i){
        if(sum[i] > max){
            max = sum[i];
        }
    }

    if(max == sum[0]){
        std::cout << sum[0] << "\n";
        for (int i = 0; i < n; i++) {
            std::cout << arr[i][i][i] << " ";
        }
    } else if(max == sum[1]) {
        std::cout << sum[1] << "\n";
        for (int i = 0; i < n; i++) {
            std::cout << arr[i][i][n - 1] << " ";
        }
    }else if(max == sum[2]){
        std::cout << sum[2] << "\n";
        for (int i = 0; i < n; i++) {
            std::cout << arr[i][0][i] << " ";
        }
    }
    else if(max == sum[3]){
        std::cout << sum[3] << "\n";
        for (int i = 0; i < n; i++) {
            std::cout << arr[i][n - 1][i] << " ";
        }
    }
    else if(max == sum[4]){
        std::cout << sum[4] << "\n";
        for (int i = 0; i < n; i++) {
            std::cout << arr[0][i][i] << " ";
        }
    }
    else if(max == sum[5]){
        std::cout << sum[5] << "\n";
        for (int i = 0; i < n; i++) {
            std::cout << arr[n-1][i][i] << " ";
        }
    }
    else if(max == sum[6]){
        std::cout << sum[6] << "\n";
        for (int i = 0; i < n; i++) {
            std::cout << arr[n - i - 1][i][i] << " ";
        }
    }
    else if(max == sum[7]){
        std::cout << sum[7] << "\n";
        for (int i = 0; i < n; i++) {
            std::cout << arr[i][n - i - 1][i] << " ";
        }
    }
    else if(max == sum[8]){
        std::cout << sum[8] << "\n";
        for (int i = 0; i < n; i++) {
            std::cout << arr[i][i][n - i - 1] << " ";
        }
    }
    else if(max == sum[9]){
        std::cout << sum[9] << "\n";
        for (int i = 0; i < n; i++) {
            std::cout << arr[i][n-i-1][n - i - 1] << " ";
        }
    }
    else if(max == sum[10]){
        std::cout << sum[10] << "\n";
        for (int i = 0; i < n; i++) {
            std::cout << arr[n - i - 1][i][0] << " ";
        }
    }
    else if(max == sum[11]){
        std::cout << sum[11] << "\n";
        for (int i = 0; i < n; i++) {
            std::cout << arr[n - i - 1][i][n-1] << " ";
        }
    }
    else if(max == sum[12]){
        std::cout << sum[12] << "\n";
        for (int i = 0; i < n; i++) {
            std::cout << arr[0][i][n - i - 1] << " ";
        }
    }else if(max == sum[13]){
        std::cout << sum[13] << "\n";
        for (int i = 0; i < n; i++) {
            std::cout << arr[n-1][i][n - i - 1] << " ";
        }
    }else if(max == sum[14]){
        std::cout << sum[14] << "\n";
        for (int i = 0; i < n; i++) {
            std::cout << arr[n - i - 1][0][i] << " ";
        }
    }else if(max == sum[15]){
        std::cout << sum[15] << "\n";
        for (int i = 0; i < n; i++) {
            std::cout << arr[n - i - 1][n-1][i] << " ";
        }
    }


    for (int i = 0; i < n; ++i) {
        for (int j = 0; j < n; ++j) {
            free(arr[i][j]);
        }
        free(arr[i]);
    }
    free(arr);

    return 0;
}