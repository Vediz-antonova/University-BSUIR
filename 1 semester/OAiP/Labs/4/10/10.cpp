#include<iostream>
#include<algorithm>
#include<vector>

#define ll long long

ll n, m;
std::vector<ll> arr, result;

void ad(std::vector<ll> arr) {
    ll c = 0;
    ll n = result.size();

    for (int i = n - 1; i >= 0; --i) {
        ll nm = (arr[i] + result[i]) / 10;
        result[i] = (result[i] + arr[i]) % 10 + c;
        c = nm;
    }
}

std::vector<ll> to_vect(ll x, ll kolz) {
    std::vector<ll> b;
    while (x) {
        b.push_back(x % 10);
        x /= 10;
    }

    while (b.size() != result.size() - kolz){
        b.push_back(0);
    }

    reverse(b.begin(), b.end());

    while (kolz--){
        b.push_back(0);
    }

    return b;
}

void sum(ll n, ll x, ll kolz) {
    if (n == 0){
        return;
    }

    ad(to_vect(x, kolz));
    sum(n - 1, x, kolz);
}


int main() {
    result.resize(40);
    for (int i = 0; i < 40; i++)
        result[i] = 0;

    std::cin >> n;

    if (n == 0) {
        return std::cout << "1", 0;
    }
    if (n == 1){
        return std::cout << "1", 0;
    }
    m = n / 2;
    if (n % 2 == 0){
        n--;
    }

    arr = to_vect(n, 0);
    reverse(arr.begin(), arr.end());
    for (int i = 0; i < arr.size(); i++) {
        sum(arr[i], m, i);
    }
    sum(1, 1, 0);
    ll i = 0;
    while (result[i] == 0){
        i++;
    }

    for (i; i < 40; i++)
        std::cout << result[i];

    return 0;
}