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
      value = String(newValue);
      value_changed = true;
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


