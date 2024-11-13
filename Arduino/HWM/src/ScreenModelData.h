#ifndef SCREEN_MODEL_DATA_H
#define SCREEN_MODEL_DATA_H
#include "AbstractModelData.h"

class ScreenModelData : public AbstractModelData {
  public:
    String modelValue;
    String modelTitle;
    String modelAmount;
    String modelSizes[10];
    String modelUnits[10];
    String modelLabels[10];

  public:
    ScreenModelData(String);
  //   void setValue(String);
  //   void setValue(int);
  //   String getValue();
  //   bool valueChanged();
};

#endif