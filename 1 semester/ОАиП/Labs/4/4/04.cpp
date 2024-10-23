#include <iostream>
#include <vector>

bool isLocalMinimum(const std::vector<std::vector<int>>& array, int i, int j, int n, int m) {
    for (int di = std::max(0, i - 1); di <= std::min(n - 1, i + 1); ++di) {
        for (int dj = std::max(0, j - 1); dj <= std::min(m - 1, j + 1); ++dj) {
            if ((di != i || dj != j) && array[i][j] >= array[di][dj]) {
                return false;
            }
        }
    }

    return true;
}

int main() {
    // Локальный минимум.
    int n, m;
    std::cin >> n >> m;
    std::vector<std::vector<int>> array(n, std::vector<int>(m));

    for (int i = 0; i < n; ++i) {
        for (int j = 0; j < m; ++j) {
            std::cin >> array[i][j];
        }
    }

    int ans = 0;

    for (int i = 0; i < n; ++i) {
        for (int j = 0; j < m; ++j) {
            if (isLocalMinimum(array, i, j, n, m)) {
                ++ans;
            }
        }
    }

    std::cout << ans << '\n';

    return 0;
}