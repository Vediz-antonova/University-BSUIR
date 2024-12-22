#include "..\include\stdio.h"
#include "..\include\stdlib.h"
#include "..\include\time.h"
#include "..\include\stdarg.h"

extern "C" short func();
extern "C" void printHelp();
extern "C" short soundKey();
extern "C" void colorize();
extern "C" void rotate();

void log(FILE* file, const char* format, ...) {
    time_t currentTime;
    time(&currentTime);
    struct tm* timeInfo = localtime(&currentTime);
    char timeString[9];
    strftime(timeString, sizeof(timeString), "%H.%M.%S", timeInfo);
    fprintf(file, "[%s]: ", timeString);

    va_list args;
    va_start(args, format);
    vfprintf(file, format, args);
    va_end(args);

    fprintf(file, "\n");
}

int main() {
    int duration = 0;
    short key = 0;

    FILE* file = fopen("logs.txt", "w");

    if (file == NULL) {
        printf("Failed to open logs.txt\n");
        return 0;
    }

    while (1) {
        key = 0;
        key = func();
        if (key == 0) {
            continue;
        } else if (key == 1) {
			printf("'F1' pressed\n`");
            log(file, "'F1' pressed");
            printHelp();
        } else if (key == 2) {
            log(file, "'s + d' pressed.");
            duration = soundKey();
			log(file, "Sound played. Duration: %d", duration);
        } else if (key == 3) {
            log(file, "'s + c' pressed");
            colorize();
        } else if (key == 4) {
            log(file, "'s + r' pressed");
            rotate();
        } else if (key == 5) {
            log(file, "'s + e' pressed");
            return 0;
        }
    }

    fclose(file);
    return 0;
}