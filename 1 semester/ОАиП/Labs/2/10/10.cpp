#include <iostream>
#include <cmath>

int main(){
    double x1, y1, r1, x2, y2, r2, R;
    std::cin >> x1 >> y1 >> r1 >> x2 >> y2 >> r2;

    R = sqrt((x2 - x1)*(x2 - x1) - (y2 - y1)*(y2 - y1));
    if(R <= r2){
        std::cout << "Yes" << '\n';
    } else if (R <= r2 && R <= r1){
        std::cout << "Yes, but the opposite is true for 2 figures" << '\n';
    } else if (R <= (r1 + r2)){
        std::cout << "The figures intersect" << '\n';
    } else{
        std::cout << "Not a single condition is met" << '\n';
    }


    return 0;
}