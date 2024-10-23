#include <iostream>

int main(){
    long double w, h, x1, y1, x2, y2, w1, h1, ans = 0, widthX, heightY, X, Y, W, H;
    std::cin >> w >> h >> x1 >> y1 >> x2 >> y2 >> w1 >> h1;

    if (w <= 0 || h <= 0 || w1 <= 0 || h1 <= 0) ans = -1;
    else {
        widthX = x2 - x1;
        heightY = y2 - y1;
        if (widthX >= w && heightY >= h) ans = -1;
        else{
            if(widthX + w1 <= w) {
                W = w1 - x2;
                if(x1 >= W ) X = w1 - x1;
                else X = w1 - W;

                if (X >= 0) {
                    ans = X;
                } else
                    ans = 0;
            }
            else if (heightY + h1 <= h){
                H = h1 - y2;
                if(y1 >= H) Y = h1 - y1;
                else Y = h1 - H;

                if (Y >= 0) {
                    ans = Y;
                } else
                    ans = 0;
            }
            else ans = -1;
        }
    }


    std::cout << ans;

    return 0;
}