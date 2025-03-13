#include <gtest/gtest.h>
#include <sstream>
#include "../ConsoleUI.h"

TEST(ConsoleUITest, CheckingForInput_ValidInput) {
    std::istringstream input("5\n");
    std::cin.rdbuf(input.rdbuf());

    ConsoleUI console;
    int number;
    console.checkingForInput(number, true);

    EXPECT_EQ(number, 5);
}

TEST(ConsoleUITest, CheckingForInput_InvalidInput) {
    std::istringstream input("abc\n5\n");
    std::cin.rdbuf(input.rdbuf());

    ConsoleUI console;
    int number;
    console.checkingForInput(number, true);

    EXPECT_EQ(number, 5);
}

TEST(ConsoleUITest, CheckingForInput_NegativeInput) {
    std::istringstream input("-10\n5\n");
    std::cin.rdbuf(input.rdbuf());

    ConsoleUI console;
    int number;
    console.checkingForInput(number, true);

    EXPECT_EQ(number, 5);
}

TEST(ConsoleUITest, UserChoice_AddFigure) {
    std::istringstream input("1\n5\n5\n5\n1\n1\n4\n2\n");
    std::cin.rdbuf(input.rdbuf());

    std::ostringstream output;
    std::cout.rdbuf(output.rdbuf());

    ConsoleUI console;
    console.addFigure();

    std::string out = output.str();
    EXPECT_TRUE(out.find("Choose figure to add") != std::string::npos);
    EXPECT_TRUE(out.find("Enter ID:") != std::string::npos);
    EXPECT_TRUE(out.find("Enter X:") != std::string::npos);
    EXPECT_TRUE(out.find("Enter Y:") != std::string::npos);
}

TEST(ConsoleUITest, Run_Exit) {
    std::istringstream input("0\n");
    std::cin.rdbuf(input.rdbuf());

    std::ostringstream output;
    std::cout.rdbuf(output.rdbuf());

    ConsoleUI console;
    console.run();

    std::string out = output.str();
    EXPECT_TRUE(out.find("Choose an option:") != std::string::npos);
    EXPECT_TRUE(out.find("Exiting program.") != std::string::npos);
}