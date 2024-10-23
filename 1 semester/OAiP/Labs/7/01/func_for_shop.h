#pragma once

#ifndef INC_7_FUNC_FOR_SHOP_H
#define INC_7_FUNC_FOR_SHOP_H

#include "Shop.h"

void FillingInTheStructure(Shop &sh, int i);

void Show(const Shop &arr, int i);

bool Check(const Shop &sh, const Shop &stop);

void ChooseFromPreviousAnswers(Shop *array, int n);

void SortByInsert(Shop *arr, int size);

void Sort(Shop *arr, int size);

void AddElementsOfArray(Shop *array, int &n);

void FindElementAndOutput(Shop *array, int n, const Shop &param);

void DeleteElementOrChange(Shop *array, int &n, int index);

int Structure(Shop *array);

#endif //INC_7_FUNC_FOR_SHOP_H
