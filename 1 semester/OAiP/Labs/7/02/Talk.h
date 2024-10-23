#pragma once

#ifndef INC_7_TALK_H
#define INC_7_TALK_H

#include <string>

typedef long long ll;
typedef long double ld;

struct Talk {
    std::string data;
    std::string code;
    std::string name;
    ll time;
    ld tarif;
    ll city_number;
    ll phone_number;
};

#endif //INC_7_TALK_H
