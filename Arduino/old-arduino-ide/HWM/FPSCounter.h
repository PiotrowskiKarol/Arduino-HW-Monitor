#ifndef FPS_COUNTER_H
#define FPS_COUNTER_H

#include "DisplayedValue.h"

class FPSCounter
{
  private:
    unsigned long millis_fps_time_stamp;
    int procced_loops;

  public:
    FPSCounter();  // Constructor
    void countFps();
    DisplayedValue fpsValue;
};

#endif