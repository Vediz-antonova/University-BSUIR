#include "1/task1.h"
#include "2/task2.h"
//#include "3/task3.h"
#include "4/task4.h"

int main() {
    std::cout << "Choose number of task\n";
    short n;
    std::cin >> n;

    switch (n) {
        case '1':
            task1();
            break;

        case '2':
            task2();
            break;

        case '3':
            break;

        case '4':
            task4();
            break;
    }
}