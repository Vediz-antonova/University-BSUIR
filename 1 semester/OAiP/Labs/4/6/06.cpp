#include <iostream>
#include <string>
#include <vector>

bool arr[10];
int main() {
    // Дано натуральное число n. Требуется подсчитать K - количество всех
    // различных цифр в числе и определить, какая цифра стоит в разряде числа n с
    // порядковым номером i. Вывести индексы всех разрядов, где встречается
    // найденная цифра.
    std::vector<long int> array;

    std::string x;
    std::cin >> x;

    for(int i = 0; i < x.size(); i++){
        array.push_back(x[i]-'0');
    }

    int k;
    std::cin >> k;

    for (long long i = 0; i < array.size(); ++i) {
        if (arr[array[i]] == 0) {
            arr[array[i]] = 1;
        }
    }

    long int ans = 0;

    for (long long i = 0; i < 10; ++i) {
        ans += arr[i];
    }

    std::cout << ans << '\n';

    int pos = 0;

    for (long long i = array.size() - 1; i >= 0; --i) {
        if (array[i] == k) {
            if(pos == 0){
                std::cout << array.size() - i - 1;
                ++pos;
            }
            else {
                std::cout << ' ' << array.size() - i - 1;
            }
        }
    }

    if(pos == 0){
        std::cout << "-1";
    }

    return 0;
}