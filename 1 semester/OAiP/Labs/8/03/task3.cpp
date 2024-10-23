#include <iostream>

bool checkConsonant(char c) {
    if (c == 'a' || c == 'e' || c == 'i' || c == 'o' || c == 'u' || c == 'y' || (c < 97 || c > 123)) {
        return false;
    }

    return true;
}

std::string applyReforms(std::string text) {
    bool isUp;

    for (int i = 0; i < text.length(); ++i) {
        if (text[i] == 'C' || text[i] == 'Q' || text[i] == 'W' || text[i] == 'X') {
            if (text.length() == 1) {
                return "\0";
            }
            text.erase(i, 1);

            --i;
            continue;
        }
    }

    for (int i = 0; i < text.length(); ++i) {
        isUp = false;
        if (isupper(text[i])) {
            text[i] += 32;
            isUp = true;
        }

        // Замены сочетаний букв
        if (text[i] == 'p' && (text[i + 1] == 'h' || text[i + 1] == 'H')) {
            text[i] = 'f';
            text.erase(i + 1, 1);
        } else if (text[i] == 't' && (text[i + 1] == 'h' || text[i + 1] == 'H')) {
            text[i] = 'z';
            text.erase(i + 1, 1);
        } else if (text[i] == 'e' && (text[i + 1] == 'e' || text[i + 1] == 'E')) {
            text[i] = 'i';
            text.erase(i + 1, 1);
        } else if (text[i] == 'q' && (text[i + 1] == 'u' || text[i + 1] == 'U')) {
            text[i + 1] += (text[i + 1] == 'U' ? 32 : 0);
            text[i] = 'k';
            text[i + 1] = 'v';
        } else if (text[i] == 'y' && (text[i + 1] == 'o' || text[i + 1] == 'O') && (text[i + 2] == 'u'
                                                                                    || text[i + 2] == 'U')) {
            text[i] = 'u';
            text.erase(i + 1, 2);
        } else if (text[i] == 'o' && (text[i + 1] == 'o' || text[i + 1] == 'O')) {
            text[i] = 'u';
            text.erase(i + 1, 1);
        }

            // Замена q на k
        else if (text[i] == 'q') {
            text[i] = 'k';
        }
            // Замена x на ks
        else if (text[i] == 'x') {
            text[i] = 'k';
            text.insert(i + 1, "s");
        }
            // Замена w на v
        else if (text[i] == 'w') {
            text[i] = 'v';
        }

        if (isUp) {
            text[i] -= 32;
        }
    }

    for (int i = 0; i < text.length(); ++i) {
        // Замена c на s или k
        if (text[i] == 'c') {
            bool up = false;
            if (isupper(text[i + 1])) {
                up = true;
                text[i + 1] += 32;
            }
            if (text[i + 1] == 'e' || text[i + 1] == 'i' || text[i + 1] == 'y') {
                text[i] = 's';
            } else {
                text[i] = 'k';
            }

            if (up) {
                text[i + 1] -= 32;
            }
        }
    }

    // Замена двойных согласных
    for (int i = 0; i < text.length() - 1; ++i) {
        isUp = false;
        if (isupper(text[i])) {
            text[i] += 32;
            isUp = true;
        }

        if (checkConsonant(text[i])) {
            while (text[i] == text[i + 1] || text[i] == text[i + 1] + 32) {
                text.erase(i + 1, 1);
            }
        }

        if (isUp) {
            text[i] -= 32;
        }
    }

    return text;
}

int main() {
    std::string inputText;
    std::getline(std::cin, inputText);

    std::string reformedText = applyReforms(inputText);

    std::cout << reformedText;

    return 0;
}