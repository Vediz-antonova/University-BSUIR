#include <iostream>
#include <string>

int Sum(char* str, int n) {
    int sum = 0;
    for (int i = 0; i < n; ++i) {
        sum += str[i];
    }

    return sum;
}

void reverse(char* str, int size) {
    for (int i = 0; i < size / 2; ++i) {
        char temp = str[i];
        str[i] = str[size - 1 - i];
        str[size - 1 - i] = temp;
    }
}

int main() {
    int n, m;
    std::cout << "Enter size of matrix: ";
    std::cin >> n >> m;
    std::cin.ignore();

    char ***array = new char **[n];
    int **size = new int *[n];
    for (int i = 0; i < n; ++i) {
        array[i] = new char*[m];
        size[i] = new int[m];
    }

    for(int i = 0; i < n; ++i){
        for(int j = 0; j < m; ++j){
            size[i][j] = 0;
        }
    }

    for (int i = 0; i < n; ++i) {
        for (int j = 0; j < m; ++j) {
            std::cout << "Enter string " << i << '|' << j << ": ";
            char buffer[100];
            std::cin.getline(buffer, 100);


            while (buffer[size[i][j]] != '\0') {
                ++size[i][j];
            }

            array[i][j] = new char[size[i][j] + 1];
            for (int k = 0; k < size[i][j]; ++k) {
                array[i][j][k] = buffer[k];
            }
            array[i][j][size[i][j]] = '\0';
        }
    }

    int **sum = new int *[n];
    for (int i = 0; i < n; ++i) {
        sum[i] = new int[m];
    }

    for (int i = 0; i < n; ++i) {
        for (int j = 0; j < m; ++j) {
            sum[i][j] = Sum(array[i][j], size[i][j]);

            if (sum[i][j] % 2 == 0) {
                reverse(array[i][j], size[i][j]);
            }
        }
    }

    std::cout << "\n\n";
    for (int i = 0; i < n; ++i) {
        for (int j = 0; j < m; ++j) {
            std::cout << sum[i][j] << ' ';
        }
        std::cout << '\n';
    }

    std::cout << "\n\n";
    for (int i = 0; i < n; ++i) {
        for (int j = 0; j < m; ++j) {
            std::cout << array[i][j] << ' ';
        }
        std::cout << '\n';
    }

    return 0;
}