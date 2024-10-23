#include <iostream>
#define ll long long

ll binpow (ll a, ll n, ll t) {
    ll res = 1;
    while (n) {
        if (n & 1){
            res *= a;
            res %= t;
        }
        a *= a;
        a %= t;
        n >>= 1;
    }
    return res;
}

int main() {
    ll k, n, t, mt = 1, ans;
    std::cin >> k >> n >> t;
    ll j = 1;
    while (true) {
        mt = 1;
        for (ll i = 1; i <= t; ++i) {
            mt *= 10;
        }

        n %= mt;
        k %= mt;

        ans = binpow(k, n, mt);

        std::cout << "\nCase #" << j << ": " << ans;
        std::cin >> k >> n >> t;
        ++j;

        if (n == 0 && k == 0 && t == 0) return 0;
    }
}