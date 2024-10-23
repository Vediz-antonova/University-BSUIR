#pragma once

#ifndef INC_7_FUNC_FOR_TALK_H
#define INC_7_FUNC_FOR_TALK_H

#include "Talk.h"

void FillingInTheStructure(Talk &sh, int i);

void Show(const Talk &arr, int i);

bool Check(const Talk &sh, const Talk &stop);

void ChooseFromPreviousAnswers(Talk *array, int n);

void AddElementsOfArray(Talk *array, int &n);

void DeleteElement(Talk *array, int &n, ll index);

void ChangeElement(Talk *array, ll index);

void Sort(Talk* array, int n);

void ShowAnswer(Talk* array, int n);

int Structure(Talk *array);

#endif //INC_7_FUNC_FOR_TALK_H
