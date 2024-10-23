#include <iostream>

int main(){
    // Матрица В это измененная матрица А, в которой элементы меняются на максимальный из тех,
    // что находятся выше и левее.
    int n, m;
    std::cin >> n >> m;

    int** array = new int*[n];
    for(int i = 0; i < n; ++i){
        array[i] = new int[m];
    }

    for(int i = 0; i < n; ++i){
        for(int j = 0; j < m; ++j){
            std::cin >> array[i][j];
        }
    }

    for(int i = 0; i < n; ++i){
        for(int j = 0; j < m; ++j){
            if(i == 0){
                if(j == 0){
                    continue;
                }
                else{
                    array[i][j] = std::max(array[i][j-1], array[i][j]);
                }
            }
            else{
                if(j == 0){
                    array[i][j] = std::max(array[i-1][j], array[i][j]);
                }
                else{
                    array[i][j] = std::max(array[i-1][j], std::max(array[i][j], array[i][j-1]));
                }
            }
        }
    }

    for(int i = 0; i < n; ++i){
        for(int j = 0; j < m; ++j){
            std::cout << array[i][j] << ' ';
        }
        std::cout << '\n';
    }

    for(int i = 0; i < n; ++i){
        delete[] array[i];
    }
    delete[] array;

    return 0;
}