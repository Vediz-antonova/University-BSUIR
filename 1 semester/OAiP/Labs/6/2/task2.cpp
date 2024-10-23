#include "task2.h"

void task2() {
    // Шапка.
    std::cout << "2. Enter the telegram." << '\n';
    std::cout << "Created by Antonava Lidia 353504" << '\n';
    std::cout << "Option 1" << "\n\n";

    const int MAX_LENGTH = 1000;

    char message[MAX_LENGTH + 1];
    int wordCount = 0;

    std::cout << "Enter a message for the telegram: " << '\n';

    char ch = std::cin.get();
    int length = 0;
    while (ch != '\n' && length < MAX_LENGTH) {
        message[length++] = ch;
        ch = std::cin.get();
    }
    message[length] = '\0';

    bool inWord = false;
    for (int i = 0; i < length; ++i) {
        if ((message[i] >= 'A' && message[i] <= 'Z') || (message[i] >= 'a' && message[i] <= 'z')) {
            if (!inWord) {
                inWord = true;
                wordCount++;
            }
        } else if (message[i] == ' ' || message[i] == '\t' || message[i] == '\n') {
            inWord = false;
        }
    }

    double perWord;
    std::cout << "The cost of one word: ";
    std::cin >> perWord;

    double totalCost = wordCount * perWord;

    std::cout << "\nReceipt of payment for the telegram:\n";
    std::cout << "Message text: " << message << '\n';
    std::cout << "Number of words: " << wordCount << '\n';
    std::cout << "Cost (per word): $" << perWord << '\n';
    std::cout << "Total cost: $" << totalCost << '\n';
}
