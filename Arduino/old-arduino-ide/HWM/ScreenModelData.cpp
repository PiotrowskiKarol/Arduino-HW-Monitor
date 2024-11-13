#include <Arduino.h>
#include "ScreenModelData.h"
#include <map>
#include <WString.h>

ScreenModelData::ScreenModelData(String input) {
  std::map<String, String> resultMap = stringToDictionary(input);

  modelValue = resultMap["V"];
  modelTitle = resultMap["T"];
  modelAmount = resultMap["A"];
  extractArrayFromString(resultMap["S"],modelSizes,4);
  extractArrayFromString(resultMap["U"],modelUnits,4);
  extractArrayFromString(resultMap["L"],modelLabels,4);

  Serial.println(modelSizes[0]);

}
