#include <Arduino.h>
#include "FPSCounter.h"

    FPSCounter::FPSCounter() {
      millis_fps_time_stamp = 0;
      procced_loops = 0;
      fps = 0;
      valueUpdated = false;
    }

    void FPSCounter::countFps() {
      procced_loops++;
      if (millis() - millis_fps_time_stamp >= 1000) {
        valueUpdated = true;
        fps = procced_loops;
        procced_loops = 0;
        millis_fps_time_stamp = millis();
      }
    }

    int FPSCounter::getFps() {
      valueUpdated = false;
      return fps;
    }

    bool FPSCounter::fpsChanged() {
      return valueUpdated;
    }
