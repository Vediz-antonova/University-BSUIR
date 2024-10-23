#include "task4.h"

std::string applyReforms(std::string text) {
    bool isUp = false;

    for (int i = 0; i < text.length(); ++i) {
        isUp = false;
        if(isupper(text[i]) == true){
            text[i] += 32;
            isUp = true;
        }

        // Замена c на s или k
        if (text[i] == 'c') {
            if (i < text.length() - 1 && (text[i + 1] == 'e' || text[i + 1] == 'i' || text[i + 1] == 'y')) {
                text[i] = (isUp ? 'S' : 's');
            } else {
                text[i] = 'k';
            }
        }

        // Замена q на k
        else if (text[i] == 'q') {
            text[i] = (isUp ? 'K' : 'k');
        }

        // Замена сочетания qu на kv
        else if (i < text.length() - 1 && text[i] == 'q' && text[i + 1] == 'u') {
            text[i] = (isUp ? 'K' : 'k');
            text.erase(i + 1, 1);
            text.insert(i + 1, "v");
        }

        // Замена x на ks
        else if (text[i] == 'x') {
            text[i] = (isUp ? 'K' : 'k');
            text.insert(i + 1, "s");
        }

        // Замена w на v
        else if (text[i] == 'w') {
            text[i] = (isUp ? 'V' : 'v');
        }

        // Замены сочетаний букв
        else if (text[i] == 'p' && i < text.length() - 1 && text[i + 1] == 'h') {
            text[i] = (isUp ? 'F' : 'f');
            text.erase(i + 1, 1);
        } else if (text[i] == 'y' && i < text.length() - 1 && text[i + 1] == 'o' && i < text.length() - 2 && text[i + 2] == 'u') {
            text[i] = (isUp ? 'U' : 'u');
            text.erase(i + 1 , 2);
        } else if (text[i] == 'o' && i < text.length() - 1 && text[i + 1] == 'o') {
            text[i] = (isUp ? 'U' : 'u');
            text.erase(i + 1, 1);
        } else if (text[i] == 'e' && i < text.length() - 1 && text[i + 1] == 'e') {
            text[i] = (isUp ? 'I' : 'i');
            text.erase(i + 1, 1);
        } else if (text[i] == 't' && i < text.length() - 1 && text[i + 1] == 'h') {
            text[i] = (isUp ? 'Z' : 'z');
            text.erase(i + 1, 1);
        }
    }

    // Замена двойных согласных
    for (int i = 0; i < text.length() - 1; ++i) {
        if (isalpha(text[i]) && text[i] == text[i + 1]) {
            text.erase(i + 1, 1);
            --i;
        }
    }

    return text;
}

void main() {
    // Шапка.
    std::cout << "4. Reform text." << '\n';
    std::cout << "Created by Antonava Lidia 353504" << '\n';

    std::string inputText;
    std::cout << "Enter the text to be processed by the reform: ";
    std::getline(std::cin, inputText);

    std::string reformedText = applyReforms(inputText);

    std::cout << "Text after the reform: " << reformedText << '\n';
}