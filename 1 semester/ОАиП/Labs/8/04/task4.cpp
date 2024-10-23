#include <iostream>
#include <string>

long long factorial(long long a) {
    long long ans = 1;
    for (long int i = 2; i <= a; ++i) {
        ans *= i;
    }

    return ans;
}

int main() {
    // Шапка
    std::cout << "4. Find amount of anagrams"
                 "\n Created by Antonava Lidia 353504";

    long long anagrams, k, n;
    std::string str;
    std::cin >> str;

    n = str.size();
    anagrams = factorial(n);

    for (long long i = 0; i < n; i++) {
        k = 1;
        for (long long j = i + 1; j < n; j++) {
            if (str[i] == str[j] && str[i] != 0) {
                str[j] = 0;
                k++;
            }
        }
        anagrams /= factorial(k);
    }
    std::cout << anagrams;
    return 0;
}