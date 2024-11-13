#include <Arduino.h>
#include "DisplayedValue.h"
#include "FPSCounter.h"

    FPSCounter::FPSCounter() {
      millis_fps_time_stamp = 0;
      procced_loops = 0;
      fpsValue = DisplayedValue();
    }

    void FPSCounter::countFps() {
      procced_loops++;
      if (millis() - millis_fps_time_stamp >= 1000) {
        fpsValue.setValue(procced_loops);
        procced_loops = 0;
        millis_fps_time_stamp = millis();
      }
    }
