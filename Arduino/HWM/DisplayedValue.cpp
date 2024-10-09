#include <Arduino.h>
#include "DisplayedValue.h"

    DisplayedValue::DisplayedValue() {
      value_changed = false;
    }

    void DisplayedValue::setValue(String newValue)
    {
      value = newValue;
      value_changed = true;
    }

    void DisplayedValue::setValue(int newValue)
    {
      return;
    }

    String DisplayedValue::getValue()
    {
      value_changed = false;
      return value;
    }

    bool DisplayedValue::valueChanged()
    {
      return value_changed;
    }


